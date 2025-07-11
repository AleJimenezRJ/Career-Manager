using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Tests.Unit.Entities;

/// <summary>
/// Unit tests for the <see cref="SearchResult"/> entity.
/// Validates default initialization and property assignment.
/// </summary>
public class SearchResultTests
{
    /// <summary>
    /// Ensures all properties can be set and retrieved correctly.
    /// </summary>
    [Fact]
    public void Properties_ShouldBeSettableAndRetrievable()
    {
        // Arrange
        var result = new SearchResult
        {
            TableName = "Career",
            CareerId = 1,
            CareerName = "Computer Science",
            Field = "Modality",
            ColumnName = "Modality"
        };

        // Assert
        result.TableName.Should().Be("Career");
        result.CareerId.Should().Be(1);
        result.CareerName.Should().Be("Computer Science");
        result.Field.Should().Be("Modality");
        result.ColumnName.Should().Be("Modality");
    }

}
