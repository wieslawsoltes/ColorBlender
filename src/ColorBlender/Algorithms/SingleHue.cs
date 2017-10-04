// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ColorBlender.Algorithms
{
    public class SingleHue : IAlgorithm
    {
        public Blend Match(HSV hsv)
        {
            Blend outp = new Blend();
            outp.Colors[0] = new HSV(hsv);

            HSV z = new HSV
            {
                h = hsv.h,
                s = hsv.s,
                v = hsv.v + ((hsv.v < 50) ? 20 : -20)
            };
            outp.Colors[1] = new HSV(z);

            z.s = hsv.s;
            z.v = hsv.v + ((hsv.v < 50) ? 40 : -40);
            outp.Colors[2] = new HSV(z);

            z.s = hsv.s + ((hsv.s < 50) ? 20 : -20);
            z.v = hsv.v;
            outp.Colors[3] = new HSV(z);

            z.s = hsv.s + ((hsv.s < 50) ? 40 : -40);
            z.v = hsv.v;
            outp.Colors[4] = new HSV(z);

            z.s = hsv.s + ((hsv.s < 50) ? 40 : -40);
            z.v = hsv.v + ((hsv.v < 50) ? 40 : -40);
            outp.Colors[5] = new HSV(z);

            return outp;
        }
    }
}
