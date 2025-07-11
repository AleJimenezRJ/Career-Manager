namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

/// <summary>
/// Represents a match result from a global keyword search.
/// </summary>
/// <param name="CareerId">The internal ID of the matched career.</param>
/// <param name="CareerName">The name of the career where the match was found.</param>
/// <param name="TableName">The name of the table where the match occurred.</param>
/// <param name="ColumnName">The column name that contains the matching value.</param>
/// <param name="Field">The actual matched field content.</param>
public record class SearchResultDto(
    int CareerId,
    string CareerName,
    string TableName,
    string ColumnName,
    string Field
);
