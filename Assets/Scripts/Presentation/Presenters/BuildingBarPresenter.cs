using MessagePipe;
using Repositories;
using Presentation.View;
using Application.Messages;
using UnityEngine.UIElements;
using Cysharp.Threading.Tasks;
using Domain.Models.Buildings;
using Application.Interfaces.Buildings;

namespace Presentation.Presenters
{
    public class BuildingBarPresenter : LayoutPresenterBase<BuildingBarView>
    {
        private readonly BuildingUIConfig _uiConfig;
        private readonly IBuildingRepository _buildingRepository;
        private readonly IPublisher<StartPlacementMsg> _startPlacementPublisher;
        
        public BuildingBarPresenter
        (
            BuildingBarView view, BuildingUIConfig uiConfig, 
            IPublisher<StartPlacementMsg> publisher,
            IBuildingRepository repository) : base(view)
        {
            _uiConfig = uiConfig;
            _buildingRepository = repository;
            _startPlacementPublisher = publisher;
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
            if (evt.target is VisualElement ve && ve.userData is BuildingType type)
            {
                _startPlacementPublisher.Publish(new StartPlacementMsg(type));
            }
        }
    }
}