using IF.Common.Metro.UI.Converters;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework.AppContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Assert = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.Assert;

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

        [TestMethod]
        public void NullToVisibility()
        {
            var converter = new NullToVisibility();
            object answer;

            answer = converter.Convert(null, null, null, null);
            Assert.IsTrue(answer is Visibility);
            Assert.IsTrue((Visibility)answer == Visibility.Collapsed);

            answer = converter.Convert(new object(), null, null, null);
            Assert.IsTrue(answer is Visibility);
            Assert.IsTrue((Visibility)answer == Visibility.Visible);
        }

        [TestMethod]
        public void NullToVisibilityReverse()
        {

            var converter = new NullToVisibility() {Reverse = true};
            object answer;

            answer = converter.Convert(null, null, null, null);
            Assert.IsTrue(answer is Visibility);
            Assert.IsTrue((Visibility)answer == Visibility.Visible);

            answer = converter.Convert(new object(), null, null, null);
            Assert.IsTrue(answer is Visibility);
            Assert.IsTrue((Visibility)answer == Visibility.Collapsed);
        }

        [TestMethod]
        public void NullToVisibilityConvertBack()
        {
            var converter = new NullToVisibility();

            Assert.ThrowsException<NotImplementedException>(() =>
            {
                converter.ConvertBack(true, null, null, null);
            });
        }

        [TestMethod]
        public void MediaElementStatesCheck()
        {
            // check that all states are expected states... probably some code to change if not

            var states = Enum.GetNames(typeof (MediaElementState));
            var expected = new[]
            {
                "Buffering", "Closed", "Opening", "Paused", "Playing", "Stopped"
            };

            var equal = new HashSet<string>(states).SetEquals(expected);
            Assert.IsTrue(equal);
        }

        [TestMethod]
        public void MediaElementStateToVisibility()
        {
            var converter = new MediaElementStateToVisibility();
            object answer;

            foreach (var state in Enum.GetValues(typeof(MediaElementState)).Cast<MediaElementState>())
            {
                answer = converter.Convert(state, null, null, null);

                Assert.IsTrue(answer is Visibility);

                if (state == MediaElementState.Playing)
                {
                    Assert.IsTrue((Visibility)answer == Visibility.Visible);
                }
                else
                {
                    Assert.IsTrue((Visibility)answer == Visibility.Collapsed);
                }
            }
        }

        [TestMethod]
        public void MediaElementStateToVisibilityReverse()
        {
            var converter = new MediaElementStateToVisibility() {Reverse = true};
            object answer;

            foreach (var state in Enum.GetValues(typeof(MediaElementState)).Cast<MediaElementState>())
            {
                answer = converter.Convert(state, null, null, null);

                Assert.IsTrue(answer is Visibility);

                if (state == MediaElementState.Playing)
                {
                    Assert.IsTrue((Visibility)answer == Visibility.Collapsed);
                }
                else
                {
                    Assert.IsTrue((Visibility)answer == Visibility.Visible);
                }
            }
        }


        [TestMethod]
        public void MediaElementStateToVisibilityConvertBack()
        {
            var converter = new MediaElementStateToVisibility();

            Assert.ThrowsException<NotImplementedException>(() =>
            {
                converter.ConvertBack(true, null, null, null);
            });
        }

        [UITestMethod]
        public void TrackPositionToPercentage()
        {
            var tracklength = new TimeSpan(0, 3, 20);
            var converter = new TrackPositionToPercentage() {TrackDuration = tracklength};
            object answer;

            answer = converter.Convert(TimeSpan.Zero, null, null, null);
            Assert.IsTrue(answer is double);
            Assert.IsTrue(Math.Abs((double)answer) < double.Epsilon);
            
            answer = converter.Convert(new TimeSpan(0, 1, 40), null, null, null);
            Assert.IsTrue(answer is double);
            Assert.IsTrue(Math.Abs((double)answer - 50d) < double.Epsilon);

            answer = converter.Convert(tracklength, null, null, null);
            Assert.IsTrue(answer is double);
            Assert.IsTrue(Math.Abs((double)answer - 100d) < double.Epsilon);
        }

        [UITestMethod]
        public void TrackPositionToPercentageConvertBack()
        {
            var converter = new TrackPositionToPercentage();

            Assert.ThrowsException<NotImplementedException>(() =>
            {
                converter.ConvertBack(true, null, null, null);
            });
        }
    }
}
