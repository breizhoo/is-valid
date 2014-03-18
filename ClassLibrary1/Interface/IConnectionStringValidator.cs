using System.Collections.Generic;

namespace Domain.Interface
{
    public interface IConnectionStringValidator : IEnumerable<ConnectionStringValidatoName>
    {
        IConnectionStringItemValidator Project { get; }

        IConnectionStringItemValidator File { get; }

        IConnectionStringItemValidator Provider { get; }

        IConnectionStringItemValidator Name { get; }

        IConnectionStringItemValidator ConnectionString { get; }

        IConnectionStringItemValidator this[ConnectionStringValidatoName name]
        {
            get;
        }
    }
}