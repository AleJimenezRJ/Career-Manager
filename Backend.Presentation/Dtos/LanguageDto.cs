namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

/// <summary>
/// Represents a data transfer object for a language.
/// </summary>
/// <remarks>This class is typically used to encapsulate language-related information for transfer between
/// different layers or systems. The <see cref="LanguageValue"/> property contains the language value as a
/// string.</remarks>
/// <param name="LanguageValue">The string representation of the language. Cannot be null or empty.</param>
public record class LanguageDto(string LanguageValue);