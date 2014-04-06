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
}