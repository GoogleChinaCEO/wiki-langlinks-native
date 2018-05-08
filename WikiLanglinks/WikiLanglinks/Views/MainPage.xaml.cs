using System.Threading.Tasks;
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

        public void Init()
        {
            (BindingContext as MainViewModel).Init();
        }

		private async Task OnSelectTargetLangsClicked(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new SelectTargetLangsPage());
		}
    }
}
