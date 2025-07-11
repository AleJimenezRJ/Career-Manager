using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;
using static UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern.DomainErrors;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ValueObjects;

/// <summary>
/// Represents the number of semesters in a program or career as a value object.
/// </summary>
public class SemestersNumber : ValueObject
{
    /// <summary>
    /// Gets the number of semesters.
    /// </summary>
    public int? Number { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SemestersNumber"/> class.
    /// Constructor is private to enforce validation through the <see cref="Create"/> method.
    /// </summary>
    /// <param name="number">The number of semesters.</param>
    private SemestersNumber(int? number)
    {
        Number = number;
    }

    /// <summary>
    /// Creates a new <see cref="SemestersNumber"/> instance after validating the input.
    /// </summary>
    /// <param name="number">The number of semesters to validate.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> that contains either a valid <see cref="SemestersNumber"/> object
    /// or an <see cref="Error"/> describing why the creation failed.
    /// </returns>
    public static Result<SemestersNumber> Create(int? number)
    {
        if (number <= 0 || number >= 200)
        {
            return Result<SemestersNumber>.Failure(
                Validation.InvalidNumber((int)number)
            );
        }

        return Result<SemestersNumber>.Success(new SemestersNumber(number));
    }

    /// <summary>
    /// Converts a database value to a <see cref="SemestersNumber"/> instance.
    /// </summary>
    /// <param name="number">The number of semesters retrieved from the database.</param>
    /// <returns>A <see cref="SemestersNumber"/> instance representing the specified number of semesters.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the provided <paramref name="number"/> is invalid and cannot be converted to a <see
    /// cref="SemestersNumber"/>. The exception message includes details about the invalid value and the associated
    /// error.</exception>
    public static SemestersNumber FromDatabase(int? number)
    {
        var result = Create(number);
        if (result.IsFailure)
        {
            throw new InvalidOperationException(
                $"Invalid number of semesters found in database: '{number}'. Error: {result.Error?.Message}"
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
        yield return Number;
    }
}
