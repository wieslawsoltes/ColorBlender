using ColorBlender.Colors;

namespace ColorBlender
{
    public interface IAlgorithm
    {
        Blend Match(HSV hsv);
    }
}
