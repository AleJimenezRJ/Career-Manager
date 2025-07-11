using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Tests.Unit.ValueObjects;

/// <summary>
/// Unit tests for the <see cref="DegreeTitle"/> value object,
/// verifying validation, parsing from database, and equality.
/// </summary>
public class DegreeTitleTests
{
    /// <summary>
    /// Ensures that <see cref="DegreeTitle.Create"/> returns failure when given null.
    /// </summary>
    [Fact]
    public void Create_WithNullValue_ShouldReturnFailure()
    {
        var result = DegreeTitle.Create(null);

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.Required");
    }

    /// <summary>
    /// Ensures that <see cref="DegreeTitle.Create"/> returns failure when given empty string.
    /// </summary>
    [Fact]
    public void Create_WithEmptyString_ShouldReturnFailure()
    {
        var result = DegreeTitle.Create("");

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.Required");
    }

    /// <summary>
    /// Ensures that <see cref="DegreeTitle.Create"/> returns failure when the input is not in the allowed list.
    /// </summary>
    [Fact]
    public void Create_WithInvalidTitle_ShouldReturnFailure()
    {
        var result = DegreeTitle.Create("Grandmaster");

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.InvalidInformation");
    }

    /// <summary>
    /// Ensures that <see cref="DegreeTitle.Create"/> returns success when given a valid degree title.
    /// </summary>
    [Theory]
    [InlineData("Bachelor")]
    [InlineData("Licentiate")]
    [InlineData("Master")]
    [InlineData("Doctorate")]
    [InlineData("PhD")]
    [InlineData("Associate")]
    [InlineData("Diploma")]
    [InlineData("Technical")]
    public void Create_WithValidTitle_ShouldReturnSuccess(string input)
    {
        var result = DegreeTitle.Create(input);

        result.IsSuccess.Should().BeTrue();
        result.Value!.Value.Should().Be(input);
    }

    /// <summary>
    /// Ensures that <see cref="DegreeTitle.FromDatabase"/> returns a valid object when input is valid.
    /// </summary>
    [Fact]
    public void FromDatabase_WithValidTitle_ShouldReturnObject()
    {
        var title = DegreeTitle.FromDatabase("PhD");

        title.Should().NotBeNull();
        title.Value.Should().Be("PhD");
    }

    /// <summary>
    /// Ensures that <see cref="DegreeTitle.FromDatabase"/> throws when the input is not in the allowed list.
    /// </summary>
    [Fact]
    public void FromDatabase_WithInvalidTitle_ShouldThrow()
    {
        Action act = () => DegreeTitle.FromDatabase("Magisterium");

        act.Should().Throw<InvalidOperationException>()
           .WithMessage("Invalid degree title found in database: 'Magisterium'*");
    }

    /// <summary>
    /// Verifies that two degree titles with the same value are considered equal.
    /// </summary>
    [Fact]
    public void Equality_WithSameTitle_ShouldBeEqual()
    {
        var t1 = DegreeTitle.FromDatabase("Master");
        var t2 = DegreeTitle.FromDatabase("Master");

        t1.Should().Be(t2);
        t1.GetHashCode().Should().Be(t2.GetHashCode());
    }

    /// <summary>
    /// Verifies that two degree titles with different values are considered unequal.
    /// </summary>
    [Fact]
    public void Equality_WithDifferentTitles_ShouldNotBeEqual()
    {
        var t1 = DegreeTitle.FromDatabase("Bachelor");
        var t2 = DegreeTitle.FromDatabase("Doctorate");

        t1.Should().NotBe(t2);
    }
}
