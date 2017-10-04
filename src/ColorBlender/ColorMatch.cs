// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using ColorBlender.Algorithms;

namespace ColorBlender
{
    // Color matching algorithms:
    // "classic"               ColorMatch 5K Classic
    // "colorexplorer"         ColorExplorer - "Sweet Spot Offset"
    // "singlehue"             Single Hue
    // "complementary"         Complementary
    // "splitcomplementary"    Split-Complementary
    // "analogue"              Analogue
    // "triadic"               Triadic
    // "square"                Square
    // Color matching algorithm. All work is done in HSV color space, because all
    // calculations are based on hue, saturation and value of the working color.
    // The hue spectrum is divided into sections, are the matching colors are
    // calculated differently depending on the hue of the color.
    public static class ColorMatch
    {
        public static Blend Match(HSV hs, string method)
        {
            switch (method)
            {
                case "classic":
                    return new Classic().Match(hs);
                case "colorexplorer":
                    return new ColorExplorer().Match(hs);
                case "singlehue":
                    return new SingleHue().Match(hs);
                case "complementary":
                    return new Complementary().Match(hs);
                case "splitcomplementary":
                    return new SplitComplementary().Match(hs);
                case "analogue":
                    return new Analogue().Match(hs);
                case "triadic":
                    return new Triadic().Match(hs);
                case "square":
                    return new Square().Match(hs);
            }

            return null;
        }
    }
}
