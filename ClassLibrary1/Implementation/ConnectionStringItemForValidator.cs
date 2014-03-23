using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Domain.Interface;

namespace Domain.Implementation
{
    [DataContract]
    [KnownType(typeof(ConnectionStringItemValidator))]
    public class ConnectionStringRulesValidatorSimple : IConnectionStringRulesValidatorSimple
    {
        public ConnectionStringRulesValidatorSimple()
        {
            Id = Guid.NewGuid();
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public IConnectionStringItemValidator Project { get; set; }

        [DataMember]
        public IConnectionStringItemValidator File { get; set; }

        [DataMember]
        public IConnectionStringItemValidator Provider { get; set; }

        [DataMember]
        public IConnectionStringItemValidator Name { get; set; }

        [DataMember]
        public IConnectionStringItemValidator ConnectionString { get; set; }
    }


    [DataContract]
    internal class ConnectionStringRulesValidator : ConnectionStringItemBase<IConnectionStringItemValidator>, IConnectionStringRulesValidator
    {
        public ConnectionStringRulesValidator()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Name
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        public new IEnumerator GetEnumerator()
        {
            return base.GetEnumerator();
        }
    }

    internal class ConnectionStringItemForValidator : ConnectionStringItemBase<string>, IConnectionStringItemForValidator
    {
        public new IEnumerator GetEnumerator()
        {
            return base.GetEnumerator();
        }
    }

    [DataContract]
    internal abstract class ConnectionStringItemBase<T>
    {
        private Dictionary<ConnectionStringValidatorName, T> _items;

        protected ConnectionStringItemBase()
        {
            _items = new Dictionary<ConnectionStringValidatorName, T>();
        }

        public T this[ConnectionStringValidatorName name]
        {
            get { return _items[name]; }
        }

        /// <summary>
        /// Name
        /// </summary>
        [DataMember]
        public T Name
        {
            get { return _items[ConnectionStringValidatorName.Name]; }
            set { _items[ConnectionStringValidatorName.Name] = value; }
        }

        /// <summary>
        /// The connection string.
        /// </summary>
        [DataMember]
        public T ConnectionString
        {
            get { return _items[ConnectionStringValidatorName.ConnectionString]; }
            set { _items[ConnectionStringValidatorName.ConnectionString] = value; }
        }

        /// <summary>
        /// Name of the Project
        /// </summary>
        [DataMember]
        public T Project
        {
            get { return _items[ConnectionStringValidatorName.Project]; }
            set { _items[ConnectionStringValidatorName.Project] = value; }
        }

        /// <summary>
        /// Name of the file
        /// </summary>
        [DataMember]
        public T File
        {
            get { return _items[ConnectionStringValidatorName.File]; }
            set { _items[ConnectionStringValidatorName.File] = value; }
        }

        /// <summary>
        /// Name of the provider
        /// </summary>
        [DataMember]
        public T Provider
        {
            get { return _items[ConnectionStringValidatorName.ProviderName]; }
            set { _items[ConnectionStringValidatorName.ProviderName] = value; }
        }

        /// <summary>
        /// Enumerator of keys.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<ConnectionStringValidatorName> GetEnumerator()
        {
            return _items.Keys.GetEnumerator();
        }
    }
}