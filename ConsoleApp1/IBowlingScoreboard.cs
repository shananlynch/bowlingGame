using Bowling.Frames;
using Bowling.UserInterface;

namespace Bowling
{
    public interface IBowlingScoreboard
    {
        int Calculate(IList<IFrame> frames, int frameNumber);

        IList<IFrame> GetRecalculatedFrames(IList<IFrame> frames);

        void PrintScoreboard(IList<IFrame> frames, int totalFrames, IConsole console);
    }
}
