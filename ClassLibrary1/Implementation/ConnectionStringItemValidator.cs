using System.Runtime.Serialization;
using Domain.Interface;

namespace Domain.Implementation
{
    [DataContract]
    public class ConnectionStringItemValidator : IConnectionStringItemValidator
    {

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