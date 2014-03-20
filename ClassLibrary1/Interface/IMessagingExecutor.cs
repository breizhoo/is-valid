using System;

namespace Domain.Interface
{
    /// <summary>
    /// Permit to receive a message.
    /// </summary>
    public interface IMessagingReceiver
    {
        Action<IMessage> ReceiveMessage { get; set; }
    }

    /// <summary>
    /// Send a message.
    /// </summary>
    internal interface IMessagingSender
    {
        void SendMessage(IMessage message);
    }
}