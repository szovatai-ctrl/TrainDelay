using FelevesFeladat.Model;

namespace FelevesFeladat.Application
{
    public interface ITrainService
    {
        void AddTrain(string name, int id);
        void DeleteTrain(int id);
        IEnumerable<Train> GetTrains();

        IEnumerable<Passenger> GetPassengersByTrain(int trainId);
        void ExportData(string folderPath);
        public IEnumerable<Train> GetTrainsByTotalDelay();
    }
}