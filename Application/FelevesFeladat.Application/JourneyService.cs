
using FelevesFeladat.Model;
using FelevesFeladat.Persistence;

namespace FelevesFeladat.Application
{
    public class JourneyService : IJourneyService
    {
        private readonly IJourneyRepository _journeyRepository;
        private readonly ITrainRepository _trainRepository;
        private readonly IPassengerRepository _passengerRepository;
        public JourneyService(IJourneyRepository journeyRepository, ITrainRepository trainRepository, IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        
            _journeyRepository = journeyRepository;
            _trainRepository = trainRepository;
        }

        public void Start(int trainId, string origin, string destination)
        {
            var train = _trainRepository.GetAll().FirstOrDefault(t => t.Id == trainId);
            if (train == null)
            {
                throw new InvalidOperationException("Train not found.");
            }

            var journey = new Journey
            {
                Id = Guid.NewGuid().GetHashCode(), // Generate unique ID
                Origin = origin,
                Destination = destination,
                TrainId = trainId,
                PassengerIds = train.PassengerIds.ToList(),
                Delay = new Random().Next(0, 241) // Random delay between 0 and 240 minutes
            };
            foreach (var passengerId in journey.PassengerIds)
            {
                var passenger = _passengerRepository.GetAll().FirstOrDefault(p => p.Id == passengerId);
                if (passenger != null)
                {
                    passenger.TotalDelay += journey.Delay;
                }
            }
            // Update the train's total delay
            train.TotalDelay += journey.Delay;

            _journeyRepository.Add(journey);
        }

        public void EndJourney(int trainId)
        {
            var train = _trainRepository.GetAll().FirstOrDefault(t => t.Id == trainId);
            if (train == null)
            {
                throw new InvalidOperationException("Train not found.");
            }

            // Clear passengers from the train
            train.PassengerIds.Clear();
        }

        public IEnumerable<Journey> GetAllJourneys()
        {
            return _journeyRepository.GetAll();
        }
    }
}