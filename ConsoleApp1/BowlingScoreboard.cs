using Bowling.Frames;

namespace Bowling
{
    public class BowlingScoreboard : IBowlingScoreboard
    {
        public int Calculate(IList<IFrame> frames)
        {
            var score = 0;

            for (int i = 0; i < frames.Count; i++)
            {
                switch (frames[i].GetFrameType()) 
                {
                    case FrameType.Regular:
                        score += frames[i].GetScore();
                        break;
                    case FrameType.Spare:
                        score += frames.Count - 1 > i 
                            ? 10 + frames[i + 1].GetThrows().First() 
                            : 10;
                        break;
                    case FrameType.Strike:
                        score += CalculateStrike(frames, i);
                        break;
                }

                if (frames[i].IsLast && frames[i].GetFrameType() is FrameType.Spare or FrameType.Strike)
                {
                    score += frames[i].GetThrows().Last();
                }
            }

            return score;
        }

        private static int CalculateStrike(IList<IFrame> frames, int index) 
        {
            var score = 10;

            if (frames.Count - 1 > index)
            {
                score += frames[index + 1].GetScore();
            }

            if (frames.Count - 2 > index && frames[index + 1].GetThrows().Count < 2)
            {
                score += frames[index + 2].GetThrows().First();
            }

            return score;
        }
    }
}
