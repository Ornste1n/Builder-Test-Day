namespace Domain.Gameplay.MessagesDTO
{
    public record GridInitialized
    {
        public int Width;
        public int Height;

        public GridInitialized(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }

    public record CellHovered
    {
        public int X;
        public int Y;
    }

    public record CellHighlightState
    {
        public int X; 
        public int Y; 
        public bool CanPlace;
    }
}