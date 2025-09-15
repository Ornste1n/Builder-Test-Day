namespace Domain.MessagesDTO
{
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