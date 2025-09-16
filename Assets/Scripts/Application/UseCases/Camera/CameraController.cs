using System;
using UnityEngine;
using Application.Interfaces;
using Application.Interfaces.Camera;
using Application.Interfaces.Entity;

namespace Application.UseCases.Camera
{
    public class CameraController : INonLazy, IDisposable
    {
        private readonly IDisposable _dragSubscription;
        private readonly IDisposable _moveSubscription;
        private readonly IDisposable _zoomSubscription;

        private readonly ICameraMovement _cameraMovement;
        private readonly ICameraMoveSettings _moveSettings;

        private float _zoomValue;
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
            
            _zoomSubscription = cameraInput.OnZoom.Subscribe(HandleZoom);
            _dragSubscription = cameraInput.OnDrag.Subscribe(HandleDrag);
            _moveSubscription = cameraInput.OnMove.Subscribe(HandleMove);
            
            _zoomValue = _cameraMovement.GetZoom();
            _currentPosition = _cameraMovement.GetPosition();
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
        
        private void HandleMove(Vector2 direction)
        {
            _currentPosition += new Vector3(direction.x, direction.y, 0) * _moveSettings.ArrowSpeed;
            _cameraMovement.Move(_currentPosition);
        }

        public void Dispose()
        {
            _dragSubscription?.Dispose();
            _moveSubscription?.Dispose();
            _zoomSubscription?.Dispose();
        }
    }
}