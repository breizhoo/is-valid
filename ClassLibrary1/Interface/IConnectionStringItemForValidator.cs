using System.Collections.Generic;

namespace Domain.Interface
{
    public interface IConnectionStringItemForValidator : IEnumerable<ConnectionStringValidatorName>, IConnectionStringItemForValidatorSimple
    {
        string this[ConnectionStringValidatorName name]
        {
            get;
        }
    }

    public interface IConnectionStringItemForValidatorSimple
    {
        string Project { get; }

        string File { get; }

        string Provider { get; }

        string Name { get; }

        string ConnectionString { get; }
    }
}