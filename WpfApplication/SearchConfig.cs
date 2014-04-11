using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Domain.Implementation;
using Domain.Interface;

namespace WpfApplication
{
    public partial class SearchConfig : ObservableCollection<SearchResult>
    {
        private readonly ICsprojFinder _csprojFinder;
        private readonly ICsprojParseur _csprojParseur;
        private readonly IMessagingReceiver _message;
        public ObservableCollection<SearchResult> _searchValues = new ObservableCollection<SearchResult>();
        public ObservableCollection<ConnectionRules> _rules = new ObservableCollection<ConnectionRules>();
        private readonly IConnectionStringRulesValidatorService _connectionStringRulesValidatorService;

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

        public async void Launch(string directoriesSearch)
        {
            Clear();
            await Task.Run(() => _csprojFinder.DirSearch(directoriesSearch, new ForCsProjFind(this).CsProjFind));
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
                _searchConfig._csprojParseur.ParseAsync(fileInfo.FullName);
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
    }
}