using Application.Interfaces.Grid;
using Domain.Models;
using UnityEngine;

namespace Infrastructure.Grid
{
    public class GridRepository : IGridRepository
    {
        public GridMap Grid { get; private set; }

        public void SetGrid(GridMap gridMap) => Grid = gridMap;

        public bool IsCellOccupied(int x, int y)
        {
            if (Grid == null) return true;
            return Grid.IsOccupied(x, y);
        }

        public void SetCellOccupied(int x, int y, bool occupied)
        {
            if (Grid == null) return;
            Grid.SetOccupied(x, y, occupied);
        }

        public (int x, int y) WorldToCell(Vector3 worldPos)
        {
            if (Grid == null) return (-1, -1);

            float cellSize = Grid.CellSize;
            Vector3 origin = Vector3.zero;

            float localX = worldPos.x - origin.x - cellSize * 0.5f;
            float localZ = worldPos.z - origin.z - cellSize * 0.5f;

            int x = Mathf.FloorToInt(localX / cellSize);
            int y = Mathf.FloorToInt(localZ / cellSize);

            if (x < 0 || x >= Grid.Width || y < 0 || y >= Grid.Height)
                return (-1, -1);

            return (x, y);
        }

    }
}