using FelevesFeladat.Model;
using System.IO;
using System.Linq;

namespace FelevesFeladat.Application
{
    public class TrainService
    {
        private readonly List<Train> _trains = new();

        public void AddTrain(string name, int id)
        {
            _trains.Add(new Train { Id = id, Name = name });
        }

        public void DeleteTrain(int id)
        {
            var train = _trains.FirstOrDefault(t => t.Id == id);
            if (train != null) _trains.Remove(train);
        }

        public IEnumerable<Train> GetTrainsByTotalDelay()
        {
            return _trains.OrderByDescending(t => t.TotalDelay);
        }

        public IEnumerable<Passenger> GetPassengersByTrain(int trainId, List<Passenger> allPassengers)
        {
            var train = _trains.FirstOrDefault(t => t.Id == trainId);
            if (train == null) return new List<Passenger>();

            return allPassengers.Where(p => train.PassengerIds.Contains(p.Id)).ToList();
        }

        public void ExportData(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, "trains.txt");

            using (var writer = new StreamWriter(filePath))
            {
                foreach (var train in _trains)
                {
                    writer.WriteLine($"Train ID: {train.Id}, Name: {train.Name}, Total Delay: {train.TotalDelay} minutes");
                }
            }
        }

        public List<Train> GetTrains()
        {
            return _trains;
        }
        public Train? GetById(int id)
        {
            return _trains.FirstOrDefault(t => t.Id == id);
        }
    }
}