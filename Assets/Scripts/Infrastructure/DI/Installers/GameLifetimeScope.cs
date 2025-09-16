using VContainer;
using UnityEngine;
using MessagePipe;
using Repositories;
using VContainer.Unity;
using Infrastructure.Grid;
using UnityEngine.UIElements;
using Presentation.Presenters;
using Infrastructure.Bootstrap;
using Application.UseCases.Grid;
using System.Collections.Generic;
using Application.Interfaces.Buildings;
using Application.Interfaces.Camera;
using Application.Interfaces.Entity;
using Infrastructure.InputSystem;
using Application.Interfaces.Grid;
using Application.UseCases.Building;
using Application.UseCases.Camera;
using Infrastructure.DI.Bootstrap;
using Infrastructure.InputSystem.Adapters;
using Infrastructure.InputSystem.Services;
using GridMeshRenderer = Presentation.GridMeshRenderer;

namespace Infrastructure.DI.Installers
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private UIDocument _uiDocument;
        [SerializeField] private GridConfigSo _gridConfig;
        [SerializeField] private GridMeshRenderer _gridMeshRenderer;
        [SerializeField] private CameraPresenter _cameraPresenter;
        [SerializeField] private CameraMovementConfig _cameraMovementConfig;
    
        protected override void Configure(IContainerBuilder builder)
        {
            InputSystemControls controls = new();
            controls.Enable();
            builder.RegisterInstance(controls).As<InputSystemControls>().AsSelf();

            builder.RegisterInstance(_uiDocument).As<UIDocument>().WithParameter(Lifetime.Singleton);

            builder.RegisterInstance(_gridConfig).As<IGridConfig>().AsSelf();
            builder.RegisterInstance(_cameraMovementConfig).As<ICameraMoveSettings>().AsSelf(); 
            builder.RegisterInstance(_cameraPresenter).As<ICameraMovement>().WithParameter(Lifetime.Singleton); 
            builder.RegisterInstance(_gridMeshRenderer).As<IGridRenderer>().WithParameter(Lifetime.Singleton);
            
            builder.RegisterMessagePipe();
            
            builder.Register<GridRenderPublisher>(Lifetime.Singleton).As<IGridRenderPublisher>();
            builder.Register<CameraInputAdapter>(Lifetime.Singleton).As<ICameraInput>();
            builder.Register<PlacementInputAdapter>(Lifetime.Singleton).As<IPlacementInput>();
            
            // возможно вынести в другой скоуп application
            builder.Register<IGridRepository, GridRepository>(Lifetime.Singleton);
            builder.Register<CameraInputService>(Lifetime.Singleton);
            builder.Register<InitializeGridUseCase>(Lifetime.Singleton);
 
            builder.Register<CameraController>(Lifetime.Singleton).As<INonLazy>().AsSelf(); 
            builder.Register<PlaceBuildingUseCase>(Lifetime.Singleton).As<INonLazy>().AsSelf(); 
            builder.Register<PlacementInputService>(Lifetime.Singleton).As<INonLazy>().AsSelf(); 
            
            builder.RegisterEntryPoint<CameraInputService>();
            
            builder.RegisterEntryPoint<BootstrapCameraInputService>();
            builder.RegisterEntryPoint<GridEntryPoint>();
            
            builder.RegisterBuildCallback(container =>
            {
                container.Resolve<IEnumerable<INonLazy>>();
            });
        }
    }
}
