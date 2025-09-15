using UnityEngine;

namespace Application.Interfaces.Grid
{
    public interface IGridConfig
    {
        public int Width { get; }
        public int Height { get; }
        public float CellSize { get; }
        public Vector3 Origin { get; }
        public Color DefaultColorA { get; }
        public Color DefaultColorB { get; }
        public Color CanPlaceColor { get; }
        public Color CannotPlaceColor { get; }
        public Material TileMaterial { get; }
    }
}