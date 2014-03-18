using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Domain.Interface;

namespace Domain.Implementation
{
    internal class ConnectionStringValidator
    {
        public bool AllNameMatch(
            IEnumerable<IConnectionStringItemForValidator> compareFrom,
            IEnumerable<IConnectionStringItemForValidator> compareTo)
        {
            return compareFrom.Select(x => x.Name)
                .Intersect(compareTo.Select(y => y.Name))
                .Count() == compareFrom.Count();
        }

        public IEnumerable<T> IsValid<T>(
            IConnectionStringItemForValidator connectionStringItem,
            T[] rules)
            where T : IConnectionStringValidator
        {
            return from rule in rules
                   where allCriteriaMatch(rule, connectionStringItem)
                   select rule;
        }

        private bool allCriteriaMatch(IConnectionStringValidator itemValidator, IConnectionStringItemForValidator realValue)
        {
            return (from val in itemValidator
                    where
                        !itemValidator[val].Match.HasValue ||
                        Regex.Match(realValue[val], itemValidator[val].Regex, RegexOptions.IgnoreCase).Success ==
                        itemValidator[val].Match.Value
                    select true).Count() == itemValidator.Count();
        }
    }
}
