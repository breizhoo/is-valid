using Domain.Interface;

namespace Domain.Implementation
{
    internal class Messaging: IMessage
    {
        public int ErrorCode { get; set; }

        public string Message { get; set; }

        public TypeError TypeError { get; set; }

        public string Id { get; set; }
    }
}