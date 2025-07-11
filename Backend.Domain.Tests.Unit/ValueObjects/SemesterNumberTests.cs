using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Tests.Unit.ValueObjects;

/// <summary>
/// Unit tests for the <see cref="SemestersNumber"/> value object.
/// </summary>
public class SemestersNumberTests
{
    /// <summary>
    /// Ensures that <see cref="SemestersNumber.Create"/> fails when the number is zero.
    /// </summary>
    [Fact]
    public void Create_WithZero_ShouldReturnFailure()
    {
        var result = SemestersNumber.Create(0);

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.InvalidNumber");
    }

    /// <summary>
    /// Ensures that <see cref="SemestersNumber.Create"/> fails when the number is negative.
    /// </summary>
    [Fact]
    public void Create_WithNegativeValue_ShouldReturnFailure()
    {
        var result = SemestersNumber.Create(-5);

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.InvalidNumber");
    }

    /// <summary>
    /// Ensures that <see cref="SemestersNumber.Create"/> fails when the number is too large.
    /// </summary>
    [Fact]
    public void Create_WithTooLargeValue_ShouldReturnFailure()
    {
        var result = SemestersNumber.Create(250);

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.InvalidNumber");
    }

    /// <summary>
    /// Ensures that <see cref="SemestersNumber.Create"/> returns success for a valid value.
    /// </summary>
    [Fact]
    public void Create_WithValidValue_ShouldReturnSuccess()
    {
        var result = SemestersNumber.Create(10);

        result.IsSuccess.Should().BeTrue();
        result.Value!.Number.Should().Be(10);
    }

    /// <summary>
    /// Ensures that <see cref="SemestersNumber.FromDatabase"/> returns the correct instance for a valid value.
    /// </summary>
    [Fact]
    public void FromDatabase_WithValidValue_ShouldReturnInstance()
    {
        var number = SemestersNumber.FromDatabase(6);

        number.Number.Should().Be(6);
    }

    /// <summary>
    /// Ensures that <see cref="SemestersNumber.FromDatabase"/> throws when the value is invalid.
    /// </summary>
    [Fact]
    public void FromDatabase_WithInvalidValue_ShouldThrow()
    {
        Action act = () => SemestersNumber.FromDatabase(0);

        act.Should().Throw<InvalidOperationException>()
           .WithMessage("Invalid number of semesters found in database: *");
    }

    /// <summary>
    /// Ensures that two <see cref="SemestersNumber"/> instances with the same number are equal.
    /// </summary>
    [Fact]
    public void Equality_WithSameValue_ShouldBeEqual()
    {
        var a = SemestersNumber.FromDatabase(8);
        var b = SemestersNumber.FromDatabase(8);

        a.Should().Be(b);
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    /// <summary>
    /// Ensures that two <see cref="SemestersNumber"/> instances with different numbers are not equal.
    /// </summary>
    [Fact]
    public void Equality_WithDifferentValue_ShouldNotBeEqual()
    {
        var a = SemestersNumber.FromDatabase(5);
        var b = SemestersNumber.FromDatabase(6);

        a.Should().NotBe(b);
    }
}
