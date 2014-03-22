using System.Collections.Generic;

namespace Domain.Interface
{
    public interface IConnectionStringItemForValidator : IEnumerable<ConnectionStringValidatorName>
    {
        string Project { get; }

        string File { get; }

        string Provider { get; }

        string Name { get; }

        string ConnectionString { get; }

        string this[ConnectionStringValidatorName name]
        {
            get;
        }
    }
}