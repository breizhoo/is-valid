using System.Collections.Generic;

namespace Domain.Interface
{
    public interface IConnectionStringItemForValidator : IEnumerable<ConnectionStringValidatoName>
    {
        string Project { get; }

        string File { get; }

        string Provider { get; }

        string Name { get; }

        string ConnectionString { get; }

        string this[ConnectionStringValidatoName name]
        {
            get;
        }
    }
}