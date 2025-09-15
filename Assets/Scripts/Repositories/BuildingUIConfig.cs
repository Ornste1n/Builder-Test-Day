using UnityEngine;
using UnityEngine.UIElements;

namespace Repositories
{
    [CreateAssetMenu(menuName = "Configurations/Buildings/UI Elements")]
    public class BuildingUIConfig : ScriptableObject
    {
        [field: SerializeField] public VisualTreeAsset BuildItem { get; private set; }
    }
}