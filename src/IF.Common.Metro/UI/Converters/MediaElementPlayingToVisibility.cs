using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace IF.Common.Metro.UI.Converters
{
    public sealed class MediaElementPlayingToVisibility : IValueConverter
    {
        public bool Reverse { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is MediaElementState)
            {
                var state = (MediaElementState) value;
                var vis = state == MediaElementState.Playing;
                if (Reverse)
                {
                    vis = !vis;
                }

                return vis ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}