using ColorBlender.Colors;
using Xunit;

namespace ColorBlender.UnitTests
{
    public class BlendTests
    {
        [Fact]
        public void HSV_Colors()
        {
            var blend = new Blend();
            Assert.IsType<HSV[]>(blend.Colors);
        }

        [Fact]
        public void Initialize_Colors()
        {
            var blend = new Blend();
            var actual = blend.Colors.Length;
            Assert.Equal(6, actual);
        }
    }
}
