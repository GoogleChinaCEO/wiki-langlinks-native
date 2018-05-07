using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WikiLanglinks
{
    public class AppPropertiesProvider : IAppPropertiesProvider
    {
        public Language SourceLanguage
        {
            get 
            {
                object language;
                Application.Current.Properties.TryGetValue("sourceLanguage", out language);
                return language as Language;
            }
            set 
            { 
                Application.Current.Properties["sourceLanguage"] = value; 
            }
        }

        public IList<Language> TargetLanguages
        {
            get
            {
                object languages;
                Application.Current.Properties.TryGetValue("targetLanguages", out languages);
                return languages as IList<Language>;
            }
            set
            {
                Application.Current.Properties["targetLanguages"] = value;
            }
        }

        public async Task SaveAsync()
        {
            await Application.Current.SavePropertiesAsync();
        }
    }
}
