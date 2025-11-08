

using FelevesFeladat.Model;

namespace FelevesFeladat.Application
{
    public class JourneyService
    {
        private readonly List<Journey> _journeys = new();
        private readonly List<Train> _trains;
        private readonly List<Passenger> _passengers;

        public JourneyService()
        {
            _trains = new List<Train>();
            _passengers = new List<Passenger>();
        }

        public void SetTrains(List<Train> trains)
        {
            _trains.Clear();
            _trains.AddRange(trains);
        }

        public void SetPassengers(List<Passenger> passengers)
        {
            _passengers.Clear();
            _passengers.AddRange(passengers);
        }

        public void Start(int trainId, string origin, string destination)
        {
            var train = _trains.FirstOrDefault(t => t.Id == trainId);
            if (train == null)
                throw new InvalidOperationException("Train not found.");

            var journey = new Journey
            {
                Id = 1,  // Guid.NewGuid().GetHashCode(), // Unique ID
                Origin = origin,
                Destination = destination,
                TrainId = trainId,
                PassengerIds = train.PassengerIds.ToList(),
                Delay = new Random().Next(0, 241) // random delay 0-240
            }; 

            foreach (var passengerId in journey.PassengerIds)
            {
                var passenger = _passengers.FirstOrDefault(p => p.Id == passengerId);
                if (passenger != null)
                    passenger.TotalDelay += journey.Delay;
            }

            train.TotalDelay += journey.Delay;

            _journeys.Add(journey);
        }

        public void EndJourney(int trainId)
        {
            var train = _trains.FirstOrDefault(t => t.Id == trainId);
            if (train == null)
                throw new InvalidOperationException("Train not found.");

            train.PassengerIds.Clear();
        }

        public IEnumerable<Journey> GetAllJourneys()
        {
            return _journeys;
        }
    }
}