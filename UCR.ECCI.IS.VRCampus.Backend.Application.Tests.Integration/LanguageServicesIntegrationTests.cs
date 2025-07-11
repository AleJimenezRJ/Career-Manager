using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services.Implementations;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Tests.Integration;

/// <summary>
/// Provides integration tests for verifying the behavior of language-related services.
/// </summary>
/// <remarks>This class contains tests that ensure the correct functionality of the <see
/// cref="ILanguageServices"/> implementation, including operations such as listing available languages and adding new
/// languages. Mock dependencies are used to isolate the behavior of the language services from external
/// systems.</remarks>
public class LanguageServicesIntegrationTests
{
    /// <summary>
    /// A mock implementation of the <see cref="ILanguageRepository"/> interface used for testing purposes.
    /// </summary>
    /// <remarks>This field is intended to simulate the behavior of <see cref="ILanguageRepository"/> in unit
    /// tests. It allows controlled testing scenarios by setting up mock responses and verifying interactions.</remarks>
    private readonly Mock<ILanguageRepository> _languageRepositoryMock;

    /// <summary>
    /// Represents the language services used for processing and managing language-specific operations.
    /// </summary>
    /// <remarks>This field is read-only and is intended to store an instance of a service that provides
    /// language-specific functionality.</remarks>
    private readonly ILanguageServices _languageServices;

    /// <summary>
    /// Initializes a new instance of the <see cref="LanguageServicesIntegrationTests"/> class.
    /// </summary>
    /// <remarks>This constructor sets up the necessary dependencies for testing the integration of language
    /// services. It creates a mock implementation of <see cref="ILanguageRepository"/> and initializes a  <see
    /// cref="LanguageServices"/> instance using the mocked repository.</remarks>
    public LanguageServicesIntegrationTests()
    {
        _languageRepositoryMock = new Mock<ILanguageRepository>();
        _languageServices = new LanguageServices(_languageRepositoryMock.Object);
    }

    /// <summary>
    /// Tests that the <see cref="ILanguageServices.ListLanguagesAsync"/> method returns the available languages.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="ILanguageServices.ListLanguagesAsync"/> method
    /// correctly retrieves the list of languages from the repository and returns it. It ensures that the result is not
    /// null, contains the expected number of items, and that the repository method is called exactly once.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListLanguagesAsync_ShouldReturnAvailableLanguages()
    {
        // Arrange
        var languages = new List<Language>
        {
            new Language(LanguageVO.Create("English").Value!)
        };

        _languageRepositoryMock
            .Setup(r => r.ListLanguagesAsync())
            .ReturnsAsync(languages);

        // Act
        var result = await _languageServices.ListLanguagesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        _languageRepositoryMock.Verify(r => r.ListLanguagesAsync(), Times.Once);
    }

    /// <summary>
    /// Tests the <c>AddLanguageAsync</c> method to ensure that it calls the repository to add a language and returns a
    /// success result.
    /// </summary>
    /// <remarks>This test verifies that the <c>AddLanguageAsync</c> method correctly interacts with the
    /// repository and produces a successful result when valid inputs are provided. It also ensures that the repository
    /// method is called exactly once.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task AddLanguageAsync_ShouldCallRepositoryAndReturnSuccess()
    {
        // Arrange
        int workInformationId = 10;
        var language = new Language(LanguageVO.Create("French").Value!);

        _languageRepositoryMock
            .Setup(r => r.AddLanguageAsync(workInformationId, language))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await _languageServices.AddLanguageAsync(workInformationId, language);

        // Assert
        Assert.True(result.IsSuccess);
        _languageRepositoryMock.Verify(r => r.AddLanguageAsync(workInformationId, language), Times.Once);
    }
}
