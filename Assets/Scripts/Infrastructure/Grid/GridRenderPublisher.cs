using MessagePipe;
using Application.UseCases.Grid;
using Application.Interfaces.Grid;

namespace Infrastructure.Grid
{
    public class GridRenderPublisher : IGridRenderPublisher
    {
        private readonly IPublisher<GridRenderData> _publisher;
        
        public GridRenderPublisher(IPublisher<GridRenderData> publisher)
        {
            _publisher = publisher;
        }
        
        public void Publish(GridRenderData data)
        {
            _publisher.Publish(data);
        }
    }
}