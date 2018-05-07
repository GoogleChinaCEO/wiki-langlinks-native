using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace WikiLanglinks
{
    public class SearchViewModel : BaseViewModel
    {
        private readonly IWikiLanglinksApiClient _apiClient;
        private readonly IAppPropertiesProvider _appPropertiesProvider;

		public event Action<IList<Language>> ResetResults;
        public event Action LoadingStarted;
        public event Action<SearchResults, IList<Language>> LoadingFinished;

        public SearchViewModel(IWikiLanglinksApiClient apiClient, IAppPropertiesProvider appPropertiesProvider)
        {
			_apiClient = apiClient;
            _appPropertiesProvider = appPropertiesProvider;

            SearchCommand = new Command(async () => await Search());
			
            MessagingCenter.Subscribe<LangResultViewModel>(this, EventNames.NewSourceLangRequested, async s => await OnNewSourceLangRequested(s));
        }

        public ICommand SearchCommand { get; }

        public string SearchTerm
        {
            get { return _searchTerm; }
            set { SetValue(ref _searchTerm, value); }
        }
        private string _searchTerm;

        public Language SourceLang
        {
            get { return _sourceLang; }
            set { SetValue(ref _sourceLang, value); }
        }
        private Language _sourceLang;

        public IList<Language> TargetLangs
        {
            get { return _targetLangs; }
            set { SetValue(ref _targetLangs, value); }
        }
        private IList<Language> _targetLangs;

        public void Init()
        {
            SourceLang = _appPropertiesProvider.SourceLanguage ?? 
                                               new Language { Id = "en", Autonym = "English" };

            TargetLangs = _appPropertiesProvider.TargetLanguages ??
                                                new List<Language>
                                                {
                                                    new Language { Id = "de", Autonym = "Deutsch" },
                                                    new Language { Id = "es", Autonym = "español" },
                                                    new Language { Id = "fr", Autonym = "français" }
                                                };

            ResetResults?.Invoke(TargetLangs);
        }

        private async Task OnNewSourceLangRequested(LangResultViewModel sender)
        {
            var newSourceLang = TargetLangs.FirstOrDefault(t => t.Id == sender.Lang);
            if (newSourceLang == null)
            {
                return;
            }

            var newSourceLangIndex = TargetLangs.IndexOf(newSourceLang);
            TargetLangs.RemoveAt(newSourceLangIndex);
            TargetLangs.Insert(newSourceLangIndex, SourceLang);

            SourceLang = sender.ToLanguage();

            ResetResults?.Invoke(TargetLangs);

            await PersistState();
        }

        private async Task PersistState()
        {
            _appPropertiesProvider.SourceLanguage = SourceLang;
            _appPropertiesProvider.TargetLanguages = TargetLangs;
            await _appPropertiesProvider.SaveAsync();
        }

        private async Task Search()
        {
            var searchRequest = new SearchRequest
            {
                SearchTerm = SearchTerm,
                Source = SourceLang.Id,
                Targets = TargetLangs.Select(tl => tl.Id).ToArray()
            };

            LoadingStarted?.Invoke();

            var results = new SearchResults();
            try
            {
                results = await _apiClient.GetLanglinks(searchRequest);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e.Message}");
            }
            finally
            {
                LoadingFinished?.Invoke(results, TargetLangs);
            }
        }
    }
}
