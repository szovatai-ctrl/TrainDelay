
using FelevesFeladat.Model;

namespace FelevesFeladat.Persistence
{
    public interface IJourneyRepository
    {
        void Add(Journey journey);
        void Delete(int id);
        IEnumerable<Journey> GetAll();
        void Update(Journey journey);
    }
}