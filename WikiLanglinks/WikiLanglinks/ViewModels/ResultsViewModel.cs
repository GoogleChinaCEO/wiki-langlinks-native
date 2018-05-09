using System;
using System.Collections.Generic;
using System.Linq;

namespace WikiLanglinks
{
    public class ResultsViewModel : BaseViewModel
    {
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetValue(ref _isLoading, value); }
        }
        private bool _isLoading;

        public IList<LangResultViewModel> SearchResults
        {
            get { return _searchResults; }
            set { SetValue(ref _searchResults, value); }
        }
        private IList<LangResultViewModel> _searchResults;

        public List<Language> TargetLangs { get; set; }

        public void ResetSearchResults()
        {
            SearchResults = TargetLangs
                .Select(LangResultViewModel.FromLanguage)
                .ToArray();
        }

        public void ApplySearchResults(SearchResults searchResults)
        {
            SearchResults = TargetLangs
                .Select(l =>
                {
                    var result = searchResults.LangLinks?.FirstOrDefault(sr => sr.Lang == l.Id);
                    return result == null
                        ? LangResultViewModel.FromLanguage(l)
                        : LangResultViewModel.FromLangSearchResult(result);
                })
                .ToArray();
            
        }

        public void ReplaceTargetLang(string langId, Language newLanguage)
        {
            var currentLanguage = TargetLangs.FirstOrDefault(t => t.Id == langId);
            if (currentLanguage == null)
            {
                return;
            }

            var languageIndex = TargetLangs.IndexOf(currentLanguage);
            TargetLangs.RemoveAt(languageIndex);
            TargetLangs.Insert(languageIndex, newLanguage);
        }
    }
}
