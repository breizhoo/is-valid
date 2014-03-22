using System.Collections.Generic;

namespace Domain.Interface
{
    public interface IConnectionStringValidator : IEnumerable<ConnectionStringValidatorName>
    {
        IConnectionStringItemValidator Project { get; }

        IConnectionStringItemValidator File { get; }

        IConnectionStringItemValidator Provider { get; }

        IConnectionStringItemValidator Name { get; }

        IConnectionStringItemValidator ConnectionString { get; }

        IConnectionStringItemValidator this[ConnectionStringValidatorName name]
        {
            get;
        }
    }
}