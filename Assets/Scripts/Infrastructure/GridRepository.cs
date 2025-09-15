using UseCases.Application;
using Domain.Gameplay.Models;

namespace Infrastructure
{
    public class GridRepository : IGridRepository
    {
        public GridMap Grid { get; private set; }

        public void SetGrid(GridMap gridMap) => Grid = gridMap;
    }
}