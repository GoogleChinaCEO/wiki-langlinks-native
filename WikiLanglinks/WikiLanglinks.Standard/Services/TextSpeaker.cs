using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
namespace WikiLanglinks
{
    public static class TextSpeaker
    {
        private static readonly IDictionary<string, Locale?> langToLocaleMap = new ConcurrentDictionary<string, Locale?>();

        public static async Task SpeakAsync(string text, Locale locale)
        {
            var settings = new SpeakSettings
            { 
                Pitch = 1.0f,
                Volume = 0.75f,
                Locale = locale
            };

            await TextToSpeech.SpeakAsync(text, settings);
        }

        public static async Task<Locale?> LanguageToLocale(string language)
        {
            /*
                locale objects:
                Android: {ID: "", Name: "English (United Kingdom)", Language: "en", Country: "GB"}
                iOS: {ID: "com.apple.ttsbundle.Daniel-compact", Name: "en-GB", Language: "en-GB", Country: ""}
            */

            if (langToLocaleMap.ContainsKey(language))
            {
                return langToLocaleMap[language];
            }

            var locale = await GetLocaleForLanguage(language);
            langToLocaleMap[language] = locale;
            return locale;
        }

        private static async Task<Locale?> GetLocaleForLanguage(string language)
        {
            var supportedLocales = await TextToSpeech.GetLocalesAsync();
            var matchingLocales = supportedLocales.Where(loc => loc.Language?.StartsWith(language) == true).ToList();
            if (!matchingLocales.Any())
            {
                return null;
            }

            var matchingCountry = matchingLocales.FirstOrDefault(loc => loc.Country?.ToLowerInvariant() == language);

            if (matchingCountry.Language != null)
            {
                return matchingCountry;
            }

            var matchingCountryCode = matchingLocales.FirstOrDefault(loc =>
            {
                var split = loc.Language.Split('-');
                return split.Length > 1 && split[1]?.ToLowerInvariant() == language;
            });

            if (matchingCountryCode.Language != null)
            {
                return matchingCountryCode;
            }

            return matchingLocales.First();
        }


    }
}
