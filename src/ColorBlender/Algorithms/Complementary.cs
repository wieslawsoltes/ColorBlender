// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;

namespace ColorBlender.Algorithms
{
    public class Complementary : IAlgorithm
    {
        public Blend Match(HSV hsv)
        {
            Blend outp = new Blend();
            outp.Colors[0] = new HSV(hsv);

            HSV z = new HSV
            {
                h = hsv.h,
                s = (hsv.s > 50) ? (hsv.s * 0.5) : (hsv.s * 2),
                v = (hsv.v < 50) ? (Math.Min(hsv.v * 1.5, 100)) : (hsv.v / 1.5)
            };
            outp.Colors[1] = new HSV(z);

            var w = MathHelpers.HueToWheel(hsv.h);
            z.h = MathHelpers.WheelToHue((w + 180) % 360);
            z.s = hsv.s;
            z.v = hsv.v;
            outp.Colors[2] = new HSV(z);

            z.s = (z.s > 50) ? (z.s * 0.5) : (z.s * 2);
            z.v = (z.v < 50) ? (Math.Min(z.v * 1.5, 100)) : (z.v / 1.5);
            outp.Colors[3] = new HSV(z);

            z = new HSV
            {
                s = 0,
                h = 0,
                v = hsv.v
            };
            outp.Colors[4] = new HSV(z);

            z.v = 100 - hsv.v;
            outp.Colors[5] = new HSV(z);

            return outp;
        }
    }
}
