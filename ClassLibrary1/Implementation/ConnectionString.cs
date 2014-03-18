using System.Collections.Generic;
using Domain.Interface;

namespace Domain.Implementation
{
    internal class ConnectionStringItemItem : Dictionary<ConnectionStringItemName, string>, IConnectionStringItem
    {
        public string Name
        {
            get { return this[ConnectionStringItemName.Name]; }
            set { this[ConnectionStringItemName.Name] = value; }
        }

        public string ProviderName
        {
            get { return this[ConnectionStringItemName.ProviderName]; }
            set { this[ConnectionStringItemName.ProviderName] = value; }
        }

        public string ConnectionString
        {
            get { return this[ConnectionStringItemName.ConnectionString]; }
            set { this[ConnectionStringItemName.ConnectionString] = value; }
        }

        public new IEnumerator<ConnectionStringItemName> GetEnumerator()
        {
            return Keys.GetEnumerator();
        }
    }
}