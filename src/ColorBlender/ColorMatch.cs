// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;

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
        public static Blend Classic(HSV hs)
        {
            Blend outp = new Blend();
            outp.Colors[0] = new HSV(hs);

            HSV y = new HSV();
            HSV yx = new HSV();

            y.s = hs.s;
            y.h = hs.h;
            if (hs.v > 70) { y.v = hs.v - 30; } else { y.v = hs.v + 30; };
            outp.Colors[1] = new HSV(y);

            if ((hs.h >= 0) && (hs.h < 30))
            {
                yx.h = y.h = hs.h + 30; yx.s = y.s = hs.s; y.v = hs.v;
                if (hs.v > 70) { yx.v = hs.v - 30; } else { yx.v = hs.v + 30; }
            }

            if ((hs.h >= 30) && (hs.h < 60))
            {
                yx.h = y.h = hs.h + 150;
                y.s = MathHelpers.RC(hs.s - 30, 100);
                y.v = MathHelpers.RC(hs.v - 20, 100);
                yx.s = MathHelpers.RC(hs.s - 50, 100);
                yx.v = MathHelpers.RC(hs.v + 20, 100);
            }

            if ((hs.h >= 60) && (hs.h < 180))
            {
                yx.h = y.h = hs.h - 40;
                y.s = yx.s = hs.s;
                y.v = hs.v; if (hs.v > 70) { yx.v = hs.v - 30; } else { yx.v = hs.v + 30; }
            }

            if ((hs.h >= 180) && (hs.h < 220))
            {
                yx.h = hs.h - 170;
                y.h = hs.h - 160;
                yx.s = y.s = hs.s;
                y.v = hs.v;
                if (hs.v > 70) { yx.v = hs.v - 30; } else { yx.v = hs.v + 30; }

            }
            if ((hs.h >= 220) && (hs.h < 300))
            {
                yx.h = y.h = hs.h;
                yx.s = y.s = MathHelpers.RC(hs.s - 40, 100);
                y.v = hs.v;
                if (hs.v > 70) { yx.v = hs.v - 30; } else { yx.v = hs.v + 30; }
            }
            if (hs.h >= 300)
            {
                if (hs.s > 50) { y.s = yx.s = hs.s - 40; } else { y.s = yx.s = hs.s + 40; }
                yx.h = y.h = (hs.h + 20) % 360;
                y.v = hs.v;
                if (hs.v > 70) { yx.v = hs.v - 30; } else { yx.v = hs.v + 30; }
            }

            outp.Colors[2] = new HSV(y);
            outp.Colors[3] = new HSV(yx);

            y.h = 0;
            y.s = 0;
            y.v = 100 - hs.v;
            outp.Colors[4] = new HSV(y);

            y.h = 0;
            y.s = 0;
            y.v = hs.v;
            outp.Colors[5] = new HSV(y);

            return outp;
        }

        public static Blend ColorExplorer(HSV hs)
        {
            Blend outp = new Blend();
            outp.Colors[0] = new HSV(hs);

            HSV z = new HSV
            {
                h = hs.h,
                s = Math.Round(hs.s * 0.3),
                v = Math.Min(Math.Round(hs.v * 1.3), 100)
            };
            outp.Colors[1] = new HSV(z);

            z = new HSV
            {
                h = (hs.h + 300) % 360,
                s = hs.s,
                v = hs.v
            };
            outp.Colors[3] = new HSV(z);

            z.s = Math.Min(Math.Round(z.s * 1.2), 100);
            z.v = Math.Min(Math.Round(z.v * 0.5), 100);
            outp.Colors[2] = new HSV(z);

            z.s = 0;
            z.v = (hs.v + 50) % 100;
            outp.Colors[4] = new HSV(z);

            z.v = (z.v + 50) % 100;
            outp.Colors[5] = new HSV(z);

            return outp;
        }

        public static Blend SingleHue(HSV hs)
        {
            Blend outp = new Blend();
            outp.Colors[0] = new HSV(hs);

            HSV z = new HSV
            {
                h = hs.h,
                s = hs.s,
                v = hs.v + ((hs.v < 50) ? 20 : -20)
            };
            outp.Colors[1] = new HSV(z);

            z.s = hs.s;
            z.v = hs.v + ((hs.v < 50) ? 40 : -40);
            outp.Colors[2] = new HSV(z);

            z.s = hs.s + ((hs.s < 50) ? 20 : -20);
            z.v = hs.v;
            outp.Colors[3] = new HSV(z);

            z.s = hs.s + ((hs.s < 50) ? 40 : -40);
            z.v = hs.v;
            outp.Colors[4] = new HSV(z);

            z.s = hs.s + ((hs.s < 50) ? 40 : -40);
            z.v = hs.v + ((hs.v < 50) ? 40 : -40);
            outp.Colors[5] = new HSV(z);

            return outp;
        }

        public static Blend Complementary(HSV hs)
        {
            Blend outp = new Blend();
            outp.Colors[0] = new HSV(hs);

            HSV z = new HSV
            {
                h = hs.h,
                s = (hs.s > 50) ? (hs.s * 0.5) : (hs.s * 2),
                v = (hs.v < 50) ? (Math.Min(hs.v * 1.5, 100)) : (hs.v / 1.5)
            };
            outp.Colors[1] = new HSV(z);

            var w = MathHelpers.HueToWheel(hs.h);
            z.h = MathHelpers.WheelToHue((w + 180) % 360);
            z.s = hs.s;
            z.v = hs.v;
            outp.Colors[2] = new HSV(z);

            z.s = (z.s > 50) ? (z.s * 0.5) : (z.s * 2);
            z.v = (z.v < 50) ? (Math.Min(z.v * 1.5, 100)) : (z.v / 1.5);
            outp.Colors[3] = new HSV(z);

            z = new HSV
            {
                s = 0,
                h = 0,
                v = hs.v
            };
            outp.Colors[4] = new HSV(z);

            z.v = 100 - hs.v;
            outp.Colors[5] = new HSV(z);

            return outp;
        }

        public static Blend SplitComplementary(HSV hs)
        {
            Blend outp = new Blend();
            outp.Colors[0] = new HSV(hs);

            var w = MathHelpers.HueToWheel(hs.h);
            HSV z = new HSV
            {
                h = hs.h,
                s = hs.s,
                v = hs.v
            };

            z.h = MathHelpers.WheelToHue((w + 150) % 360);
            z.s = hs.s;
            z.v = hs.v;
            outp.Colors[1] = new HSV(z);

            z.h = MathHelpers.WheelToHue((w + 210) % 360);
            z.s = hs.s;
            z.v = hs.v;
            outp.Colors[2] = new HSV(z);

            z.s = 0;
            z.v = hs.s;
            outp.Colors[3] = new HSV(z);

            z.s = 0;
            z.v = hs.v;
            outp.Colors[4] = new HSV(z);

            z.s = 0;
            z.v = (100 - hs.v);
            outp.Colors[5] = new HSV(z);

            return outp;
        }

        public static Blend Analogue(HSV hs)
        {
            Blend outp = new Blend();
            outp.Colors[0] = new HSV(hs);

            var w = MathHelpers.HueToWheel(hs.h);
            HSV z = new HSV
            {
                h = MathHelpers.WheelToHue((w + 30) % 360),
                s = hs.s,
                v = hs.v
            };
            outp.Colors[1] = new HSV(z);

            z = new HSV
            {
                h = MathHelpers.WheelToHue((w + 60) % 360),
                s = hs.s,
                v = hs.v
            };
            outp.Colors[2] = new HSV(z);

            z = new HSV
            {
                s = 0,
                h = 0,
                v = 100 - hs.v
            };
            outp.Colors[3] = new HSV(z);

            z.v = Math.Round(hs.v * 1.3) % 100;
            outp.Colors[4] = new HSV(z);

            z.v = Math.Round(hs.v / 1.3) % 100;
            outp.Colors[5] = new HSV(z);

            return outp;
        }

        public static Blend Triadic(HSV hs)
        {
            Blend outp = new Blend();
            outp.Colors[0] = new HSV(hs);

            var w = MathHelpers.HueToWheel(hs.h);
            HSV z = new HSV
            {
                s = hs.s,
                h = hs.h,
                v = 100 - hs.v
            };
            outp.Colors[1] = new HSV(z);

            z = new HSV
            {
                h = MathHelpers.WheelToHue((w + 120) % 360),
                s = hs.s,
                v = hs.v
            };
            outp.Colors[2] = new HSV(z);

            z.v = 100 - z.v;
            outp.Colors[3] = new HSV(z);

            z = new HSV
            {
                h = MathHelpers.WheelToHue((w + 240) % 360),
                s = hs.s,
                v = hs.v
            };
            outp.Colors[4] = new HSV(z);

            z.v = 100 - z.v;
            outp.Colors[5] = new HSV(z);

            return outp;
        }

        public static Blend Square(HSV hs)
        {
            Blend outp = new Blend();
            outp.Colors[0] = new HSV(hs);

            var w = MathHelpers.HueToWheel(hs.h);
            HSV z = new HSV
            {
                h = MathHelpers.WheelToHue((w + 90) % 360),
                s = hs.s,
                v = hs.v
            };
            outp.Colors[1] = new HSV(z);

            z.h = MathHelpers.WheelToHue((w + 180) % 360);
            z.s = hs.s;
            z.v = hs.v;
            outp.Colors[2] = new HSV(z);

            z.h = MathHelpers.WheelToHue((w + 270) % 360);
            z.s = hs.s;
            z.v = hs.v;
            outp.Colors[3] = new HSV(z);

            z.s = 0;
            outp.Colors[4] = new HSV(z);

            z.v = 100 - z.v;
            outp.Colors[5] = new HSV(z);

            return outp;
        }

        public static Blend Match(HSV hs, string method)
        {
            switch (method)
            {
                case "classic":
                    return Classic(hs);
                case "colorexplorer":
                    return ColorExplorer(hs);
                case "singlehue":
                    return SingleHue(hs);
                case "complementary":
                    return Complementary(hs);
                case "splitcomplementary":
                    return SplitComplementary(hs);
                case "analogue":
                    return Analogue(hs);
                case "triadic":
                    return Triadic(hs);
                case "square":
                    return Square(hs);
            }

            return null;
        }
    }
}
