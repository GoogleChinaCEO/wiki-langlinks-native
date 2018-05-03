using System;

namespace WikiLanglinks
{
    public class ResultsViewModel : BaseViewModel
    {
        public bool IsLoading 
        {
            get { return _isLoading; }
            set { SetValue(ref _isLoading, value); }
        }
        private bool _isLoading;
    }
}
