﻿using System;
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
        public ConnectionStringRulesValidatorSimple(IConnectionStringRulesValidatorSimple copy)
        {
            RuleName = copy.RuleName;
            Id = copy.Id;
            Project = new ConnectionStringItemValidator(copy.Project);
            File = new ConnectionStringItemValidator(copy.File);
            Provider = new ConnectionStringItemValidator(copy.Provider);
            Name = new ConnectionStringItemValidator(copy.Name);
            ConnectionString = new ConnectionStringItemValidator(copy.ConnectionString);
        }

        public ConnectionStringRulesValidatorSimple()
        {
            Id = Guid.NewGuid();
            Project = new ConnectionStringItemValidator();
            File = new ConnectionStringItemValidator();
            Provider = new ConnectionStringItemValidator();
            Name = new ConnectionStringItemValidator();
            ConnectionString = new ConnectionStringItemValidator();
        }
        
        [DataMember]
        public string RuleName { get; set; }

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
    internal class ConnectionStringRulesValidator : ConnectionStringItemBase<IConnectionStringItemValidator>, IConnectionStringRulesValidator, IConnectionStringRulesValidatorSimple
    {
        public ConnectionStringRulesValidator(IConnectionStringRulesValidatorSimple copy)
        {
            Id               = copy.Id;
            Project          = copy.Project;
            File             = copy.File;
            Provider         = copy.Provider;
            Name             = copy.Name;
            ConnectionString = copy.ConnectionString;
            RuleName         = copy.RuleName;
        }

        public ConnectionStringRulesValidator()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Name
        /// </summary>
        [DataMember]
        public string RuleName { get; set; }

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

    internal class ConnectionStringItemForValidator : ConnectionStringItemBase<string>, IConnectionStringItemForValidator, IConnectionStringItemForValidatorSimple
    {
        public new IEnumerator GetEnumerator()
        {
            return base.GetEnumerator();
        }
    }

    [DataContract]
    internal abstract class ConnectionStringItemBase<T>
    {
        private readonly Dictionary<ConnectionStringValidatorName, T> _items;

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