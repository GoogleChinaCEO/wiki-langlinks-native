using System;
using System.Collections.ObjectModel;

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
            ResultsVM.SearchResults = searchResults.LangLinks;
        }
    }
}
