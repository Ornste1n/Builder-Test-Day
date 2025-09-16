using System;
using System.Collections.Generic;
using Application.Interfaces.Buildings;
using Repositories;
using UnityEngine.UIElements;
using Domain.Models.Buildings;
using AYellowpaper.SerializedCollections;

namespace Presentation.View
{
    public class BuildingBarView : LayoutViewBase
    {
        public event Action OnBuildIconClicked;
        
        protected override void InitializeElements() { }
        
        /// <summary>
        /// Инициализация панели с элементами зданий
        /// </summary>
        public void SetupPanel(IBuildingRepository repository,
            VisualTreeAsset template, EventCallback<ClickEvent> callback)
        {
            UnityEngine.Debug.Log("Setup Panel");

            VisualElement container = root.Q<VisualElement>("building-panel");
            container.Clear();

            foreach ((BuildingType type, IBuildingConfig config) in repository.GetAll())
            {
                TemplateContainer buildContainer = template.Instantiate();
                VisualElement buildIcon = buildContainer.Q<VisualElement>("build-icon");
                buildIcon.style.backgroundImage = new StyleBackground(config.Icon);

                buildIcon.RegisterCallback(callback);
                
                container.Add(buildContainer);
            }
        }
    }
}