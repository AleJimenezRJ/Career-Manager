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

public class GetLanguageHandlerTests
{
    private readonly Mock<ILanguageRepository> _languageRepositoryMock;
    private readonly List<Language> _languageData;

    public GetLanguageHandlerTests()
    {
        _languageRepositoryMock = new Mock<ILanguageRepository>(MockBehavior.Loose);
        _languageData = new List<Language>();
    }

    [Fact]
    public async Task HandleAsync_WhenLanguagesExist_ShouldReturnOkWithList()
    {
        // Arrange
        var testLanguage = TestLanguage("English");
        _languageData.Add(testLanguage);

        _languageRepositoryMock.Setup(r => r.ListLanguagesAsync())
            .ReturnsAsync(_languageData);

        // Act
        var result = await GetLanguageHandler.HandleAsync(_languageRepositoryMock.Object);

        // Assert
        var ok = Assert.IsType<Ok<GetLanguageResponse>>(result.Result);
        ok.Value!.LanguageDtos.Should().ContainSingle(l => l.LanguageValue == "English");

        _languageRepositoryMock.Verify(r => r.ListLanguagesAsync(), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WhenNoLanguagesExist_ShouldReturnNotFound()
    {
        // Arrange
        _languageRepositoryMock.Setup(r => r.ListLanguagesAsync())
            .ReturnsAsync(_languageData); // Empty list

        // Act
        var result = await GetLanguageHandler.HandleAsync(_languageRepositoryMock.Object);

        // Assert
        var notFound = Assert.IsType<NotFound<Error>>(result.Result);
        notFound.Value.Should().NotBeNull();
        notFound.Value!.Code.Should().Be("Data.NotFound");

        _languageRepositoryMock.Verify(r => r.ListLanguagesAsync(), Times.Once);
    }

    private static Language TestLanguage(string value)
    {
        return new Language(LanguageVO.FromDatabase(value));
    }
}
