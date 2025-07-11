using FluentAssertions;
using Moq;
using Microsoft.AspNetCore.Http.HttpResults;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Requests;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Tests.Unit.Handlers;

public class PostOpportunityHandlerTests
{
    private readonly Mock<IOpportunityServices> _mockOpportunityService;

    public PostOpportunityHandlerTests()
    {
        _mockOpportunityService = new Mock<IOpportunityServices>(MockBehavior.Strict);
    }

    [Fact]
    public async Task HandleAsync_ReturnsOk_WhenOpportunityIsCreatedSuccessfully()
    {
        // Arrange
        var dto = new AddOpportunityDto(
            "Remote internship available",
            "Costa Rica"
        );
        var request = new PostOpportunityRequest(dto);

        var entity = new Opportunity(
            Description.FromDatabase(dto.InformationDescription),
            Country.FromDatabase(dto.Country)
        );

        _mockOpportunityService
            .Setup(s => s.AddOpportunitiesAsync(It.IsAny<int>(), It.Is<Opportunity>(o =>
                o.InformationDescription!.Content == entity.InformationDescription!.Content &&
                o.Country!.Value == entity.Country!.Value)))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await PostOpportunityHandler.HandleAsync(_mockOpportunityService.Object, request, 1);

        // Assert
        var ok = result.Result as Ok<PostResponse>;
        ok.Should().NotBeNull();
        ok!.Value!.Response.Should().Be("Opportunity successfully created.");
    }


    [Fact]
    public async Task HandleAsync_ReturnsConflict_WhenPersistenceFails()
    {
        // Arrange
        var dto = new AddOpportunityDto(
            "Internship in conflict",
            "Costa Rica"
        );
        var request = new PostOpportunityRequest(dto);

        var entity = new Opportunity(
            Description.FromDatabase(dto.InformationDescription),
            Country.FromDatabase(dto.Country)
        );

        var error = Error.Failure("Opportunity", "Conflict occurred");

        _mockOpportunityService
            .Setup(s => s.AddOpportunitiesAsync(It.IsAny<int>(), It.Is<Opportunity>(o =>
                o.InformationDescription!.Content == entity.InformationDescription!.Content &&
                o.Country!.Value == entity.Country!.Value)))
            .ReturnsAsync(Result.Failure(error));

        // Act
        var result = await PostOpportunityHandler.HandleAsync(_mockOpportunityService.Object, request, 1);

        // Assert
        var conflict = result.Result as Conflict<Error>;
        conflict.Should().NotBeNull();
    }
}
