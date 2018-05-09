using System;
using Xamarin.Forms;
using WikiLanglinks.Droid.Services;
using Android.Speech.Tts;

[assembly: Dependency(typeof(TextToSpeechImpl))]
namespace WikiLanglinks.Droid.Services
{
	public class TextToSpeechImpl : Java.Lang.Object, TextToSpeech.IOnInitListener, ITextToSpeech
	{
		TextToSpeech speaker;
        string toSpeak;
		bool isLanguageAvailable;

        public void Speak(string text, string language)
        {
			var locale = new Java.Util.Locale(language);
			toSpeak = text;
			isLanguageAvailable = speaker.IsLanguageAvailable(locale) >= 0;

            if (speaker == null)
            {
                speaker = new TextToSpeech(MainActivity.Instance, this);

				if (isLanguageAvailable)
				{					
					speaker.SetLanguage(locale);
                }
            }
            else
            {
				if (isLanguageAvailable)
				{					
					speaker.Speak(toSpeak, QueueMode.Flush, null, null);
                }
            }
        }

        public void OnInit(OperationResult status)
        {
			if (status.Equals(OperationResult.Success) && isLanguageAvailable)
            {
				speaker.Speak(toSpeak, QueueMode.Flush, null, null);
            }
        }
    }   
}