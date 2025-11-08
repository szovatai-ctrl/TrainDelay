using FelevesFeladat.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FelevesFeladat.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var trainService = new TrainService();
            var passengerService = new PassengerService();
            var journeyService = new JourneyService();

            var app = new Application(trainService, passengerService, journeyService);
            app.Run();
        }
    }
}