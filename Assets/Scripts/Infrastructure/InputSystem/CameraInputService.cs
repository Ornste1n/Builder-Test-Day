using MessagePipe;
using UnityEngine;
using System.Threading;
using Application.Messages;
using Domain.MessagesDTO;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Infrastructure.InputSystem
{
    public class CameraInputService
    {
        private readonly InputSystemControls _inputControls;
        
        private readonly IPublisher<CameraDragMsg> _dragPublisher;
        private readonly IPublisher<CameraMoveMsg> _movePublisher;
        private readonly IPublisher<CameraZoomMsg> _zoomPublisher;
        
        private CancellationTokenSource _cts;
        
        public CameraInputService
        (
            InputSystemControls controls,
            IPublisher<CameraDragMsg> dragPublisher,
            IPublisher<CameraMoveMsg> movePublisher,
            IPublisher<CameraZoomMsg> zoomPublisher
        )
        {
            _inputControls = controls;
            _dragPublisher = dragPublisher;
            _movePublisher = movePublisher;
            _zoomPublisher = zoomPublisher;
            
            Subscribes(_inputControls.Camera);
        }

        private void Subscribes(InputSystemControls.CameraActions camera)
        {
            camera.Enable();
            camera.Zoom.performed += HandleZoom;
            
            camera.RightClick.started += DragStarted;
            camera.RightClick.canceled += DragCanceled;
            
            camera.Move.started += OnArrow;
            camera.Move.performed += OnArrow;
            camera.Move.canceled += OnArrow;
        }
        
        private void HandleZoom(InputAction.CallbackContext ctx)
        {
            float delta = ctx.ReadValue<float>();
            _zoomPublisher.Publish(new CameraZoomMsg(delta));
        }

        private void DragStarted(InputAction.CallbackContext ctx)
        {
            _cts = new CancellationTokenSource();
            DragLoopAsync(_cts.Token).Forget();
        }

        private void DragCanceled(InputAction.CallbackContext obj)
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
        
        private async UniTaskVoid DragLoopAsync(CancellationToken token)
        {
            DeltaControl deltaControl  = Mouse.current.delta;
            
            while (!token.IsCancellationRequested)
            {
                Vector2 mouseDelta = deltaControl.ReadValue();
                if (mouseDelta != Vector2.zero)
                {
                    mouseDelta *= -1f;
                    _dragPublisher.Publish(new CameraDragMsg(mouseDelta));
                }

                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
        }
        
        private void OnArrow(InputAction.CallbackContext ctx)
        {
            Vector2 direction = ctx.ReadValue<Vector2>();
            direction.Normalize();
            _movePublisher.Publish(new CameraMoveMsg(direction));
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
            
            _inputControls.Camera.Move.started -= OnArrow;
            _inputControls.Camera.Move.performed -= OnArrow;
            _inputControls.Camera.Move.canceled -= OnArrow;
            
            _inputControls.Camera.RightClick.started -= DragStarted;
            _inputControls.Camera.RightClick.canceled -= DragCanceled;
        }
    }
}