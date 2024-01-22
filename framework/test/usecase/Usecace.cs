using Moq;
using NUnit.Framework;
using simple_rest.domain.query;
using simple_rest.domain.models;
using simple_rest.usecase;


namespace simple_rest.framework.test.usecase;

[TestFixture]
public class MyServiceTests
{
    [Test]
    public void ProcessData_Should_ReturnDataFromDataService()
    {
        // var mockQuery = new Mock<IProjectQuery>();
        // mockQuery.Setup(d => d.GetAllData()).Returns(
        //     new List<Project>{
        //         new() {Id = 1},
        //         new() {Id = 2},
        //         new() {Id = 3},
        //     }
        // );

        // var usecase = new UsecaseTest(mockQuery);
        // var result = usecase.GetAllData();
        // Assert.Equals(3, result.Count());
    }
}