// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Windows.Media;
using ColorBlender;

namespace ColorBlenderWPF
{
    public static class ColorExtensions
    {
        public static RGB ToRGB(this Color c)
        {
            return new RGB(c.R, c.G, c.B);
        }

        public static HSV ToHSV(this Color c)
        {
            return ToRGB(c).ToHSV();
        }

        public static Color ToColor(this HSV hs)
        {
            return ToColor(hs.ToRGB());
        }

        public static Color ToColor(this RGB rgb)
        {
            return Color.FromRgb(
                (byte)Math.Round(rgb.R),
                (byte)Math.Round(rgb.G),
                (byte)Math.Round(rgb.B));
        }
    }
}
