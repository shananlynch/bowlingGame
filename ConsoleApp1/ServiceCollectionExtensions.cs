using Bowling.UserInterface;
using Microsoft.Extensions.DependencyInjection;

namespace Bowling
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IConsole, ConsoleWrapper>();
            services.AddScoped<IBowlingScoreboard, BowlingScoreboard>();
            services.AddScoped<IGame, BowlingGame>();

            return services;
        }
    }
}
