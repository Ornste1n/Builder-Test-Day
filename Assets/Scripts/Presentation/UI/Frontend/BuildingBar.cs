using UnityEngine;
using Repositories;
using UnityEngine.UIElements;
using AYellowpaper.SerializedCollections;

namespace Presentation.UI.Frontend
{
    public class BuildingBar
    {
        private readonly UIDocument _uiDocument;
        private readonly BuildingUIConfig _uiConfig;
        
        public BuildingBar(UIDocument uiDocument, BuildingUIConfig uiConfig, BuildingCatalogConfig catalogConfig)
        {
            _uiDocument = uiDocument;
            _uiConfig = uiConfig;

            /*var a = catalogConfig.Catalog.Values;
            
            PresetPanel(catalogConfig.Catalog);*/
        }

        private void PresetPanel()//SerializedDictionary<BuildingType, BuildingConfig> catalog)
        {
            
            /*Debug.Log(_uiDocument + " " + _uiConfig);
            
            BuildingUIConfig uiConfig = _uiConfig;
            VisualElement container = _uiDocument.rootVisualElement.Q<VisualElement>("building-panel");
            VisualTreeAsset buildItem = uiConfig.BuildItem;
            
            foreach (BuildingConfig config in catalog.Values)
            {
                TemplateContainer buildContainer = buildItem.Instantiate();
                VisualElement buildIcon = buildContainer.Q<VisualElement>("build-icon");

                buildIcon.style.backgroundImage = new StyleBackground(config.Icon);
                
                container.Add(buildContainer);
            }*/
        }
    }
}
