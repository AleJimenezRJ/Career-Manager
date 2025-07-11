using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Tests.Unit.Handlers;

public class GetScholarshipHandlerTests
{
    private readonly Mock<ICareerRepository> _careerRepositoryMock;

    public GetScholarshipHandlerTests()
    {
        _careerRepositoryMock = new Mock<ICareerRepository>(MockBehavior.Loose);
    }

    [Fact]
    public async Task HandleAsync_WhenScholarshipCalculationSucceeds_ShouldReturnOk()
    {
        // Arrange
        var careerName = "Engineering";
        _careerRepositoryMock.Setup(r => r.CalculateScholarshipAsync(careerName))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await GetScholarshipHandler.HandleAsync(_careerRepositoryMock.Object, careerName);

        // Assert
        var ok = Assert.IsType<Ok<PostResponse>>(result.Result);
        ok.Value!.Response.Should().Be("Successfully calculated the scholarship.");

        _careerRepositoryMock.Verify(r => r.CalculateScholarshipAsync(careerName), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WhenScholarshipCalculationFails_ShouldReturnConflict()
    {
        // Arrange
        var careerName = "Architecture";
        _careerRepositoryMock.Setup(r => r.CalculateScholarshipAsync(careerName))
            .ReturnsAsync(Result.Failure(Error.Failure("SomeCode", "Some message")));

        // Act
        var result = await GetScholarshipHandler.HandleAsync(_careerRepositoryMock.Object, careerName);

        // Assert
        var conflict = Assert.IsType<Conflict<Error>>(result.Result);
        conflict.Value!.Code.Should().Be("Career.Calculus error");
        conflict.Value.Message.Should().Contain("unknown error");

        _careerRepositoryMock.Verify(r => r.CalculateScholarshipAsync(careerName), Times.Once);
    }
}
