using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Tests.Unit.ValueObjects;

/// <summary>
/// Unit tests for the <see cref="Description"/> value object,
/// validating creation, database conversion, and equality logic.
/// </summary>
public class DescriptionTests
{
    /// <summary>
    /// Ensures that <see cref="Description.Create"/> fails when given null.
    /// </summary>
    [Fact]
    public void Create_WithNullValue_ShouldReturnFailure()
    {
        var result = Description.Create(null);

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.Required");
    }

    /// <summary>
    /// Ensures that <see cref="Description.Create"/> fails when given empty string.
    /// </summary>
    [Fact]
    public void Create_WithEmptyString_ShouldReturnFailure()
    {
        var result = Description.Create("");

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.Required");
    }

    /// <summary>
    /// Ensures that <see cref="Description.Create"/> fails when the string contains only whitespace.
    /// </summary>
    [Fact]
    public void Create_WithWhitespaceOnly_ShouldReturnFailure()
    {
        var result = Description.Create("   ");

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.Required");
    }

    /// <summary>
    /// Ensures that <see cref="Description.Create"/> fails when the input exceeds 700 characters.
    /// </summary>
    [Fact]
    public void Create_WithTooLongContent_ShouldReturnFailure()
    {
        var longInput = new string('a', 701);

        var result = Description.Create(longInput);

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Description.MaxLength");
    }

    /// <summary>
    /// Ensures that <see cref="Description.Create"/> returns a valid object when input is acceptable.
    /// </summary>
    [Fact]
    public void Create_WithValidContent_ShouldReturnSuccess()
    {
        var result = Description.Create("Valid description.");

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Content.Should().Be("Valid description.");
    }

    /// <summary>
    /// Ensures that <see cref="Description.FromDatabase"/> returns a valid object with valid content.
    /// </summary>
    [Fact]
    public void FromDatabase_WithValidContent_ShouldReturnDescription()
    {
        var description = Description.FromDatabase("Stored from DB.");

        description.Should().NotBeNull();
        description.Content.Should().Be("Stored from DB.");
    }

    /// <summary>
    /// Ensures that <see cref="Description.FromDatabase"/> throws when the content is invalid.
    /// </summary>
    [Fact]
    public void FromDatabase_WithInvalidContent_ShouldThrow()
    {
        var invalid = new string('x', 750);

        Action act = () => Description.FromDatabase(invalid);

        act.Should().Throw<InvalidOperationException>()
           .WithMessage("Invalid description found in database: *");
    }

    /// <summary>
    /// Verifies that two descriptions with the same content are considered equal.
    /// </summary>
    [Fact]
    public void Equality_WithSameContent_ShouldBeEqual()
    {
        var d1 = Description.FromDatabase("Same content");
        var d2 = Description.FromDatabase("Same content");

        d1.Should().Be(d2);
        d1.GetHashCode().Should().Be(d2.GetHashCode());
    }

    /// <summary>
    /// Verifies that two descriptions with different content are not considered equal.
    /// </summary>
    [Fact]
    public void Equality_WithDifferentContent_ShouldNotBeEqual()
    {
        var d1 = Description.FromDatabase("Content A");
        var d2 = Description.FromDatabase("Content B");

        d1.Should().NotBe(d2);
    }
}
