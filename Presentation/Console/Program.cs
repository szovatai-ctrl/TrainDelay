using FelevesFeladat.Application;
using FelevesFeladat.Persistence;
using FelevesFeladat.Persistence.MsSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace FelevesFeladat.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<TrainDbContext>();

                try
                {
                    InitializeDatabase(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during database initialization: {ex.Message}");
                    return;
                }
            }

            var app = host.Services.GetRequiredService<Application>();
            app.Run();
        }

        static void InitializeDatabase(TrainDbContext context)
        {
            Console.WriteLine("TrainApp - Console Application");
            Console.WriteLine("1. Initialize empty database");
            Console.WriteLine("2. Load database from JSON");

            int choice;
            while (true)
            {
                Console.WriteLine("Enter your choice (1 or 2):");
                if (int.TryParse(Console.ReadLine(), out choice) && (choice == 1 || choice == 2))
                {
                    break;
                }
                Console.WriteLine("Invalid input. Please enter 1 or 2.");
            }

            if (choice == 1)
            {
                Console.WriteLine("Initializing empty database...");
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                Console.WriteLine("Empty database initialized.");
            }
            else if (choice == 2)
            {
                Console.WriteLine("Enter JSON file path:");
                string jsonFilePath = Console.ReadLine();

                if (string.IsNullOrEmpty(jsonFilePath) || !File.Exists(jsonFilePath))
                {
                    Console.WriteLine("Invalid file path. Exiting...");
                    return;
                }

                try
                {
                    var jsonData = File.ReadAllText(jsonFilePath);
                    DatabaseInitializer.InitializeDatabase(context, jsonData);
                    Console.WriteLine("Database loaded from JSON.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load database from JSON: {ex.Message}");
                }
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    // DbContext
                    // services.AddDbContext<TrainDbContext>();
                    services.AddDbContext<TrainDbContext>(options =>
                    options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TrainApp;Trusted_Connection=True;"));

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