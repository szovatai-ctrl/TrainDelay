
using FelevesFeladat.Model;

namespace FelevesFeladat.Persistence
{
    public interface IPassengerRepository
    {
        void Add(Passenger passenger);
        void Delete(int id);
        IEnumerable<Passenger> GetAll();
    }
}