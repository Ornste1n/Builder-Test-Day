using System;
using MessagePipe;
using UnityEngine;
using Application.Messages;
using UnityEngine.Rendering;
using Domain.Models.Buildings;

namespace Infrastructure.Building
{
    public class PreviewGraphicsService : IDisposable
    {
        private readonly IDisposable _posSub;
        private readonly IDisposable _startSub;
        private readonly IDisposable _cancelSub;
        private readonly IDisposable _confirmSub;

        //private readonly IBuildingMeshRegistry _registry;

        private bool _isActive;
        private BuildingType _currentType;
        private Mesh _mesh;
        private Material _material;
        private Matrix4x4 _matrix = Matrix4x4.identity;
        private Color _tint = new Color(0f, 1f, 0f, 0.6f);

        private readonly MaterialPropertyBlock _mpb = new MaterialPropertyBlock();
        private readonly float _snapSize = 1f;
        private readonly float _moveThreshold = 0.01f;
        private Vector3 _lastPos = Vector3.positiveInfinity;
        private Quaternion _rotation = Quaternion.identity;

        private bool _renderCallbacksRegistered;

        public PreviewGraphicsService
        (
            ISubscriber<StartPlacementMsg> startSub,
            ISubscriber<PointerWorldPosMsg> posSub,
            ISubscriber<ConfirmPlacementMsg> confirmSub,
            ISubscriber<CancelPlacementMsg> cancelSub
            //IBuildingMeshRegistry registry
        )
        {
            //_registry = registry;

            _startSub = startSub.Subscribe(OnStartPlacement);
            _posSub = posSub.Subscribe(OnPointerMove);
            _confirmSub = confirmSub.Subscribe(OnConfirm);
            _cancelSub = cancelSub.Subscribe(_ => Cancel());

            RegisterRenderCallbacks(); 
        }

        private void RegisterRenderCallbacks()
        {
            if (_renderCallbacksRegistered) return;

            RenderPipelineManager.endCameraRendering += RenderForCamera;
            _renderCallbacksRegistered = true;
        }

        private void UnregisterRenderCallbacks()
        {
            if (!_renderCallbacksRegistered) return;

            RenderPipelineManager.beginCameraRendering -= RenderForCamera;

            _renderCallbacksRegistered = false;
        }

        private void OnStartPlacement(StartPlacementMsg msg)
        {
            /*_currentType = msg.Type;
            _mesh = _registry.GetMesh(_currentType);
            _material = _registry.GetMaterial(_currentType);
            _isActive = _mesh != null && _material != null;
            _lastPos = Vector3.positiveInfinity;*/
        }

        private void OnPointerMove(PointerWorldPosMsg msg)
        {
            if (!_isActive) return;

            Vector3 pos = SnapToGrid(msg.WorldPosition);
            if (Vector3.Distance(pos, _lastPos) < _moveThreshold) return;

            _lastPos = pos;
            _matrix = Matrix4x4.TRS(pos, _rotation, Vector3.one);

            bool valid = true;
            _tint = valid ? new Color(0f, 1f, 0f, 0.6f) : new Color(1f, 0f, 0f, 0.6f);
        }

        private void OnConfirm(ConfirmPlacementMsg msg)
        {
            Cancel();
        }

        private void Cancel()
        {
            _isActive = false;
            _mesh = null;
            _material = null;
        }

        private Vector3 SnapToGrid(Vector3 v)
        {
            return new Vector3(
                Mathf.Round(v.x / _snapSize) * _snapSize,
                v.y,
                Mathf.Round(v.z / _snapSize) * _snapSize
            );
        }

        private void RenderForCamera(ScriptableRenderContext scriptableRenderContext, Camera camera)
        {
            if (!_isActive || _mesh == null || _material == null) return;

            _mpb.Clear();
            if (_material.HasProperty("_Color"))
                _mpb.SetColor("_Color", _tint);
            else if (_material.HasProperty("_BaseColor"))
                _mpb.SetColor("_BaseColor", _tint);

            //Graphics.DrawMesh(_mesh, _matrix, _material, _registry.PreviewLayer, camera, 0, _mpb, castShadows: false, receiveShadows: false);
        }

        public void Dispose()
        {
            _startSub?.Dispose();
            _posSub?.Dispose();
            _confirmSub?.Dispose();
            _cancelSub?.Dispose();

            UnregisterRenderCallbacks();
        }
    }
}
