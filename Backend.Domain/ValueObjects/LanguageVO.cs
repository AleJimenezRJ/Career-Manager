using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using static UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern.DomainErrors;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

/// <summary>
/// Represents a validated spoken language within the domain.
/// </summary>
public class LanguageVO : ValueObject
{
    /// <summary>
    /// A set of allowed spoken languages.
    /// Validation is case-insensitive.
    /// </summary>
    private static readonly HashSet<string> AllowedLanguages = new(StringComparer.OrdinalIgnoreCase)
    {
        "English",
        "Spanish",
        "French",
        "German",
        "Portuguese",
        "Mandarin",
        "Arabic",
        "Hindi",
        "Russian",
        "Japanese",
        "Korean",
        "Italian",
        "Dutch",
        "Turkish",
        "Swedish",
        "Norwegian",
        "Danish",
        "Finnish",
        "Polish",
        "Czech",
        "Hungarian",
        "Greek",
        "Bulgarian",
        "Romanian",
        "Ukrainian",
        "Thai",
        "Vietnamese",
        "Indonesian",
        "Malay",
        "Filipino",
        "Chinese"
    };

    /// <summary>
    /// The validated language value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Private constructor. Use <see cref="Create"/> to instantiate a new <see cref="Language"/>.
    /// </summary>
    /// <param name="value">The validated language string.</param>
    private LanguageVO(string value)
    {
        Value = value;
    }

    private LanguageVO()
    {
        Value = string.Empty;
    }

    /// <summary>
    /// Creates a new <see cref="Language"/> instance if the input is valid.
    /// </summary>
    /// <param name="value">The input language string.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> indicating success or failure.
    /// Returns failure if the value is null, empty, or not in the allowed list.
    /// </returns>
    public static Result<LanguageVO> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<LanguageVO>.Failure(
                Validation.Required(nameof(LanguageVO))
            );
        }

        if (!AllowedLanguages.Contains(value))
        {
            return Result<LanguageVO>.Failure(
                Validation.InvalidInformation(nameof(LanguageVO))
            );
        }

        return Result<LanguageVO>.Success(new LanguageVO(value));
    }

    /// <summary>
    /// Constructs a <see cref="Language"/> from a database value.
    /// Throws an exception if the value is invalid.
    /// </summary>
    /// <param name="value">The language value from the database.</param>
    /// <returns>A valid <see cref="Language"/> instance.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the database value is not a recognized language.
    /// </exception>
    public static LanguageVO FromDatabase(string value)
    {
        var result = Create(value);
        if (result.IsFailure)
        {
            throw new InvalidOperationException(
                $"Invalid Language value found in database: '{value}'. Error: {result.Error?.Message}"
            );
        }
        return result.Value!;
    }

    /// <summary>
    /// Used for equality comparisons between value objects.
    /// </summary>
    /// <returns>An enumerable of the components that define equality.</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}