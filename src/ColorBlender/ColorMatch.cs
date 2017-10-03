// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;

namespace ColorBlender
{
    public static class ColorMatch
    {
        public static double RC(double x, double m)
        {
            if (x > m) { return m; }
            if (x < 0) { return 0; } else { return x; }
        }

        public static double HueToWheel(double h)
        {
            if (h <= 120)
            {
                return (Math.Round(h * 1.5));
            }
            else
            {
                return (Math.Round(180 + (h - 120) * 0.75));
            }
        }

        public static double WheelToHue(double w)
        {
            if (w <= 180)
            {
                return (Math.Round(w / 1.5));
            }
            else
            {
                return (Math.Round(120 + (w - 180) / 0.75));
            }
        }

        public static void Classic(HSV hs, HSV y, HSV yx, Blend outp)
        {
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
                y.s = RC(hs.s - 30, 100);
                y.v = RC(hs.v - 20, 100);
                yx.s = RC(hs.s - 50, 100);
                yx.v = RC(hs.v + 20, 100);
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
                yx.s = y.s = RC(hs.s - 40, 100);
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
        }

        public static void ColorExplorer(HSV hs, Blend outp)
        {
            HSV z = new HSV();

            z.h = hs.h;
            z.s = Math.Round(hs.s * 0.3);
            z.v = Math.Min(Math.Round(hs.v * 1.3), 100);
            outp.Colors[1] = new HSV(z);

            z = new HSV();
            z.h = (hs.h + 300) % 360;
            z.s = hs.s;
            z.v = hs.v;
            outp.Colors[3] = new HSV(z);

            z.s = Math.Min(Math.Round(z.s * 1.2), 100);
            z.v = Math.Min(Math.Round(z.v * 0.5), 100);
            outp.Colors[2] = new HSV(z);

            z.s = 0;
            z.v = (hs.v + 50) % 100;
            outp.Colors[4] = new HSV(z);

            z.v = (z.v + 50) % 100;
            outp.Colors[5] = new HSV(z);
        }

        public static void SingleHue(HSV hs, Blend outp)
        {
            HSV z = new HSV();

            z.h = hs.h;
            z.s = hs.s;
            z.v = hs.v + ((hs.v < 50) ? 20 : -20);
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
        }

        public static void Complementary(HSV hs, Blend outp)
        {
            HSV z = new HSV();

            z.h = hs.h;
            z.s = (hs.s > 50) ? (hs.s * 0.5) : (hs.s * 2);
            z.v = (hs.v < 50) ? (Math.Min(hs.v * 1.5, 100)) : (hs.v / 1.5);
            outp.Colors[1] = new HSV(z);

            var w = HueToWheel(hs.h);
            z.h = WheelToHue((w + 180) % 360);
            z.s = hs.s;
            z.v = hs.v;
            outp.Colors[2] = new HSV(z);

            z.s = (z.s > 50) ? (z.s * 0.5) : (z.s * 2);
            z.v = (z.v < 50) ? (Math.Min(z.v * 1.5, 100)) : (z.v / 1.5);
            outp.Colors[3] = new HSV(z);

            z = new HSV();
            z.s = 0;
            z.h = 0;
            z.v = hs.v;
            outp.Colors[4] = new HSV(z);

            z.v = 100 - hs.v;
            outp.Colors[5] = new HSV(z);
        }

        public static void SplitComplementary(HSV hs, Blend outp)
        {
            var w = HueToWheel(hs.h);
            HSV z = new HSV();

            z.h = hs.h;
            z.s = hs.s;
            z.v = hs.v;

            z.h = WheelToHue((w + 150) % 360);
            z.s = hs.s;
            z.v = hs.v;
            outp.Colors[1] = new HSV(z);

            z.h = WheelToHue((w + 210) % 360);
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
        }

        public static void Analogue(HSV hs, Blend outp)
        {
            var w = HueToWheel(hs.h);
            HSV z = new HSV();

            z.h = WheelToHue((w + 30) % 360);
            z.s = hs.s;
            z.v = hs.v;
            outp.Colors[1] = new HSV(z);

            z = new HSV();
            z.h = WheelToHue((w + 60) % 360);
            z.s = hs.s;
            z.v = hs.v;
            outp.Colors[2] = new HSV(z);

            z = new HSV();
            z.s = 0;
            z.h = 0;
            z.v = 100 - hs.v;
            outp.Colors[3] = new HSV(z);

            z.v = Math.Round(hs.v * 1.3) % 100;
            outp.Colors[4] = new HSV(z);

            z.v = Math.Round(hs.v / 1.3) % 100;
            outp.Colors[5] = new HSV(z);
        }

        public static void Triadic(HSV hs, Blend outp)
        {
            var w = HueToWheel(hs.h);
            HSV z = new HSV();

            z.s = hs.s;
            z.h = hs.h;
            z.v = 100 - hs.v;
            outp.Colors[1] = new HSV(z);

            z = new HSV();
            z.h = WheelToHue((w + 120) % 360);
            z.s = hs.s;
            z.v = hs.v;
            outp.Colors[2] = new HSV(z);

            z.v = 100 - z.v;
            outp.Colors[3] = new HSV(z);

            z = new HSV();
            z.h = WheelToHue((w + 240) % 360);
            z.s = hs.s;
            z.v = hs.v;
            outp.Colors[4] = new HSV(z);

            z.v = 100 - z.v;
            outp.Colors[5] = new HSV(z);
        }

        public static void Square(HSV hs, Blend outp)
        {
            var w = HueToWheel(hs.h);
            HSV z = new HSV();

            z.h = WheelToHue((w + 90) % 360);
            z.s = hs.s;
            z.v = hs.v;
            outp.Colors[1] = new HSV(z);

            z.h = WheelToHue((w + 180) % 360);
            z.s = hs.s;
            z.v = hs.v;
            outp.Colors[2] = new HSV(z);

            z.h = WheelToHue((w + 270) % 360);
            z.s = hs.s;
            z.v = hs.v;
            outp.Colors[3] = new HSV(z);

            z.s = 0;
            outp.Colors[4] = new HSV(z);

            z.v = 100 - z.v;
            outp.Colors[5] = new HSV(z);
        }

        public static Blend Match(RGB rg)
        {
            return Match(rg.ToHSV());
        }

        public static Blend Match(HSV hs)
        {
            // Color matching algorithm. All work is done in HSV color space, because all
            // calculations are based on hue, saturation and value of the working color.
            // The hue spectrum is divided into sections, are the matching colors are
            // calculated differently depending on the hue of the color.

            Blend z = new Blend();
            HSV y = new HSV();
            HSV yx = new HSV();

            z.Colors[0] = new HSV(hs);

            y.s = hs.s;
            y.h = hs.h;

            if (hs.v > 70) { y.v = hs.v - 30; } else { y.v = hs.v + 30; }

            z.Colors[1] = new HSV(y);

            if ((hs.h >= 0) && (hs.h < 30))
            {
                yx.h = y.h = hs.h + 30; yx.s = y.s = hs.s; y.v = hs.v;
                if (hs.v > 70) { yx.v = hs.v - 30; } else { yx.v = hs.v + 30; }
            }

            if ((hs.h >= 30) && (hs.h < 60))
            {
                yx.h = y.h = hs.h + 150;
                y.s = RC(hs.s - 30, 100);
                y.v = RC(hs.v - 20, 100);
                yx.s = RC(hs.s - 50, 100);
                yx.v = RC(hs.v + 20, 100);
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
                yx.s = y.s = RC(hs.s - 40, 100);
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

            z.Colors[2] = new HSV(y);
            z.Colors[3] = new HSV(yx);

            y.h = 0;
            y.s = 0;
            y.v = 100 - hs.v;

            z.Colors[4] = new HSV(y);

            y.h = 0;
            y.s = 0;
            y.v = hs.v;

            z.Colors[5] = new HSV(y);

            return z;
        }

        // Color matching algorithms:
        // "classic"               ColorMatch 5K Classic
        // "colorexplorer"         ColorExplorer - "Sweet Spot Offset"
        // "singlehue"             Single Hue
        // "complementary"         Complementary
        // "splitcomplementary"    Split-Complementary
        // "analogue"              Analogue
        // "triadic"               Triadic
        // "square"                Square
        public static Blend Match(RGB rg, string method)
        {
            return Match(rg.ToHSV(), method);
        }

        public static Blend Match(HSV hs, string method)
        {
            // Color matching algorithm. All work is done in HSV color space, because all
            // calculations are based on hue, saturation and value of the working color.
            // The hue spectrum is divided into sections, are the matching colors are
            // calculated differently depending on the hue of the color.
            Blend outp = new Blend();
            HSV y = new HSV();
            HSV yx = new HSV();

            outp.Colors[0] = new HSV(hs);

            switch (method)
            {
                case "classic":
                    {
                        Classic(hs, y, yx, outp);
                    }
                    break;

                case "colorexplorer":
                    {
                        ColorExplorer(hs, outp);
                    }
                    break;

                case "singlehue":
                    {
                        SingleHue(hs, outp);
                    }
                    break;

                case "complementary":
                    {
                        Complementary(hs, outp);
                    }
                    break;

                case "splitcomplementary":
                    {
                        SplitComplementary(hs, outp);
                    }
                    break;

                case "analogue":
                    {
                        Analogue(hs, outp);
                    }
                    break;

                case "triadic":
                    {
                        Triadic(hs, outp);
                    }
                    break;

                case "square":
                    {
                        Square(hs, outp);
                    }
                    break;
            }

            return outp;
        }
    }
}
