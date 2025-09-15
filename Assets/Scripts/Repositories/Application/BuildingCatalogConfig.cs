using UnityEngine;
using Domain.Gameplay.Models.Buildings;
using AYellowpaper.SerializedCollections;

//todo возможно стоит пересмотреть реализацию каталога
namespace Repositories.Application
{
    [CreateAssetMenu(menuName = "Configurations/Buildings/BuildingCatalogConfig")]
    public class BuildingCatalogConfig : ScriptableObject
    {
        [SerializeField, SerializedDictionary("Build Type", "Config Ref")] 
        private SerializedDictionary<BuildingType, BuildingConfig> _buildingCatalog;
    }
}