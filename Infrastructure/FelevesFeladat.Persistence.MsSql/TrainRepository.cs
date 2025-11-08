
using FelevesFeladat.Model;

namespace FelevesFeladat.Persistence
{
    public class TrainRepository : ITrainRepository
    {
        private readonly List<Train> _trains = new();

        public void Add(Train train)
        {
            if (train.Id == 0)
            {
                train.Id = _trains.Count > 0 ? _trains.Max(t => t.Id) + 1 : 1;
            }
            _trains.Add(train);
        }

        public void Delete(int id)
        {
            var train = _trains.FirstOrDefault(t => t.Id == id);
            if (train != null)
            {
                _trains.Remove(train);
            }
        }

        public IEnumerable<Train> GetAll()
        {
            return _trains;
        }

        public Train? GetById(int id)
        {
            return _trains.FirstOrDefault(t => t.Id == id);
        }
    }
}