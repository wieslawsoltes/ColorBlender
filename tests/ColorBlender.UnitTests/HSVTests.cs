// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using ColorBlender.Colors;
using Xunit;

namespace ColorBlender.UnitTests
{
    public class HSVTests
    {
        [Fact]
        public void HSV_ToRGB()
        {
            var hsv = new HSV(213.0, 46.0, 49.0);
            var actual = hsv.ToRGB();
            Assert.Equal(67.0, actual.R);
            Assert.Equal(93.0, actual.G);
            Assert.Equal(125.0, actual.B);
        }
    }
}
