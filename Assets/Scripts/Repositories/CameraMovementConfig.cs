using UnityEngine;
using Application.UseCases.Camera;

namespace Repositories
{
    [CreateAssetMenu(menuName = "Configurations/CameraMovementConfig")]
    public class CameraMovementConfig : ScriptableObject, ICameraMoveSettings
    {
        [field : SerializeField] public float DragSpeed { get; private set; }
        [field : SerializeField] public float ArrowSpeed { get; private set; }
        [field : SerializeField] public float ZoomSpeed { get; private set; }
        [field : SerializeField] public Vector2 ZoomBound { get; private set; }

        private void Reset()
        {
            DragSpeed = 1f;
            ZoomSpeed = 1f;
            ArrowSpeed = 1f;
            ZoomBound = Vector2.one;
        }
    }
}