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
}