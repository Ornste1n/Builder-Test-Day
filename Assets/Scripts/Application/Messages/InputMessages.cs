using UnityEngine;

namespace Application.Messages
{
    public readonly struct CameraDragMsg
    {
        public Vector2 Delta { get; }

        public CameraDragMsg(Vector2 delta) => Delta = delta;
    }
    
    public readonly struct CameraZoomMsg
    {
        public float Delta { get; }
        public CameraZoomMsg(float delta) => Delta = delta;
    }
    
    public readonly struct CameraMoveMsg
    {
        public Vector2 Direction { get; }

        public CameraMoveMsg(Vector2 direction) => Direction = direction;
    }

    public readonly struct PointerClickMsg
    {
        public Vector2 ScreenPosition { get; }

        public PointerClickMsg(Vector2 screenPos) => ScreenPosition = screenPos;
    }
}