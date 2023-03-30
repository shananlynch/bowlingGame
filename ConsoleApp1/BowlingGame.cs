using Bowling.Frames;
using Bowling.UserInterface;

namespace Bowling
{
    public class BowlingGame : IGame
    {
        private IList<IFrame> frames = new List<IFrame>();
        private readonly IBowlingScoreboard _scoreboard;
        private readonly IConsole _console;
        private readonly int _rounds = 10;

        public BowlingGame(IBowlingScoreboard scoreboard, IConsole console)
        {
            _scoreboard = scoreboard;
            _console = console;
        }

        public void Start()
        {
            for (int round = 1; round <= _rounds; round++)
            {
                var frame = round == _rounds ? new Frame(_console, true) : new Frame(_console);
                frame.ReadAndAdd("first");

                if (frame.GetFrameType() != FrameType.Strike || frame.IsLast)
                {
                    frame.ReadAndAdd("second");
                }

                if (frame.IsLast && frame.GetFrameType() is FrameType.Strike or FrameType.Spare)
                {
                    frame.ReadAndAdd("third");
                }

                frames.Add(frame);
                frames = _scoreboard.GetRecalculatedFrames(frames);
                _scoreboard.PrintScoreboard(frames, _rounds, _console);
            }
        }
    }
}
