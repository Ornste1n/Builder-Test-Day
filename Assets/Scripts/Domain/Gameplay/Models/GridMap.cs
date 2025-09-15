namespace Domain.Gameplay.Models
{
    public class GridMap
    {
        public int Width { get; }
        public int Height { get; }

        private bool[,] _occupied;
        
        public GridMap(int width, int height)
        {
            Width = width;
            Height = height;
            _occupied = new bool[width, height];
        }
        
    }
}
