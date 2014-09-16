using System;
using Windows.UI.Xaml;
using IF.Common.Metro.UI.Converters;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace IF.Common.Tests.UI
{
    [TestClass]
    public class ConverterTests
    {
        [TestMethod]
        public void BooleanToVisibility()
        {
            var converter = new BooleanToVisibility();
            object answer;

            answer = converter.Convert(true, null, null, null);
            Assert.IsTrue(answer is Visibility);
            Assert.IsTrue((Visibility)answer == Visibility.Visible);

            answer = converter.Convert(false, null, null, null);
            Assert.IsTrue(answer is Visibility);
            Assert.IsTrue((Visibility)answer == Visibility.Collapsed);
        }

        [TestMethod]
        public void BooleanToVisibilityReversed()
        {
            var converter = new BooleanToVisibility() { Reverse = true };
            object answer;

            answer = converter.Convert(true, null, null, null);
            Assert.IsTrue(answer is Visibility);
            Assert.IsTrue((Visibility)answer == Visibility.Collapsed);

            answer = converter.Convert(false, null, null, null);
            Assert.IsTrue(answer is Visibility);
            Assert.IsTrue((Visibility)answer == Visibility.Visible);
        }

        [TestMethod]
        public void BooleanToVisibilityConvertBack()
        {
            var converter = new BooleanToVisibility();

            Assert.ThrowsException<NotImplementedException>(() =>
            {
                converter.ConvertBack(true, null, null, null);
            });
        }
    }
}
