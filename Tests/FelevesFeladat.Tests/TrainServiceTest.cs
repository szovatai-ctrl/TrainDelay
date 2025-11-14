using Xunit;
using Moq;
using FelevesFeladat.Application;
using FelevesFeladat.Model;
using FelevesFeladat.Persistence;

public class TrainServiceTest
{
    [Fact]
    public void AddTrain_ShouldAddTrainToRepository()
    {
        // Arrange  
        var mockRepo = new Mock<ITrainRepository>();
        var mockPassengerRepo = new Mock<IPassengerRepository>();
        var service = new TrainService(mockRepo.Object, mockPassengerRepo.Object);

        // Act  
        service.AddTrain("Express", 1);

        // Assert  
        mockRepo.Verify(r => r.Add(It.Is<Train>(t => t.Name == "Express" && t.Id == 1)), Times.Once);
    }

    [Fact]
    public void DeleteTrain_ShouldRemoveTrainFromRepository()
    {
        // Arrange  
        var mockRepo = new Mock<ITrainRepository>();
        var mockPassengerRepo = new Mock<IPassengerRepository>();
        var service = new TrainService(mockRepo.Object, mockPassengerRepo.Object);

        // Act  
        service.DeleteTrain(1);

        // Assert  
        mockRepo.Verify(r => r.Delete(1), Times.Once);
    }

    [Fact]
    public void GetTrainsByTotalDelay_ShouldReturnTrainsSortedByDelay()
    {
        // Arrange  
        var mockRepo = new Mock<ITrainRepository>();
        var mockPassengerRepo = new Mock<IPassengerRepository>();
        mockRepo.Setup(r => r.GetAll()).Returns(new List<Train>
       {
           new Train { Id = 1, Name = "Express", PassengerIds = new List<int>(), TotalDelay = 30 },
           new Train { Id = 2, Name = "Regional", PassengerIds = new List<int>(), TotalDelay = 10 }
       });
        var service = new TrainService(mockRepo.Object, mockPassengerRepo.Object);

        // Act  
        var result = service.GetTrainsByTotalDelay();

        // Assert  
        Assert.Equal(2, result.Count());
        Assert.Equal(30, result.First().TotalDelay);
    }
}
