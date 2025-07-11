using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Tests.Unit.ValueObjects;

/// <summary>
/// Unit tests for the <see cref="Workers"/> value object.
/// </summary>
public class WorkersTests
{
    /// <summary>
    /// Ensures that <see cref="Workers.Create"/> fails when the value is negative.
    /// </summary>
    [Fact]
    public void Create_WithNegativeValue_ShouldReturnFailure()
    {
        var result = Workers.Create(-1);

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.InvalidNumber");
    }

    /// <summary>
    /// Ensures that <see cref="Workers.Create"/> fails when the value exceeds the maximum allowed integer.
    /// </summary>
    [Fact]
    public void Create_WithMaxInt_ShouldReturnFailure()
    {
        var result = Workers.Create(int.MaxValue);

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.InvalidNumber");
    }

    /// <summary>
    /// Ensures that <see cref="Workers.Create"/> returns success for a valid number.
    /// </summary>
    [Fact]
    public void Create_WithValidValue_ShouldReturnSuccess()
    {
        var result = Workers.Create(42);

        result.IsSuccess.Should().BeTrue();
        result.Value!.Number.Should().Be(42);
    }

    /// <summary>
    /// Ensures that <see cref="Workers.FromDatabase"/> returns an instance for valid value.
    /// </summary>
    [Fact]
    public void FromDatabase_WithValidValue_ShouldReturnInstance()
    {
        var workers = Workers.FromDatabase(1000);

        workers.Number.Should().Be(1000);
    }

    /// <summary>
    /// Ensures that <see cref="Workers.FromDatabase"/> throws for an invalid value.
    /// </summary>
    [Fact]
    public void FromDatabase_WithInvalidValue_ShouldThrow()
    {
        Action act = () => Workers.FromDatabase(-10);

        act.Should().Throw<InvalidOperationException>()
           .WithMessage("Invalid number of collaborators (workers) found in database: *");
    }

    /// <summary>
    /// Ensures that two <see cref="Workers"/> instances with the same value are equal.
    /// </summary>
    [Fact]
    public void Equality_WithSameValue_ShouldBeEqual()
    {
        var a = Workers.FromDatabase(999);
        var b = Workers.FromDatabase(999);

        a.Should().Be(b);
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    /// <summary>
    /// Ensures that two <see cref="Workers"/> instances with different values are not equal.
    /// </summary>
    [Fact]
    public void Equality_WithDifferentValues_ShouldNotBeEqual()
    {
        var a = Workers.FromDatabase(5);
        var b = Workers.FromDatabase(10);

        a.Should().NotBe(b);
    }
}
