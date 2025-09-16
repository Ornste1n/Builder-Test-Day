using UnityEngine;
using Domain.Models.Buildings;
using System.Collections.Generic;
using Application.Interfaces.Buildings;
using AYellowpaper.SerializedCollections;

namespace Repositories
{
    [CreateAssetMenu(menuName = "Configurations/Buildings/BuildingConfig")]
    public class BuildingConfig : ScriptableObject, IBuildingConfig
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }

        [SerializedDictionary("Level", "Properties")]
        [SerializeField] private SerializedDictionary<int, BuildingLevel> _buildingLevels = new();

        public IDictionary<int, BuildingLevel> BuildingLevels => _buildingLevels;
    }
}