using Xamarin.Forms;
using WikiLanglinks.ViewModels;

namespace WikiLanglinks.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
		
        private MainViewModel ViewModel 
		{ 
			get { return BindingContext as MainViewModel; } 
		}		
    }
}
