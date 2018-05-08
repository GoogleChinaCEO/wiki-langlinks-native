using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WikiLanglinks
{
    public partial class SelectTargetLangsPage : ContentPage
    {
		public SelectTargetLangsPage(IEnumerable<Language> targetLanguages, IEnumerable<Language> preSelectedLanguages)
        {
            InitializeComponent();
			var vm = new SelectTargetLangsViewModel(targetLanguages, preSelectedLanguages);
            vm.SelectionApplied += async () => await OnSelectionApplied();
            vm.SelectionRejected += async message => await OnSelectionRejected(message);
			BindingContext = vm;
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

		private async Task OnSelectionRejected(string message)
		{
			await DisplayAlert("Cannot apply selection", message, "OK");
		}
    }
}
