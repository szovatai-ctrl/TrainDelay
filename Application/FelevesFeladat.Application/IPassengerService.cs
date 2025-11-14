
using FelevesFeladat.Model;

namespace FelevesFeladat.Application
{
    public interface IPassengerService
    {
        void AddPassenger(string name, int id);
        void DeletePassenger(int id);
        IEnumerable<Passenger> GetAllPassengers();
        Passenger GetUnluckiestPassenger();
        double GetAverageDelayPerPassenger();
    }
}