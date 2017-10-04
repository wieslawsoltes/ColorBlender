// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ColorBlender.Algorithms
{
    public class Classic : IAlgorithm
    {
        public Blend Match(HSV hsv)
        {
            Blend outp = new Blend();
            outp.Colors[0] = new HSV(hsv);

            HSV y = new HSV();
            HSV yx = new HSV();

            y.s = hsv.s;
            y.h = hsv.h;
            if (hsv.v > 70) { y.v = hsv.v - 30; } else { y.v = hsv.v + 30; };
            outp.Colors[1] = new HSV(y);

            if ((hsv.h >= 0) && (hsv.h < 30))
            {
                yx.h = y.h = hsv.h + 30; yx.s = y.s = hsv.s; y.v = hsv.v;
                if (hsv.v > 70) { yx.v = hsv.v - 30; } else { yx.v = hsv.v + 30; }
            }

            if ((hsv.h >= 30) && (hsv.h < 60))
            {
                yx.h = y.h = hsv.h + 150;
                y.s = MathHelpers.RC(hsv.s - 30, 100);
                y.v = MathHelpers.RC(hsv.v - 20, 100);
                yx.s = MathHelpers.RC(hsv.s - 50, 100);
                yx.v = MathHelpers.RC(hsv.v + 20, 100);
            }

            if ((hsv.h >= 60) && (hsv.h < 180))
            {
                yx.h = y.h = hsv.h - 40;
                y.s = yx.s = hsv.s;
                y.v = hsv.v; if (hsv.v > 70) { yx.v = hsv.v - 30; } else { yx.v = hsv.v + 30; }
            }

            if ((hsv.h >= 180) && (hsv.h < 220))
            {
                yx.h = hsv.h - 170;
                y.h = hsv.h - 160;
                yx.s = y.s = hsv.s;
                y.v = hsv.v;
                if (hsv.v > 70) { yx.v = hsv.v - 30; } else { yx.v = hsv.v + 30; }

            }
            if ((hsv.h >= 220) && (hsv.h < 300))
            {
                yx.h = y.h = hsv.h;
                yx.s = y.s = MathHelpers.RC(hsv.s - 40, 100);
                y.v = hsv.v;
                if (hsv.v > 70) { yx.v = hsv.v - 30; } else { yx.v = hsv.v + 30; }
            }
            if (hsv.h >= 300)
            {
                if (hsv.s > 50) { y.s = yx.s = hsv.s - 40; } else { y.s = yx.s = hsv.s + 40; }
                yx.h = y.h = (hsv.h + 20) % 360;
                y.v = hsv.v;
                if (hsv.v > 70) { yx.v = hsv.v - 30; } else { yx.v = hsv.v + 30; }
            }

            outp.Colors[2] = new HSV(y);
            outp.Colors[3] = new HSV(yx);

            y.h = 0;
            y.s = 0;
            y.v = 100 - hsv.v;
            outp.Colors[4] = new HSV(y);

            y.h = 0;
            y.s = 0;
            y.v = hsv.v;
            outp.Colors[5] = new HSV(y);

            return outp;
        }
    }
}
