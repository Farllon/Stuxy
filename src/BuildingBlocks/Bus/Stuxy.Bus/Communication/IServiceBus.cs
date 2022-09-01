using System;
using System.Threading;
using System.Threading.Tasks;

using Stuxy.Bus.Messaging;

namespace Stuxy.Bus.Communication
{
    public interface IServiceBus
    {
        IServiceProvider Context { get; }

        Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand;

        Task<TResponse> SendAsync<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand<TResponse>;

        Task<TResponse> QueryAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : IQuery<TResponse>;

        Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : Event;
    }
}
