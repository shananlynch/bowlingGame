using Bowling.UserInterface;

namespace Bowling.Frames
{
    public class Frame : IFrame
    {
        private readonly IList<int> _throws = new List<int>();
        private readonly IConsole _console;

        private FrameType FrameType = FrameType.Regular;

        public bool IsLast { get; }

        public int FrameTotal { get; set; } = 0;
        public string ThrowsAsString { get ; set ; } = string.Empty;

        public Frame(IConsole console, bool isLast = false) 
        {
            _console = console;
            IsLast = isLast;
        }

        public void AddThrow(int value)
        {
            _throws.Add(value);
            if (value == 10)
            {
                FrameType = FrameType.Strike;
                if (IsLast)
                {
                    ThrowsAsString += "X ";
                }
                else
                {
                    ThrowsAsString += "X - ";
                }
            }
            else if (_throws.Count == 2 && _throws.Sum() == 10) 
            {
                FrameType = FrameType.Spare;
                ThrowsAsString += "/ ";
            }
            else 
            {
                ThrowsAsString += $"{value} ";
            }
        }

        public int GetScore()
        {
            return _throws.Count > 1 ? _throws.First() + _throws[1] : _throws.First();
        }

        public IList<int> GetThrows()
        {
            return _throws;
        }

        public FrameType GetFrameType()
        {
            return FrameType;
        }

        public void ReadAndAdd(string throwNumber)
        {
            _console.Write($"Enter number of pins knocked down in {throwNumber} throw: ");
            try 
            {
                var roll = Convert.ToInt32(_console.ReadLine());
                if (roll > 10 || roll < 0)
                {
                    _console.WriteLine("Please enter a number between 0 - 10");
                    ReadAndAdd(throwNumber);
                }
                else if (throwNumber == "second" && roll + _throws.First() > 10 && !IsLast)
                {
                    _console.WriteLine($"Please enter a number between 0 - {10 - _throws.First()}");
                    ReadAndAdd(throwNumber);
                }
                else
                {
                    AddThrow(roll);
                }
            }
            catch
            {
                _console.WriteLine("Please enter a number between 0 - 10");
                ReadAndAdd(throwNumber);
            }
        }
    }
}
