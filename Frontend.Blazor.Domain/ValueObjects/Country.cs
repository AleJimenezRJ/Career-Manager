using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;
using static UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern.DomainErrors;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ValueObjects;

/// <summary>
/// Represents a validated country within the domain.
/// </summary>
public class Country : ValueObject
{
    /// <summary>
    /// A set of allowed country names.
    /// Validation is case-insensitive.
    /// </summary>
    private static readonly HashSet<string> AllowedCountries = new(StringComparer.OrdinalIgnoreCase)
    {
        "Afghanistan", "Albania", "Algeria", "Andorra", "Angola", "Argentina", "Armenia", "Australia", "Austria", "Azerbaijan",
        "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bhutan", "Bolivia",
        "Bosnia and Herzegovina", "Botswana", "Brazil", "Brunei", "Bulgaria", "Burkina Faso", "Burundi", "Cambodia", "Cameroon", "Canada",
        "Cape Verde", "Central African Republic", "Chad", "Chile", "China", "Colombia", "Comoros", "Congo", "Costa Rica", "Croatia",
        "Cuba", "Cyprus", "Czech Republic", "Denmark", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea",
        "Eritrea", "Estonia", "Eswatini", "Ethiopia", "Fiji", "Finland", "France", "Gabon", "Gambia", "Georgia",
        "Germany", "Ghana", "Greece", "Grenada", "Guatemala", "Guinea", "Guyana", "Haiti", "Honduras",
        "Hungary", "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Italy", "Jamaica",
        "Japan", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Kuwait", "Kyrgyzstan", "Laos", "Latvia", "Lebanon",
        "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Madagascar", "Malawi", "Malaysia", "Maldives",
        "Mali", "Malta", "Mauritania", "Mauritius", "Mexico", "Moldova", "Monaco", "Mongolia", "Montenegro", "Morocco",
        "Mozambique", "Myanmar", "Namibia", "Nicaragua", "Nepal", "Netherlands", "New Zealand", "Nicaragua", "Niger", "Nigeria", "North Macedonia",
        "Norway", "Oman", "Pakistan", "Palestine", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Poland", "United States"
    };

    /// <summary>
    /// The validated country value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Private constructor. Use <see cref="Create"/> to instantiate a new <see cref="Country"/>.
    /// </summary>
    /// <param name="value">The validated country string.</param>
    private Country(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new <see cref="Country"/> instance if the input is valid.
    /// </summary>
    /// <param name="value">The input country string.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> indicating success or failure.
    /// Returns failure if the value is null, empty, or not in the allowed list.
    /// </returns>
    public static Result<Country> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<Country>.Failure(
                Validation.Required(nameof(Country))
            );
        }

        if (!AllowedCountries.Contains(value))
        {
            return Result<Country>.Failure(
                Validation.InvalidInformation(nameof(Country))
            );
        }

        return Result<Country>.Success(new Country(value));
    }

    /// <summary>
    /// Constructs a <see cref="Country"/> from a database value.
    /// Throws an exception if the value is invalid.
    /// </summary>
    /// <param name="value">The country value from the database.</param>
    /// <returns>A valid <see cref="Country"/> instance.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the database value is not a recognized country.
    /// </exception>
    public static Country FromDatabase(string value)
    {
        var result = Create(value);
        if (result.IsFailure)
        {
            throw new InvalidOperationException(
                $"Invalid Country value found in database: '{value}'. Error: {result.Error?.Message}"
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
