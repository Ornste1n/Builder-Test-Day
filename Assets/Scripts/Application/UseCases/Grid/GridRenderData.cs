using UnityEngine;

namespace Application.UseCases.Grid
{
    public record GridRenderData
    {
        public int Width;
        public int Height;
        public float CellSize;
        public Vector3 Origin;
        public Color DefaultColorA;
        public Color DefaultColorB;
        public Material GridMaterial;
    }
}