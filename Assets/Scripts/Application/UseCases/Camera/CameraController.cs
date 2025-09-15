using System;
using UnityEngine;
using Application.Messages;

namespace Application.UseCases.Camera
{
    public class CameraController : IDisposable
    {
        private readonly IDisposable _dragSubscription;
        private readonly IDisposable _moveSubscription;
        private readonly IDisposable _zoomSubscription;

        private readonly ICameraInput _cameraInput;
        private readonly ICameraMovement _cameraMovement;
        private readonly ICameraMoveSettings _moveSettings;

        private float _zoomValue;
        private Vector2 _direction; 
        private Vector3 _currentPosition;
        
        public CameraController
        (
            ICameraInput cameraInput,
            ICameraMovement cameraMovement,
            ICameraMoveSettings moveSettings
        )
        {
            _moveSettings = moveSettings;
            _cameraMovement = cameraMovement;
            
            _zoomSubscription = _cameraInput.OnDrag.Subscribe(HandleZoom);
            _dragSubscription = _cameraInput.OnDrag.Subscribe(HandleDrag);
            _moveSubscription = _cameraInput.OnDrag.Subscribe(HandleMove);
            
            _zoomValue = _cameraMovement.GetZoom();
            _currentPosition = _cameraMovement.GetPosition();
        }

        public void Tick()
        {
            if(_direction == Vector2.zero) return;
            
            _currentPosition += new Vector3(_direction.x, _direction.y, 0) * _moveSettings.ArrowSpeed;
            _cameraMovement.Move(_currentPosition);
        }
        
        private void HandleDrag(Vector2 delta)
        {
            _currentPosition += new Vector3(delta.x, delta.y, 0) * _moveSettings.DragSpeed;
            _cameraMovement.Move(_currentPosition);
        }

        private void HandleZoom(float value)
        {
            Vector2 bound = _moveSettings.ZoomBound;
            float newValue = _zoomValue - (value * _moveSettings.ZoomSpeed);
            
            _zoomValue = Mathf.Clamp(newValue, bound.x, bound.y);
            _cameraMovement.SetZoom(_zoomValue);
        }
        
        private void HandleMove(Vector3 direction)
        {
            _direction = direction;
        }

        public void Dispose()
        {
            _dragSubscription?.Dispose();
            _moveSubscription?.Dispose();
            _zoomSubscription?.Dispose();
        }
    }
}