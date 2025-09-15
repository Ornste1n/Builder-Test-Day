using System;
using MessagePipe;
using VContainer.Unity;
using UseCases.Application;
using Domain.Gameplay.MessagesDTO;
using Presentation.Gameplay;

namespace Infrastructure.Bootstrap
{
    public class GridEntryPoint : IStartable, IDisposable
    {
        private readonly InitializeGridUseCase _initUseCase;
        private readonly ISubscriber<GridInitialized> _gridInitSub;
        private readonly GridMeshRenderer _gridMeshRenderer;

        public GridEntryPoint
        (
            InitializeGridUseCase initUseCase,
            GridMeshRenderer renderer, 
            ISubscriber<GridInitialized> gridInitSub
        )
        {
            _initUseCase = initUseCase;
            _gridInitSub = gridInitSub;
            _gridMeshRenderer = renderer;
        }
        
        public void Start()
        {
            _gridInitSub.Subscribe(dto => _gridMeshRenderer.Build(dto.Width, dto.Height));
            _initUseCase.Execute();
        }
        
        public void Dispose()
        {
        }
    }
}
