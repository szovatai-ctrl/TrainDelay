using FelevesFeladat.Application;

namespace FelevesFeladat.ConsoleApp
{
    public class Application
    {
        private readonly ITrainService _trainService;
        private readonly IPassengerService _passengerService;
        private readonly IJourneyService _journeyService;

        public Application(ITrainService trainService, IPassengerService passengerService, IJourneyService journeyService)
        {
            _trainService = trainService;
            _passengerService = passengerService;
            _journeyService = journeyService;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\nOptions:");
                Console.WriteLine("1. Add Train");
                Console.WriteLine("2. Add Passenger");
                Console.WriteLine("3. Delete Train");
                Console.WriteLine("4. Delete Passenger");
                Console.WriteLine("5. Start Journey");
                Console.WriteLine("6. End Journey");
                Console.WriteLine("7. List Trains by Total Delay");
                Console.WriteLine("8. Find Unluckiest Passenger");
                Console.WriteLine("9. Average Delay per Passenger");
                Console.WriteLine("10. List Train Passengers");
                Console.WriteLine("11. Export Data to TXT");
                Console.WriteLine("12. Exit");

                Console.WriteLine("Enter your choice:");
                int choice = int.Parse(Console.ReadLine() ?? "12");

                switch (choice)
                {
                    case 1:
                        AddTrain();
                        break;
                    case 2:
                        AddPassenger();
                        break;
                    case 3:
                        DeleteTrain();
                        break;
                    case 4:
                        DeletePassenger();
                        break;
                    case 5:
                        Start();
                        break;
                    case 6:
                        EndJourney();
                        break;
                    case 7:
                        ListTrainsByTotalDelay();
                        break;
                    case 8:
                        FindUnluckiestPassenger();
                        break;
                    case 9:
                        AverageDelayPerPassenger();
                        break;
                    case 10:
                        ListTrainPassengers();
                        break;
                    case 11:
                        ExportDataToTxt();
                        break;
                    case 12:
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void AddTrain()
        {
            Console.WriteLine("Enter Train Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Train ID:");
            int id = int.Parse(Console.ReadLine() ?? "0");
            _trainService.AddTrain(name, id);
            Console.WriteLine("Train added.");
        }

        private void AddPassenger()
        {
            Console.WriteLine("Enter Passenger Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Passenger ID:");
            int id = int.Parse(Console.ReadLine() ?? "0");
            _passengerService.AddPassenger(name, id);
            Console.WriteLine("Passenger added.");
        }

        private void DeleteTrain()
        {
            Console.WriteLine("Enter Train ID to delete:");
            int id = int.Parse(Console.ReadLine() ?? "0");
            _trainService.DeleteTrain(id);
            Console.WriteLine("Train deleted.");
        }

        private void DeletePassenger()
        {
            Console.WriteLine("Enter Passenger ID to delete:");
            int id = int.Parse(Console.ReadLine() ?? "0");
            _passengerService.DeletePassenger(id);
            Console.WriteLine("Passenger deleted.");
        }

        private void Start()
        {
            Console.WriteLine("Enter Train ID:");
            int trainId = int.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine("Enter Origin:");
            string origin = Console.ReadLine();
            Console.WriteLine("Enter Destination:");
            string destination = Console.ReadLine();
            _journeyService.Start(trainId, origin, destination);
            Console.WriteLine("Journey started.");
        }

        private void EndJourney()
        {
            Console.WriteLine("Enter Train ID:");
            int trainId = int.Parse(Console.ReadLine() ?? "0");
            _journeyService.EndJourney(trainId);
            Console.WriteLine("Journey ended.");
        }

        private void ListTrainsByTotalDelay()
        {
            var trains = _trainService.GetTrainsByTotalDelay();
            foreach (var train in trains)
            {
                Console.WriteLine($"Train ID: {train.Id}, Total Delay: {train.TotalDelay} minutes");
            }
        }

        private void FindUnluckiestPassenger()
        {
            var passenger = _passengerService.GetUnluckiestPassenger();
            Console.WriteLine($"Unluckiest Passenger: {passenger.Name}, Total Delay: {passenger.TotalDelay} minutes");
        }

        private void AverageDelayPerPassenger()
        {
            var avgDelay = _passengerService.GetAverageDelayPerPassenger();
            Console.WriteLine($"Average delay per passenger: {avgDelay:F2} minutes");
        }

        private void ListTrainPassengers()
        {
            Console.WriteLine("Enter Train ID:");
            int trainId = int.Parse(Console.ReadLine() ?? "0");
            var passengers = _trainService.GetPassengersByTrain(trainId);
            foreach (var passenger in passengers)
            {
                Console.WriteLine($"Passenger ID: {passenger.Id}, Name: {passenger.Name}");
            }
        }

        private void ExportDataToTxt()
        {
            Console.WriteLine("Enter export folder path:");
            string folderPath = Console.ReadLine();
            _trainService.ExportData(folderPath);
            Console.WriteLine("Data exported to TXT files.");
        }
    }
}