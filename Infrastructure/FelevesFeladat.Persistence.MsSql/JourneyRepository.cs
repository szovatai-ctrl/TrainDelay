
using FelevesFeladat.Model;

namespace FelevesFeladat.Persistence
{
    public class JourneyRepository : IJourneyRepository
    {
        private readonly List<Journey> _journeys = new(); // memóriában tároljuk

        public void Add(Journey journey)
        {
            // Ha nincs ID, adjunk neki egyet (1-tõl indul)
            if (journey.Id == 0)
            {
                journey.Id = _journeys.Count > 0 ? _journeys.Max(j => j.Id) + 1 : 1;
            }
            _journeys.Add(journey);
        }

        public void Delete(int id)
        {
            var journey = _journeys.FirstOrDefault(j => j.Id == id);
            if (journey != null)
            {
                _journeys.Remove(journey);
            }
        }

        public IEnumerable<Journey> GetAll()
        {
            return _journeys;
        }

        public void Update(Journey journey)
        {
            var existingJourney = _journeys.FirstOrDefault(j => j.Id == journey.Id);
            if (existingJourney != null)
            {
                existingJourney.TrainId = journey.TrainId;
                existingJourney.Origin = journey.Origin;
                existingJourney.Destination = journey.Destination;
                existingJourney.PassengerIds = journey.PassengerIds;
                existingJourney.Delay = journey.Delay;
            }
        }

        public Journey? GetById(int id)
        {
            return _journeys.FirstOrDefault(j => j.Id == id);
        }
    }
}