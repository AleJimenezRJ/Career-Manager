using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;

/// <summary>
/// Provides methods for mapping between recruitment domain entities and Data Transfer Objects (DTOs).
/// </summary>
/// <remarks>This class contains static methods to convert recruitment-related data between domain models and DTOs
/// used for data transfer. It supports mapping in both directions: from domain entities to DTOs and from DTOs to domain
/// entities.</remarks>
internal static class RecruitmentDtoMapper
{
    /// <summary>
    /// Converts a <see cref="Recruitment"/> object to an <see cref="AddRecruitmentDto"/> object.
    /// </summary>
    /// <param name="recruitment">The <see cref="Recruitment"/> object to convert. Must not be null, and its properties must be initialized.</param>
    /// <returns>An <see cref="AddRecruitmentDto"/> object containing the data from the specified <see cref="Recruitment"/>
    /// object.</returns>
    internal static AddRecruitmentDto ToDto(Recruitment recruitment)
    {
        return new AddRecruitmentDto(
            recruitment.InformationDescription!.Content,
            recruitment.Steps!.Content,
            recruitment.Requisites!.Content,
            recruitment.LanguageRequested
                .Select(LanguageDtoMapper.ToDto)
                .ToList()
                .AsReadOnly()

        );
    }

    /// <summary>
    /// Converts a <see cref="Recruitment"/> object to a <see cref="ListRecruitmentDto"/>.
    /// </summary>
    /// <param name="recruitment">The <see cref="Recruitment"/> object to convert. Must not be null, and its required properties must be
    /// initialized.</param>
    /// <returns>A <see cref="ListRecruitmentDto"/> containing the relevant data from the specified <see cref="Recruitment"/>
    /// object.</returns>
    internal static ListRecruitmentDto ToListDto(Recruitment recruitment)
    {
        return new ListRecruitmentDto(
            recruitment.WorkInformationInternalId,
            recruitment.InformationDescription!.Content,
            recruitment.Steps!.Content,
            recruitment.Requisites!.Content,
            recruitment.LanguageRequested
                .Select(LanguageDtoMapper.ToDto)
                .ToList()
                .AsReadOnly()
        );
    }

    /// <summary>
    /// Converts an <see cref="AddRecruitmentDto"/> instance into a <see cref="Result{T}"/> containing a <see
    /// cref="Recruitment"/> entity.
    /// </summary>
    /// <remarks>This method validates the properties of the provided <paramref name="dto"/> and attempts to
    /// create a  <see cref="Recruitment"/> entity. If any validation fails, the method returns a failure result with
    /// the  corresponding errors.</remarks>
    /// <param name="dto">The data transfer object containing the recruitment details to be converted.</param>
    /// <returns>A <see cref="Result{T}"/> containing a <see cref="Recruitment"/> entity if the conversion is successful; 
    /// otherwise, a failure result containing a list of errors encountered during the conversion.</returns>
    internal static Result<Recruitment> ToEntity(AddRecruitmentDto dto)
    {
        var errors = new List<Error>();

        var stepsResult = Description.Create(dto.Steps);
        var contentDescriptionResult = Description.Create(dto.InformationDescription);
        var requisitesResult = Description.Create(dto.Requisites);

        if (stepsResult.IsFailure) errors.Add(stepsResult.Error!);
        if (contentDescriptionResult.IsFailure) errors.Add(contentDescriptionResult.Error!);
        if (requisitesResult.IsFailure) errors.Add(requisitesResult.Error!);

        // Map LanguageDtos to Language entities and collect errors
        var languageResults = dto.LanguageRequested
            .Select(LanguageDtoMapper.ToEntity)
            .ToList();

        if (errors.Any())
            return Result<Recruitment>.Failure(errors);

        var languages = languageResults
            .Where(x => x.IsSuccess)
            .Select(x => x.Value!)
            .ToList();

        var recruitment = new Recruitment(
            contentDescriptionResult.Value!,
            stepsResult.Value!,
            requisitesResult.Value!,
            languages
        );

        return Result<Recruitment>.Success(recruitment);
    }
}
