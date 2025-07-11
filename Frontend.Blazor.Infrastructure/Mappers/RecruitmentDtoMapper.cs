using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Mappers;

/// <summary>
/// Provides extension methods to map between <see cref="Recruitment"/> and its DTO representations.
/// </summary>
internal static class RecruitmentDtoMapper
{
    /// <summary>
    /// Maps an <see cref="AddRecruitmentDto"/> instance to a <see cref="Recruitment"/> domain entity.
    /// </summary>
    /// <param name="dto">The DTO to convert. Must not be <c>null</c>.</param>
    /// <returns>A <see cref="Result{T}"/> containing the converted <see cref="Recruitment"/> or an error if mapping failed.</returns>
    internal static Result<Recruitment> ToEntity(this AddRecruitmentDto dto)
    {
        // Map LanguageDto to Language using the LanguageDtoMapper
        var languageResults = dto.LanguageRequested?
            .Select(LanguageDtoMapper.ToEntity)
            .ToList() ?? new List<Result<Language>>();

        // Collect only successful mappings
        var languages = languageResults
            .Where(r => r.IsSuccess)
            .Select(r => r.Value!)
            .ToList();

        var recruitment = new Recruitment(
            Description.FromDatabase(dto.InformationDescription!),
            Description.FromDatabase(dto.Steps!),
            Description.FromDatabase(dto.Requisites!),
            languages
        );

        return Result<Recruitment>.Success(recruitment);
    }

    /// <summary>
    /// Converts a <see cref="Recruitment"/> instance to a <see cref="ListRecruitmentDto"/> representation.
    /// </summary>
    /// <param name="recruitment">The <see cref="Recruitment"/> to convert.</param>
    /// <returns>A <see cref="ListRecruitmentDto"/> populated with data from the <paramref name="recruitment"/>.</returns>
    internal static ListRecruitmentDto ToListDto(this Recruitment recruitment)
    {
        return new ListRecruitmentDto
        {
            WorkInformationInternalId = recruitment.WorkInformationInternalId,
            InformationDescription = recruitment.InformationDescription?.Content,
            Steps = recruitment.Steps?.Content,
            Requisites = recruitment.Requisites?.Content,
            LanguageRequested = recruitment.LanguageRequested
                .Select(LanguageDtoMapper.ToDto)
                .ToList()
        };
    }

    /// <summary>
    /// Converts a <see cref="Recruitment"/> instance to an <see cref="AddRecruitmentDto"/>.
    /// </summary>
    /// <param name="recruitment">The <see cref="Recruitment"/> to convert.</param>
    /// <returns>An <see cref="AddRecruitmentDto"/> populated with values from the domain model.</returns>
    internal static AddRecruitmentDto ToDto(this Recruitment recruitment)
    {
        return new AddRecruitmentDto
        {
            InformationDescription = recruitment.InformationDescription?.Content,
            Steps = recruitment.Steps?.Content,
            Requisites = recruitment.Requisites?.Content,
            LanguageRequested = recruitment.LanguageRequested
                .Select(LanguageDtoMapper.ToDto)
                .ToList()
        };
    }
}
