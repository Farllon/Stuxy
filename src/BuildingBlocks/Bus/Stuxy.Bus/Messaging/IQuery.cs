using MediatR;

namespace Stuxy.Bus.Messaging
{
    public interface IQuery<TResponse> : IMessage, IRequest<TResponse>
    {
    }
}
