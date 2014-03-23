using System;
using System.Collections.Generic;

namespace Domain.Interface
{
    public interface IConnectionStringRulesValidator : IEnumerable<ConnectionStringValidatorName>
    {
        IConnectionStringItemValidator this[ConnectionStringValidatorName name]
        {
            get;
        }
    }

    public interface IConnectionStringRulesValidatorSimple 
    {
        Guid Id { get; }

        IConnectionStringItemValidator Project { get; }

        IConnectionStringItemValidator File { get; }

        IConnectionStringItemValidator Provider { get; }

        IConnectionStringItemValidator Name { get; }

        IConnectionStringItemValidator ConnectionString { get; }
    }
}