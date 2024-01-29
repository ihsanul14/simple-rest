using Moq;
using rest.Usecase;
using rest.Domain.Query;
using rest.Domain.Models;

namespace tests.usecase; 

public class UsecaseTests
{
    [Test]
    public void GetAllData()
    {
        var mockQuery = new Mock<Query>();
        var testData = new List<Project> { new(), new () };
        mockQuery.Setup(query => query.GetAllData()).Returns(testData);

        var usecase = new ProjectUsecase(mockQuery.Object);
        var result = usecase.GetAllData();
        
        Assert.That(result, Is.EqualTo(testData));
    }
}