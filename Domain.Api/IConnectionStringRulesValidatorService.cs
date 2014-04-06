using System.Collections.Generic;
using Domain.Interface;

namespace Domain.Implementation
{
    public interface IConnectionStringRulesValidatorService
    {
        IConnectionStringRulesValidatorSimple GetNew();

        IEnumerable<IConnectionStringRulesValidatorSimple> Get();

        void Delete(IConnectionStringRulesValidatorSimple connectionStringRulesValidator);

        void Save(IConnectionStringRulesValidatorSimple connectionStringRulesValidator);

        void Save(IEnumerable<IConnectionStringRulesValidatorSimple> connectionStringRulesValidators);


    }
}