using FelevesFeladat.Application;
using FelevesFeladat.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FelevesFeladat.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var app = host.Services.GetRequiredService<Application>();
            app.Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    // Repositories
                    services.AddScoped<ITrainRepository, TrainRepository>();
                    services.AddScoped<IPassengerRepository, PassengerRepository>();
                    services.AddScoped<IJourneyRepository, JourneyRepository>();

                    // Services
                    services.AddScoped<ITrainService, TrainService>();
                    services.AddScoped<IPassengerService, PassengerService>();
                    services.AddScoped<IJourneyService, JourneyService>();

                    // Application
                    services.AddScoped<Application>();
                });
    }
}