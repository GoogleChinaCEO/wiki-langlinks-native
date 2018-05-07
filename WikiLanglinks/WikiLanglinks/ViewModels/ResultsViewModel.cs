using System;
using System.Collections.Generic;

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

    }
}
