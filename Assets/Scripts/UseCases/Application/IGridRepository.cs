using Domain.Gameplay.Models;

namespace UseCases.Application
{
    public interface IGridRepository
    {
        GridMap Grid { get; }
        void SetGrid(GridMap gridMap);
    }
}
