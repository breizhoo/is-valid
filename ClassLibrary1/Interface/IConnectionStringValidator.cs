using System.Collections.Generic;

namespace Domain.Interface
{
    internal interface IConnectionStringValidator
    {
        /// <summary>
        /// Check if connectionString match and return the value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionStringItem"></param>
        /// <param name="rules"></param>
        /// <returns></returns>
        IEnumerable<T> IsValid<T>(
            IConnectionStringItemForValidator connectionStringItem,
            IEnumerable<T> rules)
            where T : IConnectionStringRulesValidator;
    }
}