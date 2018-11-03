// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using ColorBlender.Colors;
using Xunit;

namespace ColorBlender.UnitTests
{
    public class RGBTests
    {
        [Fact]
        public void RGB_ToHSV()
        {
            var rgb = new RGB(67.0, 93.0, 125.0);
            var actual = rgb.ToHSV();
            Assert.Equal(213.0, actual.H);
            Assert.Equal(46.0, actual.S);
            Assert.Equal(49.0, actual.V);
        }
    }
}
