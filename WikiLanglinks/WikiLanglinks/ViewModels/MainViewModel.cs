using System;
using System.Collections.Generic;
using System.Linq;

namespace WikiLanglinks
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(IWikiLanglinksApiClient apiClient)
		{
			ResultsVM = new ResultsViewModel();

            SearchVM = new SearchViewModel(apiClient);
            SearchVM.LoadingStarted += OnLoadingStarted;
            SearchVM.LoadingFinished += OnLoadingFinished;
            SearchVM.ResetResults += OnResetResults;
		}

        public SearchViewModel SearchVM { get; }

        public ResultsViewModel ResultsVM { get; }

        private void OnLoadingStarted()
        {
            ResultsVM.IsLoading = true;
        }

        private void OnLoadingFinished(SearchResults searchResults, IList<Language> targetLanguages)
        {
            ResultsVM.IsLoading = false;

            if (searchResults.LangLinks != null)
            {
                ResultsVM.SearchResults = targetLanguages
                    .Select(l =>
                    {
                        var result = searchResults.LangLinks.FirstOrDefault(sr => sr.Lang == l.Id);
                        return result == null 
                            ? LangResultViewModel.FromLanguage(l) 
                            : LangResultViewModel.FromLangSearchResult(result);
                    })
                    .ToArray();
            }
        }

        private void OnResetResults(IList<Language> targetLanguages)
        {
            SearchVM.SearchTerm = null;

            ResultsVM.SearchResults = targetLanguages
                .Select(LangResultViewModel.FromLanguage)
                .ToArray();
        }

    }
}
