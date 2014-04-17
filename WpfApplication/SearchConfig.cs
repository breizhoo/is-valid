using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using Domain.Implementation;
using Domain.Interface;

namespace WpfApplication
{
    public partial class SearchConfig : ObservableCollection<SearchResult>, INotifyPropertyChanged
    {
        private readonly ICsprojFinder _csprojFinder;
        private readonly ICsprojParseur _csprojParseur;
        private readonly IMessagingReceiver _message;
        public ObservableCollection<SearchResult> _searchValues = new ObservableCollection<SearchResult>();
        public ObservableCollection<ConnectionRules> _rules = new ObservableCollection<ConnectionRules>();
        private readonly IConnectionStringRulesValidatorService _connectionStringRulesValidatorService;
        private string _stateMessage;

        public string StateMessage
        {
            get { return _stateMessage; }
            set
            {
                _stateMessage = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<SearchResult> SearchValues
        {
            get { return _searchValues; }
            set { _searchValues = value; }
        }

        public ObservableCollection<ConnectionRules> Rules
        {
            get { return _rules; }
            set { _rules = value; }
        }


        public SearchConfig(
            ICsprojFinder csprojFinder,
            ICsprojParseur csprojParseur,
            IMessagingReceiver message,
            IConnectionStringRulesValidatorService connectionStringRulesValidatorService
            )
        {
            _connectionStringRulesValidatorService = connectionStringRulesValidatorService;
            _message = message;
            _csprojFinder = csprojFinder;
            _csprojParseur = csprojParseur;
            var items = connectionStringRulesValidatorService.Get();
            foreach (var connectionStringRulesValidatorSimple in items)
                Rules.Add(new ConnectionRules(connectionStringRulesValidatorSimple));
        }

        public void SaveRules()
        {
            _connectionStringRulesValidatorService.Save(Rules.Select(x => x.Convert()).ToArray());
        }

        public void CreateNewRules()
        {
            var elm = new ConnectionRules(_connectionStringRulesValidatorService.GetNew())
                      {
                          Name = "New to"
                      };
            Rules.Add(elm);
        }

        public void DeleteRules(ConnectionRules rule)
        {
            Rules.Remove(rule);
        }
        public void CloneRules(ConnectionRules rule)
        {
            var elm = new ConnectionRules(_connectionStringRulesValidatorService.GetNew(rule.Convert()));
            elm.Name += " (copie)";
            Rules.Add(elm);
        }
        
        public async void Launch(string directoriesSearch)
        {
            Clear();
            SearchValues.Clear();

            await Task.Run(() =>
            {
                StateMessage = "Search begin.";
                _csprojFinder.DirSearch(directoriesSearch, new ForCsProjFind(this).CsProjFind);
                StateMessage = "Search finish.";
            });

        }

        private class ForCsProjFind
        {
            private readonly SearchConfig _searchConfig;

            public ForCsProjFind(SearchConfig searchConfig)
            {
                _searchConfig = searchConfig;
            }

            public void CsProjFind(FileInfo fileInfo)
            {
                if (!fileInfo.Exists)
                    return;

                _searchConfig._message.ReceiveMessage += ReceiveMessage;
                _searchConfig._csprojParseur.Parse(fileInfo.FullName);
            }

            private void ReceiveMessage(IMessage message)
            {
                var searchResult = new SearchResult
                {
                    Message = message.Message,
                    ErrorCode = message.ErrorCode,
                    Id = message.Id,
                    TypeError = (TypeError)message.TypeError
                };

                Application.Current.Dispatcher.Invoke(() =>
                    _searchConfig.SearchValues.Add(searchResult));
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}