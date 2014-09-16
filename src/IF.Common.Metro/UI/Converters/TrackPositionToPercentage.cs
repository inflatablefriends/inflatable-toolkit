using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace IF.Common.Metro.UI.Converters
{
    public sealed class TrackPositionToPercentage : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty TrackDurationProperty = DependencyProperty.Register(
            "TrackDuration", typeof (TimeSpan), typeof (TrackPositionToPercentage), new PropertyMetadata(default(TimeSpan)));

        public TimeSpan TrackDuration
        {
            get { return (TimeSpan) GetValue(TrackDurationProperty); }
            set { SetValue(TrackDurationProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is TimeSpan))
            {
                return 0;
            }

            var position = (TimeSpan) value;

            var percentage = (position.TotalSeconds/TrackDuration.TotalSeconds) * 100;

            return percentage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var percent = (double) value;
            var fraction = percent/100;

            var seconds = (int) (TrackDuration.TotalSeconds * fraction);
            var newTime = new TimeSpan(0,0,seconds);

            return newTime;
        }
    }
}