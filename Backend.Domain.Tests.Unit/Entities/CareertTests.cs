using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Tests.Unit.Entities;

/// <summary>
/// Unit tests for the <see cref="Career"/> entity.
/// Validates constructor behavior and property initialization.
/// </summary>
public class CareerTests
{
    private const int _careerInternalId = 1;
    private readonly EntityName _name;
    private readonly Description _description;
    private readonly SemestersNumber _semesters;
    private readonly Modality _modality;
    private readonly DegreeTitle _degree;
    private readonly decimal _scholarship;
    private readonly bool _isSteam;
    private readonly List<WorkInformation> _workInformations;

    /// <summary>
    /// Initializes dependencies for the <see cref="Career"/> tests.
    /// </summary>
    public CareerTests()
    {
        _name = EntityName.FromDatabase("Computer Science");
        _description = Description.FromDatabase("A program focused on software development and algorithms.");
        _semesters = SemestersNumber.FromDatabase(10);
        _modality = Modality.FromDatabase("Presential");
        _degree = DegreeTitle.FromDatabase("Bachelor");
        _scholarship = 1500m;
        _isSteam = true;
        _workInformations = new List<WorkInformation>();
    }

    /// <summary>
    /// Ensures the constructor with ID sets all properties correctly.
    /// </summary>
    [Fact]
    public void ConstructorWithId_ShouldSetAllPropertiesCorrectly()
    {
        // Act
        var career = new Career(
            _careerInternalId,
            _name,
            _description,
            _semesters,
            _modality,
            _degree,
            _workInformations,
            _scholarship,
            _isSteam);

        // Assert
        career.CareerInternalId.Should().Be(_careerInternalId);
        career.Name.Should().Be(_name);
        career.Description.Should().Be(_description);
        career.SemestersNumber.Should().Be(_semesters);
        career.Modality.Should().Be(_modality);
        career.DegreeTitle.Should().Be(_degree);
        career.Scholarship.Should().Be(_scholarship);
        career.IsSteam.Should().BeTrue();
        career.WorkInformations.Should().BeEquivalentTo(_workInformations);
    }

    /// <summary>
    /// Ensures the constructor without ID sets default values and initializes properties.
    /// </summary>
    [Fact]
    public void ConstructorWithoutId_ShouldSetAllPropertiesCorrectly()
    {
        // Act
        var career = new Career(
            _name,
            _description,
            _semesters,
            _modality,
            _degree,
            _isSteam);

        // Assert
        career.CareerInternalId.Should().Be(0);
        career.Name.Should().Be(_name);
        career.Description.Should().Be(_description);
        career.SemestersNumber.Should().Be(_semesters);
        career.Modality.Should().Be(_modality);
        career.DegreeTitle.Should().Be(_degree);
        career.Scholarship.Should().Be(0);
        career.IsSteam.Should().BeTrue();
        career.WorkInformations.Should().BeEmpty();
    }

    /// <summary>
    /// Ensures that an empty work information collection can be passed to the constructor.
    /// </summary>
    [Fact]
    public void ConstructorWithId_WhenWorkInformationIsEmpty_ShouldStillInitializeProperly()
    {
        // Act
        var career = new Career(
            _careerInternalId,
            _name,
            _description,
            _semesters,
            _modality,
            _degree,
            Enumerable.Empty<WorkInformation>(),
            _scholarship,
            _isSteam);

        // Assert
        career.WorkInformations.Should().BeEmpty();
    }

    /// <summary>
    /// Ensures that the constructor without ID accepts a null name and sets it correctly.
    /// </summary>
    [Fact]
    public void ConstructorWithoutId_WhenNameIsNull_ShouldSetNameToNull()
    {
        // Act
        var career = new Career(
            null,
            _description,
            _semesters,
            _modality,
            _degree,
            _isSteam);

        // Assert
        career.Name.Should().BeNull();
    }
}
