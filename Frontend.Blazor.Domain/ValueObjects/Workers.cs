using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;
using static UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern.DomainErrors;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ValueObjects;

/// <summary>
/// Represents a value object for the number of workers, ensuring the value is valid and within the allowed range.
/// </summary>
/// <remarks>The <see cref="Workers"/> class enforces validation rules for the number of workers, ensuring it is
/// non-negative and does not exceed the maximum allowable value for an integer. Instances of this class are immutable
/// and can only be created through the <see cref="Create(int)"/> or <see cref="FromDatabase(int)"/> methods.</remarks>
public class Workers : ValueObject
{
    /// <summary>
    /// Gets the number associated with this instance.
    /// </summary>
    public int? Number { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Workers"/> class with the specified number of workers.
    /// </summary>
    /// <param name="number">The number of workers to initialize. Must be a positive integer.</param>
    private Workers(int? number)
    {
        Number = number;
    }

    /// <summary>
    /// Creates a new <see cref="Workers"/> instance with the specified number of workers.
    /// </summary>
    /// <param name="number">The number of workers to create. Must be a non-negative integer less than 2,147,483,647.</param>
    /// <returns>A <see cref="Result{T}"/> containing the created <see cref="Workers"/> instance if the operation is successful;
    /// otherwise, a failure result with validation details.</returns>
    public static Result<Workers> Create(int? number)
    {
        if (number < 0 || number >= 2147483647)
        {
            return Result<Workers>.Failure(
                Validation.InvalidNumber((int)number)
            );
        }

        return Result<Workers>.Success(new Workers(number));
    }

    /// <summary>
    /// Creates a <see cref="Workers"/> instance based on the specified number of collaborators retrieved from the
    /// database.
    /// </summary>
    /// <param name="number">The number of collaborators to initialize the <see cref="Workers"/> instance with. Must be a valid, non-negative
    /// integer.</param>
    /// <returns>A <see cref="Workers"/> instance representing the specified number of collaborators.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the specified <paramref name="number"/> is invalid or results in an error during the creation of the
    /// <see cref="Workers"/> instance. The exception message includes details about the invalid number and the
    /// associated error.</exception>
    public static Workers FromDatabase(int? number)
    {
        var result = Create(number);
        if (result.IsFailure)
        {
            throw new InvalidOperationException(
                $"Invalid number of collaborators (workers) found in database: '{number}'. Error: {result.Error?.Message}"
            );
        }
        return result.Value!;
    }

    /// <summary>
    /// Gets the equality components used for value object comparison.
    /// </summary>
    /// <returns>A sequence containing the number of semesters.</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number!;
    }
}
