using System.Text.Json;
using FelevesFeladat.Model;

namespace FelevesFeladat.Persistence.MsSql
{
    public static class DatabaseInitializer
    {
        public static void InitializeDatabase(TrainDbContext context, string jsonData)
        {
            if (context.Database.EnsureCreated())
            {
                Console.WriteLine("Database created.");
            }

            var data = JsonSerializer.Deserialize<JsonData>(jsonData);

            foreach (var train in data.Trains)
            {
                context.Trains.Add(new Train
                {
                    Id = train.Id,
                    Name = train.Name,
                    PassengerIds = train.PassengerIds.ToList()
                });
            }

            foreach (var passenger in data.Passengers)
            {
                context.Passengers.Add(new Passenger
                {
                    Id = passenger.Id,
                    Name = passenger.Name
                });
            }

            foreach (var journey in data.Journeys)
            {
                context.Journeys.Add(new Journey
                {
                    Id = journey.Id,
                    Origin = journey.Origin,
                    Destination = journey.Destination,
                    TrainId = journey.TrainId,
                    PassengerIds = journey.PassengerIds.ToList(),
                    Delay = journey.Delay
                });
            }

            context.SaveChanges();
        }

        private class JsonData
        {
            public List<Train> Trains { get; set; }
            public List<Passenger> Passengers { get; set; }
            public List<Journey> Journeys { get; set; }
        }

    }
}
