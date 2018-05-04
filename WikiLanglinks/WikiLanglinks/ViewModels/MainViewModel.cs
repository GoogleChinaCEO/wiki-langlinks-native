using System;
using System.Linq;

namespace WikiLanglinks
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(IWikiLanglinksApiClient apiClient)
		{
            SearchVM = new SearchViewModel(apiClient);
            ResultsVM = new ResultsViewModel();
            SearchVM.LoadingStarted += OnLoadingStarted;
            SearchVM.LoadingFinished += OnLoadingFinished;
		}

        public SearchViewModel SearchVM { get; }

        public ResultsViewModel ResultsVM { get; }

        private void OnLoadingStarted()
        {
            ResultsVM.IsLoading = true;
        }

        private void OnLoadingFinished(SearchResults searchResults)
        {
            ResultsVM.IsLoading = false;

            if (searchResults.LangLinks != null)
            {
				ResultsVM.SearchResults = searchResults.LangLinks
					.Select(LangResultViewModel.FromLangSearchResult)
					.ToArray();
            }
        }
    }
}
