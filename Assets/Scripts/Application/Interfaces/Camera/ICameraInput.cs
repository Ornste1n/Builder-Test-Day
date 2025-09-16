using Application.Interfaces.Entity;
using UnityEngine;

namespace Application.Interfaces.Camera
{
    public interface ICameraInput
    {
        IEvent<Vector2> OnDrag { get; }
        IEvent<Vector2> OnMove { get; }
        IEvent<float>  OnZoom { get; }
    }
}