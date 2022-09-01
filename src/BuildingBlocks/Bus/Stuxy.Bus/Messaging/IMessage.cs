using System;

namespace Stuxy.Bus.Messaging
{
    public interface IMessage
    {
        Guid MessageId { get; }

        DateTime SentDate { get; }

        string SenderType { get; }
    }
}
