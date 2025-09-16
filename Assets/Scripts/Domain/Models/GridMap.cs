namespace Domain.Models
{
    public class GridMap
    {
        public int Width { get; }
        public int Height { get; }
        public float CellSize { get; set; } = 1f;

        private bool[,] _occupied;

        public GridMap(int width, int height, float cellSize)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            _occupied = new bool[width, height];
        }

        public bool IsOccupied(int x, int y) => _occupied[x, y];
        public void SetOccupied(int x, int y, bool value) => _occupied[x, y] = value;
    }
}