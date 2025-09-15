using UnityEngine;
using Domain.Gameplay.Models.Buildings;
using AYellowpaper.SerializedCollections;

namespace Repositories.Application
{
    [CreateAssetMenu(menuName = "Configurations/Buildings/BuildingConfig")]
    public class BuildingConfig : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        
        [SerializedDictionary("Level", "Properties")]
        [SerializeField] private SerializedDictionary<int, BuildingLevel> _buildingLevels = new();
    }
}