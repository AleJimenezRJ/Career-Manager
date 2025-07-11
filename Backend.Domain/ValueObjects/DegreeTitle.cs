using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using static UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern.DomainErrors;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

/// <summary>
/// Represents the academic degree title awarded upon completing a career program.
/// </summary>
public class DegreeTitle : ValueObject
{
    /// <summary>
    /// List of allowed academic degree titles. Case-insensitive.
    /// </summary>
    private static readonly HashSet<string> AllowedTitles = new(StringComparer.OrdinalIgnoreCase)
    {
        "Bachelor",
        "Licentiate",
        "Master",
        "Doctorate",
        "PhD",
        "Associate",
        "Diploma",
        "Technical"
    };

    /// <summary>
    /// Gets the value of the degree title.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Private constructor to enforce controlled creation.
    /// </summary>
    /// <param name="value">The validated degree title.</param>
    private DegreeTitle(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Factory method for creating a validated <see cref="DegreeTitle"/> instance.
    /// </summary>
    /// <param name="value">The academic title to validate.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing a valid <see cref="DegreeTitle"/> or an error.
    /// </returns>
    public static Result<DegreeTitle> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<DegreeTitle>.Failure(
                Validation.Required(nameof(DegreeTitle))
            );
        }

        if (!AllowedTitles.Contains(value))
        {
            return Result<DegreeTitle>.Failure(
                Validation.InvalidInformation(nameof(DegreeTitle))
            );
        }

        return Result<DegreeTitle>.Success(new DegreeTitle(value));
    }

    /// <summary>
    /// Converts a string value from the database into a <see cref="DegreeTitle"/> instance.
    /// </summary>
    /// <param name="value">The string representation of the degree title retrieved from the database.</param>
    /// <returns>A <see cref="DegreeTitle"/> instance corresponding to the provided database value.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the provided <paramref name="value"/> is invalid or cannot be converted into a <see
    /// cref="DegreeTitle"/>.</exception>
    public static DegreeTitle FromDatabase(string value)
    {
        var result = Create(value);
        if (result.IsFailure)
        {
            throw new InvalidOperationException(
                $"Invalid degree title found in database: '{value}'. Error: {result.Error?.Message}"
            );
        }
        return result.Value!;
    }

    /// <summary>
    /// Gets the equality components used to compare value object instances.
    /// </summary>
    /// <returns>A sequence of components that define object equality.</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
