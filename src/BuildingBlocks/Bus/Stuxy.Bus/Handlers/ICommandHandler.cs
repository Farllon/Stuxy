using System;

using MediatR;

using Stuxy.Bus.Messaging;

namespace Stuxy.Bus.Handlers
{
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>, IDisposable
        where TCommand : ICommand<TResponse>
    {

    }

    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand>, IDisposable
        where TCommand : ICommand
    {

    }
}
