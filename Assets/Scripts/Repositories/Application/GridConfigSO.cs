using UnityEngine;

namespace Repositories.Application
{
    [CreateAssetMenu(menuName = "Configurations/GridConfig")]
    public class GridConfigSo : ScriptableObject
    {
        [field: SerializeField] public int Width { get; private set; }
        [field: SerializeField] public int Height { get; private set; }
        [field: SerializeField] public float CellSize { get; private set; }
        [field: SerializeField] public Vector3 Origin { get; private set; }
        
        [field: SerializeField] public Color DefaultColorA { get; private set; }
        [field: SerializeField] public Color DefaultColorB { get; private set; }
        
        [field: SerializeField] public Color CanPlaceColor { get; private set; }
        [field: SerializeField] public Color CannotPlaceColor { get; private set; }
        [field: SerializeField] public Material TileMaterial { get; private set; }

        private void Reset()
        {
            Width = 32;
            Height = 32;
            CellSize = 1;
            Origin = Vector3.zero;
            DefaultColorA = new Color(0,0,0,0);
            DefaultColorA = new Color(1,1,1,1);
            CanPlaceColor = new Color(0,1,0,0.5f);
            CannotPlaceColor = new Color(1,0,0,0.5f);
        }
    }
}
