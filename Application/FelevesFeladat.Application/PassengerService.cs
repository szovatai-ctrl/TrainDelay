
using FelevesFeladat.Model;
using FelevesFeladat.Persistence;

namespace FelevesFeladat.Application
{
    public class PassengerService : IPassengerService
    {
        private readonly IPassengerRepository _passengerRepository;

        public PassengerService(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        public void AddPassenger(string name, int id)
        {
            var passenger = new Passenger { Id = id, Name = name };
            _passengerRepository.Add(passenger);
        }

        public void DeletePassenger(int id)
        {
            _passengerRepository.Delete(id);
        }

        public IEnumerable<Passenger> GetAllPassengers()
        {
            return _passengerRepository.GetAll();
        }
        public Passenger GetUnluckiestPassenger()
        {
            var passengers = _passengerRepository.GetAll();
            return passengers.OrderByDescending(p => p.TotalDelay).FirstOrDefault();
        }

        public double GetAverageDelayPerPassenger()
        {
            var passengers = _passengerRepository.GetAll();
            return passengers.Any() ? passengers.Average(p => p.TotalDelay) : 0;
        }
    }
}