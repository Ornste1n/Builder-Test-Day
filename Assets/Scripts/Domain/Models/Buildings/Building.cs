namespace Domain.Models.Buildings
{
    public class Building
    {
        public int Id { get; }
        public BuildingType BuildingType { get; }
        public int CurrentLevel { get; private set; }
        public Position GridPosition { get; private set; }

        public Building(int id, BuildingType type, Position gridPosition)
        {
            Id = id;
            BuildingType = type;
            GridPosition = gridPosition;
        }
    }
}