using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace WikiLanglinks
{
    public class LangResultViewModel : BaseViewModel
    {
        protected LangResultViewModel()
        {
            OpenUrlCommand = new Command(OpenUrl);
        }

        public string Lang { get; set; }
        public string Autonym { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public ICommand OpenUrlCommand { get; }

        private void OpenUrl()
        {
            Device.OpenUri(new Uri(Url));
        }

        public static LangResultViewModel FromLangSearchResult(LangSearchResult langSearchResult)
        {
            if (langSearchResult == null)
            {
                throw new ArgumentNullException(nameof(langSearchResult));
            }

            return new LangResultViewModel
            {
                Lang = langSearchResult.Lang,
                Autonym = langSearchResult.Autonym,
                Title = langSearchResult.Title,
                Url = langSearchResult.Url
            };
        }
    }
}
