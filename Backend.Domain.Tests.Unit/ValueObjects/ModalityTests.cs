using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Tests.Unit.ValueObjects;

/// <summary>
/// Unit tests for the <see cref="Modality"/> value object,
/// ensuring correct validation, creation, and equality handling.
/// </summary>
public class ModalityTests
{
    /// <summary>
    /// Ensures that <see cref="Modality.Create"/> fails with null input.
    /// </summary>
    [Fact]
    public void Create_WithNullValue_ShouldReturnFailure()
    {
        var result = Modality.Create(null);

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.Required");
    }

    /// <summary>
    /// Ensures that <see cref="Modality.Create"/> fails with an empty string.
    /// </summary>
    [Fact]
    public void Create_WithEmptyString_ShouldReturnFailure()
    {
        var result = Modality.Create("");

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.Required");
    }

    /// <summary>
    /// Ensures that <see cref="Modality.Create"/> fails with an invalid modality.
    /// </summary>
    [Fact]
    public void Create_WithInvalidModality_ShouldReturnFailure()
    {
        var result = Modality.Create("Remote");

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.InvalidInformation");
    }

    /// <summary>
    /// Ensures that <see cref="Modality.Create"/> returns success for a valid modality.
    /// </summary>
    [Fact]
    public void Create_WithValidModality_ShouldReturnSuccess()
    {
        var result = Modality.Create("Presential");

        result.IsSuccess.Should().BeTrue();
        result.Value!.Value.Should().Be("Presential");
    }

    /// <summary>
    /// Ensures that <see cref="Modality.FromDatabase"/> returns a valid object for a valid input.
    /// </summary>
    [Fact]
    public void FromDatabase_WithValidModality_ShouldReturnModality()
    {
        var modality = Modality.FromDatabase("Virtual");

        modality.Should().NotBeNull();
        modality.Value.Should().Be("Virtual");
    }

    /// <summary>
    /// Ensures that <see cref="Modality.FromDatabase"/> throws for an invalid modality.
    /// </summary>
    [Fact]
    public void FromDatabase_WithInvalidModality_ShouldThrow()
    {
        Action act = () => Modality.FromDatabase("OnDemand");

        act.Should().Throw<InvalidOperationException>()
           .WithMessage("Invalid Modality value found in database: *");
    }

    /// <summary>
    /// Verifies that two <see cref="Modality"/> instances with the same value are equal.
    /// </summary>
    [Fact]
    public void Equality_WithSameValue_ShouldBeEqual()
    {
        var m1 = Modality.FromDatabase("Hybrid");
        var m2 = Modality.FromDatabase("Hybrid");

        m1.Should().Be(m2);
        m1.GetHashCode().Should().Be(m2.GetHashCode());
    }

    /// <summary>
    /// Verifies that two <see cref="Modality"/> instances with different values are not equal.
    /// </summary>
    [Fact]
    public void Equality_WithDifferentValue_ShouldNotBeEqual()
    {
        var m1 = Modality.FromDatabase("Virtual");
        var m2 = Modality.FromDatabase("Presential");

        m1.Should().NotBe(m2);
    }
}
