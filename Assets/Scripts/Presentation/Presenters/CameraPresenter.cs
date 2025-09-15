using UnityEngine;
using Application.UseCases.Camera;

namespace Presentation.Presenters
{
    [RequireComponent(typeof(Camera))]
    public class CameraPresenter : MonoBehaviour, ICameraMovement
    {
        [SerializeField] private Camera _camera;

        public void Move(Vector3 newPosition)
        {
            transform.position = newPosition;
        }

        public void SetZoom(float value)
        {
            _camera.orthographicSize = value;
        }

        public float GetZoom() => _camera.orthographicSize;
        
        public Vector3 GetPosition() => transform.position;
    }
}