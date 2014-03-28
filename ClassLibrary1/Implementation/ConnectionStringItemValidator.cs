using System.Runtime.Serialization;
using Domain.Interface;

namespace Domain.Implementation
{
    [DataContract]
    public class ConnectionStringItemValidator : IConnectionStringItemValidator
    {
        public ConnectionStringItemValidator(IConnectionStringItemValidator copy)
        {
            if (copy == null)
                return;

            Regex = copy.Regex;
            Match = copy.Match;
            Criteria = copy.Criteria;
            Active = copy.Active;
        }

        public ConnectionStringItemValidator()
        {
        }

        [DataMember]
        public string Regex { get; set; }

        [DataMember]
        public bool Match { get; set; }

        [DataMember]
        public bool Criteria { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}