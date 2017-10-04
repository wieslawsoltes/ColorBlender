// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ColorBlender.Algorithms
{
    public class SplitComplementary : IAlgorithm
    {
        public Blend Match(HSV hsv)
        {
            Blend outp = new Blend();
            outp.Colors[0] = new HSV(hsv);

            var w = MathHelpers.HueToWheel(hsv.h);
            HSV z = new HSV
            {
                h = hsv.h,
                s = hsv.s,
                v = hsv.v
            };

            z.h = MathHelpers.WheelToHue((w + 150) % 360);
            z.s = hsv.s;
            z.v = hsv.v;
            outp.Colors[1] = new HSV(z);

            z.h = MathHelpers.WheelToHue((w + 210) % 360);
            z.s = hsv.s;
            z.v = hsv.v;
            outp.Colors[2] = new HSV(z);

            z.s = 0;
            z.v = hsv.s;
            outp.Colors[3] = new HSV(z);

            z.s = 0;
            z.v = hsv.v;
            outp.Colors[4] = new HSV(z);

            z.s = 0;
            z.v = (100 - hsv.v);
            outp.Colors[5] = new HSV(z);

            return outp;
        }
    }
}
