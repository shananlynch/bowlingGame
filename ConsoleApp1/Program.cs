using Bowling;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection().ConfigureServices().BuildServiceProvider();

        var game = serviceProvider.GetService<IGame>();
        game!.Start();

        Console.WriteLine("Game Over");
        Console.ReadLine();
    }
}