using System;
using MessagePipe;
using Application.Interfaces;
using Application.Interfaces.Entity;

namespace Infrastructure.MessagePipeUtility
{
    public class MessagePipeEvent<T> : IEvent<T>
    {
        private readonly ISubscriber<T> _subscriber;

        public MessagePipeEvent(ISubscriber<T> subscriber)
        {
            _subscriber = subscriber;
        }

        public IDisposable Subscribe(Action<T> handler)
        {
            return _subscriber.Subscribe(handler);
        }
    }
}