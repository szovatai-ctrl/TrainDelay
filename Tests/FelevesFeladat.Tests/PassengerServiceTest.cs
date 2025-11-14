using Xunit;
using Moq;
using FelevesFeladat.Application;
using FelevesFeladat.Model;
using FelevesFeladat.Persistence;

public class PassengerServiceTest
{
    [Fact]
    public void AddPassenger_ShouldAddPassengerToRepository()
    {
        // Arrange
        var mockRepo = new Mock<IPassengerRepository>();
        var service = new PassengerService(mockRepo.Object);

        // Act
        service.AddPassenger("Alice", 1);

        // Assert
        mockRepo.Verify(r => r.Add(It.Is<Passenger>(p => p.Name == "Alice" && p.Id == 1)), Times.Once);
    }

    [Fact]
    public void DeletePassenger_ShouldRemovePassengerFromRepository()
    {
        // Arrange
        var mockRepo = new Mock<IPassengerRepository>();
        var service = new PassengerService(mockRepo.Object);

        // Act
        service.DeletePassenger(1);

        // Assert
        mockRepo.Verify(r => r.Delete(1), Times.Once);
    }

    [Fact]
    public void GetUnluckiestPassenger_ShouldReturnPassengerWithMostDelays()
    {
        // Arrange
        var mockRepo = new Mock<IPassengerRepository>();
        mockRepo.Setup(r => r.GetAll()).Returns(new List<Passenger>
        {
            new Passenger { Id = 1, Name = "Alice", TotalDelay = 50 },
            new Passenger { Id = 2, Name = "Bob", TotalDelay = 30 }
        });
        var service = new PassengerService(mockRepo.Object);

        // Act
        var result = service.GetUnluckiestPassenger();

        // Assert
        Assert.Equal("Alice", result.Name);
    }

    [Fact]
    public void GetAverageDelayPerPassenger_ShouldReturnCorrectAverage()
    {
        // Arrange
        var mockRepo = new Mock<IPassengerRepository>();
        mockRepo.Setup(r => r.GetAll()).Returns(new List<Passenger>
        {
            new Passenger { Id = 1, Name = "Alice", TotalDelay = 50 },
            new Passenger { Id = 2, Name = "Bob", TotalDelay = 30 }
        });
        var service = new PassengerService(mockRepo.Object);

        // Act
        var result = service.GetAverageDelayPerPassenger();

        // Assert
        Assert.Equal(40, result);
    }
}