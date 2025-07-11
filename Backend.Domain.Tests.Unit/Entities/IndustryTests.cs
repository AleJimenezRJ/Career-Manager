using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Tests.Unit.Entities;

/// <summary>
/// Unit tests for the <see cref="Industry"/> entity.
/// Validates constructor behavior and property initialization.
/// </summary>
public class IndustryTests
{
    private const int _workInformationId = 42;
    private readonly Description _description;
    private readonly EntityName _name;
    private readonly bool _csRelated;

    /// <summary>
    /// Initializes dependencies for the <see cref="Industry"/> tests.
    /// </summary>
    public IndustryTests()
    {
        _description = Description.FromDatabase("Software development and tech industry.");
        _name = EntityName.FromDatabase("Tech Industry");
        _csRelated = true;
    }

    /// <summary>
    /// Ensures the constructor with ID sets all properties correctly.
    /// </summary>
    [Fact]
    public void ConstructorWithId_ShouldSetAllPropertiesCorrectly()
    {
        // Act
        var industry = new Industry(_workInformationId, _description, _name, _csRelated);

        // Assert
        industry.WorkInformationInternalId.Should().Be(_workInformationId);
        industry.InformationDescription.Should().Be(_description);
        industry.Name.Should().Be(_name);
        industry.CSRelated.Should().BeTrue();
    }

    /// <summary>
    /// Ensures the constructor without ID sets all properties correctly and defaults ID to 0.
    /// </summary>
    [Fact]
    public void ConstructorWithoutId_ShouldSetAllPropertiesCorrectly()
    {
        // Act
        var industry = new Industry(_description, _name, _csRelated);

        // Assert
        industry.WorkInformationInternalId.Should().Be(0);
        industry.InformationDescription.Should().Be(_description);
        industry.Name.Should().Be(_name);
        industry.CSRelated.Should().BeTrue();
    }

    /// <summary>
    /// Ensures the constructor with ID can accept a null name.
    /// </summary>
    [Fact]
    public void ConstructorWithId_WhenNameIsNull_ShouldSetNameToNull()
    {
        // Act
        var industry = new Industry(_workInformationId, _description, null, _csRelated);

        // Assert
        industry.Name.Should().BeNull();
    }

    /// <summary>
    /// Ensures the constructor without ID can accept a null name.
    /// </summary>
    [Fact]
    public void ConstructorWithoutId_WhenNameIsNull_ShouldSetNameToNull()
    {
        // Act
        var industry = new Industry(_description, null, _csRelated);

        // Assert
        industry.Name.Should().BeNull();
    }

    /// <summary>
    /// Ensures the CSRelated property is correctly set to false.
    /// </summary>
    [Fact]
    public void ConstructorWithId_WhenCSRelatedIsFalse_ShouldSetPropertyToFalse()
    {
        // Act
        var industry = new Industry(_workInformationId, _description, _name, false);

        // Assert
        industry.CSRelated.Should().BeFalse();
    }

    /// <summary>
    /// Ensures the CSRelated property is correctly set to false using constructor without ID.
    /// </summary>
    [Fact]
    public void ConstructorWithoutId_WhenCSRelatedIsFalse_ShouldSetPropertyToFalse()
    {
        // Act
        var industry = new Industry(_description, _name, false);

        // Assert
        industry.CSRelated.Should().BeFalse();
    }
}
