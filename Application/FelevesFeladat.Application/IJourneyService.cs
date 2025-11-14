
using FelevesFeladat.Model;

namespace FelevesFeladat.Application
{
    public interface IJourneyService
    {
        void Start(int trainId, string origin, string destination);
        void EndJourney(int trainId);
        IEnumerable<Journey> GetAllJourneys();
    }
}