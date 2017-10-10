// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Globalization;
using System.Windows.Data;

namespace WPF.Controls.ColorBlender
{
    public class HtmlColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string color = "#";
            foreach (byte val in values)
            {
                color += val.ToString("X2");
            }
            return color;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            object[] bytes = new object[3];
            string html = value as string;
            bytes[0] = byte.Parse(html.Substring(1, 2), NumberStyles.HexNumber);
            bytes[1] = byte.Parse(html.Substring(3, 2), NumberStyles.HexNumber);
            bytes[2] = byte.Parse(html.Substring(5, 2), NumberStyles.HexNumber);
            return bytes;
        }
    }
}
