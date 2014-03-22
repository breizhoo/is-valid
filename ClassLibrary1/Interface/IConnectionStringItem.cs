using System.Collections.Generic;

namespace Domain.Interface
{
    public interface IConnectionStringItem : IEnumerable<ConnectionStringItemName>
    {
        string Name { get; }
        string ProviderName { get; }
        string ConnectionString { get; }

        string this[ConnectionStringItemName name]
        {
            get;
        }
    }
}