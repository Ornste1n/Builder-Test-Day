using UnityEngine;
using Domain.Models.Buildings;
using Application.Interfaces.Entity;

namespace Application.Interfaces.Buildings
{
    public interface IPlacementInput
    {
        IEvent<Vector3> OnPointerMove { get; }
        IEvent<Vector3> OnConfirm { get; }
        //IEvent<Unit> OnCancel { get; } 
        IEvent<BuildingType> OnStartPlacement { get; }
    }
}