namespace Bowling.Frames
{
    public interface IFrame
    {
        bool IsLast { get; }

        int FrameTotal { get; set; }

        string ThrowsAsString { get; set; }

        int GetScore();

        IList<int> GetThrows();

        void AddThrow(int value);

        FrameType GetFrameType();

        void ReadAndAdd(string throwNumber);
    }
}
