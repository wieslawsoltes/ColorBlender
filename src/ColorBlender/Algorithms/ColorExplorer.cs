// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;

namespace ColorBlender.Algorithms
{
    public class ColorExplorer : IAlgorithm
    {
        public Blend Match(HSV hsv)
        {
            Blend outp = new Blend();
            outp.Colors[0] = new HSV(hsv);

            HSV z = new HSV
            {
                h = hsv.h,
                s = Math.Round(hsv.s * 0.3),
                v = Math.Min(Math.Round(hsv.v * 1.3), 100)
            };
            outp.Colors[1] = new HSV(z);

            z = new HSV
            {
                h = (hsv.h + 300) % 360,
                s = hsv.s,
                v = hsv.v
            };
            outp.Colors[3] = new HSV(z);

            z.s = Math.Min(Math.Round(z.s * 1.2), 100);
            z.v = Math.Min(Math.Round(z.v * 0.5), 100);
            outp.Colors[2] = new HSV(z);

            z.s = 0;
            z.v = (hsv.v + 50) % 100;
            outp.Colors[4] = new HSV(z);

            z.v = (z.v + 50) % 100;
            outp.Colors[5] = new HSV(z);

            return outp;
        }
    }
}
