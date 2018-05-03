using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace WikiLanglinks
{
    public class SearchViewModel : BaseViewModel
    {
        private readonly IWikiLanglinksApiClient _apiClient;

        public event Action LoadingStarted;

        public event Action<SearchResults> LoadingFinished;

        public SearchViewModel(IWikiLanglinksApiClient apiClient)
        {
            SearchCommand = new Command(async () => await Search());
            _apiClient = apiClient;
        }

        public ICommand SearchCommand { get; }

        public string SearchTerm
        {
            get { return _searchTerm; }
            set { SetValue(ref _searchTerm, value); }
        }
        private string _searchTerm;

        private async Task Search()
        {
            var searchRequest = new SearchRequest
            {
                SearchTerm = SearchTerm,
                Source = "en",
                Targets = new[] { "de", "es" }
            };

            LoadingStarted?.Invoke();

            var results = new SearchResults();
            try
            {
                results = await _apiClient.GetLanglinks(searchRequest);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e.Message}");
            }
            finally
            {
                LoadingFinished?.Invoke(results);
            }
        }
    }
}
