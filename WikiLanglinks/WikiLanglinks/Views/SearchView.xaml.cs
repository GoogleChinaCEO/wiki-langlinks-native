using System;
using Xamarin.Forms;

namespace WikiLanglinks
{
    public partial class SearchView : ContentView
    {
        public SearchView()
        {
            InitializeComponent();
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
