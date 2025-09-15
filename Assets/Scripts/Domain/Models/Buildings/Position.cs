namespace Domain.Models.Buildings
{
    public readonly struct Position
    {
        public int Row { get; }
        public int Col { get; }

        public Position(int row, int col) => (Row, Col) = (row, col);
    }
}