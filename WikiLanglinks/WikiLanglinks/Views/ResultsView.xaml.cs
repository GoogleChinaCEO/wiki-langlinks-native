using System;
using Xamarin.Forms;

namespace WikiLanglinks
{
    public partial class ResultsView : ContentView
    {
        public ResultsView()
        {
            InitializeComponent();
        }

        private ResultsViewModel ViewModel
        {
            get { return BindingContext as ResultsViewModel; }
        }
    }
}
