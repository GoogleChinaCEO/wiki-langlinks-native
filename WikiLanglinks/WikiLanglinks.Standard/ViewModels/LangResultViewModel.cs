using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace WikiLanglinks
{
    public class LangResultViewModel : BaseViewModel
    {
		protected LangResultViewModel()
        {
            OpenUrlCommand = new Command(OpenUrl);
            MakeSourceLangCommand = new Command(MakeSourceLang);
			SpeakCommand = new Command(async () => await Speak(), () => CanSpeak);
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

        public static LangResultViewModel FromLanguage(Language language)
        {
            if (language == null)
            {
                throw new ArgumentNullException(nameof(language));
            }

            return new LangResultViewModel
            {
                Lang = language.Id,
                Autonym = language.Autonym
            };
        }

        public Language ToLanguage()
        {
            return new Language
            {
                Id = this.Lang,
                Autonym = this.Autonym
            };
        }

        public string Lang { get; set; }
        public string Autonym { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public bool CanSpeak
		{
			get 
            {
                return !string.IsNullOrWhiteSpace(Title);
            }
		}

        public ICommand OpenUrlCommand { get; }
        public ICommand MakeSourceLangCommand { get; }
		public ICommand SpeakCommand { get; }

        private void OpenUrl()
        {
            Device.OpenUri(new Uri(Url));
        }

        private void MakeSourceLang()
        {
            MessagingCenter.Send(this, EventNames.NewSourceLangRequested);
        }

        private async Task Speak()
		{
            var locale = await TextSpeaker.LanguageToLocale(Lang);
            if (locale != null)
            {
                await TextSpeaker.SpeakAsync(Title, locale.Value);
            }
            else
            {
                DependencyService.Get<IAlert>().Short("Language not supported");
            }
		}
    }
}
