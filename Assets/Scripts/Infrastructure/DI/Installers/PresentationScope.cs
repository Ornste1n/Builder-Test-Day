using VContainer;
using UnityEngine;
using Repositories;
using VContainer.Unity;
using Presentation.View;
using Presentation.Presenters;
using Infrastructure.DI.Bootstrap;

namespace Infrastructure.DI.Installers
{ 
    public class PresentationScope : LifetimeScope
    {
        [SerializeField] private BuildingCatalogConfig _buildingCatalog;
        [SerializeField] private BuildingUIConfig _buildingUIConfig; 
        [SerializeField] private BuildingBarView buildingBarView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_buildingUIConfig).As<BuildingUIConfig>().AsSelf();
            builder.RegisterInstance(_buildingCatalog).As<BuildingCatalogConfig>().AsSelf();
            
            builder.RegisterInstance(buildingBarView).AsSelf().As<LayoutViewBase>()
                .As<BuildingBarView>();
            builder.Register<BuildingBarPresenter>(Lifetime.Singleton);

            builder.RegisterEntryPoint<PresentationBootstrap>();
        }
    }
}