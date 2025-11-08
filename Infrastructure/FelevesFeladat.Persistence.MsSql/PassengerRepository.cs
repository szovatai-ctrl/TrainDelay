
using FelevesFeladat.Model;

namespace FelevesFeladat.Persistence
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly List<Passenger> _passengers = new();

        public void Add(Passenger passenger)
        {
            if (passenger.Id == 0)
            {
                passenger.Id = _passengers.Count > 0 ? _passengers.Max(p => p.Id) + 1 : 1;
            }
            _passengers.Add(passenger);
        }

        public void Delete(int id)
        {
            var passenger = _passengers.FirstOrDefault(p => p.Id == id);
            if (passenger != null)
            {
                _passengers.Remove(passenger);
            }
        }

        public IEnumerable<Passenger> GetAll()
        {
            return _passengers;
        }

        public Passenger? GetById(int id)
        {
            return _passengers.FirstOrDefault(p => p.Id == id);
        }
    }
}