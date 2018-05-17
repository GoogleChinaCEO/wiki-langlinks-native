using System;

namespace WikiLanglinks
{
    public interface ITextToSpeech
    {
		void Speak(string text, string language);
	}
}
