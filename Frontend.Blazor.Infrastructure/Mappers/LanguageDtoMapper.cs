using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;


namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Mappers;

/// <summary>
/// Provides methods for mapping between <see cref="LanguageDto"/> and <see cref="Language"/> objects.
/// </summary>
/// <remarks>This class contains extension methods to facilitate the conversion of data transfer objects (DTOs) 
/// to domain entities and vice versa. These methods ensure that the data is correctly transformed  while preserving the
/// integrity of the underlying values.</remarks>
internal static class LanguageDtoMapper
{
    /// <summary>
    /// Converts a <see cref="LanguageDto"/> instance to a <see cref="Language"/> entity.
    /// </summary>
    /// <param name="dto">The <see cref="LanguageDto"/> object to convert. Must not be null and must contain a valid <c>LanguageValue</c>.</param>
    /// <returns>A <see cref="Result{Language}"/> containing the successfully converted <see cref="Language"/> entity.</returns>
    internal static Result<Language> ToEntity(this LanguageDto dto)
    {
        var language = new Language(LanguageVO.FromDatabase(dto.LanguageValue!));
        return Result<Language>.Success(language);
    }

    /// <summary>
    /// Converts a <see cref="Language"/> instance to a <see cref="LanguageDto"/> representation.
    /// </summary>
    /// <param name="language">The <see cref="Language"/> instance to convert. Cannot be null.</param>
    /// <returns>A <see cref="LanguageDto"/> object containing the converted data from the <paramref name="language"/> instance.</returns>
    internal static LanguageDto ToDto(this Language language)
    {
        return new LanguageDto
        {
            LanguageValue = language.LanguageValue.Value
        };
    }

}
