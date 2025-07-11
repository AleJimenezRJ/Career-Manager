using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;
using static UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern.DomainErrors;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ValueObjects;

/// <summary>
/// Represents the modality of a career (Presential, Virtual, Hybrid) as a value object.
/// </summary>
public class Modality : ValueObject
{
    /// <summary>
    /// A set of valid modality values allowed in the domain.
    /// Case-insensitive.
    /// </summary>
    private static readonly HashSet<string> AllowedModalities = new(StringComparer.OrdinalIgnoreCase)
    {
        "Presential",
        "Virtual",
        "Hybrid"
    };

    /// <summary>
    /// Gets the string representation of the modality.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Modality"/> class.
    /// Constructor is private to enforce controlled creation through <see cref="Create"/>.
    /// </summary>
    /// <param name="value">The modality value.</param>
    private Modality(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new <see cref="Modality"/> instance after validating the input.
    /// </summary>
    /// <param name="value">The input string to validate and convert into a modality.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing either a valid <see cref="Modality"/> object
    /// or an error describing why the creation failed.
    /// </returns>
    public static Result<Modality> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<Modality>.Failure(
                Validation.Required(nameof(Modality))
            );
        }

        if (!AllowedModalities.Contains(value))
        {
            return Result<Modality>.Failure(
                Validation.InvalidInformation(nameof(Modality))
            );
        }

        return Result<Modality>.Success(new Modality(value));
    }

    /// <summary>
    /// Converts a database string value to a <see cref="Modality"/> instance.
    /// </summary>
    /// <param name="value">The string representation of the modality retrieved from the database. Cannot be null or empty.</param>
    /// <returns>A <see cref="Modality"/> instance corresponding to the provided database value.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the provided <paramref name="value"/> is invalid or cannot be converted to a <see cref="Modality"/>.
    /// The exception message includes details about the invalid value and the associated error.</exception>
    public static Modality FromDatabase(string value)
    {
        var result = Create(value);
        if (result.IsFailure)
        {
            throw new InvalidOperationException(
                $"Invalid Modality value found in database: '{value}'. Error: {result.Error?.Message}"
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
