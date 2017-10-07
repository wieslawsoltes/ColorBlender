// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Markup;

namespace ColorBlenderAvalonia
{
    public class HtmlColorConverter : IMultiValueConverter
    {
        public static HtmlColorConverter Instance = new HtmlColorConverter();

        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            string color = "#";
            foreach (byte val in values)
            {
                color += val.ToString("X2");
            }
            return color;
        }
    }
}
