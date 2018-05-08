using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Xamarin.Forms;

namespace WikiLanglinks.Tests.ViewModels
{
	[TestFixture]
	public class MainViewModelTests
    {
		private MainViewModel _viewModel;
		private Mock<IWikiLanglinksApiClient> _apiClientMock;
		private Mock<IAppPropertiesProvider> _appPropertiesProviderMock;
        
		[SetUp]
        public void Setup()
		{
			_apiClientMock = new Mock<IWikiLanglinksApiClient>();
			_appPropertiesProviderMock = new Mock<IAppPropertiesProvider>();
			_viewModel = new MainViewModel(_apiClientMock.Object, _appPropertiesProviderMock.Object);

			_appPropertiesProviderMock
                .Setup(m => m.SourceLanguage)
                .Returns(new Language { Id = "en" });

            _appPropertiesProviderMock
                .Setup(m => m.TargetLanguages)
                .Returns(new[] { new Language { Id = "de" }, new Language { Id = "it" } });

            _viewModel.Init();
        }

        [Test]
        public void Init_LoadsSourceAndTargetLangsFromAppProperties()
		{
			Assert.NotNull(_viewModel.SearchVM.SourceLang);
			Assert.AreEqual("en", _viewModel.SearchVM.SourceLang.Id);

			Assert.NotNull(_viewModel.ResultsVM.TargetLangs);
			CollectionAssert.AreEquivalent(new[] { "de", "it" }, _viewModel.ResultsVM.TargetLangs.Select(tl => tl.Id));
		}
    }
}
