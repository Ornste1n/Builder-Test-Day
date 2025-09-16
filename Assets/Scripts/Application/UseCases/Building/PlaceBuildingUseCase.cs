using System;
using UnityEngine;
using Domain.Models.Buildings;
using Application.Interfaces.Grid;
using Application.Interfaces.Entity;
using Application.Interfaces.Buildings;

namespace Application.UseCases.Building
{
    public class PlaceBuildingUseCase : INonLazy, IDisposable
    {
        private readonly IDisposable _startSub;
        private IDisposable _confirmSub;

        private readonly IPlacementInput _input;
        private readonly IGridRepository _gridRepository;
        private readonly IBuildingFactory _buildingFactory;

        private bool _isPlacing;
        private BuildingType _currentType;

        public PlaceBuildingUseCase(IPlacementInput input, IGridRepository gridRepository, 
            IBuildingFactory buildingFactory)
        {
            _input = input;
            _gridRepository = gridRepository;
            _buildingFactory = buildingFactory;

            _startSub = _input.OnStartPlacement.Subscribe(StartPlacement);
        }

        private void StartPlacement(BuildingType type)
        {
            _currentType = type;
            _isPlacing = true;

            _confirmSub?.Dispose();
            _confirmSub = _input.OnConfirm.Subscribe(ConfirmPlacement);
        }

        private void ConfirmPlacement(Vector3 worldPos)
        {
            if (!_isPlacing) return;

            (int x, int y) = _gridRepository.WorldToCell(worldPos);

            if (_gridRepository.IsCellOccupied(x, y))
                return;

            _buildingFactory.Create(_currentType, worldPos);
            _gridRepository.SetCellOccupied(x, y, true);

            _isPlacing = false;
            _confirmSub?.Dispose();
        }

        public void Dispose()
        {
            _startSub?.Dispose();
            _confirmSub?.Dispose();
        }
    }
}
