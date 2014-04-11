using System;

namespace Domain.Interface
{
    public interface IConnectionStringRulesValidatorSimple 
    {
        Guid Id { get; set; }

        string RuleName { get; set; }

        IConnectionStringItemValidator Project { get; set; }

        IConnectionStringItemValidator File { get; set; }

        IConnectionStringItemValidator Provider { get; set; }

        IConnectionStringItemValidator Name { get; set; }

        IConnectionStringItemValidator ConnectionString { get; set; }
    }
}