using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using WikiLanglinks.Models;
using WikiLanglinks.Services;
using Xamarin.Forms;

namespace WikiLanglinks.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        private readonly IWikiLanglinksApiClient _apiClient;

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

            var results = await _apiClient.GetLanglinks(searchRequest);
            Debug.WriteLine($"Fetched {results.LangLinks.Length} results");
        }
    }
}
