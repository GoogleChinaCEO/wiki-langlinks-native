using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace WikiLanglinks
{
    public class AppPropertiesProvider : IAppPropertiesProvider
    {
        public Language SourceLanguage
        {
            get { return GetValue<Language>("sourceLanguage"); }
            set { SetValue("sourceLanguage", value); }
        }

        public IList<Language> TargetLanguages
        {
            get { return GetValue<IList<Language>>("targetLanguages"); }
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
