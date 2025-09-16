using System;
using MessagePipe;
using UnityEngine;
using System.Threading;
using Application.Messages;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Infrastructure.InputSystem.Services
{
    public class CameraInputService
    {
        private readonly InputSystemControls _inputControls;
        
        private readonly IPublisher<CameraDragMsg> _dragPublisher;
        private readonly IPublisher<CameraMoveMsg> _movePublisher;
        private readonly IPublisher<CameraZoomMsg> _zoomPublisher;
        
        private Vector2 _direction;
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
            
            camera.Move.started += MoveStarted;
            camera.Move.performed += MovePerformed;
            camera.Move.canceled += MoveCanceled;
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

        private void DragCanceled(InputAction.CallbackContext ctx) => TryTokenDispose();

        private void MoveStarted(InputAction.CallbackContext ctx)
        {
            _cts = new CancellationTokenSource();
            ReadMoveDirection(ctx);
            MoveLoopAsync(_cts.Token).Forget();
        }

        private void MovePerformed(InputAction.CallbackContext ctx) => ReadMoveDirection(ctx);
        
        private void MoveCanceled(InputAction.CallbackContext ctx) => TryTokenDispose();
        
        private void ReadMoveDirection(InputAction.CallbackContext ctx)
        {
            _direction = ctx.ReadValue<Vector2>();
            _direction.Normalize();
        }
        
        private async UniTaskVoid MoveLoopAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested && _direction != Vector2.zero)
                {
                    _movePublisher.Publish(new CameraMoveMsg(_direction));
                    await UniTask.Yield(PlayerLoopTiming.Update, token);
                }
            }
            catch (OperationCanceledException) { }
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

        private void TryTokenDispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
        
        public void Dispose()
        {
            TryTokenDispose();

            InputSystemControls.CameraActions camera = _inputControls.Camera;
            camera.Move.started -= MoveStarted;
            camera.Move.canceled -= MoveCanceled;
            camera.RightClick.started -= DragStarted;
            camera.RightClick.canceled -= DragCanceled;
        }
    }
}