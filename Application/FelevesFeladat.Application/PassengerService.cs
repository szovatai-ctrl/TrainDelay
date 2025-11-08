

using FelevesFeladat.Model;

namespace FelevesFeladat.Application
{
    public class PassengerService
    {
        private readonly List<Passenger> _passengers = new();

        public void AddPassenger(string name, int id)
        {
            _passengers.Add(new Passenger { Id = id, Name = name });
        }

        public void DeletePassenger(int id)
        {
            var passenger = _passengers.FirstOrDefault(p => p.Id == id);
            if (passenger != null)
            {
                _passengers.Remove(passenger);
            }
        }

        public List<Passenger> GetAllPassengers()
        {
            return _passengers;
        }

        public Passenger? GetById(int id)
        {
            return _passengers.FirstOrDefault(p => p.Id == id);
        }

        public Passenger? GetUnluckiestPassenger()
        {
            return _passengers.OrderByDescending(p => p.TotalDelay).FirstOrDefault();
        }

        public double GetAverageDelayPerPassenger()
        {
            if (_passengers.Count == 0) return 0;
            return _passengers.Average(p => p.TotalDelay);
        }
    }
}