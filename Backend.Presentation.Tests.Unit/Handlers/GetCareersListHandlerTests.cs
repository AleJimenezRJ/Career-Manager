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

public class GetCareersListHandlerTests
{
    private readonly Mock<ICareerRepository> _careerRepositoryMock;
    private readonly List<Career> _careerData;

    public GetCareersListHandlerTests()
    {
        _careerRepositoryMock = new Mock<ICareerRepository>(MockBehavior.Loose);
        _careerData = new List<Career>();
    }

    [Fact]
    public async Task HandleAsync_WhenCareersExist_ShouldReturnOkWithCareerList()
    {
        // Arrange
        var testCareer = TestCareer("Engineering");
        _careerData.Add(testCareer);

        _careerRepositoryMock.Setup(r => r.ListCareersAsync())
            .ReturnsAsync(_careerData);

        // Act
        var result = await GetCareersListHandler.HandleAsync(_careerRepositoryMock.Object);

        // Assert
        var ok = Assert.IsType<Ok<GetCareerListResponse>>(result.Result);
        ok.Value!.Career.Should().ContainSingle(c => c.Name == "Engineering");

        _careerRepositoryMock.Verify(r => r.ListCareersAsync(), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WhenNoCareersExist_ShouldReturnNotFound()
    {
        // Arrange
        _careerRepositoryMock.Setup(r => r.ListCareersAsync())
            .ReturnsAsync(_careerData); // Empty list

        // Act
        var result = await GetCareersListHandler.HandleAsync(_careerRepositoryMock.Object);

        // Assert
        var notFound = Assert.IsType<NotFound<Error>>(result.Result);
        notFound.Value.Should().NotBeNull();
        notFound.Value!.Code.Should().Be("Data.NotFound");

        _careerRepositoryMock.Verify(r => r.ListCareersAsync(), Times.Once);
    }

    private static Career TestCareer(string name)
    {
        return new Career(
            1,
            EntityName.FromDatabase(name),
            Description.FromDatabase("Test description"),
            SemestersNumber.FromDatabase(8),
            Modality.FromDatabase("Presential"),
            DegreeTitle.FromDatabase("Bachelor"),
            new List<WorkInformation>(),
            1500m,
            true
        );
    }
}
    