using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Tests.Unit.Handlers;

public class GetCareerByNameHandlerTests
{
    private readonly Mock<ICareerRepository> _careerRepositoryMock;

    public GetCareerByNameHandlerTests()
    {
        _careerRepositoryMock = new Mock<ICareerRepository>(MockBehavior.Strict);
    }

    [Fact]
    public async Task HandleAsync_WhenCareerExists_ShouldReturnOk()
    {
        // Arrange

        var careerName = "Engineering";
        IEnumerable<WorkInformation> workInformation = new List<WorkInformation> { };
        var career = new Career(
            1,
            EntityName.FromDatabase(careerName),
            Description.FromDatabase("Test description"),
            SemestersNumber.FromDatabase(8),
            Modality.FromDatabase("Presential"),
            DegreeTitle.FromDatabase("Bachelor"),
            workInformation,
            1500m,
            true
        );

        _careerRepositoryMock.Setup(r => r.CalculateScholarshipAsync(careerName))
            .Returns(Task.FromResult(Result.Success()));


        _careerRepositoryMock.Setup(x => x.ListSpecificCareerAsync(careerName))
            .ReturnsAsync(career);

        // Act
        var result = await GetCareerByNameHandler.HandleAsync(_careerRepositoryMock.Object, careerName);

        // Assert
        var ok = Assert.IsType<Ok<GetCareerResponse>>(result.Result);
        ok.Value!.Career.Name.Should().Be(careerName);

        _careerRepositoryMock.Verify(x => x.CalculateScholarshipAsync(careerName), Times.Once);
        _careerRepositoryMock.Verify(x => x.ListSpecificCareerAsync(careerName), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WhenCareerNotFound_ShouldReturnNotFound()
    {
        // Arrange
        var careerName = "UnknownCareer";

        _careerRepositoryMock.Setup(r => r.CalculateScholarshipAsync(careerName))
            .Returns(Task.FromResult(Result.Success()));


        _careerRepositoryMock.Setup(x => x.ListSpecificCareerAsync(careerName))
            .ReturnsAsync((Career?)null);

        // Act
        var result = await GetCareerByNameHandler.HandleAsync(_careerRepositoryMock.Object, careerName);

        // Assert
        var notFound = Assert.IsType<NotFound<Error>>(result.Result);
        notFound.Value.Should().NotBeNull();
        notFound.Value!.Code.Should().Be("Entity.NotFound");

        _careerRepositoryMock.Verify(x => x.CalculateScholarshipAsync(careerName), Times.Once);
        _careerRepositoryMock.Verify(x => x.ListSpecificCareerAsync(careerName), Times.Once);
    }
}
