
using FelevesFeladat.Model;

namespace FelevesFeladat.Persistence
{
    public class TrainRepository : ITrainRepository
    {
        private readonly TrainDbContext _context;

        public TrainRepository(TrainDbContext context)
        {
            _context = context;
        }

        public void Add(Train train)
        {
            _context.Trains.Add(train);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var train = _context.Trains.FirstOrDefault(t => t.Id == id);
            if (train != null)
            {
                _context.Trains.Remove(train);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Train> GetAll()
        {
            return _context.Trains.ToList();
        }
    }
}