using Bowling.Frames;

namespace Bowling
{
    public interface IBowlingScoreboard
    {
        int Calculate(IList<IFrame> frames);
    }
}
