using Bowling.Frames;
using Bowling.UserInterface;

namespace Bowling
{
    public class BowlingScoreboard : IBowlingScoreboard
    {
        public int Calculate(IList<IFrame> frames, int frameNumber)
        {
            var score = 0;

            for (int i = 0; i < frameNumber; i++)
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
                    score += frames.Last().GetThrows().Last();
                }
            }

            return score;
        }

        public IList<IFrame> GetRecalculatedFrames(IList<IFrame> frames)
        {
            for (int i = 0; i < frames.Count; i++)
            {
                frames[i].FrameTotal = Calculate(frames, i + 1);
            }

            return frames;
        }

        public void PrintScoreboard(IList<IFrame> frames, int totalFrames, IConsole console)
        {
            var frameHeaders = string.Empty;
            var scores = string.Empty;

            for (int i = 0; i < totalFrames; i++)
            {
                frameHeaders += $"|F{i + 1}";
                if (i < frames.Count) 
                {
                    scores += $"|{frames[i].ThrowsAsString}  {frames[i].FrameTotal}";
                }
                else
                {
                    scores += "|       ";
                }
                var lenDiff = scores.Length - frameHeaders.Length;
                for (int j = 0; j < lenDiff; j++)
                {
                    frameHeaders += " ";
                }
                if (i == totalFrames - 1)
                {
                    frameHeaders += "|";
                    scores += "|";
                }
            }

            console.WriteLine(frameHeaders);
            console.WriteLine(scores);
        }

        private static int CalculateStrike(IList<IFrame> frames, int index) 
        {
            if (frames[index].IsLast)
            {
                return frames.Last().GetScore();
            }
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
