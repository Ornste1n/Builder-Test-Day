using MessagePipe;
using Domain.Gameplay.Models;
using Repositories.Application;
using Domain.Gameplay.MessagesDTO;

namespace UseCases.Application
{
    public class InitializeGridUseCase
    {
        private readonly GridConfigSo _gridConfig;
        private readonly IGridRepository _gridRepository;
        private readonly IPublisher<GridInitialized> _publisher;

        public InitializeGridUseCase(GridConfigSo config, IGridRepository repo,
            IPublisher<GridInitialized> publisher)
        {
            _gridConfig = config;
            _gridRepository = repo;
            _publisher = publisher;
        }

        public void Execute()
        {
            GridConfigSo config = _gridConfig;
            GridMap gridMap = new (config.Width, config.Height);
            
            _gridRepository.SetGrid(gridMap);
            _publisher.Publish(new GridInitialized(gridMap.Width, gridMap.Height));
        }
    }
}