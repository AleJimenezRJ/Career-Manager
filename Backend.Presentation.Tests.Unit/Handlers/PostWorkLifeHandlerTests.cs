using FluentAssertions;
using Moq;
using Microsoft.AspNetCore.Http.HttpResults;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Requests;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Tests.Unit.Handlers;

public class PostWorkLifeHandlerTests
{
    private readonly Mock<IWorkLifeServices> _mockService;

    public PostWorkLifeHandlerTests()
    {
        _mockService = new Mock<IWorkLifeServices>(MockBehavior.Strict);
    }

    [Fact]
    public async Task HandleAsync_ReturnsOk_WhenWorkLifeCreatedSuccessfully()
    {
        // Arrange
        var dto = new AddWorkLifeDto("Example", 10, 20);
        var request = new PostWorkLifeRequest(dto);

        _mockService
            .Setup(s => s.AddWorkLifeAsync(It.IsAny<int>(), It.IsAny<WorkLife>()))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await PostWorkLifeHandler.HandleAsync(_mockService.Object, request, 321);

        // Assert
        var ok = result.Result as Ok<PostResponse>;
        ok.Should().NotBeNull();
        ok!.Value!.Response.Should().Be("Work Life successfully created.");
    }

    [Fact]
    public async Task HandleAsync_ReturnsBadRequest_WhenValidationFails()
    {
        // Arrange
        var invalidDto = new AddWorkLifeDto("", -1, -2);
        var request = new PostWorkLifeRequest(invalidDto);

        _mockService
            .Setup(s => s.AddWorkLifeAsync(It.IsAny<int>(), It.IsAny<WorkLife>()))
            .ReturnsAsync(Result.Failure(new List<Error>
            {
                Error.Validation("WorkLife.Description", "Description is required."),
                Error.Validation("WorkLife.Min", "Must be >= 0.")
            }));

        // Act
        var result = await PostWorkLifeHandler.HandleAsync(_mockService.Object, request, 321);

        // Assert
        var badRequest = result.Result as BadRequest<List<Error>>;
        badRequest.Should().NotBeNull();
    }

    [Fact]
    public async Task HandleAsync_ReturnsConflict_WhenWorkLifeAlreadyExists()
    {
        // Arrange
        var dto = new AddWorkLifeDto("Example", 10, 20);
        var request = new PostWorkLifeRequest(dto);
        var conflictError = Error.DuplicatedConflict("WorkLife.Duplicate", "WorkLife already exists.");

        _mockService
            .Setup(s => s.AddWorkLifeAsync(It.IsAny<int>(), It.IsAny<WorkLife>()))
            .ReturnsAsync(Result.Failure(conflictError));

        // Act
        var result = await PostWorkLifeHandler.HandleAsync(_mockService.Object, request, 321);

        // Assert
        var conflict = result.Result as Conflict<Error>;
        conflict.Should().NotBeNull();
        conflict!.Value!.Code.Should().Be("WorkLife.Duplicate");
    }

    [Fact]
    public async Task HandleAsync_ReturnsConflict_WhenUnknownErrorOccurs()
    {
        // Arrange
        var dto = new AddWorkLifeDto("Example", 10, 20);
        var request = new PostWorkLifeRequest(dto);
        var unknownError = Error.Failure("Unknown", "Unexpected error.");

        _mockService
            .Setup(s => s.AddWorkLifeAsync(It.IsAny<int>(), It.IsAny<WorkLife>()))
            .ReturnsAsync(Result.Failure(unknownError));

        // Act
        var result = await PostWorkLifeHandler.HandleAsync(_mockService.Object, request, 321);

        // Assert
        var conflict = result.Result as Conflict<Error>;
        conflict.Should().NotBeNull();
        conflict!.Value!.Code.Should().Be("Unknown");
    }
}
