using Bowling.Frames;
using Bowling.UserInterface;

namespace Bowling
{
    public class BowlingGame : IGame
    {
        private readonly IList<IFrame> frames = new List<IFrame>();
        private readonly IBowlingScoreboard scoreboard = new BowlingScoreboard();
        private readonly IConsole _console = new ConsoleWrapper();

        public void Start()
        {
            for (int round = 1; round <= 10; round++)
            {
                var frame = round == 10 ? new Frame(_console, true) : new Frame(_console);
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
                Console.WriteLine($"Score after frame {round}: {scoreboard.Calculate(frames)}");
            }
        }
    }
}
