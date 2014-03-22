using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Domain.Interface;

namespace Domain.Implementation
{
    internal class ConnectionStringValidator : IConnectionStringValidator
    {
        /// <summary>
        /// Check if connectionString match and return the value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionStringItem"></param>
        /// <param name="rules"></param>
        /// <returns></returns>
        public IEnumerable<T> IsValid<T>(
            IConnectionStringItemForValidator connectionStringItem,
            IEnumerable<T> rules)
            where T : IConnectionStringRulesValidator
        {
            return from rule in rules
                   where AllCriteriaMatch(rule, connectionStringItem, true)
                   where !AllCriteriaMatch(rule, connectionStringItem, false)
                   select rule;
        }

        /// <summary>
        /// Check items.
        /// </summary>
        /// <param name="itemRulesValidator"></param>
        /// <param name="realValue"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        private bool AllCriteriaMatch(
            IConnectionStringRulesValidator itemRulesValidator,
            IConnectionStringItemForValidator realValue,
                        bool criteria)
        {
            return (from val in FilterItem(itemRulesValidator, criteria)
                    let item = itemRulesValidator[val]
                    where Regex.Match(realValue[val], item.Regex, RegexOptions.IgnoreCase).Success == item.Match
                    select true).Count() == itemRulesValidator.Count();
        }

        private static IEnumerable<ConnectionStringValidatorName> FilterItem(
            IConnectionStringRulesValidator itemRulesValidator,
            bool criteria)
        {
            return itemRulesValidator.Where(x => 
                itemRulesValidator[x] != null && 
                itemRulesValidator[x].Active && 
                itemRulesValidator[x].Active == criteria);
        }


    }
}
