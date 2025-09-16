using Presentation.Presenters;
using VContainer.Unity;

namespace Infrastructure.DI.Bootstrap
{
    public class PresentationBootstrap : IStartable
    {
        private readonly BuildingBarPresenter _buildingBarPresenter;
        
        public PresentationBootstrap(BuildingBarPresenter barPresenter)
        {
            _buildingBarPresenter = barPresenter;
        }
        
        public async void Start()
        {
            await _buildingBarPresenter.InitializeAsync();
        }
    }
}