
using FelevesFeladat.Model;

namespace FelevesFeladat.Persistence
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly TrainDbContext _context;

        public PassengerRepository(TrainDbContext context)
        {
            _context = context;
        }

        public void Add(Passenger passenger)
        {
            _context.Passengers.Add(passenger);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var passenger = _context.Passengers.FirstOrDefault(p => p.Id == id);
            if (passenger != null)
            {
                _context.Passengers.Remove(passenger);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Passenger> GetAll()
        {
            return _context.Passengers.ToList();
        }
    }
}