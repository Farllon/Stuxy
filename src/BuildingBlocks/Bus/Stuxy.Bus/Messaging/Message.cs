using System;

namespace Stuxy.Bus.Messaging
{
    public abstract class Message : IMessage
    {
        public Guid MessageId { get; }

        public DateTime SentDate { get; }

        public string SenderType { get; }

        public Message()
        {
            MessageId = Guid.NewGuid();
            SentDate = DateTime.UtcNow;
            SenderType = GetType().Name;
        }
    }
}
