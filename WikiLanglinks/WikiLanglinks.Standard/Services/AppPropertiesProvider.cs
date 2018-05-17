using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace WikiLanglinks
{
    public class AppPropertiesProvider : IAppPropertiesProvider
    {
        private static readonly Language DefaultSourceLanguage = new Language { Id = "en", Autonym = "English" };

        private static readonly IList<Language> DefaultTargetLanguages = new List<Language>
                                                {
                                                    new Language { Id = "de", Autonym = "Deutsch" },
                                                    new Language { Id = "es", Autonym = "español" },
                                                    new Language { Id = "fr", Autonym = "français" }
                                                };

        public Language SourceLanguage
        {
            get { return GetValue<Language>("sourceLanguage") ?? DefaultSourceLanguage; }
            set { SetValue("sourceLanguage", value); }
        }

        public IList<Language> TargetLanguages
        {
            get { return GetValue<IList<Language>>("targetLanguages") ?? DefaultTargetLanguages; }
            set { SetValue("targetLanguages", value); }
        }

        private T GetValue<T>(string key)
        {
            if (Application.Current.Properties.ContainsKey(key))
            {
                var obj = Application.Current.Properties[key];
                if (obj == null)
                {
                    return default(T);
                }

                return JsonConvert.DeserializeObject<T>(obj.ToString());
            }

            return default(T);
        }

        private void SetValue<T>(string key, T value)
        {
            Application.Current.Properties[key] = JsonConvert.SerializeObject(value);
        }

        public async Task SaveAsync()
        {
            await Application.Current.SavePropertiesAsync();
        }
    }
}
