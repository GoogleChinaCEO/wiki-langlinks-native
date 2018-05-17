using System;

namespace WikiLanglinks
{
	public class LangSelectionViewModel : BaseViewModel
	{
		protected LangSelectionViewModel() { }

		public static LangSelectionViewModel FromLanguage(Language language)
		{
			return new LangSelectionViewModel
			{
				Lang = language.Id,
				Autonym = language.Autonym
			};
		}

		public Language ToLanguage()
		{
			return new Language
			{
				Id = this.Lang,
				Autonym = this.Autonym
			};
		}

		public string Lang { get; set; }

		public string Autonym { get; set; }

		public bool IsSelected
		{
			get { return _isSelected; }
			set { SetValue(ref _isSelected, value); }
		}
		private bool _isSelected;

		public bool CanSelect
        {
			get { return _canSelect; }
			set { SetValue(ref _canSelect, value); }
        }
        private bool _canSelect = true;
	}
}
