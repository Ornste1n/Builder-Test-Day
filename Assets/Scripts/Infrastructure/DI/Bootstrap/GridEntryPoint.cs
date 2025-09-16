using System;
using MessagePipe;
using VContainer.Unity;
using Application.UseCases.Grid;
using Application.Interfaces.Grid;

namespace Infrastructure.DI.Bootstrap
{
    public class GridEntryPoint : IStartable, IDisposable
    {
        private readonly InitializeGridUseCase _initUseCase;
        private readonly ISubscriber<GridRenderData> _gridInitSub;
        private readonly IGridRenderer _gridMeshRenderer;

        public GridEntryPoint
        (
            InitializeGridUseCase initUseCase,
            IGridRenderer renderer, 
            ISubscriber<GridRenderData> gridInitSub
        )
        {
            _initUseCase = initUseCase;
            _gridInitSub = gridInitSub;
            _gridMeshRenderer = renderer;
        }
        
        public void Start()
        {
            _gridInitSub.Subscribe(Build);
            _initUseCase.Execute();
        }

        private void Build(GridRenderData gridDto)
        {
            _gridMeshRenderer.Render(gridDto);
        }
        
        public void Dispose()
        {
        }
    }
}
