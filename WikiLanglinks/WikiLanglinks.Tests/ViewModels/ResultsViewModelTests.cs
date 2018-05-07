using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace WikiLanglinks.Tests.ViewModels
{
    [TestFixture]
    public class ResultsViewModelTests
    {
        private ResultsViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new ResultsViewModel();

            _viewModel.SearchResults = new List<LangResultViewModel>
            {
                LangResultViewModel.FromLangSearchResult(new LangSearchResult
                {
                    Lang = "en",
                    Autonym = "English",
                    Title = "Result",
                    Url = "http://example.com/result"
                }),
                LangResultViewModel.FromLangSearchResult(new LangSearchResult
                {
                    Lang = "de",
                    Autonym = "Deutsch",
                    Title = "Ergebnis",
                    Url = "http://example.com/de/result"
                }),
            };

            _viewModel.TargetLangs = new List<Language>
            {
                new Language { Id = "en", Autonym = "English" },
                new Language { Id = "de", Autonym = "Deutsch" }
            };
        }

        [Test]
        public void ResetSearchResults_SetsUrlAndTitleToNull_ForAllTargetLangs()
        {
            _viewModel.ResetSearchResults();

            CollectionAssert.AreEquivalent(new[] { "en", "de" }, _viewModel.SearchResults.Select(r => r.Lang));
            CollectionAssert.AreEquivalent(new string[] { null, null }, _viewModel.SearchResults.Select(r => r.Title));
            CollectionAssert.AreEquivalent(new string[] { null, null }, _viewModel.SearchResults.Select(r => r.Url));
        }

        [Test]
        public void ApplySearchResults_WhenNotAllTargetLangsAreFoundInResults()
        {
            _viewModel.ApplySearchResults(new SearchResults {
                LangLinks = new LangSearchResult[] {
                    new LangSearchResult {
                        Lang = "en",
                        Autonym = "English",
                        Title = "Result",
                        Url = "http://example.com/result"
                    }
                }
            });

            CollectionAssert.AreEqual(new[] { "en", "de" }, _viewModel.SearchResults.Select(r => r.Lang));
            CollectionAssert.AreEqual(new[] { "English", "Deutsch" }, _viewModel.SearchResults.Select(r => r.Autonym));
            CollectionAssert.AreEqual(new string[] { "Result", null }, _viewModel.SearchResults.Select(r => r.Title));
            CollectionAssert.AreEqual(new string[] { "http://example.com/result", null }, _viewModel.SearchResults.Select(r => r.Url));
        }

        [Test]
        public void ReplaceTargetLang()
        {
            _viewModel.ReplaceTargetLang("de", new Language
            {
                Id = "it",
                Autonym = "Italiano"
            });

            CollectionAssert.AreEqual(new[] { "en", "it" }, _viewModel.TargetLangs.Select(l => l.Id));
            CollectionAssert.AreEqual(new[] { "English", "Italiano" }, _viewModel.TargetLangs.Select(l => l.Autonym));
        }
    }
}
