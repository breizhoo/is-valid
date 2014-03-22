using Domain.Interface;

namespace Domain.Implementation
{
    internal class ConnectionStringItemValidator : IConnectionStringItemValidator
    {

        public string Regex { get; set; }

        public bool Match { get; set; }

        public bool Criteria { get; set; }

        public bool Active { get; set; }
    }
}