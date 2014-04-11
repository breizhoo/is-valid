namespace Domain.Interface
{
    /// <summary>
    /// Message to displayed
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Code of the error.
        /// </summary>
        int ErrorCode { get; }

        /// <summary>
        /// The message of the error.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Type of the error. (Info, Warn, Error)
        /// </summary>
        TypeError TypeError { get; }

        /// <summary>
        /// Identifier of the error.
        /// </summary>
        string Id { get; }

    }
}