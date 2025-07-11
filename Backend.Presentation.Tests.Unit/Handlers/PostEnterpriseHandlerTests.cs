using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Requests;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Tests.Unit.Handlers;

public class PostEnterpriseHandlerTests
{
    private readonly Enterprise _enterprise;
    private readonly Mock<IEnterpriseServices> _mockEnterpriseServices;

    public PostEnterpriseHandlerTests()
    {
        _mockEnterpriseServices = new Mock<IEnterpriseServices>(MockBehavior.Loose);

        _enterprise = new Enterprise(
            Description.FromDatabase("Software and consulting firm."),
            EntityName.FromDatabase("Initech Corp"),
            Country.FromDatabase("Costa Rica")
        );

    }

    [Fact]
    public async Task HandleAsync_ReturnsOk_WhenEnterpriseIsSuccessfullyCreated()
    {
        // Arrange
        var dto = CreateValidDto();
        var request = new PostEnterpriseRequest(dto);

        _mockEnterpriseServices
            .Setup(s => s.AddEnterpriseAsync(It.IsAny<int>(), It.IsAny<Enterprise>()))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await PostEnterpriseHandler.HandleAsync(_mockEnterpriseServices.Object, request, 1);

        // Assert
        var okResult = result.Result as Ok<PostResponse>;
        okResult.Should().NotBeNull();
        okResult!.Value!.Response.Should().Be("Enterprise successfully created.");
    }

    [Fact]
    public async Task HandleAsync_ReturnsBadRequest_WhenMappingFails()
    {
        // Arrange
        var invalidDto = new AddEnterpriseDto("", "", "");
        var request = new PostEnterpriseRequest(invalidDto);

        // Act
        var result = await PostEnterpriseHandler.HandleAsync(_mockEnterpriseServices.Object, request, 1);

        // Assert
        var badRequest = result.Result as BadRequest<List<Error>>;
        badRequest.Should().NotBeNull();
        badRequest!.Value.Should().NotBeEmpty();
    }

    [Fact]
    public async Task HandleAsync_ReturnsBadRequest_WhenServiceReturnsValidationErrors()
    {
        // Arrange
        var dto = CreateValidDto();
        var request = new PostEnterpriseRequest(dto);
        var validationErrors = new List<Error> { Error.Validation("Enterprise.Name", "Name is required.") };

        _mockEnterpriseServices
            .Setup(s => s.AddEnterpriseAsync(It.IsAny<int>(), It.IsAny<Enterprise>()))
            .ReturnsAsync(Result.Failure(validationErrors));

        // Act
        var result = await PostEnterpriseHandler.HandleAsync(_mockEnterpriseServices.Object, request, 1);

        // Assert
        var badRequest = result.Result as BadRequest<List<Error>>;
        badRequest.Should().NotBeNull();
    }

    [Fact]
    public async Task HandleAsync_ReturnsBadRequest_WhenServiceReturnsDuplicateError()
    {
        // Arrange
        var dto = CreateInvalidDto();
        var request = new PostEnterpriseRequest(dto);
        var conflictError = Error.DuplicatedConflict("Enterprise.Duplicate", "Enterprise already exists.");

        _mockEnterpriseServices
            .Setup(s => s.AddEnterpriseAsync(It.IsAny<int>(), It.IsAny<Enterprise>()))
            .ReturnsAsync(Result.Failure(conflictError));

        // Act
        var result = await PostEnterpriseHandler.HandleAsync(_mockEnterpriseServices.Object, request, 1);

        // Assert
        var badRequest = result.Result as BadRequest<List<Error>>;
        badRequest.Should().NotBeNull();
    }


    [Fact]
    public async Task HandleAsync_ReturnsConflict_WhenServiceReturnsUnknownError()
    {
        // Arrange
        var dto = CreateValidDto();
        var request = new PostEnterpriseRequest(dto);
        var unknownError = Error.Failure("Unknown", "Unexpected error.");

        _mockEnterpriseServices
            .Setup(s => s.AddEnterpriseAsync(It.IsAny<int>(), It.IsAny<Enterprise>()))
            .ReturnsAsync(Result.Failure(unknownError));

        // Act
        var result = await PostEnterpriseHandler.HandleAsync(_mockEnterpriseServices.Object, request, 1);

        // Assert
        var conflict = result.Result as Conflict<Error>;
        conflict.Should().NotBeNull();
        conflict!.Value!.Code.Should().Be("Unknown");
    }

    [Fact]
    public async Task HandleAsync_ReturnsConflict_WhenServiceReturnsNullError()
    {
        // Arrange
        var dto = CreateValidDto();
        var request = new PostEnterpriseRequest(dto);

        _mockEnterpriseServices
            .Setup(s => s.AddEnterpriseAsync(It.IsAny<int>(), It.IsAny<Enterprise>()))
            .ReturnsAsync(Result.Failure((Error?)null));

        // Act
        var result = await PostEnterpriseHandler.HandleAsync(_mockEnterpriseServices.Object, request, 1);

        // Assert
        var conflict = result.Result as Conflict<Error>;
        conflict.Should().NotBeNull();
        conflict!.Value!.Code.Should().Be("Enterprise.PersistenceFailure");
    }

    private static AddEnterpriseDto CreateValidDto() =>
        new(
            "Initech Corp is a good enterprise",
            "Initech Corp",
            "Costa Rica"
        );

    private static AddEnterpriseDto CreateInvalidDto() =>
        new(
            "Initech Corp is a good enterprise",
            " ",
            "Costa Rica"
        );
}
