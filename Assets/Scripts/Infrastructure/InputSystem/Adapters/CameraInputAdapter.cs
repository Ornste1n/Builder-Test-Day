using MessagePipe;
using UnityEngine;
using Application.Messages;
using Application.Interfaces.Camera;
using Application.Interfaces.Entity;
using Infrastructure.MessagePipeUtility;

namespace Infrastructure.InputSystem.Adapters
{
    public class CameraInputAdapter : ICameraInput
    {
        public IEvent<Vector2> OnDrag { get; }
        public IEvent<Vector2> OnMove { get; }
        public IEvent<float>  OnZoom { get; }

        public CameraInputAdapter
        (
            ISubscriber<CameraDragMsg> dragMsgSub,
            ISubscriber<CameraMoveMsg> moveMsgSub,
            ISubscriber<CameraZoomMsg> zoomMsgSub
        )
        {
            OnDrag = new MessagePipeEventMapper<CameraDragMsg, Vector2>(dragMsgSub, msg => msg.Delta);
            OnMove = new MessagePipeEventMapper<CameraMoveMsg, Vector2>(moveMsgSub, msg => msg.Direction);
            OnZoom = new MessagePipeEventMapper<CameraZoomMsg, float>(zoomMsgSub, msg => msg.Delta);
        }
    }
}