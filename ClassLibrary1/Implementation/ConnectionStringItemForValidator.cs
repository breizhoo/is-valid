using System;
using System.Collections.Generic;
using Domain.Interface;

namespace Domain.Implementation
{
    internal class ConnectionStringRulesValidator : ConnectionStringItemBase<IConnectionStringItemValidator>, IConnectionStringRulesValidator
    {
        public ConnectionStringRulesValidator()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Name
        /// </summary>
        public Guid Id { get; set; }
    }

    internal class ConnectionStringItemForValidator : ConnectionStringItemBase<string>, IConnectionStringItemForValidator
    {
        
    }

    internal abstract class ConnectionStringItemBase<T> : Dictionary<ConnectionStringValidatorName, T>
    {

        /// <summary>
        /// Name
        /// </summary>
        public T Name
        {
            get { return this[ConnectionStringValidatorName.Name]; }
            set { this[ConnectionStringValidatorName.Name] = value; }
        }

        /// <summary>
        /// The connection string.
        /// </summary>
        public T ConnectionString
        {
            get { return this[ConnectionStringValidatorName.ConnectionString]; }
            set { this[ConnectionStringValidatorName.ConnectionString] = value; }
        }

        /// <summary>
        /// Name of the Project
        /// </summary>
        public T Project
        {
            get { return this[ConnectionStringValidatorName.Project]; }
            set { this[ConnectionStringValidatorName.Project] = value; }
        }

        /// <summary>
        /// Name of the file
        /// </summary>
        public T File
        {
            get { return this[ConnectionStringValidatorName.File]; }
            set { this[ConnectionStringValidatorName.File] = value; }
        }

        /// <summary>
        /// Name of the provider
        /// </summary>
        public T Provider
        {
            get { return this[ConnectionStringValidatorName.ProviderName]; }
            set { this[ConnectionStringValidatorName.ProviderName] = value; }
        }

        /// <summary>
        /// Enumerator of keys.
        /// </summary>
        /// <returns></returns>
        public new IEnumerator<ConnectionStringValidatorName> GetEnumerator()
        {
            return Keys.GetEnumerator();
        }

    }
}