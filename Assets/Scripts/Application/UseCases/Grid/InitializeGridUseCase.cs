using Domain.Models;
using Application.Interfaces.Grid;

namespace Application.UseCases.Grid
{
    public class InitializeGridUseCase
    {
        private readonly IGridConfig _gridConfig;
        private readonly IGridRepository _gridRepository;
        private readonly IGridRenderPublisher _gridPublisher;

        public InitializeGridUseCase(IGridConfig config, IGridRepository repo,
            IGridRenderPublisher publisher)
        {
            _gridConfig = config;
            _gridRepository = repo;
            _gridPublisher = publisher;
        }

        public void Execute()
        {
            IGridConfig config = _gridConfig;
            GridMap gridMap = new(config.Width, config.Height, config.CellSize);
            
            _gridRepository.SetGrid(gridMap);

            GridRenderData gridRenderData = new()
            {
                Width = config.Width,
                Height = config.Height,
                CellSize = config.CellSize,
                DefaultColorA = config.DefaultColorA,
                DefaultColorB = config.DefaultColorB,
                GridMaterial = config.TileMaterial
            };
            
            _gridPublisher.Publish(gridRenderData);
        }
    }
}