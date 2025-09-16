using System;

namespace Application.Interfaces.Entity
{
    public interface IEvent<T>
    {
        IDisposable Subscribe(Action<T> handler);
    }
}