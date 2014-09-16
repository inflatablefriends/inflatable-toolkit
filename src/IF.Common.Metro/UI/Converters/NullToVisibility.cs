using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace IF.Common.Metro.UI.Converters
{
    public sealed class NullToVisibility : IValueConverter
    {
        public bool Reverse { get; set; }

        /// <summary>
        /// Converts null to collapsed.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var vis = value != null;
            if (Reverse)
            {
                vis = !vis;
            }

            return vis
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}