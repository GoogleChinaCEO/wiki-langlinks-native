using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace WikiLanglinks
{
    public class SearchViewModel : BaseViewModel
    {
        public SearchViewModel()
        {
            SearchCommand = new Command(Search);
        }
		
        public ICommand SearchCommand { get; }

        public string SearchTerm
        {
            get { return _searchTerm; }
            set { SetValue(ref _searchTerm, value); }
        }
        private string _searchTerm;

        public Language SourceLang
        {
            get { return _sourceLang; }
            set { SetValue(ref _sourceLang, value); }
        }
        private Language _sourceLang;

        private void Search()
        {
			MessagingCenter.Send(this, EventNames.SearchRequested);
        }
    }
}
