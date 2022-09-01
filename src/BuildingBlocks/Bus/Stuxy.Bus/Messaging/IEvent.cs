using MediatR;

namespace Stuxy.Bus.Messaging
{
    public interface IEvent : IMessage, INotification
    {
    }
}
