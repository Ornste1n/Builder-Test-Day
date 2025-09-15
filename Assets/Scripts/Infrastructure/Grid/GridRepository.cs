using Application.Interfaces.Grid;
using Domain.Models;

namespace Infrastructure.Grid
{
    public class GridRepository : IGridRepository
    {
        public GridMap Grid { get; private set; }

        public void SetGrid(GridMap gridMap) => Grid = gridMap;
    }
}