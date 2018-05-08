using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WikiLanglinks
{
    public partial class SelectTargetLangsPage : ContentPage
    {
		public SelectTargetLangsPage(IEnumerable<Language> targetLanguages)
        {
            InitializeComponent();
			var vm = new SelectTargetLangsViewModel(targetLanguages);
			BindingContext = vm;
			vm.SelectionApplied += async () => await OnSelectionApplied();
        }

		private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = sender as ListView;
			if (listView != null)
            {
                listView.SelectedItem = null;  // disable selection
            }
        }

		private async Task OnSelectionApplied()
		{
			await Navigation.PopAsync();
		}
    }
}
