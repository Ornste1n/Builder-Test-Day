using Repositories;
using Presentation.View;
using UnityEngine.UIElements;
using Cysharp.Threading.Tasks;
using Application.Interfaces.Buildings;

namespace Presentation.Presenters
{
    public class BuildingBarPresenter : LayoutPresenterBase<BuildingBarView>
    {
        private readonly BuildingUIConfig _uiConfig;
        private readonly IBuildingRepository _buildingRepository;
        
        public BuildingBarPresenter(BuildingBarView view, BuildingUIConfig uiConfig, 
            IBuildingRepository repository) : base(view)
        {
            _uiConfig = uiConfig;
            _buildingRepository = repository;
        }
        
        /// <summary>
        /// Инициализация презентера: показывает UI и наполняет панель
        /// </summary>
        public async UniTask InitializeAsync()
        {
            await ActivateAsync(); // Показываем View
            view.SetupPanel(_buildingRepository, _uiConfig.BuildItem, HandleBuildItemCallback);
        }
        
        private void HandleBuildItemCallback(ClickEvent evt)
        {
            
        }
    }
}