using System;

namespace Application.Interfaces
{
    public interface IEvent<T>
    {
        IDisposable Subscribe(Action<T> handler);
    }
}