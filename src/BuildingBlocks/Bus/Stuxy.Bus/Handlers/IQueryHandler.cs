using System;

using MediatR;

using Stuxy.Bus.Messaging;

namespace Stuxy.Bus.Handlers
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>, IDisposable
        where TQuery : IQuery<TResponse>
    {

    }
}
