using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Tests.Unit.Handlers;

public class GetRecruitmentHandlerTests
{
    private readonly Mock<IRecruitmentServices> _recruitmentServiceMock;
    private readonly List<Recruitment> _recruitmentData;

    public GetRecruitmentHandlerTests()
    {
        _recruitmentServiceMock = new Mock<IRecruitmentServices>(MockBehavior.Loose);
        _recruitmentData = new List<Recruitment>();
    }

    [Fact]
    public async Task HandleAsync_WhenRecruitmentsExist_ShouldReturnOkWithList()
    {
        // Arrange
        var testRecruitment = TestRecruitment("Internship Opportunity");
        _recruitmentData.Add(testRecruitment);

        _recruitmentServiceMock.Setup(s => s.ListRecruitmentsAsync())
            .ReturnsAsync(_recruitmentData);

        // Act
        var result = await GetRecruitmentHandler.HandleAsync(_recruitmentServiceMock.Object);

        // Assert
        var ok = Assert.IsType<Ok<GetRecruitmentResponse>>(result.Result);
        ok.Value!.Recruitments.Should().ContainSingle(r => r.InformationDescription == "Internship Opportunity");

        _recruitmentServiceMock.Verify(s => s.ListRecruitmentsAsync(), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WhenNoRecruitmentsExist_ShouldReturnNotFound()
    {
        // Arrange
        _recruitmentServiceMock.Setup(s => s.ListRecruitmentsAsync())
            .ReturnsAsync(_recruitmentData); // Empty list

        // Act
        var result = await GetRecruitmentHandler.HandleAsync(_recruitmentServiceMock.Object);

        // Assert
        var notFound = Assert.IsType<NotFound<Error>>(result.Result);
        notFound.Value.Should().NotBeNull();
        notFound.Value!.Code.Should().Be("Data.NotFound");

        _recruitmentServiceMock.Verify(s => s.ListRecruitmentsAsync(), Times.Once);
    }

    private Recruitment TestRecruitment(string description)
    {
        return new Recruitment(
            Description.FromDatabase(description),
            Description.FromDatabase("Steps to apply"),
            Description.FromDatabase("Requirements"),
            new List<Language>()
        )
        {
            WorkInformationInternalId = 1
        };
    }
}
