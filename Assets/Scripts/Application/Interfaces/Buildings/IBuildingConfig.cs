using UnityEngine;
using Domain.Models.Buildings;
using System.Collections.Generic;

namespace Application.Interfaces.Buildings
{
    public interface IBuildingConfig
    {
        public Sprite Icon { get; }
        public GameObject Prefab { get; }
        public IDictionary<int, BuildingLevel> BuildingLevels { get; }
    }
}