using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;

/// <summary>
/// Represents a language entity in the domain model.
/// </summary>
public class Language
{

    /// <summary>
    /// Gets or sets the internal identifier for the language entity.
    /// This value is typically used as a primary key in the database.
    /// </summary>
    public int LanguageInternalId { get; set; }

    /// <summary>
    /// Gets or sets the value object representing the language.
    /// Ensures the language follows the domain rules and allowed values.
    /// </summary>
    public LanguageVO LanguageValue { get; set; }

    /// <summary>
    /// Gets or sets the recruitment details associated with the current entity.
    /// </summary>
    public Recruitment Recruitment { get; set; }

    public int RecruitmentPK { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Language"/> class.
    /// </summary>
    /// <param name="language">The validated language value object.</param>
    public Language(LanguageVO language)
    {
        LanguageValue = language;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Language"/> class.
    /// </summary>
    /// <remarks>This constructor is private and cannot be directly invoked. It is intended for internal use
    /// only by EF Core if needed</remarks>
    private Language() { }
}
