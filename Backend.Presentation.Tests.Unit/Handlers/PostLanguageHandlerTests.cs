using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Requests;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Tests.Unit.Handlers;

public class PostLanguageHandlerTests
{
    private readonly Mock<ILanguageRepository> _mockLanguageRepository;

    public PostLanguageHandlerTests()
    {
        _mockLanguageRepository = new Mock<ILanguageRepository>(MockBehavior.Loose);
    }

    [Fact]
    public async Task HandleAsync_ReturnsOk_WhenLanguageIsSuccessfullyCreated()
    {
        // Arrange
        var dto = new LanguageDto("Spanish");
        var request = new PostLanguageRequest(dto);

        _mockLanguageRepository
            .Setup(r => r.AddLanguageAsync(It.IsAny<int>(), It.IsAny<Language>()))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await PostLanguageHandler.HandleAsync(_mockLanguageRepository.Object, 1, request);

        // Assert
        var ok = result.Result as Ok<PostResponse>;
        ok.Should().NotBeNull();
        ok!.Value!.Response.Should().Be("Language successfully created.");
    }

    [Fact]
    public async Task HandleAsync_ReturnsConflict_WhenPersistenceFails()
    {
        // Arrange
        var dto = new LanguageDto("Spanish");
        var request = new PostLanguageRequest(dto);

        _mockLanguageRepository
            .Setup(r => r.AddLanguageAsync(It.IsAny<int>(), It.IsAny<Language>()))
            .ReturnsAsync(Result.Failure(Error.DuplicatedConflict("Language", "Duplicate")));

        // Act
        var result = await PostLanguageHandler.HandleAsync(_mockLanguageRepository.Object, 1, request);

        // Assert
        var conflict = result.Result as Conflict<Error>;
        conflict.Should().NotBeNull();
        conflict!.Value!.Message.Should().Be("Duplicate");
    }
}
