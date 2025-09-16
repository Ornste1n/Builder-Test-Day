using UnityEngine;
using System.Linq;
using Domain.Models.Buildings;
using System.Collections.Generic;
using Application.Interfaces.Buildings;
using AYellowpaper.SerializedCollections;

namespace Repositories
{
    [CreateAssetMenu(menuName = "Configurations/Buildings/BuildingCatalogConfig")]
    public class BuildingCatalogConfig : ScriptableObject
    {
        [SerializeField, SerializedDictionary("Build Type", "Config Ref")] 
        private SerializedDictionary<BuildingType, BuildingConfig> _buildingCatalog;

        public IReadOnlyDictionary<BuildingType, IBuildingConfig> Catalog 
            => _buildingCatalog.ToDictionary(kvp => kvp.Key, kvp => (IBuildingConfig)kvp.Value);
    }
}