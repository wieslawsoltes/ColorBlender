﻿using ColorBlender.Colors;

namespace ColorBlender.Algorithms
{
    public class SplitComplementary : IAlgorithm
    {
        public override string ToString() => "Split Complementary";

        public Blend Match(HSV hsv)
        {
            Blend outp = new Blend();
            outp.Colors[0] = new HSV(hsv);

            var w = MathHelpers.HueToWheel(hsv.H);

            HSV z = hsv.WithH(MathHelpers.WheelToHue((w + 150) % 360));
            outp.Colors[1] = new HSV(z);

            z = hsv.WithH(MathHelpers.WheelToHue((w + 210) % 360));
            outp.Colors[2] = new HSV(z);

            z = z.WithS(0);
            z = z.WithV(hsv.S);
            outp.Colors[3] = new HSV(z);

            z = z.WithS(0);
            z = z.WithV(hsv.V);
            outp.Colors[4] = new HSV(z);

            z = z.WithS(0);
            z = z.WithV(100 - hsv.V);
            outp.Colors[5] = new HSV(z);

            return outp;
        }
    }
}
