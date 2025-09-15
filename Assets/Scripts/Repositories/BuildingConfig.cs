using AYellowpaper.SerializedCollections;
using Domain.Models.Buildings;
using UnityEngine;

namespace Repositories
{
    [CreateAssetMenu(menuName = "Configurations/Buildings/BuildingConfig")]
    public class BuildingConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        
        [SerializedDictionary("Level", "Properties")]
        [SerializeField] private SerializedDictionary<int, BuildingLevel> _buildingLevels = new();
    }
}