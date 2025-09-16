using System;
using MessagePipe;
using UnityEngine;
using Application.Messages;
using UnityEngine.InputSystem;
using Application.Interfaces.Entity;
using Infrastructure.WorldSettings;

namespace Infrastructure.InputSystem.Services
{
    public class PlacementInputService : INonLazy, IDisposable
    {
        private readonly InputSystemControls _controls;
        private readonly IPublisher<PointerWorldPosMsg> _posPublisher;
        private readonly IPublisher<ConfirmPlacementMsg> _confirmPublisher;
        private readonly IPublisher<CancelPlacementMsg> _cancelPublisher;

        private bool _subscribed;

        public PlacementInputService
        (
            InputSystemControls controls,
            IPublisher<PointerWorldPosMsg> posPub,
            IPublisher<ConfirmPlacementMsg> confirmPub,
            IPublisher<CancelPlacementMsg> cancelPub
        )
        {
            _controls = controls;
            _posPublisher = posPub;
            _confirmPublisher = confirmPub;
            _cancelPublisher = cancelPub;
            
            Debug.Log("PlacementInputService");

            Subscribe(_controls.UI);
        }

        private void Subscribe(InputSystemControls.UIActions ui)
        {
            if (_subscribed) return;

            ui.Enable();

            ui.Click.performed += OnClickPerformed;
            ui.Cancel.performed += OnCancelPerformed;

            _subscribed = true;
        }

        private void OnClickPerformed(InputAction.CallbackContext ctx)
        {
            Debug.Log("OnClickPerformed");
            
            if (!TryGetScreenPosition(ctx, out Vector2 screen)) return;
            
            Debug.Log("TryGetScreenPosition");
            
            if (TryScreenPointToWorld(screen, out Vector3 world))
            {
                Debug.Log("TryScreenPointToWorld");
                _confirmPublisher.Publish(new ConfirmPlacementMsg(world));
            }
        }

        private void OnCancelPerformed(InputAction.CallbackContext ctx)
        {
            _cancelPublisher.Publish(new CancelPlacementMsg());
        }
        
        private bool TryGetScreenPosition(InputAction.CallbackContext ctx, out Vector2 screen)
        {
            Mouse mouse = Mouse.current;
            if (mouse != null)
            {
                screen = mouse.position.ReadValue();
                return true;
            }

            Touchscreen touch = Touchscreen.current;
            if (touch != null && touch.primaryTouch.press.isPressed)
            {
                screen = touch.primaryTouch.position.ReadValue();
                return true;
            }

            try
            {
                screen = ctx.ReadValue<Vector2>();
                return true;
            }
            catch
            {
                screen = default;
                return false;
            }
        }

        private bool TryScreenPointToWorld(Vector2 screen, out Vector3 world)
        {
            Camera cam = Camera.main;
            if (cam == null)
            {
                world = Vector3.zero;
                return false;
            }

            Ray ray = cam.ScreenPointToRay(screen);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, 1 << (int)WorldLayers.Ground))
            {
                world = hit.point;
                return true;
            }

            world = Vector3.zero;
            return false;
        }

        private void Unsubscribe(InputSystemControls.UIActions ui)
        {
            if (!_subscribed) return;
            
            ui.Click.performed -= OnClickPerformed;
            ui.Cancel.performed -= OnCancelPerformed;

            ui.Disable();

            _subscribed = false;
        }
        
        public void Dispose()
        {
            Unsubscribe(_controls.UI);
        }
    }
}
