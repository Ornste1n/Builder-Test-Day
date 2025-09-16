using UnityEngine;
using Domain.Models.Buildings;

namespace Application.Messages
{
    public readonly struct StartPlacementMsg
    {
        public BuildingType Type { get; }
        
        public StartPlacementMsg(BuildingType type)
        {
            Type = type;
        }
    }

    public readonly struct PointerWorldPosMsg
    {
        public Vector3 WorldPosition { get; }
        
        public PointerWorldPosMsg(UnityEngine.Vector3 worldPosition)
        {
            WorldPosition = worldPosition;
        }
    }
    
    public readonly struct ConfirmPlacementMsg
    {
        public Vector3 WorldPosition { get; }

        public ConfirmPlacementMsg(UnityEngine.Vector3 worldPosition)
        {
            WorldPosition = worldPosition;
        }
    }
    
    public record CancelPlacementMsg();   
}