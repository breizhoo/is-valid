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
}