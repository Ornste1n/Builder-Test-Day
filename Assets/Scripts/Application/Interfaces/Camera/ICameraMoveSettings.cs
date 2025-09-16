using UnityEngine;

namespace Application.Interfaces.Camera
{
    public interface ICameraMoveSettings
    {
        public float DragSpeed { get; }
        public float ArrowSpeed { get; }
        public float ZoomSpeed { get; }
        public Vector2 ZoomBound { get; }
    }
}