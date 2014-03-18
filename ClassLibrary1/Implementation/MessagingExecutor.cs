using System;
using Domain.Interface;

namespace Domain.Implementation
{
    internal class MessagingExecutor : IMessagingExecutor
    {
        public Action<IMessage> ReceiveMessage { get; set; }

        public void SendMessage(IMessage message)
        {
            if (ReceiveMessage != null)
                ReceiveMessage(message);
        }
    }
}