using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Tests.Unit.Entities;

/// <summary>
/// Unit tests for the <see cref="Enterprise"/> entity.
/// Validates constructors and property initialization.
/// </summary>
public class EnterpriseTests
{
    private const int _workInformationId = 1;
    private readonly Description _description;
    private readonly EntityName _name;
    private readonly Country _country;

    /// <summary>
    /// Initializes dependencies for the <see cref="Enterprise"/> tests.
    /// </summary>
    public EnterpriseTests()
    {
        _description = Description.FromDatabase("An international technology company.");
        _name = EntityName.FromDatabase("Globex Corporation");
        _country = Country.FromDatabase("United States");
    }

    /// <summary>
    /// Ensures the constructor with ID sets all properties correctly.
    /// </summary>
    [Fact]
    public void ConstructorWithId_ShouldSetAllPropertiesCorrectly()
    {
        // Act
        var enterprise = new Enterprise(_workInformationId, _description, _name, _country);

        // Assert
        enterprise.WorkInformationInternalId.Should().Be(_workInformationId);
        enterprise.InformationDescription.Should().Be(_description);
        enterprise.Name.Should().Be(_name);
        enterprise.Country.Should().Be(_country);
    }

    /// <summary>
    /// Ensures the constructor without ID sets all properties correctly and leaves ID at default.
    /// </summary>
    [Fact]
    public void ConstructorWithoutId_ShouldSetAllPropertiesCorrectly()
    {
        // Act
        var enterprise = new Enterprise(_description, _name, _country);

        // Assert
        enterprise.WorkInformationInternalId.Should().Be(0); // default int
        enterprise.InformationDescription.Should().Be(_description);
        enterprise.Name.Should().Be(_name);
        enterprise.Country.Should().Be(_country);
    }

    /// <summary>
    /// Ensures the constructor accepts a null name and sets it correctly.
    /// </summary>
    [Fact]
    public void ConstructorWithId_WhenNameIsNull_ShouldSetNameToNull()
    {
        // Act
        var enterprise = new Enterprise(_workInformationId, _description, null, _country);

        // Assert
        enterprise.Name.Should().BeNull();
    }

    /// <summary>
    /// Ensures the constructor accepts a null country and sets it correctly.
    /// </summary>
    [Fact]
    public void ConstructorWithId_WhenCountryIsNull_ShouldSetCountryToNull()
    {
        // Act
        var enterprise = new Enterprise(_workInformationId, _description, _name, null);

        // Assert
        enterprise.Country.Should().BeNull();
    }

    /// <summary>
    /// Ensures the constructor without ID accepts a null name and sets it correctly.
    /// </summary>
    [Fact]
    public void ConstructorWithoutId_WhenNameIsNull_ShouldSetNameToNull()
    {
        // Act
        var enterprise = new Enterprise(_description, null, _country);

        // Assert
        enterprise.Name.Should().BeNull();
    }

    /// <summary>
    /// Ensures the constructor without ID accepts a null country and sets it correctly.
    /// </summary>
    [Fact]
    public void ConstructorWithoutId_WhenCountryIsNull_ShouldSetCountryToNull()
    {
        // Act
        var enterprise = new Enterprise(_description, _name, null);

        // Assert
        enterprise.Country.Should().BeNull();
    }
}
