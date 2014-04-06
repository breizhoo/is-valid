namespace Domain.Interface
{
    /// <summary>
    /// Send a message.
    /// </summary>
    internal interface IMessagingSender
    {
        void SendMessage(IMessage message);
    }
}