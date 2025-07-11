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

public class GetIndustryHandlerTests
{
    private readonly Mock<IIndustryServices> _industryServiceMock;
    private readonly List<Industry> _industryData;

    public GetIndustryHandlerTests()
    {
        _industryServiceMock = new Mock<IIndustryServices>(MockBehavior.Loose);
        _industryData = new List<Industry>();
    }

    [Fact]
    public async Task HandleAsync_WhenIndustriesExist_ShouldReturnOkWithList()
    {
        // Arrange
        var testIndustry = TestIndustry("Microsoft");
        _industryData.Add(testIndustry);

        _industryServiceMock.Setup(s => s.ListIndustriesAsync())
            .ReturnsAsync(_industryData);

        // Act
        var result = await GetIndustryHandler.HandleAsync(_industryServiceMock.Object);

        // Assert
        var ok = Assert.IsType<Ok<GetIndustryResponse>>(result.Result);
        ok.Value!.Industries.Should().ContainSingle(i => i.Name == "Microsoft");

        _industryServiceMock.Verify(s => s.ListIndustriesAsync(), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WhenNoIndustriesExist_ShouldReturnNotFound()
    {
        // Arrange
        _industryServiceMock.Setup(s => s.ListIndustriesAsync())
            .ReturnsAsync(_industryData); // Empty

        // Act
        var result = await GetIndustryHandler.HandleAsync(_industryServiceMock.Object);

        // Assert
        var notFound = Assert.IsType<NotFound<Error>>(result.Result);
        notFound.Value.Should().NotBeNull();
        notFound.Value!.Code.Should().Be("Data.NotFound");

        _industryServiceMock.Verify(s => s.ListIndustriesAsync(), Times.Once);
    }

    private Industry TestIndustry(string name)
    {
        return new Industry(
            Description.FromDatabase("Tech Industry"),
            EntityName.FromDatabase(name),
            true
        )
        {
            WorkInformationInternalId = 1
        };
    }
}
