using UnityEngine;

namespace Application.UseCases.Camera
{
    public interface ICameraMoveSettings
    {
        public float DragSpeed { get; }
        public float ArrowSpeed { get; }
        public float ZoomSpeed { get; }
        public Vector2 ZoomBound { get; }
    }
}