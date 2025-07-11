using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Tests.Unit.Entities;

/// <summary>
/// Unit tests for the <see cref="Opportunity"/> entity.
/// Validates constructor logic and property initialization.
/// </summary>
public class OpportunityTests
{
    private const int _workInformationId = 123;
    private readonly Description _description;
    private readonly Country _country;

    /// <summary>
    /// Initializes test dependencies for <see cref="Opportunity"/> tests.
    /// </summary>
    public OpportunityTests()
    {
        _description = Description.FromDatabase("Remote frontend development opportunities.");
        _country = Country.FromDatabase("Germany");
    }

    /// <summary>
    /// Ensures that the constructor with ID sets all properties correctly.
    /// </summary>
    [Fact]
    public void ConstructorWithId_ShouldSetAllPropertiesCorrectly()
    {
        // Act
        var opportunity = new Opportunity(_workInformationId, _description, _country);

        // Assert
        opportunity.WorkInformationInternalId.Should().Be(_workInformationId);
        opportunity.InformationDescription.Should().Be(_description);
        opportunity.Country.Should().Be(_country);
    }

    /// <summary>
    /// Ensures that the constructor without ID sets properties correctly and defaults ID to 0.
    /// </summary>
    [Fact]
    public void ConstructorWithoutId_ShouldSetAllPropertiesCorrectly()
    {
        // Act
        var opportunity = new Opportunity(_description, _country);

        // Assert
        opportunity.WorkInformationInternalId.Should().Be(0);
        opportunity.InformationDescription.Should().Be(_description);
        opportunity.Country.Should().Be(_country);
    }

    /// <summary>
    /// Ensures the constructor with ID accepts a null country.
    /// </summary>
    [Fact]
    public void ConstructorWithId_WhenCountryIsNull_ShouldSetCountryToNull()
    {
        // Act
        var opportunity = new Opportunity(_workInformationId, _description, null);

        // Assert
        opportunity.Country.Should().BeNull();
    }

    /// <summary>
    /// Ensures the constructor without ID accepts a null country.
    /// </summary>
    [Fact]
    public void ConstructorWithoutId_WhenCountryIsNull_ShouldSetCountryToNull()
    {
        // Act
        var opportunity = new Opportunity(_description, null);

        // Assert
        opportunity.Country.Should().BeNull();
    }

    /// <summary>
    /// Ensures the constructor with ID accepts a null description.
    /// </summary>
    [Fact]
    public void ConstructorWithId_WhenDescriptionIsNull_ShouldSetDescriptionToNull()
    {
        // Act
        var opportunity = new Opportunity(_workInformationId, null, _country);

        // Assert
        opportunity.InformationDescription.Should().BeNull();
    }

    /// <summary>
    /// Ensures the constructor without ID accepts a null description.
    /// </summary>
    [Fact]
    public void ConstructorWithoutId_WhenDescriptionIsNull_ShouldSetDescriptionToNull()
    {
        // Act
        var opportunity = new Opportunity(null, _country);

        // Assert
        opportunity.InformationDescription.Should().BeNull();
    }
}
