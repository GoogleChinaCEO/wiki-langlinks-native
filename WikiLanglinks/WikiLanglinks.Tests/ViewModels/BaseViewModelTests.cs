using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace WikiLanglinks.Tests.ViewModels
{
    [TestFixture]
    public class BaseViewModelTests
    {
        private class TestViewModel : BaseViewModel
        {
            public string Id 
            { 
                get { return _id; }
                set { SetValue(ref _id, value); }
            }
            private string _id;
        }

        private TestViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new TestViewModel { Id = "old_id" };
        }

        [Test]
        public void SetValue__WhenValueIsNew_RaisesPropertyChanged_WithRespectivePropertyName()
        {
            var propertyNames = new List<string>();
            _viewModel.PropertyChanged += (sender, e) => { propertyNames.Add(e.PropertyName); };

            _viewModel.Id = "new_id";

            Assert.AreEqual("new_id", _viewModel.Id);
            Assert.AreEqual(1, propertyNames.Count);
            Assert.AreEqual("Id", propertyNames[0]);
        } 

        [Test]
        public void SetValue__WhenValueIsTheSame_DoesNotRaisPropertyChanged()
        {
            var propertyNames = new List<string>();
            _viewModel.PropertyChanged += (sender, e) => { propertyNames.Add(e.PropertyName); };

            _viewModel.Id = "old_id";

            Assert.AreEqual("old_id", _viewModel.Id);
            Assert.AreEqual(0, propertyNames.Count);
        } 

    }
}
