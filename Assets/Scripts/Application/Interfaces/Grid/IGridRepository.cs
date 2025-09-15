using Domain.Models;

namespace Application.Interfaces.Grid
{
    public interface IGridRepository
    {
        GridMap Grid { get; }
        void SetGrid(GridMap gridMap);
    }
}
