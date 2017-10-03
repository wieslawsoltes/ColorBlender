// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;

namespace ColorBlender
{
    public class RGB
    {
        public double r;
        public double g;
        public double b;

        public RGB() { }

        public RGB(double r, double g, double b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public RGB(RGB rg)
        {
            this.r = rg.r;
            this.g = rg.g;
            this.b = rg.b;
        }

        public RGB(HSV hs)
        {
            RGB rg = hs.ToRGB();
            this.r = rg.r;
            this.g = rg.g;
            this.b = rg.b;
        }

        public HSV ToHSV()
        {
            HSV hs = new HSV();
            RGB rg = new RGB(this.r, this.g, this.b);

            var m = rg.r;
            if (rg.g < m) { m = rg.g; }
            if (rg.b < m) { m = rg.b; }
            var v = rg.r;
            if (rg.g > v) { v = rg.g; }
            if (rg.b > v) { v = rg.b; }
            var value = 100 * v / 255;
            var delta = v - m;
            if (v == 0.0) { hs.s = 0; } else { hs.s = 100 * delta / v; }

            if (hs.s == 0) { hs.h = 0; }
            else
            {
                if (rg.r == v) { hs.h = 60.0 * (rg.g - rg.b) / delta; }
                else if (rg.g == v) { hs.h = 120.0 + 60.0 * (rg.b - rg.r) / delta; }
                else if (rg.b == v) { hs.h = 240.0 + 60.0 * (rg.r - rg.g) / delta; }
                if (hs.h < 0.0) { hs.h = hs.h + 360.0; }
            }

            hs.h = Math.Round(hs.h);
            hs.s = Math.Round(hs.s);
            hs.v = Math.Round(value);

            return hs;
        }
    }
}
