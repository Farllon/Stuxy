using MediatR;

namespace Stuxy.Bus.Messaging
{
    public interface ICommand<TResponse> : IMessage, IRequest<TResponse>
    {

    }

    public interface ICommand : IMessage, IRequest
    {

    }
}
