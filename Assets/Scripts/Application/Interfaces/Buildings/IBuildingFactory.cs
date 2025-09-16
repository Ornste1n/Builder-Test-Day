using UnityEngine;
using Domain.Models.Buildings;

namespace Application.Interfaces.Buildings
{
    public interface IBuildingFactory
    {
        void Create(BuildingType type, Vector3 worldPosition);
    }
}