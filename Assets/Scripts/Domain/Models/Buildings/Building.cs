namespace Domain.Models.Buildings
{
    public struct Building
    {
        public int Id { get; }
        public BuildingType BuildingType { get; }
        public int CurrentLevel { get; private set; }
        public Position GridPosition { get; private set; }

        public Building(int id, BuildingType type, Position gridPosition)
        {
            Id = id;
            CurrentLevel = 0;
            BuildingType = type;
            GridPosition = gridPosition;
        }
    }
}