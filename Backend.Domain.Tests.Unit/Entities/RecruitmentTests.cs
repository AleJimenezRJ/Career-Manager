using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Tests.Unit.Entities;

/// <summary>
/// Contains unit tests for the <see cref="Recruitment"/> class, verifying its behavior and correctness.
/// </summary>
/// <remarks>This class includes tests for various constructors of the <see cref="Recruitment"/> class, ensuring
/// that properties are initialized correctly and edge cases, such as empty lists, are handled as expected.</remarks>
public class RecruitmentTests
{
    /// <summary>
    /// Tests whether the <see cref="Recruitment"/> constructor correctly initializes all properties when provided with
    /// valid input data.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="Recruitment"/> object is properly constructed with the
    /// expected values for its properties, including descriptions, steps, requisites, and requested languages. It
    /// ensures that the constructor assigns the provided arguments to the corresponding properties and validates the
    /// integrity of the object state.</remarks>
    [Fact]
    public void Constructor_WithAllDetails_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var description = Description.FromDatabase("Job Description");
        var steps = Description.FromDatabase("Step 1");
        var requisites = Description.FromDatabase("C# experience");
        var languages = new List<Language>
        {
            new Language(LanguageVO.FromDatabase("English")),
            new Language(LanguageVO.FromDatabase("Spanish"))
        };

        // Act
        var recruitment = new Recruitment(description, steps, requisites, languages);

        // Assert
        recruitment.InformationDescription.Should().Be(description);
        recruitment.Steps.Should().Be(steps);
        recruitment.Requisites.Should().Be(requisites);
        recruitment.LanguageRequested.Should().HaveCount(2);
        recruitment.LanguageRequested.Should().Contain(l => l.LanguageValue.Value == "English");
        recruitment.LanguageRequested.Should().Contain(l => l.LanguageValue.Value == "Spanish");
    }

    /// <summary>
    /// Verifies that the <see cref="Recruitment"/> constructor initializes an empty list for the  <see
    /// cref="Recruitment.LanguageRequested"/> property when provided with an empty collection of languages.
    /// </summary>
    /// <remarks>This test ensures that the <see cref="Recruitment.LanguageRequested"/> property is not null
    /// and contains no elements  when the constructor is called with an empty list of languages. It validates the
    /// default behavior of the constructor  in handling empty input for the languages parameter.</remarks>
    [Fact]
    public void Constructor_WithEmptyLanguages_ShouldInitializeEmptyList()
    {
        // Arrange
        var recruitment = new Recruitment(
            Description.FromDatabase("Description"),
            Description.FromDatabase("Steps"),
            Description.FromDatabase("Requisites"),
            new List<Language>()
        );

        // Assert
        recruitment.LanguageRequested.Should().NotBeNull();
        recruitment.LanguageRequested.Should().BeEmpty();
    }

    /// <summary>
    /// Tests that the default constructor of the <see cref="Recruitment"/> class initializes the  <see
    /// cref="Recruitment.LanguageRequested"/> property to an empty list.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="Recruitment.LanguageRequested"/> property is not null 
    /// and contains no elements when the <see cref="Recruitment"/> class is instantiated using its  default
    /// constructor.</remarks>
    [Fact]
    public void DefaultConstructor_ShouldInitializeEmptyLanguageList()
    {
        // Arrange
        var recruitment = (Recruitment)Activator.CreateInstance(typeof(Recruitment), nonPublic: true)!;

        // Assert
        recruitment.LanguageRequested.Should().NotBeNull();
        recruitment.LanguageRequested.Should().BeEmpty();
    }

}
