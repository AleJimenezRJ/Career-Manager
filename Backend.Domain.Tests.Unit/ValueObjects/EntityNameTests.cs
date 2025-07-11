using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Tests.Unit.ValueObjects;

/// <summary>
/// Unit tests for the <see cref="EntityName"/> value object,
/// ensuring validation, creation, and equality logic.
/// </summary>
public class EntityNameTests
{
    /// <summary>
    /// Ensures that <see cref="EntityName.Create"/> fails when given null.
    /// </summary>
    [Fact]
    public void Create_WithNullValue_ShouldReturnFailure()
    {
        var result = EntityName.Create(null);

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.Required");
    }

    /// <summary>
    /// Ensures that <see cref="EntityName.Create"/> fails when given empty string.
    /// </summary>
    [Fact]
    public void Create_WithEmptyString_ShouldReturnFailure()
    {
        var result = EntityName.Create("");

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.Required");
    }

    /// <summary>
    /// Ensures that <see cref="EntityName.Create"/> fails when the string contains only whitespace.
    /// </summary>
    [Fact]
    public void Create_WithWhitespaceOnly_ShouldReturnFailure()
    {
        var result = EntityName.Create("   ");

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.Required");
    }

    /// <summary>
    /// Ensures that <see cref="EntityName.Create"/> fails when the name exceeds 100 characters.
    /// </summary>
    [Fact]
    public void Create_WithTooLongName_ShouldReturnFailure()
    {
        var longName = new string('X', 101);

        var result = EntityName.Create(longName);

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("EntityName.MaxLength");
    }

    /// <summary>
    /// Ensures that <see cref="EntityName.Create"/> returns a valid object when input is acceptable.
    /// </summary>
    [Fact]
    public void Create_WithValidName_ShouldReturnSuccess()
    {
        var result = EntityName.Create("Ingeniería en Computación");

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Name.Should().Be("Ingeniería en Computación");
    }

    /// <summary>
    /// Ensures that <see cref="EntityName.FromDatabase"/> returns a valid object with valid content.
    /// </summary>
    [Fact]
    public void FromDatabase_WithValidName_ShouldReturnEntityName()
    {
        var name = EntityName.FromDatabase("Universidad de Costa Rica");

        name.Should().NotBeNull();
        name.Name.Should().Be("Universidad de Costa Rica");
    }

    /// <summary>
    /// Ensures that <see cref="EntityName.FromDatabase"/> throws when the content is invalid.
    /// </summary>
    [Fact]
    public void FromDatabase_WithInvalidName_ShouldThrow()
    {
        var invalid = new string('A', 150);

        Action act = () => EntityName.FromDatabase(invalid);

        act.Should().Throw<InvalidOperationException>()
           .WithMessage("Invalid Name found in database: *");
    }

    /// <summary>
    /// Verifies that two names with the same content are considered equal.
    /// </summary>
    [Fact]
    public void Equality_WithSameContent_ShouldBeEqual()
    {
        var n1 = EntityName.FromDatabase("Tecnología de Alimentos");
        var n2 = EntityName.FromDatabase("Tecnología de Alimentos");

        n1.Should().Be(n2);
        n1.GetHashCode().Should().Be(n2.GetHashCode());
    }

    /// <summary>
    /// Verifies that two names with different content are not considered equal.
    /// </summary>
    [Fact]
    public void Equality_WithDifferentContent_ShouldNotBeEqual()
    {
        var n1 = EntityName.FromDatabase("Derecho");
        var n2 = EntityName.FromDatabase("Economía");

        n1.Should().NotBe(n2);
    }
}
