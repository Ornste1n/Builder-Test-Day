using UnityEngine;
using Domain.Models.Buildings;
using AYellowpaper.SerializedCollections;

//todo возможно стоит пересмотреть реализацию каталога
namespace Repositories
{
    [CreateAssetMenu(menuName = "Configurations/Buildings/BuildingCatalogConfig")]
    public class BuildingCatalogConfig : ScriptableObject
    {
        [SerializeField, SerializedDictionary("Build Type", "Config Ref")] 
        private SerializedDictionary<BuildingType, BuildingConfig> _buildingCatalog;

        public SerializedDictionary<BuildingType, BuildingConfig> Catalog => _buildingCatalog;
    }
}