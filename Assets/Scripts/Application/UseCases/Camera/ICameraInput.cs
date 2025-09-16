using UnityEngine;
using Application.Interfaces;

namespace Application.UseCases.Camera
{
    public interface ICameraInput
    {
        IEvent<Vector2> OnDrag { get; }
        IEvent<Vector2> OnMove { get; }
        IEvent<float>  OnZoom { get; }
    }
}