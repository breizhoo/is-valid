using Domain.Interface;

namespace Domain.Implementation
{
    /// <summary>
    /// Message to displayed
    /// </summary>
    internal class Messaging: IMessage
    {
        /// <summary>
        /// Code of the error.
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// The message of the error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Type of the error. (Info, Warn, Error)
        /// </summary>
        public TypeError TypeError { get; set; }

        /// <summary>
        /// Identifier of the error.
        /// </summary>
        public string Id { get; set; }
    }
}