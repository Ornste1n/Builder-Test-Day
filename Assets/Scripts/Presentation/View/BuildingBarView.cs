using UnityEngine.UIElements;
using Domain.Models.Buildings;
using Application.Interfaces.Buildings;

namespace Presentation.View
{
    public class BuildingBarView : LayoutViewBase
    {
        protected override void InitializeElements() { }
        
        /// <summary>
        /// Инициализация панели с элементами зданий
        /// </summary>
        public void SetupPanel(IBuildingRepository repository,
            VisualTreeAsset template, EventCallback<ClickEvent> callback)
        {
            VisualElement container = root.Q<VisualElement>("building-panel");
            container.Clear();

            foreach ((BuildingType type, IBuildingConfig config) in repository.GetAll())
            {
                TemplateContainer buildContainer = template.Instantiate();
                VisualElement buildIcon = buildContainer.Q<VisualElement>("build-icon");
                buildIcon.style.backgroundImage = new StyleBackground(config.Icon);

                buildIcon.userData = type;
                buildIcon.RegisterCallback(callback);
                container.Add(buildContainer);
            }
        }
    }
}