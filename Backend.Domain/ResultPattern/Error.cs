namespace UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;

/// <summary>
/// Represents a domain-specific error with an associated code, message, and error type.
/// </summary>
public class Error
{
    /// <summary>
    /// Gets the unique code identifying the error.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Gets the descriptive message for the error.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Gets the type of the error, indicating its nature.
    /// </summary>
    public ErrorType ErrorType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class.
    /// This constructor is private to enforce the use of static creation methods.
    /// </summary>
    /// <param name="code">The unique error code.</param>
    /// <param name="message">The human-readable error message.</param>
    /// <param name="errorType">The category of the error.</param>
    private Error(string code, string message, ErrorType errorType)
    {
        Code = code;
        Message = message;
        ErrorType = errorType;
    }

    /// <summary>
    /// Creates a new general failure error.
    /// </summary>
    /// <param name="code">The unique error code.</param>
    /// <param name="message">The error message.</param>
    /// <returns>An <see cref="Error"/> representing a failure.</returns>
    public static Error Failure(string code, string message) =>
        new(code, message, ErrorType.Failure);

    /// <summary>
    /// Creates a new "not found" error.
    /// </summary>
    /// <param name="code">The unique error code.</param>
    /// <param name="message">The error message.</param>
    /// <returns>An <see cref="Error"/> indicating that a resource was not found.</returns>
    public static Error NotFound(string code, string message) =>
        new(code, message, ErrorType.NotFound);

    /// <summary>
    /// Creates a new validation error.
    /// </summary>
    /// <param name="code">The unique error code.</param>
    /// <param name="message">The error message.</param>
    /// <returns>An <see cref="Error"/> representing a validation failure.</returns>
    public static Error Validation(string code, string message) =>
        new(code, message, ErrorType.Validation);

    /// <summary>
    /// Creates a new error representing a duplicated conflict.
    /// </summary>
    /// <param name="code">The unique error code.</param>
    /// <param name="message">The error message.</param>
    /// <returns>An <see cref="Error"/> representing a duplication conflict.</returns>
    public static Error DuplicatedConflict(string code, string message) =>
        new(code, message, ErrorType.DuplicatedConflict);

    /// <summary>
    /// Creates a new error representing a concurrency conflict.
    /// </summary>
    /// <param name="code">The unique error code.</param>
    /// <param name="message">The error message.</param>
    /// <returns>An <see cref="Error"/> representing a concurrency conflict.</returns>
    public static Error ConcurrencyConflict(string code, string message) =>
        new(code, message, ErrorType.ConcurrencyConflict);
}
