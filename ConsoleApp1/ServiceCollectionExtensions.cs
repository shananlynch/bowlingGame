using Bowling.Frames;
using Bowling.UserInterface;
using Microsoft.Extensions.DependencyInjection;

namespace Bowling
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IFrame, Frame>();
            services.AddScoped<IConsole, ConsoleWrapper>();

            return services;
        }
    }
}
