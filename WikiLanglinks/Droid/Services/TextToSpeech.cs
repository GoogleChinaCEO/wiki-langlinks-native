using System;
using Xamarin.Forms;
using WikiLanglinks.Droid.Services;
using Android.Speech.Tts;

[assembly: Dependency(typeof(TextToSpeechImpl))]
namespace WikiLanglinks.Droid.Services
{
	public class TextToSpeechImpl : Java.Lang.Object, TextToSpeech.IOnInitListener, ITextToSpeech
	{
		private TextToSpeech speaker;
        private string toSpeak;
		private bool isLanguageAvailable;

		public event Action<string> LanguageNotAvailable;

		public void Speak(string text, string language)
        {
			var locale = GetLocale(language);
			toSpeak = text;

            if (speaker == null)
            {
                speaker = new TextToSpeech(MainActivity.Instance, this);
				isLanguageAvailable = speaker.IsLanguageAvailable(locale) >= 0;

				if (isLanguageAvailable)
				{					
					speaker.SetLanguage(locale);
                }
            }
            else
            {
				SpeakIfPossible();
            }
        }

        public void OnInit(OperationResult status)
        {
			if (status.Equals(OperationResult.Success))
            {
				SpeakIfPossible();
            }
        }

		private static Java.Util.Locale GetLocale(string language)
		{
			switch (language)
			{
				case "en":
					return Java.Util.Locale.ForLanguageTag("en-US");
				default:
					return Java.Util.Locale.ForLanguageTag(language);
			}
		}

		private void SpeakIfPossible()
		{
			if (isLanguageAvailable)
            {
                speaker.Speak(toSpeak, QueueMode.Flush, null, null);
            }
            else
            {
                LanguageNotAvailable?.Invoke("No voice found for language.");
            }
		}
    }   
}