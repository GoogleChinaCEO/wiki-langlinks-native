using System;
using AVFoundation;
using WikiLanglinks.iOS.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(TextToSpeech))]
namespace WikiLanglinks.iOS.Services
{
	public class TextToSpeech : ITextToSpeech
	{
		public void Speak(string text, string language)
		{
			var synthesizer = new AVSpeechSynthesizer();

			var voice = AVSpeechSynthesisVoice.FromLanguage(language);

			if (voice == null)
			{
				return;
			}

			var utterance = new AVSpeechUtterance(text)
			{
				Rate = AVSpeechUtterance.MaximumSpeechRate / 4,
				Voice = AVSpeechSynthesisVoice.FromLanguage(language),
				Volume = 0.5f,
				PitchMultiplier = 1.0f
			};

			synthesizer.SpeakUtterance(utterance);
		}
	}
}
