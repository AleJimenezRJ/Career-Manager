namespace UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;

/// <summary>
/// Represents the result of a validation operation that failed due to one or more validation errors.
/// Inherits from <see cref="Result"/> to follow the result pattern for operations without return values.
/// This so a list of validation errors can be returned when an operation fails.
/// </summary>
public class ValidationResult : Result
{
    /// <summary>
    /// Gets the list of validation errors that caused the operation to fail.
    /// </summary>
    public List<Error> Errors { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationResult"/> class
    /// with a list of validation errors.
    /// </summary>
    /// <param name="errors">The collection of validation errors.</param>
    public ValidationResult(List<Error> errors)
        : base(false, null, errors)
    {
        Errors = errors;
    }
}
