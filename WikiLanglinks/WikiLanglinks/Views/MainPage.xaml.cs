using System;
using Xamarin.Forms;

namespace WikiLanglinks
{
    public partial class MainPage : ContentPage
    {
		public MainPage()
        {
			InitializeComponent();
            BindingContext = new MainViewModel(new WikiLanglinksApiClient(), new AppPropertiesProvider());
        }

		private MainViewModel ViewModel
		{
			get { return BindingContext as MainViewModel; }
		}

        public void Init()
        {
			ViewModel.Init();
        }

		private void OnSelectTargetLangsClicked(object sender, EventArgs e)
		{
			var selectionPage = new SelectTargetLangsPage(ViewModel.ResultsVM.TargetLangs, new[] { ViewModel.SearchVM.SourceLang });
			Navigation.PushAsync(selectionPage);
		}
    }
}
