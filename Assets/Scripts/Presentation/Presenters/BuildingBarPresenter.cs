using Repositories;
using Presentation.View;
using Cysharp.Threading.Tasks;

namespace Presentation.Presenters
{
    public class BuildingBarPresenter : LayoutPresenterBase<BuildingBarView>
    {
        private readonly BuildingCatalogConfig _catalogConfig;
        private readonly BuildingUIConfig _uiConfig;
        
        public BuildingBarPresenter(BuildingBarView view, BuildingUIConfig uiConfig, 
            BuildingCatalogConfig catalogConfig) : base(view)
        {
            _uiConfig = uiConfig;
            _catalogConfig = catalogConfig;
        }
        
        /// <summary>
        /// Инициализация презентера: показывает UI и наполняет панель
        /// </summary>
        public async UniTask InitializeAsync()
        {
            await ActivateAsync(); // Показываем View
            view.SetupPanel(_catalogConfig.Catalog, _uiConfig.BuildItem);
        }
    }
}