using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WikiLanglinks
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IWikiLanglinksApiClient _apiClient;
        private readonly IAppPropertiesProvider _appPropertiesProvider;

        public MainViewModel(IWikiLanglinksApiClient apiClient, IAppPropertiesProvider appPropertiesProvider)
		{
            _apiClient = apiClient;
            _appPropertiesProvider = appPropertiesProvider;

            SearchVM = new SearchViewModel();
			SearchVM.SearchRequested += async () => await OnSearchRequested();
   
            ResultsVM = new ResultsViewModel();

            MessagingCenter.Subscribe<LangResultViewModel>(this, EventNames.NewSourceLangRequested, async s => await OnNewSourceLangRequested(s));
		}

        public SearchViewModel SearchVM { get; }

        public ResultsViewModel ResultsVM { get; }

        public void Init()
        {
            SearchVM.SourceLang = _appPropertiesProvider.SourceLanguage;
            ResultsVM.TargetLangs = _appPropertiesProvider.TargetLanguages;
            ResetSearchState();
        }

        private async Task OnSearchRequested()
        {
            var searchRequest = new SearchRequest
            {
                SearchTerm = SearchVM.SearchTerm,
                Source = SearchVM.SourceLang.Id,
                Targets = ResultsVM.TargetLangs.Select(tl => tl.Id).ToArray()
            };

            StartLoading();

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
                FinishLoading(results);
            }
        }

        private void StartLoading()
        {
            ResultsVM.IsLoading = true;
        }

        private void FinishLoading(SearchResults searchResults)
        {
            ResultsVM.IsLoading = false;
            ResultsVM.ApplySearchResults(searchResults);
        }

        private async Task OnNewSourceLangRequested(LangResultViewModel sender)
        {
            ResultsVM.ReplaceTargetLang(sender.Lang, SearchVM.SourceLang);
            SearchVM.SourceLang = sender.ToLanguage();
            ResetSearchState();
            await PersistState();
        }

        private void ResetSearchState()
        {
            SearchVM.SearchTerm = null;
            ResultsVM.ResetSearchResults();
        }

        private async Task PersistState()
        {
            _appPropertiesProvider.SourceLanguage = SearchVM.SourceLang;
            _appPropertiesProvider.TargetLanguages = ResultsVM.TargetLangs;
            await _appPropertiesProvider.SaveAsync();
        }
    }
}
