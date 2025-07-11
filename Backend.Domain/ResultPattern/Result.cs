namespace UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;

/// <summary>
/// Represents the outcome of an operation that returns a result of type <typeparamref name="T"/>.
/// Encapsulates success and failure states along with optional error details.
/// </summary>
/// <typeparam name="T">The type of the value returned by the operation if successful.</typeparam>
public class Result<T>
{
    /// <summary>
    /// Indicates whether the operation completed successfully.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Indicates whether the operation failed.
    /// This is the inverse of <see cref="IsSuccess"/>.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Gets the value returned by a successful operation.
    /// Returns <c>null</c> if the operation failed.
    /// </summary>
    public T? Value { get; }

    /// <summary>
    /// Gets the primary error associated with a failed operation.
    /// Returns <c>null</c> if the operation was successful.
    /// </summary>
    public Error? Error { get; }

    /// <summary>
    /// Gets the list of errors associated with a failed operation.
    /// Returns <c>null</c> if the operation was successful or if only a single error is used.
    /// </summary>
    public List<Error>? Errors { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    /// <param name="isSuccess">Indicates whether the operation was successful.</param>
    /// <param name="value">The value returned if the operation was successful.</param>
    /// <param name="error">The error object if the operation failed.</param>
    /// <param name="errors">A list of error objects if the operation failed with multiple errors.</param>
    protected Result(bool isSuccess, T? value, Error? error, List<Error>? errors)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        Errors = errors;
    }

    /// <summary>
    /// Creates a successful result with the specified value.
    /// </summary>
    /// <param name="value">The value returned by the operation.</param>
    /// <returns>A <see cref="Result{T}"/> representing a successful operation.</returns>
    public static Result<T> Success(T value) =>
        new(true, value, null, null);

    /// <summary>
    /// Creates a failed result with a single error.
    /// </summary>
    /// <param name="error">The error associated with the failed operation.</param>
    /// <returns>A <see cref="Result{T}"/> representing a failed operation.</returns>
    public static Result<T> Failure(Error error) =>
        new(false, default, error, null);

    /// <summary>
    /// Creates a failed result with multiple errors.
    /// </summary>
    /// <param name="errors">The list of errors associated with the failed operation.</param>
    /// <returns>A <see cref="Result{T}"/> representing a failed operation.</returns>
    public static Result<T> Failure(List<Error> errors) =>
        new(false, default, null, errors);
}

/// <summary>
/// Represents the outcome of an operation that does not return a value.
/// Encapsulates success and failure states with optional error details.
/// </summary>
public class Result
{
    /// <summary>
    /// Indicates whether the operation completed successfully.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Indicates whether the operation failed.
    /// This is the inverse of <see cref="IsSuccess"/>.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Gets the primary error associated with a failed operation.
    /// Returns <c>null</c> if the operation was successful.
    /// </summary>
    public Error? Error { get; }

    /// <summary>
    /// Gets the list of errors associated with a failed operation.
    /// Returns <c>null</c> if the operation was successful or if only a single error is used.
    /// </summary>
    public List<Error>? Errors { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    /// <param name="isSuccess">Indicates whether the operation was successful.</param>
    /// <param name="error">The error object if the operation failed.</param>
    /// <param name="errors">A list of error objects if the operation failed with multiple errors.</param>
    protected Result(bool isSuccess, Error? error, List<Error>? errors)
    {
        IsSuccess = isSuccess;
        Error = error;
        Errors = errors;
    }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <returns>A <see cref="Result"/> representing a successful operation.</returns>
    public static Result Success() =>
        new(true, null, null);

    /// <summary>
    /// Creates a failed result with a single error.
    /// </summary>
    /// <param name="error">The error associated with the failed operation.</param>
    /// <returns>A <see cref="Result"/> representing a failed operation.</returns>
    public static Result Failure(Error error) =>
        new(false, error, null);

    /// <summary>
    /// Creates a failed result with multiple errors.
    /// </summary>
    /// <param name="errors">The list of errors associated with the failed operation.</param>
    /// <returns>A <see cref="Result"/> representing a failed operation.</returns>
    public static Result Failure(List<Error> errors) =>
        new(false, null, errors);
}
