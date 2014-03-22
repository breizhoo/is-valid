using System;
using Domain.Interface;

namespace Domain.Implementation
{
    /// <summary>
    /// Messager
    /// </summary>
    internal class MessagingExecutor : IMessagingReceiver, IMessagingSender
    {
        /// <summary>
        /// Propage message.
        /// </summary>
        public Action<IMessage> ReceiveMessage { get; set; }

        /// <summary>
        /// Send a message.
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(IMessage message)
        {
            if (ReceiveMessage != null)
                ReceiveMessage(message);
        }
    }
}