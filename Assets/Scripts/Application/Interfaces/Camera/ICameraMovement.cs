using UnityEngine;

namespace Application.Interfaces.Camera
{
    public interface ICameraMovement
    {
        void Move(Vector3 newPosition);
        void SetZoom(float value);

        float GetZoom();
        Vector3 GetPosition();
    }
}