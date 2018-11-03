// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using Avalonia.Media;
using ColorBlender.Colors;

namespace Avalonia.Controls.ColorBlender
{
    public static class Extensions
    {
        public static RGB ToRGB(this Color c)
        {
            return new RGB(c.R, c.G, c.B);
        }

        public static HSV ToHSV(this Color c)
        {
            return ToRGB(c).ToHSV();
        }

        public static Color ToColor(this HSV hsv)
        {
            return ToColor(hsv.ToRGB());
        }

        public static Color ToColor(this RGB rgb)
        {
            return Color.FromRgb(
                (byte)Math.Round(rgb.R),
                (byte)Math.Round(rgb.G),
                (byte)Math.Round(rgb.B));
        }

        public static SolidColorBrush ToSolidColorBrush(this RGB rgb)
        {
            return new SolidColorBrush(ToColor(rgb));
        }

        public static SolidColorBrush ToSolidColorBrush(this HSV hsv)
        {
            return new SolidColorBrush(ToColor(hsv));
        }
    }
}
