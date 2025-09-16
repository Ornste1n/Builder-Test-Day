using System;
using MessagePipe;
using UnityEngine;
using Application.Messages;
using UnityEngine.InputSystem;
using Infrastructure.WorldSettings;
using Application.Interfaces.Entity;

namespace Infrastructure.InputSystem.Services
{
    public class PlacementInputService : INonLazy, IDisposable
    {
        private readonly InputSystemControls _controls;
        private readonly IPublisher<ConfirmPlacementMsg> _confirmPublisher;
        private readonly IPublisher<CancelPlacementMsg> _cancelPublisher;

        private bool _subscribed;

        public PlacementInputService
        (
            InputSystemControls controls,
            IPublisher<ConfirmPlacementMsg> confirmPublisher,
            IPublisher<CancelPlacementMsg> cancelPublisher
        )
        {
            _controls = controls;
            _confirmPublisher = confirmPublisher;
            _cancelPublisher = cancelPublisher;

            Subscribe(_controls.UI);
        }

        private void Subscribe(InputSystemControls.UIActions ui)
        {
            if (_subscribed) return;

            ui.Enable();

            ui.Click.performed += OnClickPerformed;
            _controls.Player.Cancel.performed += OnCancelPerformed;

            _subscribed = true;
        }

        private void OnClickPerformed(InputAction.CallbackContext ctx)
        {
            if (TryScreenPointToWorld(out Vector3 world))
            {
                _confirmPublisher.Publish(new ConfirmPlacementMsg(world));
            }
        }

        private void OnCancelPerformed(InputAction.CallbackContext ctx)
        {
            _cancelPublisher.Publish(new CancelPlacementMsg());
        }

        private bool TryScreenPointToWorld(out Vector3 world)
        {
            Camera cam = Camera.main;
            if (cam == null)
            {
                world = Vector3.zero;
                return false;
            }

            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
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
            _controls.Player.Cancel.performed -= OnCancelPerformed;

            ui.Disable();
            _subscribed = false;
        }

        public void Dispose()
        {
            Unsubscribe(_controls.UI);
        }
    }
}
