using Microsoft.EntityFrameworkCore;
namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;

/// <summary>
/// Represents the result of a search operation, containing details about the matched entity and its associated
/// metadata.
/// </summary>
/// <remarks>This class provides information about the entity found during a search, including its table name,
/// career details,  field, and column name. It is typically used to encapsulate search results in applications that
/// query structured data.</remarks>

[Keyless]
public class SearchResult
{
    /// <summary>
    /// Gets or sets the name of the database table associated with the current entity.
    /// </summary>
    public string TableName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the unique identifier for a career.
    /// </summary>
    public int CareerId { get; set; }

    /// <summary>
    /// Gets or sets the name of the career associated with this entity.
    /// </summary>
    public string CareerName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the value of the field.
    /// </summary>
    public string Field { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the database column associated with this entity.
    /// </summary>
    public string ColumnName { get; set; } = null!;
}
