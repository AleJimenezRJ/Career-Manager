using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Tests.Unit.ValueObjects;

/// <summary>
/// Unit tests for the <see cref="LanguageVO"/> value object,
/// ensuring proper validation, construction, and equality behavior.
/// </summary>
public class LanguageVOTests
{
    /// <summary>
    /// Ensures that <see cref="LanguageVO.Create"/> fails with null input.
    /// </summary>
    [Fact]
    public void Create_WithNullValue_ShouldReturnFailure()
    {
        var result = LanguageVO.Create(null);

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.Required");
    }

    /// <summary>
    /// Ensures that <see cref="LanguageVO.Create"/> fails with an empty string.
    /// </summary>
    [Fact]
    public void Create_WithEmptyString_ShouldReturnFailure()
    {
        var result = LanguageVO.Create("");

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.Required");
    }

    /// <summary>
    /// Ensures that <see cref="LanguageVO.Create"/> fails with a language not in the allowed list.
    /// </summary>
    [Fact]
    public void Create_WithInvalidLanguage_ShouldReturnFailure()
    {
        var result = LanguageVO.Create("Elvish");

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.InvalidInformation");
    }

    /// <summary>
    /// Ensures that <see cref="LanguageVO.Create"/> returns a valid object for an allowed language.
    /// </summary>
    [Fact]
    public void Create_WithValidLanguage_ShouldReturnSuccess()
    {
        var result = LanguageVO.Create("Spanish");

        result.IsSuccess.Should().BeTrue();
        result.Value!.Value.Should().Be("Spanish");
    }

    /// <summary>
    /// Ensures that <see cref="LanguageVO.FromDatabase"/> returns a valid object for a valid database value.
    /// </summary>
    [Fact]
    public void FromDatabase_WithValidLanguage_ShouldReturnLanguageVO()
    {
        var language = LanguageVO.FromDatabase("Japanese");

        language.Should().NotBeNull();
        language.Value.Should().Be("Japanese");
    }

    /// <summary>
    /// Ensures that <see cref="LanguageVO.FromDatabase"/> throws an exception for an invalid language.
    /// </summary>
    [Fact]
    public void FromDatabase_WithInvalidLanguage_ShouldThrow()
    {
        Action act = () => LanguageVO.FromDatabase("Alienese");

        act.Should().Throw<InvalidOperationException>()
           .WithMessage("Invalid Language value found in database: *");
    }

    /// <summary>
    /// Verifies that two instances with the same value are considered equal.
    /// </summary>
    [Fact]
    public void Equality_WithSameValue_ShouldBeEqual()
    {
        var lang1 = LanguageVO.FromDatabase("German");
        var lang2 = LanguageVO.FromDatabase("German");

        lang1.Should().Be(lang2);
        lang1.GetHashCode().Should().Be(lang2.GetHashCode());
    }

    /// <summary>
    /// Verifies that two instances with different values are not considered equal.
    /// </summary>
    [Fact]
    public void Equality_WithDifferentValue_ShouldNotBeEqual()
    {
        var lang1 = LanguageVO.FromDatabase("Italian");
        var lang2 = LanguageVO.FromDatabase("Dutch");

        lang1.Should().NotBe(lang2);
    }
}
