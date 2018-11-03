// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Avalonia.Controls.ColorBlender
{
    public class HtmlColorConverter : IMultiValueConverter
    {
        public static HtmlColorConverter Instance = new HtmlColorConverter();

        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            return $"#{((byte)values[0]).ToString("X2")}{((byte)values[1]).ToString("X2")}{((byte)values[2]).ToString("X2")}";
        }
    }
}
