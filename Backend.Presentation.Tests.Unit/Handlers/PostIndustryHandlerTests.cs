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

public class PostIndustryHandlerTests
{
    private readonly Industry _industry;

    private readonly Mock<IIndustryServices> _mockIndustryServices;

    public PostIndustryHandlerTests()
    {
        _mockIndustryServices = new Mock<IIndustryServices>(MockBehavior.Strict);

        _industry = new Industry(
        1,
        Description.FromDatabase("Technology and innovation focused sector."),
        EntityName.FromDatabase("Tech Sector"),
        true
        );

    }


    [Fact]
    public async Task HandleAsync_ReturnsOk_WhenIndustryIsSuccessfullyCreated()
    {
        // Arrange
        var dto = CreateValidDto();
        var request = new PostIndustryRequest(dto);

        _mockIndustryServices
            .Setup(s => s.AddIndustriesAsync(It.IsAny<int>(), It.IsAny<Industry>()))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await PostIndustryHandler.HandleAsync(_mockIndustryServices.Object, request, 1);

        // Assert
        var okResult = result.Result as Ok<PostResponse>;
        okResult.Should().NotBeNull();
        okResult!.Value!.Response.Should().Be("Industry successfully created.");
    }

    [Fact]
    public async Task HandleAsync_ReturnsBadRequest_WhenMappingFails()
    {
        // Arrange
        var invalidDto = new AddIndustryDto("", "", false);
        var request = new PostIndustryRequest(invalidDto);

        // Act
        var result = await PostIndustryHandler.HandleAsync(_mockIndustryServices.Object, request, 1);

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
        var request = new PostIndustryRequest(dto);
        var validationErrors = new List<Error> { Error.Validation("Industry.Description", "Description is required.") };

        _mockIndustryServices
            .Setup(s => s.AddIndustriesAsync(It.IsAny<int>(), It.IsAny<Industry>()))
            .ReturnsAsync(Result.Failure(validationErrors));

        // Act
        var result = await PostIndustryHandler.HandleAsync(_mockIndustryServices.Object, request, 1);

        // Assert
        var badRequest = result.Result as BadRequest<List<Error>>;
        badRequest.Should().NotBeNull();
        badRequest!.Value.Should().ContainSingle(e => e.Code == "Industry.Description");
    }

    [Fact]
    public async Task HandleAsync_ReturnsConflict_WhenServiceReturnsDuplicateError()
    {
        // Arrange
        var dto = CreateValidDto();
        var request = new PostIndustryRequest(dto);
        var conflictError = Error.DuplicatedConflict("Industry.Duplicate", "Industry already exists.");

        _mockIndustryServices
            .Setup(s => s.AddIndustriesAsync(It.IsAny<int>(), It.IsAny<Industry>()))
            .ReturnsAsync(Result.Failure(conflictError));

        // Act
        var result = await PostIndustryHandler.HandleAsync(_mockIndustryServices.Object, request, 1);

        // Assert
        var conflict = result.Result as Conflict<Error>;
        conflict.Should().NotBeNull();
        conflict!.Value!.Code.Should().Be("Industry.Duplicate");
    }

    [Fact]
    public async Task HandleAsync_ReturnsConflict_WhenServiceReturnsUnknownError()
    {
        // Arrange
        var dto = CreateValidDto();
        var request = new PostIndustryRequest(dto);
        var unknownError = Error.Failure("Unknown", "Unexpected error occurred.");

        _mockIndustryServices
            .Setup(s => s.AddIndustriesAsync(It.IsAny<int>(), It.IsAny<Industry>()))
            .ReturnsAsync(Result.Failure(unknownError));

        // Act
        var result = await PostIndustryHandler.HandleAsync(_mockIndustryServices.Object, request, 1);

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
        var request = new PostIndustryRequest(dto);

        _mockIndustryServices
            .Setup(s => s.AddIndustriesAsync(It.IsAny<int>(), It.IsAny<Industry>()))
            .ReturnsAsync(Result.Failure((Error?)null));

        // Act
        var result = await PostIndustryHandler.HandleAsync(_mockIndustryServices.Object, request, 1);

        // Assert
        var conflict = result.Result as Conflict<Error>;
        conflict.Should().NotBeNull();
        conflict!.Value!.Code.Should().Be("Industry.PersistenceFailure");
    }

    private static AddIndustryDto CreateValidDto() =>
    new("Technology and innovation focused sector.", "Tech Sector", true);
}
