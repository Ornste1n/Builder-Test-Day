using AYellowpaper.SerializedCollections;
using Domain.Models.Buildings;
using Repositories;
using UnityEngine.UIElements;

namespace Presentation.View
{
    public class BuildingBarView : LayoutViewBase
    {
        protected override void InitializeElements() { }
        
        /// <summary>
        /// Инициализация панели с элементами зданий
        /// </summary>
        public void SetupPanel(SerializedDictionary<BuildingType, BuildingConfig> catalog,
            VisualTreeAsset template)
        {
            UnityEngine.Debug.Log("Setup Panel");

            VisualElement container = root.Q<VisualElement>("building-panel");
            container.Clear();

            foreach (BuildingConfig config in catalog.Values)
            {
                TemplateContainer buildContainer = template.Instantiate();
                VisualElement buildIcon = buildContainer.Q<VisualElement>("build-icon");
                buildIcon.style.backgroundImage = new StyleBackground(config.Icon);

                container.Add(buildContainer);
            }
        }
    }
}