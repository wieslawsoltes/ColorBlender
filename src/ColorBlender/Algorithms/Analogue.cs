// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;

namespace ColorBlender.Algorithms
{
    public class Analogue : IAlgorithm
    {
        public Blend Match(HSV hsv)
        {
            Blend outp = new Blend();
            outp.Colors[0] = new HSV(hsv);

            var w = MathHelpers.HueToWheel(hsv.h);
            HSV z = new HSV
            {
                h = MathHelpers.WheelToHue((w + 30) % 360),
                s = hsv.s,
                v = hsv.v
            };
            outp.Colors[1] = new HSV(z);

            z = new HSV
            {
                h = MathHelpers.WheelToHue((w + 60) % 360),
                s = hsv.s,
                v = hsv.v
            };
            outp.Colors[2] = new HSV(z);

            z = new HSV
            {
                s = 0,
                h = 0,
                v = 100 - hsv.v
            };
            outp.Colors[3] = new HSV(z);

            z.v = Math.Round(hsv.v * 1.3) % 100;
            outp.Colors[4] = new HSV(z);

            z.v = Math.Round(hsv.v / 1.3) % 100;
            outp.Colors[5] = new HSV(z);

            return outp;
        }
    }
}
