using FluentAssertions;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services.Implementations;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Tests.Unit.Services.Implementations;

/// <summary>
/// Provides unit tests for the <see cref="LanguageServices"/> class, ensuring its methods and behaviors function as
/// expected.
/// </summary>
/// <remarks>This class is designed to test the functionality of the <see cref="LanguageServices"/> class in
/// isolation. It uses a strict mock of the <see cref="ILanguageRepository"/> interface to simulate repository behavior
/// and validate interactions. Each test ensures that the service methods perform correctly under various conditions,
/// including handling expected inputs and outputs.</remarks>
public class LanguageServicesTests
{
    /// <summary>
    /// Represents a mock implementation of the <see cref="ILanguageRepository"/> interface.
    /// </summary>
    /// <remarks>This field is intended for use in unit tests to simulate the behavior of the <see
    /// cref="ILanguageRepository"/>. It provides a controlled environment for testing without relying on the actual
    /// implementation.</remarks>
    private readonly Mock<ILanguageRepository> _languageRepositoryMock;

    /// <summary>
    /// Represents the language services instance being tested.
    /// </summary>
    /// <remarks>This field is used internally to perform operations and validations related to language
    /// services. It is read-only and cannot be modified after initialization.</remarks>
    private readonly LanguageServices _serviceUnderTest;

    /// <summary>
    /// Represents the language associated with the current instance.
    /// </summary>
    /// <remarks>This field is read-only and is intended to store the language configuration or context for
    /// the instance. It cannot be modified after initialization.</remarks>
    private readonly Language _language;

    /// <summary>
    /// Initializes a new instance of the <see cref="LanguageServicesTests"/> class.
    /// </summary>
    /// <remarks>This constructor sets up the necessary dependencies for testing the <see
    /// cref="LanguageServices"/> class. It creates a strict mock of <see cref="ILanguageRepository"/> and initializes
    /// the service under test. Additionally, it prepares a sample <see cref="Language"/> instance for use in test
    /// scenarios.</remarks>
    public LanguageServicesTests()
    {
        _languageRepositoryMock = new Mock<ILanguageRepository>(MockBehavior.Strict);
        _serviceUnderTest = new LanguageServices(_languageRepositoryMock.Object);

        _language = new Language(LanguageVO.FromDatabase("English"));
    }

    /// <summary>
    /// Tests that the <see cref="ILanguageService.ListLanguagesAsync"/> method returns a list of languages.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="ILanguageService.ListLanguagesAsync"/> method
    /// correctly retrieves a list of languages from the repository and returns it to the caller. It ensures that the
    /// method interacts with the repository as expected and that the returned list contains the expected language
    /// objects.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListLanguagesAsync_ReturnsLanguagesList()
    {
        var languages = new List<Language> { _language };

        _languageRepositoryMock
            .Setup(repo => repo.ListLanguagesAsync())
            .ReturnsAsync(languages);

        var result = await _serviceUnderTest.ListLanguagesAsync();

        result.Should().ContainSingle().Which.Should().Be(_language);
        _languageRepositoryMock.Verify(repo => repo.ListLanguagesAsync(), Times.Once);
    }

    /// <summary>
    /// Tests that the <see cref="ILanguageService.AddLanguageAsync(int, Language)"/> method  returns a successful
    /// result when the repository successfully adds the language.
    /// </summary>
    /// <remarks>This test verifies the behavior of the <see cref="ILanguageService.AddLanguageAsync(int,
    /// Language)"/>  method when the underlying repository operation completes successfully. It ensures that the
    /// service  correctly propagates the success result and calls the repository method exactly once.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task AddLanguageAsync_ReturnsSuccess_WhenRepositoryReturnsSuccess()
    {
        int workInformationId = 42;
        var expected = Result.Success();

        _languageRepositoryMock
            .Setup(repo => repo.AddLanguageAsync(workInformationId, _language))
            .ReturnsAsync(expected);

        var result = await _serviceUnderTest.AddLanguageAsync(workInformationId, _language);

        result.Should().Be(expected);
        _languageRepositoryMock.Verify(repo => repo.AddLanguageAsync(workInformationId, _language), Times.Once);
    }
}
