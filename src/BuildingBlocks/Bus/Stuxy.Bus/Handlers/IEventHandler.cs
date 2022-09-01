using System;

using MediatR;

using Stuxy.Bus.Messaging;

namespace Stuxy.Bus.Handlers
{
    public interface IEventHandler<TEvent> : INotificationHandler<TEvent>, IDisposable
        where TEvent : IEvent
    {
    }
}
