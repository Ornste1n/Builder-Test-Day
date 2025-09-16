using System;
using MessagePipe;
using UnityEngine;
using Application.Interfaces;
using Application.Interfaces.Entity;

namespace Infrastructure.MessagePipeUtility
{
    public class MessagePipeEventMapper<TIn, TOut> : IEvent<TOut>
    {
        private readonly ISubscriber<TIn> _subscriber;
        private readonly Func<TIn, TOut> _map;

        public MessagePipeEventMapper(ISubscriber<TIn> subscriber, Func<TIn, TOut> map)
        {
            _subscriber = subscriber ?? throw new ArgumentNullException(nameof(subscriber));
            _map = map ?? throw new ArgumentNullException(nameof(map));
        }

        public IDisposable Subscribe(Action<TOut> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            return _subscriber.Subscribe(inValue =>
            {
                try
                {
                    TOut outValue = _map(inValue);
                    handler(outValue);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            });
        }
    }

}