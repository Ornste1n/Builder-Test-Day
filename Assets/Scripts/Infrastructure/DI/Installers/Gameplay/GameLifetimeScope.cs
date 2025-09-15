using VContainer;
using MessagePipe;
using UnityEngine;
using VContainer.Unity;
using UseCases.Application;
using Infrastructure.Bootstrap;
using Presentation.Gameplay;
using Repositories.Application;

namespace Infrastructure.DI.Installers.Gameplay
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GridConfigSo _gridConfig;
        [SerializeField] private GridMeshRenderer _gridMeshRenderer;
    
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gridConfig).As<GridConfigSo>().AsSelf();
            builder.RegisterInstance(_gridMeshRenderer).As<GridMeshRenderer>().WithParameter(Lifetime.Singleton);;
            
            builder.RegisterMessagePipe();
            
            // возможно вынести в другой скоуп application
            builder.Register<IGridRepository, GridRepository>(Lifetime.Singleton);
            builder.Register<InitializeGridUseCase>(Lifetime.Singleton);

            builder.RegisterEntryPoint<GridEntryPoint>();
        }
    }
}
