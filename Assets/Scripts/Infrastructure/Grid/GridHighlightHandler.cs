using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using Application.Interfaces.Grid;
using Infrastructure.WorldSettings;
using Application.Interfaces.Entity;
using Application.Interfaces.Buildings;

namespace Infrastructure.Grid
{
    public class GridHighlightHandler : INonLazy
    {
        private readonly IGridRenderer _gridRenderer;

        private bool _highlightActive = false;
        private bool _cancelled = false;

        private Color _lastCellColor;

        public GridHighlightHandler(IGridRenderer gridRenderer, IPlacementInput placementInput)
        {
            _gridRenderer = gridRenderer;
            IPlacementInput placementInput1 = placementInput;

            placementInput1.OnStartPlacement.Subscribe(_ =>
            {
                _highlightActive = true;
                _cancelled = false;
                StartHighlightLoopAsync().Forget();
            });

            placementInput1.OnConfirm.Subscribe(_ =>
            {
                _cancelled = true;
                _highlightActive = false;
                HighlightCell(Vector3.positiveInfinity);
            });

            placementInput1.OnCancel.Subscribe(_ =>
            {
                _cancelled = true;
                _highlightActive = false;
                HighlightCell(Vector3.positiveInfinity);
            });
        }

        private async UniTaskVoid StartHighlightLoopAsync()
        {
            while (_highlightActive && !_cancelled)
            {
                Vector3 mouseWorldPos = GetMouseWorldPosition();
                if (mouseWorldPos != Vector3.positiveInfinity)
                {
                    HighlightCell(mouseWorldPos);
                }

                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }

        private void HighlightCell(Vector3 worldPos)
        {
            _gridRenderer.HighlightCellAtWorldPos(worldPos);
        }

        private Vector3 GetMouseWorldPosition()
        {
            if (Camera.main == null) return Vector3.positiveInfinity;

            Vector3 screenPos;
            if (Mouse.current != null)
            {
                screenPos = Mouse.current.position.ReadValue();
            }
            else
            {
                return Vector3.positiveInfinity;
            }

            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, 1 << (int)WorldLayers.Ground))
            {
                return hit.point;
            }

            return Vector3.positiveInfinity;
        }
    }
}
