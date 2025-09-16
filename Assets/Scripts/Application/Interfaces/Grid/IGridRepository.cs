using Domain.Models;
using UnityEngine;

namespace Application.Interfaces.Grid
{
    public interface IGridRepository
    {
        GridMap Grid { get; }
        void SetGrid(GridMap gridMap);

        bool IsCellOccupied(int x, int y);
        void SetCellOccupied(int x, int y, bool occupied);
        (int x, int y) WorldToCell(Vector3 worldPos);
    }
}