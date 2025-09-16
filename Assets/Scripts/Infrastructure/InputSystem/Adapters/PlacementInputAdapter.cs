using UnityEngine;
using MessagePipe;
using Application.Messages;
using Domain.Models.Buildings;
using Application.Interfaces.Entity;
using Application.Interfaces.Buildings;
using Infrastructure.MessagePipeUtility;

namespace Infrastructure.InputSystem.Adapters
{
    public class PlacementInputAdapter : IPlacementInput
    {
        public IEvent<Vector3> OnPointerMove { get; }
        public IEvent<Vector3> OnConfirm { get; }
        public IEvent<Vector3> OnCancel { get; }
        public IEvent<BuildingType> OnStartPlacement { get; }
        
        public PlacementInputAdapter
        (
            ISubscriber<PointerWorldPosMsg> posSub,
            ISubscriber<ConfirmPlacementMsg> confirmSub,
            ISubscriber<CancelPlacementMsg> cancelSub,
            ISubscriber<StartPlacementMsg> startSub
        )
        {
            OnPointerMove = new MessagePipeEventMapper<PointerWorldPosMsg, Vector3>(posSub, msg => msg.WorldPosition);
            OnConfirm = new MessagePipeEventMapper<ConfirmPlacementMsg, Vector3>(confirmSub, msg => msg.WorldPosition);
            OnCancel = new MessagePipeEventMapper<CancelPlacementMsg, Vector3>(cancelSub, _ => default);
            OnStartPlacement = new MessagePipeEventMapper<StartPlacementMsg, BuildingType>(startSub, msg => msg.Type);
        }
    }
}