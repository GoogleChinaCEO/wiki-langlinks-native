using System;
using System.Collections.Generic;
using Xamarin.Forms;
using WikiLanglinks.Services;
using WikiLanglinks.ViewModels;

namespace WikiLanglinks.Views
{
    public partial class SearchView : ContentView
    {
        public SearchView()
        {
            InitializeComponent();
            BindingContext = new SearchViewModel(new WikiLanglinksApiClient());
        }

        private SearchViewModel ViewModel
        {
            get { return BindingContext as SearchViewModel; }
        }

        private void OnSearchInputCompleted(object sender, EventArgs e)
        {
            ViewModel.SearchCommand.Execute(null);
        }
    }
}
