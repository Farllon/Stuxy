using Stuxy.Bus.Messaging;

namespace Stuxy.Bus.Notifier.Notifications
{
    public abstract class BusNotification : Event
    {
        public string Message { get; protected set; }

        public BusNotification(string message)
        {
            Message = message;
        }
    }
}
