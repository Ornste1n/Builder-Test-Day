using System;
using UnityEngine;
using Domain.Models.Buildings;
using Application.Interfaces.Entity;
using Application.Interfaces.Buildings;

namespace Application.UseCases.Building
{
    public class PlaceBuildingUseCase : INonLazy, IDisposable
    {
        private readonly IDisposable _startSub;
        private readonly IDisposable _moveSub;
        private readonly IDisposable _confirmSub;
        private readonly IDisposable _cancelSub;

        private readonly IPlacementInput _input;
        /*private readonly IBuildingValidator _validator; // domain проверки
        private readonly IBuildingRepository _repo;
        private readonly IBuildingFactory _factory;
        private readonly IEventBus _eventBus; // опционально: публиковать изменения*/

        private bool _isPlacing;
        private BuildingType _currentType;
        private Vector3 _pointerPos;

        public PlaceBuildingUseCase
        (
            IPlacementInput input
            /*IBuildingValidator validator,
            IBuildingRepository repo,
            IBuildingFactory factory,
            IEventBus eventBus // опционально*/
        )
        {
            _input = input;
            /*_validator = validator;
            _repo = repo;
            _factory = factory;
            _eventBus = eventBus;*/

            Debug.Log("PlaceBuildingUseCase");
            
            // Подпишемся на входы
            _startSub = _input.OnStartPlacement.Subscribe(HandleStart);
            _moveSub = _input.OnPointerMove.Subscribe(HandlePointerMove);
            _confirmSub = _input.OnConfirm.Subscribe(HandleConfirm);
            /*_cancelSub = _input.OnCancel?.Subscribe(_ => HandleCancel()); // если OnCancel есть*/
        }

        private void HandleStart(BuildingType type)
        {
            _currentType = type;
            _isPlacing = true;
            Debug.Log("Handle Start");
        }
        
        private void HandleConfirm(Vector3 worldPos)
        {
            Debug.Log("HandleConfirm");
        }

        private void HandlePointerMove(Vector3 worldPos)
        {
            if (!_isPlacing) return;
            _pointerPos = worldPos;
            // логика SNAP / валидация для визуала (можно публиковать PlacementPreviewChanged и т.д.)
        }

        /*private void HandleConfirm(Vector3 worldPos)
        {
            if (!_isPlacing) return;

            var placementPos = worldPos; // или _pointerPos
            var validation = _validator.CanPlace(_currentType, placementPos);
            if (!validation.IsOk)
            {
                _eventBus?.Publish(new PlacementFailedMsg(validation.Reason));
                return;
            }

            var building = Domain.Building.Create(_currentType, placementPos);
            _repo.Add(building);
            _factory.Create(building);

            _isPlacing = false;
            _eventBus?.Publish(new BuildingPlacedMsg(building.Id, placementPos));
            _eventBus?.Publish(new PlacementModeChanged(false));
        }

        private void HandleCancel()
        {
            if (!_isPlacing) return;
            _isPlacing = false;
            _eventBus?.Publish(new PlacementModeChanged(false));
        }*/

        public void Dispose()
        {
            _startSub?.Dispose();
            _moveSub?.Dispose();
            _confirmSub?.Dispose();
            _cancelSub?.Dispose();
        }
    }
}
