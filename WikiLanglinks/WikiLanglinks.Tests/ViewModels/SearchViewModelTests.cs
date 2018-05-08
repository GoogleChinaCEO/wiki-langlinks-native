using System;
using NUnit.Framework;
using Xamarin.Forms;

namespace WikiLanglinks.Tests.ViewModels
{
	[TestFixture]
	public class SearchViewModelTests
    {
		private SearchViewModel _viewModel;

		[SetUp]
		public void Setup()
		{
			_viewModel = new SearchViewModel();
		}

		[Test]
        public void WhenSearchCommandIsExecuted_SearchRequestedEventIsSent()
		{
			var eventsSent = 0;
			MessagingCenter.Subscribe<SearchViewModel>(this, EventNames.SearchRequested, s => { eventsSent++; });

			_viewModel.SearchCommand.Execute(null);

			Assert.AreEqual(1, eventsSent);
		}
    }
}
