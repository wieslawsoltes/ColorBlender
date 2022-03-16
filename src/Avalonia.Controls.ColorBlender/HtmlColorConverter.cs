using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Avalonia.Controls.ColorBlender
{
    public class HtmlColorConverter : IMultiValueConverter
    {
        public static readonly HtmlColorConverter Instance = new HtmlColorConverter();

        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            return $"#{((byte)values[0]):X2}{((byte)values[1]):X2}{((byte)values[2]):X2}";
        }
    }
}
