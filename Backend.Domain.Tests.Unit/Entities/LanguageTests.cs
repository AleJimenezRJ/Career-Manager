using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Tests.Unit.Entities;

/// <summary>
/// Unit tests for the <see cref="Language"/> entity.
/// Validates constructor logic and property assignments.
/// </summary>
public class LanguageTests
{
    private readonly LanguageVO _languageVO;

    /// <summary>
    /// Initializes test dependencies for <see cref="Language"/> tests.
    /// </summary>
    public LanguageTests()
    {
        _languageVO = LanguageVO.FromDatabase("English");
    }

    /// <summary>
    /// Ensures the constructor sets the <see cref="Language.LanguageValue"/> property correctly.
    /// </summary>
    [Fact]
    public void Constructor_ShouldSetLanguageValueCorrectly()
    {
        // Act
        var language = new Language(_languageVO);

        // Assert
        language.LanguageValue.Should().Be(_languageVO);
    }

    /// <summary>
    /// Ensures the default ID is zero upon creation.
    /// </summary>
    [Fact]
    public void Constructor_ShouldHaveDefaultLanguageInternalId()
    {
        // Act
        var language = new Language(_languageVO);

        // Assert
        language.LanguageInternalId.Should().Be(0);
    }

}
