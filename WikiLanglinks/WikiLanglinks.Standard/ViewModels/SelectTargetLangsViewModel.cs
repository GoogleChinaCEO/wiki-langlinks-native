﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace WikiLanglinks
{
	public class SelectTargetLangsViewModel : BaseViewModel
    {
		private const int MinSelectedLangsCount = 1;
		private const int MaxSelectedLangsCount = 5;

		private static IList<Language> _allLanguages = new[] 
		{
			new Language { Id = "en", Autonym = "English" },
			new Language { Id = "de", Autonym = "Deutsch" },
			new Language { Id = "es", Autonym = "español" },
			new Language { Id = "ca", Autonym = "català" },
			new Language { Id = "fr", Autonym = "français" },
			new Language { Id = "it", Autonym = "italiano" },
			new Language { Id = "pt", Autonym = "português" },
			new Language { Id = "pl", Autonym = "polski" },
			new Language { Id = "el", Autonym = "Ελληνικά" },
			new Language { Id = "tr", Autonym = "Türkçe" },
			new Language { Id = "nl", Autonym = "Nederlands" },
			new Language { Id = "no", Autonym = "norsk" },
			new Language { Id = "sv", Autonym = "svenska" },
			new Language { Id = "sv", Autonym = "svenska" },
			new Language { Id = "da", Autonym = "dansk" },
			new Language { Id = "fi", Autonym = "suomi" },
			new Language { Id = "uk", Autonym = "українська" },
			new Language { Id = "ru", Autonym = "русский" },
			new Language { Id = "ja", Autonym = "日本語" },
			new Language { Id = "ko", Autonym = "한국어" },
			new Language { Id = "zh", Autonym = "中文" }
		};

		public SelectTargetLangsViewModel(IEnumerable<Language> targetLanguages, IEnumerable<Language> preSelectedLanguages)
		{
			var currentLanguages = _allLanguages
				.Select(l => 
    			{
    				var result = LangSelectionViewModel.FromLanguage(l);
    				if (targetLanguages.Any(tl => tl.Id == result.Lang))
    				{					
    					result.IsSelected = true;
                    }

				    if (preSelectedLanguages.Any(tl => tl.Id == result.Lang))
                    {
					    result.IsSelected = true;
					    result.CanSelect = false;
                    }

    				return result;
    			})
				.OrderBy(l => l.Lang);

			Languages = new ObservableCollection<LangSelectionViewModel>(currentLanguages);
			ApplyCommand = new Command(Apply);
		}

		public event Action SelectionApplied;
		public event Action<string> SelectionRejected;

		public ICommand ApplyCommand { get; }

		public ObservableCollection<LangSelectionViewModel> Languages
		{
			get { return _languages; }
			set { SetValue(ref _languages, value); }
		}
		private ObservableCollection<LangSelectionViewModel> _languages;

		private void Apply()
		{
			var newTargetLanguages = Languages
				.Where(l => l.IsSelected && l.CanSelect)
				.Select(l => l.ToLanguage())
				.ToList();

			if (newTargetLanguages.Count < MinSelectedLangsCount || newTargetLanguages.Count > MaxSelectedLangsCount)
			{
				var message = $"Please select between {MinSelectedLangsCount} and {MaxSelectedLangsCount} languages.";
				SelectionRejected?.Invoke(message);
				return;
			}

			MessagingCenter.Send(this, EventNames.TargetLangsSelected, newTargetLanguages);

			SelectionApplied?.Invoke();
		}
    }
}
