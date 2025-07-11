using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;

/// <summary>
/// Provides methods for mapping between <see cref="Language"/> entities and <see cref="LanguageDto"/> data transfer
/// objects.
/// </summary>
/// <remarks>This class contains static methods for converting <see cref="Language"/> domain entities to <see
/// cref="LanguageDto"/> objects and vice versa. It ensures proper validation and error handling during the mapping
/// process.</remarks>
internal static class LanguageDtoMapper
{
    /// <summary>
    /// Converts a <see cref="Language"/> entity to its corresponding <see cref="LanguageDto"/> representation.
    /// </summary>
    /// <param name="entity">The <see cref="Language"/> entity to convert. Must not be null, and its <c>LanguageValue</c> property must be
    /// initialized.</param>
    /// <returns>A <see cref="LanguageDto"/> instance representing the provided <see cref="Language"/> entity.</returns>
    internal static LanguageDto ToDto(Language entity)
    {
        return new LanguageDto(
            entity.LanguageValue!.Value
        );
    }

    /// <summary>
    /// Converts a <see cref="LanguageDto"/> object to a <see cref="Language"/> entity.
    /// </summary>
    /// <param name="dto">The data transfer object containing the language value to be converted.</param>
    /// <returns>A <see cref="Result{Language}"/> object representing the outcome of the conversion.  If successful, the result
    /// contains the <see cref="Language"/> entity.  If the conversion fails, the result contains a list of errors
    /// describing the failure.</returns>
    internal static Result<Language> ToEntity(LanguageDto dto)
    {
        var errors = new List<Error>();

        var result = LanguageVO.Create(dto.LanguageValue);


        if (result.IsFailure) errors.Add(result.Error!);

        if (errors.Any())
            return Result<Language>.Failure(errors);

        var language = new Language(
            result.Value!
        );

        return Result<Language>.Success(language);
    }

}
