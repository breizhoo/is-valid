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

        string RuleName { get; set; }

        IConnectionStringItemValidator Project { get; set; }

        IConnectionStringItemValidator File { get; set; }

        IConnectionStringItemValidator Provider { get; set; }

        IConnectionStringItemValidator Name { get; set; }

        IConnectionStringItemValidator ConnectionString { get; set; }
    }
}