using Bowling.UserInterface;
using Xunit;
using NSubstitute;
using Bowling.Frames;
using FluentAssertions;

namespace BowlingTests.FrameTests
{
    public class FrameTests
    {
        private readonly IConsole _console;

        public FrameTests()
        {
            _console = Substitute.For<IConsole>();
        }

        [Fact]
        public void AddsStrike_FrameTypeEqualStrike()
        {
            var frame = new Frame(_console);

            frame.AddThrow(10);

            _console.Received().WriteLine("X");
            frame.GetFrameType().Should().Be(FrameType.Strike);
            frame.GetThrows().First().Should().Be(10);
        }

        [Fact]
        public void Adds6_FrameTypeEqualRegular()
        {
            var frame = new Frame(_console);

            frame.AddThrow(6);

            _console.Received().WriteLine("6");
            frame.GetFrameType().Should().Be(FrameType.Regular);
            frame.GetThrows().First().Should().Be(6);
        }

        [Fact]
        public void AddsSpare_FrameTypeEqualSpare()
        {
            var frame = new Frame(_console);
            frame.AddThrow(5);

            frame.AddThrow(5);

            _console.Received().WriteLine("/");
            frame.GetFrameType().Should().Be(FrameType.Spare);
            frame.GetThrows().First().Should().Be(5);
            frame.GetScore().Should().Be(10);
        }

        [Fact]
        public void ReadAndAdd_AddsNewThrow()
        {
            var frame = new Frame(_console);
            _console.ReadLine().Returns("6");

            frame.ReadAndAdd("first");


            _console.Received().WriteLine("6");
            frame.GetFrameType().Should().Be(FrameType.Regular);
            frame.GetThrows().First().Should().Be(6);
        }

        [Fact]
        public void ReadAndAdd_EnterNumberOutsideOfParameters()
        {
            var frame = new Frame(_console);
            _console.ReadLine().Returns("11", "6");

            frame.ReadAndAdd("first");

            _console.Received().WriteLine("Please enter a number between 0 - 10");
            _console.Received().WriteLine("6");
            frame.GetFrameType().Should().Be(FrameType.Regular);
            frame.GetThrows().First().Should().Be(6);
        }

        [Fact]
        public void ReadAndAdd_SumOfThrowsGreaterThan10()
        {
            var frame = new Frame(_console);
            frame.AddThrow(5);
            _console.ReadLine().Returns("6", "5");

            frame.ReadAndAdd("second");

            _console.Received().WriteLine("Please enter a number between 0 - 5");
            _console.Received().WriteLine("5");
            frame.GetFrameType().Should().Be(FrameType.Spare);
            frame.GetThrows().First().Should().Be(5);
        }

        [Fact]
        public void ReadAndAdd_EnteredNonNumeric()
        {
            var frame = new Frame(_console);
            _console.ReadLine().Returns("L", "5");

            frame.ReadAndAdd("first");

            _console.Received().WriteLine("Please enter a number between 0 - 10");
            _console.Received().WriteLine("5");
            frame.GetFrameType().Should().Be(FrameType.Regular);
            frame.GetThrows().First().Should().Be(5);
        }
    }
}
