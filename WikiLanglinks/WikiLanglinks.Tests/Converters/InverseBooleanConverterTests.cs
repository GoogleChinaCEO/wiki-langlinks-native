using System;
using NUnit.Framework;
using WikiLanglinks;

namespace WikiLanglinks.Tests
{
    [TestFixture]
    public class InverseBooleanConverterTests
    {
        private InverseBooleanConverter _converter;

        [SetUp]
        public void Setup()
        {
            _converter = new InverseBooleanConverter();
        }

        [Test]
        public void ConvertTrue_ReturnsFalse()
        {
            var actual = _converter.Convert(true, typeof(bool), null, null);

            Assert.IsInstanceOf<bool>(actual);
            Assert.AreEqual(false, actual);
        }

        [Test]
        public void ConvertFalse_ReturnsTrue()
        {
            var actual = _converter.Convert(false, typeof(bool), null, null);

            Assert.IsInstanceOf<bool>(actual);
            Assert.AreEqual(true, actual);
        }

        [Test]
        public void ConvertNonBoolean_Throws()
        {
            Assert.Throws<InvalidCastException>(() => _converter.Convert("true", typeof(bool), null, null));
        }

        [Test]
        public void ConvertToNonBoolean_Throws()
        {
            Assert.Throws<InvalidOperationException>(() => _converter.Convert(true, typeof(string), null, null));
        }
    }
}
