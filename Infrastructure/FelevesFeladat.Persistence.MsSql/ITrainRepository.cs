
using FelevesFeladat.Model;

namespace FelevesFeladat.Persistence
{
    public interface ITrainRepository
    {
        void Add(Train train);
        void Delete(int id);
        IEnumerable<Train> GetAll();
    }
}