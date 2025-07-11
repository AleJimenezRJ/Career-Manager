namespace UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;

/// <summary>
/// Defines the various categories of errors that can occur in the domain layer.
/// </summary>
public enum ErrorType
{
    /// <summary>
    /// Represents a general failure not classified under other specific types. Like an unexpected error or a failure to complete an operation.
    /// </summary>
    Failure = 0,

    /// <summary>
    /// Indicates that a requested resource could not be found.
    /// </summary>
    NotFound = 1,

    /// <summary>
    /// Represents a validation failure, typically due to invalid input by the user.
    /// </summary>
    Validation = 2,

    /// <summary>
    /// Indicates a conflict due to an attempt to create resource that already exists in the database.
    /// </summary>
    DuplicatedConflict = 3,

    /// <summary>
    /// Represents a concurrency conflict, such as when data is modified simultaneously by multiple sources.
    /// </summary>
    ConcurrencyConflict = 4
}
