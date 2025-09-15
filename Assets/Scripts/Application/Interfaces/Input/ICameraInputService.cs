using System;
using UnityEngine;

namespace Application.Interfaces.Input
{
    public interface ICameraInputService
    {
        IObservable<Vector2> OnDrag { get; }
        IObservable<Vector2> OnMove { get; }
        IObservable<float> OnZoom { get; }
    }
}