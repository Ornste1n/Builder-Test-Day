using VContainer;
using UnityEngine;
using MessagePipe;
using Repositories;
using VContainer.Unity;
using Presentation.View;
using Infrastructure.Grid;
using UnityEngine.UIElements;
using Infrastructure.Building;
using Presentation.Presenters;
using Application.UseCases.Grid;
using System.Collections.Generic;
using Infrastructure.InputSystem;
using Infrastructure.DI.Bootstrap;
using Application.UseCases.Camera;
using Application.Interfaces.Grid;
using Application.Interfaces.Entity;
using Application.Interfaces.Camera;
using Application.UseCases.Building;
using Application.Interfaces.Buildings;
using Infrastructure.InputSystem.Adapters;
using Infrastructure.InputSystem.Services;
using GridMeshRenderer = Presentation.GridMeshRenderer;

namespace Infrastructure.DI.Installers
{
    /// <summary>
    /// Один большой LifetimeScope, нужно разделить
    /// </summary>
    public class GamePresentationScope : LifetimeScope
    {
        // Presentation/UI Fields
        [Header("UI / Presentation")]
        [SerializeField] private BuildingCatalogConfig _buildingCatalog;
        [SerializeField] private BuildingUIConfig _buildingUIConfig; 
        [SerializeField] private BuildingBarView buildingBarView;
        [SerializeField] private Transform _buildingsParent;

        // Game / Runtime Fields
        [Header("Game / Runtime")]
        [SerializeField] private GridMeshRenderer _gridMeshRenderer;
        [SerializeField] private UIDocument _uiDocument;
        [SerializeField] private GridConfigSo _gridConfig;
        [SerializeField] private CameraPresenter _cameraPresenter;
        [SerializeField] private CameraMovementConfig _cameraMovementConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterMessagePipe();

            // UI / Presentation Binds
            builder.RegisterInstance(_buildingUIConfig).As<BuildingUIConfig>().AsSelf();
            builder.RegisterInstance(_buildingCatalog).As<BuildingCatalogConfig>().AsSelf();
            
            builder.RegisterInstance(buildingBarView)
                .AsSelf()
                .As<LayoutViewBase>()
                .As<BuildingBarView>();
            
            builder.Register<BuildingBarPresenter>(Lifetime.Singleton);
            builder.RegisterEntryPoint<PresentationBootstrap>();

            builder.Register<GridHighlightHandler>(Lifetime.Singleton)
                .As<INonLazy>()
                .AsSelf(); 
            
            builder.Register<ScriptableBuildingRepository>(Lifetime.Singleton)
                .As<IBuildingRepository>();
            
            builder.Register<BuildingFactory>(Lifetime.Singleton)
                .As<IBuildingFactory>()
                .WithParameter(_buildingsParent);


            // Input / Controls
            InputSystemControls controls = new();
            controls.Enable();
            builder.RegisterInstance(controls).As<InputSystemControls>().AsSelf();
            builder.RegisterInstance(_uiDocument).As<UIDocument>().WithParameter(Lifetime.Singleton);

            // Game / Grid / Camera Binds
            builder.RegisterInstance(_gridConfig).As<IGridConfig>().AsSelf();
            builder.RegisterInstance(_cameraMovementConfig).As<ICameraMoveSettings>().AsSelf(); 
            builder.RegisterInstance(_cameraPresenter).As<ICameraMovement>().WithParameter(Lifetime.Singleton); 
            
            builder.Register<GridRenderPublisher>(Lifetime.Singleton).As<IGridRenderPublisher>();
            builder.Register<CameraInputAdapter>(Lifetime.Singleton).As<ICameraInput>();
            builder.Register<PlacementInputAdapter>(Lifetime.Singleton).As<IPlacementInput>();
            
            builder.Register<IGridRepository, GridRepository>(Lifetime.Singleton);
            builder.Register<CameraInputService>(Lifetime.Singleton).As<INonLazy>().AsSelf(); 
            builder.Register<InitializeGridUseCase>(Lifetime.Singleton);

            builder.RegisterInstance(_gridMeshRenderer).As<IGridRenderer>().WithParameter(Lifetime.Singleton);
            builder.Register<CameraController>(Lifetime.Singleton).As<INonLazy>().AsSelf(); 
            builder.Register<PlaceBuildingUseCase>(Lifetime.Singleton).As<INonLazy>().AsSelf(); 
            builder.Register<PlacementInputService>(Lifetime.Singleton).As<INonLazy>().AsSelf(); 
            
            // Entry Points
            builder.RegisterEntryPoint<GridEntryPoint>();

            // Build Callback для INonLazy
            builder.RegisterBuildCallback(container =>
            {
                container.Resolve<IEnumerable<INonLazy>>();
            });
        }
    }
}
