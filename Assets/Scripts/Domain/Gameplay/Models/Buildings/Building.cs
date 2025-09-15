using UnityEngine;

namespace Domain.Gameplay.Models.Buildings
{
    public class Building
    {
        public int Id { get; }
        public BuildingType BuildingType { get; }
        public int CurrentLevel { get; private set; }
        public Vector2Int GridPosition { get; private set; }

        public Building(int id, BuildingType type, Vector2Int gridPosition)
        {
            Id = id;
            BuildingType = type;
            GridPosition = gridPosition;
        }
    }
}