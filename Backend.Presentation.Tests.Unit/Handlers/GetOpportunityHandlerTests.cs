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

public class GetOpportunityHandlerTests
{
    private readonly Mock<IOpportunityServices> _opportunityServiceMock;
    private readonly List<Opportunity> _opportunityData;

    public GetOpportunityHandlerTests()
    {
        _opportunityServiceMock = new Mock<IOpportunityServices>(MockBehavior.Loose);
        _opportunityData = new List<Opportunity>();
    }

    [Fact]
    public async Task HandleAsync_WhenOpportunitiesExist_ShouldReturnOkWithList()
    {
        // Arrange
        var testOpportunity = TestOpportunity("Frontend Internship");
        _opportunityData.Add(testOpportunity);

        _opportunityServiceMock.Setup(s => s.ListOpportunitiesAsync())
            .ReturnsAsync(_opportunityData);

        // Act
        var result = await GetOpportunityHandler.HandleAsync(_opportunityServiceMock.Object);

        // Assert
        var ok = Assert.IsType<Ok<GetOpportunityResponse>>(result.Result);
        ok.Value!.Opportunities.Should().ContainSingle(o => o.InformationDescription! == "Frontend Internship");

        _opportunityServiceMock.Verify(s => s.ListOpportunitiesAsync(), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WhenNoOpportunitiesExist_ShouldReturnNotFound()
    {
        // Arrange
        _opportunityServiceMock.Setup(s => s.ListOpportunitiesAsync())
            .ReturnsAsync(_opportunityData); // Empty list

        // Act
        var result = await GetOpportunityHandler.HandleAsync(_opportunityServiceMock.Object);

        // Assert
        var notFound = Assert.IsType<NotFound<Error>>(result.Result);
        notFound.Value.Should().NotBeNull();
        notFound.Value!.Code.Should().Be("Data.NotFound");

        _opportunityServiceMock.Verify(s => s.ListOpportunitiesAsync(), Times.Once);
    }

    private static Opportunity TestOpportunity(string description)
    {
        return new Opportunity
        (
            1,
            Description.FromDatabase(description),
            Country.FromDatabase("Costa Rica")

        );
    }
}
