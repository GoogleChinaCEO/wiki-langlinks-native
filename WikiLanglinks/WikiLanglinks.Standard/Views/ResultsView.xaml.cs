using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace WikiLanglinks
{
    public partial class ResultsView : ContentView
    {
        public ResultsView()
        {
            InitializeComponent();
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = sender as ListView;
            if (listView != null)
            {
                listView.SelectedItem = null;  // disable selection
            }
        }

    }
}
