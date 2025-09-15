using Application.UseCases.Grid;

namespace Application.Interfaces.Grid
{
    public interface IGridRenderPublisher
    {
        void Publish(GridRenderData data);
    }
}