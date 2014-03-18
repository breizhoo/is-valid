using System;

namespace Domain.Interface
{
    public interface IMessagingExecutor
    {
        Action<IMessage> ReceiveMessage { get; set; }

        void SendMessage(IMessage message);
    }
}