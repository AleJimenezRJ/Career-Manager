using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Visitors;


namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Tests.Unit.Entities;

/// <summary>
/// Provides unit tests for the <see cref="WorkInformation"/> class and its derived types.
/// </summary>
/// <remarks>This class contains tests to verify the behavior of constructors, methods, and interactions with
/// visitors for the <see cref="WorkInformation"/> class. It ensures that properties are set correctly and that visitor
/// patterns are implemented as expected.</remarks>
public class WorkInformationTests
{
    /// <summary>
    /// Tests whether the constructor of <see cref="TestWorkInformation"/> correctly initializes  the <see
    /// cref="TestWorkInformation.WorkInformationInternalId"/> and  <see
    /// cref="TestWorkInformation.InformationDescription"/> properties.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="TestWorkInformation"/> constructor assigns the
    /// provided  internal ID and description values to their respective properties. It ensures the integrity of 
    /// property initialization during object creation.</remarks>
    [Fact]
    public void Constructor_WithInternalId_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        int expectedId = 42;
        var description = Description.FromDatabase("Software Engineer role");

        // Act
        var recruitment = new TestWorkInformation(expectedId, description);

        // Assert
        recruitment.WorkInformationInternalId.Should().Be(expectedId);
        recruitment.InformationDescription.Should().Be(description);
    }

    /// <summary>
    /// Tests whether the <see cref="TestWorkInformation"/> constructor correctly sets the  <see
    /// cref="TestWorkInformation.InformationDescription"/> property when initialized with a description.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="TestWorkInformation"/> instance is created with the
    /// expected  default values, including a default internal ID of 0 and the provided description.</remarks>
    [Fact]
    public void Constructor_WithDescriptionOnly_ShouldSetDescription()
    {
        // Arrange
        var description = Description.FromDatabase("DevOps position");

        // Act
        var recruitment = new TestWorkInformation(description);

        // Assert
        recruitment.WorkInformationInternalId.Should().Be(0); // Default int value
        recruitment.InformationDescription.Should().Be(description);
    }

    /// <summary>
    /// Tests whether the <see cref="TestWorkInformation.Accept"/> method correctly triggers the visitor for a derived
    /// instance.
    /// </summary>
    /// <remarks>This test verifies that the visitor's behavior is invoked when the <see
    /// cref="TestWorkInformation"/> instance  calls the <c>Accept</c> method, ensuring proper interaction between the
    /// visitor and the derived instance.</remarks>
    [Fact]
    public void Accept_ShouldTriggerVisitorForDerivedInstance()
    {
        // Arrange
        var recruitment = new TestWorkInformation(1, Description.FromDatabase("Intern"));
        var visitor = new MockVisitor();

        // Act
        recruitment.Accept(visitor);

        // Assert
        visitor.WasVisited.Should().BeTrue();
    }

    /// <summary>
    /// Represents test-specific work information, providing functionality for handling  descriptions and interacting
    /// with visitors.
    /// </summary>
    /// <remarks>This class is a specialized implementation of <see cref="WorkInformation"/> designed  for
    /// testing purposes. It supports initialization with an ID and description or  just a description, and allows
    /// interaction with visitors through the  <see cref="Accept(IWorkInformationVisitor)"/> method.</remarks>
    private class TestWorkInformation : WorkInformation
    {
        public TestWorkInformation(int id, Description? description) : base(id, description) { }

        public TestWorkInformation(Description? description) : base(description) { }

        public override void Accept(IWorkInformationVisitor visitor)
        {
            visitor.Visit(new Recruitment(informationDescription: InformationDescription, steps: null, requisites: null, languages: new List<Language>()));
        }
    }

    /// <summary>
    /// Represents a mock implementation of the <see cref="IWorkInformationVisitor"/> interface for testing purposes.
    /// </summary>
    /// <remarks>This class is designed to simulate the behavior of a visitor in the visitor pattern,
    /// specifically for testing scenarios. It tracks whether the <see cref="Visit(Recruitment)"/> method has been
    /// called by setting the <see cref="WasVisited"/> property.</remarks>
    private class MockVisitor : IWorkInformationVisitor
    {
        public bool WasVisited { get; private set; }

        public void Visit(Enterprise _) { }

        public void Visit(WorkLife _) { }

        public void Visit(Opportunity _) { }

        public void Visit(Industry _) { }

        public void Visit(Recruitment _) => 
            WasVisited = true;
    }
}
