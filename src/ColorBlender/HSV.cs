// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;

namespace ColorBlender
{
    public class HSV
    {
        public double h;
        public double s;
        public double v;

        public HSV() { }

        public HSV(double h, double s, double v)
        {
            this.h = h;
            this.s = s;
            this.v = v;
        }

        public HSV(HSV hs)
        {
            this.h = hs.h;
            this.s = hs.s;
            this.v = hs.v;
        }

        public HSV(RGB rg)
        {
            HSV hs = rg.ToHSV();
            this.h = hs.h;
            this.s = hs.s;
            this.v = hs.v;
        }

        public RGB ToRGB()
        {
            RGB rg = new RGB();
            HSV hsx = new HSV(this.h, this.s, this.v);

            if (hsx.s == 0)
            {
                rg.r = rg.g = rg.b = Math.Round(hsx.v * 2.55); return (rg);
            }

            hsx.s = hsx.s / 100;
            hsx.v = hsx.v / 100;
            hsx.h /= 60;

            var i = Math.Floor(hsx.h);
            var f = hsx.h - i;
            var p = hsx.v * (1 - hsx.s);
            var q = hsx.v * (1 - hsx.s * f);
            var t = hsx.v * (1 - hsx.s * (1 - f));

            switch ((int)i)
            {
                case 0: rg.r = hsx.v; rg.g = t; rg.b = p; break;
                case 1: rg.r = q; rg.g = hsx.v; rg.b = p; break;
                case 2: rg.r = p; rg.g = hsx.v; rg.b = t; break;
                case 3: rg.r = p; rg.g = q; rg.b = hsx.v; break;
                case 4: rg.r = t; rg.g = p; rg.b = hsx.v; break;
                default: rg.r = hsx.v; rg.g = p; rg.b = q; break;
            }

            rg.r = Math.Round(rg.r * 255);
            rg.g = Math.Round(rg.g * 255);
            rg.b = Math.Round(rg.b * 255);

            return rg;
        }
    }
}
