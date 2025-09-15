using UnityEngine;
using Application.UseCases.Grid;

namespace Application.Interfaces.Grid
{
    public interface IGridRenderer
    {
        void Render(GridRenderData data);
        void UpdateCell(int x, int y, Color color);
    }
}