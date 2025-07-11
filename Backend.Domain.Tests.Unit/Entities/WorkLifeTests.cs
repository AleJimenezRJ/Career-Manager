using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Tests.Unit.Entities;

/// <summary>
/// Unit tests for the <see cref="WorkLife"/> entity.
/// Validates constructor behavior and property assignments.
/// </summary>
public class WorkLifeTests
{
    private const int _workInformationId = 5;
    private readonly Description _description;
    private readonly Workers _femaleWorkers;
    private readonly Workers _maleWorkers;

    /// <summary>
    /// Initializes test dependencies for <see cref="WorkLife"/> tests.
    /// </summary>
    public WorkLifeTests()
    {
        _description = Description.FromDatabase("Work-life balance data for software companies.");
        _femaleWorkers = Workers.FromDatabase(40);
        _maleWorkers = Workers.FromDatabase(60);
    }

    /// <summary>
    /// Ensures the constructor with ID sets all properties correctly.
    /// </summary>
    [Fact]
    public void ConstructorWithId_ShouldSetAllPropertiesCorrectly()
    {
        // Act
        var workLife = new WorkLife(_workInformationId, _description, _femaleWorkers, _maleWorkers);

        // Assert
        workLife.WorkInformationInternalId.Should().Be(_workInformationId);
        workLife.InformationDescription.Should().Be(_description);
        workLife.AmountFemaleWorkers.Should().Be(_femaleWorkers);
        workLife.AmountMaleWorkers.Should().Be(_maleWorkers);
    }

    /// <summary>
    /// Ensures the constructor without ID sets all properties and defaults ID to 0.
    /// </summary>
    [Fact]
    public void ConstructorWithoutId_ShouldSetAllPropertiesCorrectly()
    {
        // Act
        var workLife = new WorkLife(_description, _femaleWorkers, _maleWorkers);

        // Assert
        workLife.WorkInformationInternalId.Should().Be(0);
        workLife.InformationDescription.Should().Be(_description);
        workLife.AmountFemaleWorkers.Should().Be(_femaleWorkers);
        workLife.AmountMaleWorkers.Should().Be(_maleWorkers);
    }

    /// <summary>
    /// Ensures the constructor allows null for both worker counts.
    /// </summary>
    [Fact]
    public void ConstructorWithId_WhenWorkerCountsAreNull_ShouldAllowNullValues()
    {
        // Act
        var workLife = new WorkLife(_workInformationId, _description, null, null);

        // Assert
        workLife.AmountFemaleWorkers.Should().BeNull();
        workLife.AmountMaleWorkers.Should().BeNull();
    }

    /// <summary>
    /// Ensures the constructor without ID allows null for both worker counts.
    /// </summary>
    [Fact]
    public void ConstructorWithoutId_WhenWorkerCountsAreNull_ShouldAllowNullValues()
    {
        // Act
        var workLife = new WorkLife(_description, null, null);

        // Assert
        workLife.AmountFemaleWorkers.Should().BeNull();
        workLife.AmountMaleWorkers.Should().BeNull();
    }

    /// <summary>
    /// Ensures that the constructor with ID can accept null description.
    /// </summary>
    [Fact]
    public void ConstructorWithId_WhenDescriptionIsNull_ShouldAllowNullDescription()
    {
        // Act
        var workLife = new WorkLife(_workInformationId, null, _femaleWorkers, _maleWorkers);

        // Assert
        workLife.InformationDescription.Should().BeNull();
    }

    /// <summary>
    /// Ensures that the constructor without ID can accept null description.
    /// </summary>
    [Fact]
    public void ConstructorWithoutId_WhenDescriptionIsNull_ShouldAllowNullDescription()
    {
        // Act
        var workLife = new WorkLife(null, _femaleWorkers, _maleWorkers);

        // Assert
        workLife.InformationDescription.Should().BeNull();
    }
}
