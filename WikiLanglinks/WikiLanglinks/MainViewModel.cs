using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace WikiLanglinks
{
    public class MainViewModel : BaseViewModel
    {
		public MainViewModel()
		{
			SearchCommand = new Command<string>(async term => await Search(term));
		}
		
        public ICommand SearchCommand { get; }

        private async Task Search(string searchTerm)
        {
            await Task.Delay(1000);

            // TODO: API call here
            Debug.WriteLine($"Performing search with {searchTerm} ...");
        }
    }
}
