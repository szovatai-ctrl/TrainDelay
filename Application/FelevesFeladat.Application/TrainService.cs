using FelevesFeladat.Model;
using FelevesFeladat.Persistence;
using System.IO;
using System.Linq;

namespace FelevesFeladat.Application
{
    public class TrainService : ITrainService
    {
        private readonly ITrainRepository _trainRepository;
        private readonly IPassengerRepository _passengerRepository;

        public TrainService(ITrainRepository trainRepository, IPassengerRepository passengerRepository)
        {
            _trainRepository = trainRepository;
            _passengerRepository = passengerRepository;
        }

        public void AddTrain(string name, int id)
        {
            var train = new Train { Name = name, Id = id, PassengerIds = new List<int>() };
            _trainRepository.Add(train);
        }

        public void DeleteTrain(int id)
        {
            _trainRepository.Delete(id);
        }

        public IEnumerable<Train> GetTrainsByTotalDelay()
        {
            var trains = _trainRepository.GetAll();
            return trains.OrderByDescending(t => t.TotalDelay);
        }

        public IEnumerable<Passenger> GetPassengersByTrain(int trainId)
        {
            var train = _trainRepository.GetAll().FirstOrDefault(t => t.Id == trainId);
            if (train == null)
            {
                return Enumerable.Empty<Passenger>();
            }

            return _passengerRepository.GetAll().Where(p => train.PassengerIds.Contains(p.Id));
        }

        public void ExportData(string folderPath)
        {
            var trains = _trainRepository.GetAll();
            var filePath = Path.Combine(folderPath, "trains.txt");

            using (var writer = new StreamWriter(filePath))
            {
                foreach (var train in trains)
                {
                    writer.WriteLine($"Train ID: {train.Id}, Name: {train.Name}, Total Delay: {train.TotalDelay} minutes");
                }
            }
        }
        public IEnumerable<Train> GetTrains()
        {
            return _trainRepository.GetAll();
        }
    }
}