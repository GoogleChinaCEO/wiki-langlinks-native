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
		private string phrase;

		public void Speak(string text, string language)
        {
			var locale = Java.Util.Locale.ForLanguageTag(language);
			phrase = text;

            if (speaker == null)
            {
                speaker = new TextToSpeech(MainActivity.Instance, this);
				speaker.SetLanguage(locale);
            }
            else
            {
				Speak();
            }
        }

        public void OnInit(OperationResult status)
        {
			if (status.Equals(OperationResult.Success))
            {
				Speak();
            }
        }

		private void Speak()
		{
            speaker.Speak(phrase, QueueMode.Flush, null, null);
		}
    }   
}