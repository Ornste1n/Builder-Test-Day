using System;
using UnityEngine;

namespace Application.UseCases.Camera
{
    public interface ICameraInput
    {
        IObservable<Vector2> OnDrag { get; }
        IObservable<Vector2> OnMove { get; }
        IObservable<float> OnZoom { get; }
    }
}