using Xunit;
using Moq;
using FelevesFeladat.Application;
using FelevesFeladat.Model;
using FelevesFeladat.Persistence;

public class JourneyServiceTest
{
    [Fact]
    public void Start_ShouldAddJourneyToRepository()
    {
        // Arrange  
        var mockJourneyRepo = new Mock<IJourneyRepository>();
        var mockTrainRepo = new Mock<ITrainRepository>();
        var mockPassengerRepo = new Mock<IPassengerRepository>();
        var service = new JourneyService(mockJourneyRepo.Object, mockTrainRepo.Object, mockPassengerRepo.Object);

        // Act  
        service.Start(1, "Budapest", "Vienna");

        // Assert  
        mockJourneyRepo.Verify(r => r.Add(It.Is<Journey>(j => j.TrainId == 1 && j.Origin == "Budapest" && j.Destination == "Vienna")), Times.Once);
    }

    [Fact]
    public void EndJourney_ShouldRemovePassengersFromTrain()
    {
        // Arrange  
        var mockJourneyRepo = new Mock<IJourneyRepository>();
        var mockTrainRepo = new Mock<ITrainRepository>();
        var mockPassengerRepo = new Mock<IPassengerRepository>();
        var service = new JourneyService(mockJourneyRepo.Object, mockTrainRepo.Object, mockPassengerRepo.Object);

        // Act  
        service.EndJourney(1);

        // Assert  
        mockJourneyRepo.Verify(r => r.Update(It.IsAny<Journey>()), Times.Once);
    }

    [Fact]
    public void Start_ShouldGenerateValidDelay()
    {
        // Arrange  
        var mockJourneyRepo = new Mock<IJourneyRepository>();
        var mockTrainRepo = new Mock<ITrainRepository>();
        var mockPassengerRepo = new Mock<IPassengerRepository>();
        var service = new JourneyService(mockJourneyRepo.Object, mockTrainRepo.Object, mockPassengerRepo.Object);

        // Act  
        service.Start(1, "Budapest", "Vienna");

        // Assert  
        mockJourneyRepo.Verify(r => r.Add(It.Is<Journey>(j => j.Delay >= 0 && j.Delay <= 240)), Times.Once);
    }

    [Fact]
    public void GetAllJourneys_ShouldReturnAllJourneys()
    {
        // Arrange  
        var mockJourneyRepo = new Mock<IJourneyRepository>();
        var mockTrainRepo = new Mock<ITrainRepository>();
        var mockPassengerRepo = new Mock<IPassengerRepository>();
        mockJourneyRepo.Setup(r => r.GetAll()).Returns(new List<Journey>
       {
           new Journey { Id = 1, Origin = "Budapest", Destination = "Vienna" },
           new Journey { Id = 2, Origin = "Berlin", Destination = "Prague" }
       });
        var service = new JourneyService(mockJourneyRepo.Object, mockTrainRepo.Object, mockPassengerRepo.Object);

        // Act  
        var result = service.GetAllJourneys();

        // Assert  
        Assert.Equal(2, result.Count());
    }
}
