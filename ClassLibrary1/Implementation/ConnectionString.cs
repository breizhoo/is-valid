using System.Collections.Generic;
using Domain.Interface;

namespace Domain.Implementation
{
    /// <summary>
    /// Representation of connection string.
    /// </summary>
    internal class ConnectionStringItem : Dictionary<ConnectionStringItemName, string>, IConnectionStringItem
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get { return this[ConnectionStringItemName.Name]; }
            set { this[ConnectionStringItemName.Name] = value; }
        }

        /// <summary>
        /// Name of the provider
        /// </summary>
        public string ProviderName
        {
            get { return this[ConnectionStringItemName.ProviderName]; }
            set { this[ConnectionStringItemName.ProviderName] = value; }
        }

        /// <summary>
        /// The connection string.
        /// </summary>
        public string ConnectionString
        {
            get { return this[ConnectionStringItemName.ConnectionString]; }
            set { this[ConnectionStringItemName.ConnectionString] = value; }
        }

        /// <summary>
        /// Enumerator of keys.
        /// </summary>
        /// <returns></returns>
        public new IEnumerator<ConnectionStringItemName> GetEnumerator()
        {
            return Keys.GetEnumerator();
        }
    }
}