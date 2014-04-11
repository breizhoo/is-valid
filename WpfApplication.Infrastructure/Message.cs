namespace WpfApplication.Infrastructure
{
    public class MMessage
    {
        public int ErrorCode { get; set; }

        string Message { get; set; }

        public TypeError TypeError { get; set; }

        public string Id { get; set; }
    }
}
