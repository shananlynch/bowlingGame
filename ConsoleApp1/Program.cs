using Bowling;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.ConfigureServices();

        IGame bowlingGame = new BowlingGame();
        bowlingGame.Start();

        Console.WriteLine("Game Over");
        Console.ReadLine();
    }
}