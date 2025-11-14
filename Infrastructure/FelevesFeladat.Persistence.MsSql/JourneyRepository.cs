
using FelevesFeladat.Model;

namespace FelevesFeladat.Persistence
{
    public class JourneyRepository : IJourneyRepository
    {
        private readonly TrainDbContext _context;

        public JourneyRepository(TrainDbContext context)
        {
            _context = context;
        }

        public void Add(Journey journey)
        {
            _context.Journeys.Add(journey);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var journey = _context.Journeys.FirstOrDefault(j => j.Id == id);
            if (journey != null)
            {
                _context.Journeys.Remove(journey);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Journey> GetAll()
        {
            return _context.Journeys.ToList();
        }

        public void Update(Journey journey)
        {
            var existingJourney = _context.Journeys.FirstOrDefault(j => j.Id == journey.Id);

        }
    }
}