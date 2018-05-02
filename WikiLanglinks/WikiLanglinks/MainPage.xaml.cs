using Xamarin.Forms;

namespace WikiLanglinks
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
		
        private MainViewModel ViewModel 
		{ 
			get { return BindingContext as MainViewModel; } 
		}		

        private void OnSearchInputCompleted(object sender, System.EventArgs e)
        {
            ViewModel.SearchCommand.Execute((sender as Entry).Text);
        }
    }
}
