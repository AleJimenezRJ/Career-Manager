using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Tests.Unit.ValueObjects;

/// <summary>
/// Unit tests for the <see cref="Country"/> value object,
/// verifying validation logic, FromDatabase behavior, and equality.
/// </summary>
public class CountryTests
{
    /// <summary>
    /// Ensures that <see cref="Country.Create"/> returns failure when given null.
    /// </summary>
    [Fact]
    public void Create_WithNullValue_ShouldReturnFailure()
    {
        // Act
        var result = Country.Create(null);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.Required");
    }

    /// <summary>
    /// Ensures that <see cref="Country.Create"/> returns failure when given empty string.
    /// </summary>
    [Fact]
    public void Create_WithEmptyString_ShouldReturnFailure()
    {
        var result = Country.Create("");

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.Required");
    }

    /// <summary>
    /// Ensures that <see cref="Country.Create"/> returns failure when input is not in the allowed list.
    /// </summary>
    [Fact]
    public void Create_WithInvalidCountry_ShouldReturnFailure()
    {
        var result = Country.Create("Wakanda");

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Validation.InvalidInformation");
    }

    /// <summary>
    /// Ensures that <see cref="Country.Create"/> returns success and a valid object for allowed country.
    /// </summary>
    [Fact]
    public void Create_WithValidCountry_ShouldReturnSuccess()
    {
        var result = Country.Create("Costa Rica");

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Value.Should().Be("Costa Rica");
    }

    /// <summary>
    /// Ensures that <see cref="Country.FromDatabase"/> returns a valid object when given valid country.
    /// </summary>
    [Fact]
    public void FromDatabase_WithValidCountry_ShouldReturnCountry()
    {
        var country = Country.FromDatabase("Germany");

        country.Should().NotBeNull();
        country.Value.Should().Be("Germany");
    }

    /// <summary>
    /// Ensures that <see cref="Country.FromDatabase"/> throws when given an invalid country.
    /// </summary>
    [Fact]
    public void FromDatabase_WithInvalidCountry_ShouldThrow()
    {
        Action act = () => Country.FromDatabase("Middle Earth");

        act.Should().Throw<InvalidOperationException>()
           .WithMessage("Invalid Country value found in database: 'Middle Earth'*");
    }

    /// <summary>
    /// Verifies that two countries with the same value are equal.
    /// </summary>
    [Fact]
    public void Equality_WithSameCountry_ShouldBeEqual()
    {
        var c1 = Country.FromDatabase("Italy");
        var c2 = Country.FromDatabase("Italy");

        c1.Should().Be(c2);
        c1.GetHashCode().Should().Be(c2.GetHashCode());
    }

    /// <summary>
    /// Verifies that two countries with different values are not equal.
    /// </summary>
    [Fact]
    public void Equality_WithDifferentCountries_ShouldNotBeEqual()
    {
        var c1 = Country.FromDatabase("United States");
        var c2 = Country.FromDatabase("France");

        c1.Should().NotBe(c2);
    }
}
