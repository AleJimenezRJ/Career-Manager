using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Mappers;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Mappers;

/// <summary>
/// Provides methods for mapping polymorphic <see cref="ListWorkInformationDto"/> objects to specific domain entities.
/// </summary>
internal static class WorkInformationDtoMapper
{
    /// <summary>
    /// Maps a single <see cref="ListWorkInformationDto"/> to a known <see cref="WorkInformation"/> derived domain entity.
    /// </summary>
    /// <param name="dto">The DTO to map. Must not be <c>null</c>.</param>
    /// <returns>A <see cref="WorkInformation"/> instance representing the mapped entity.</returns>
    /// <exception cref="NotSupportedException">Thrown if the DTO type is unrecognized.</exception>
    internal static WorkInformation ToDomain(this ListWorkInformationDto dto)
    {
        return dto switch
        {
            ListIndustryDto industry => new Industry(
                industry.WorkInformationInternalId ?? 0,
                Description.FromDatabase(industry.InformationDescription!),
                EntityName.FromDatabase(industry.Name!),
                industry.CsRelated
            ),

            ListOpportunityDto opportunity => new Opportunity(
                opportunity.WorkInformationInternalId ?? 0,
                Description.FromDatabase(opportunity.InformationDescription!),
                Country.FromDatabase(opportunity.Country!)
            ),

            ListWorkLifeDto workLife => new WorkLife(
                workLife.WorkInformationInternalId ?? 0,
                Description.FromDatabase(workLife.InformationDescription!),
                Workers.FromDatabase(workLife.AmountFemaleWorkers ?? 0),
                Workers.FromDatabase(workLife.AmountMaleWorkers ?? 0)
            ),

            ListRecruitmentDto recruitment => new Recruitment(
                recruitment.WorkInformationInternalId ?? 0,
                Description.FromDatabase(recruitment.InformationDescription!),
                Description.FromDatabase(recruitment.Steps!),
                Description.FromDatabase(recruitment.Requisites!),
                recruitment.LanguageRequested?
                    .Select(LanguageDtoMapper.ToEntity)
                    .Where(r => r.IsSuccess)
                    .Select(r => r.Value!)
                    .ToList()
                ?? new List<Language>()
            ),

            ListEnterpriseDto enterprise => new Enterprise(
                enterprise.WorkInformationInternalId ?? 0,
                Description.FromDatabase(enterprise.InformationDescription!),
                EntityName.FromDatabase(enterprise.Name!),
                Country.FromDatabase(enterprise.Country!)
            ),

            _ => throw new NotSupportedException($"DTO type {dto.GetType().Name} is not supported for mapping.")
        };
    }

    /// <summary>
    /// Maps a collection of <see cref="ListWorkInformationDto"/> to domain entities.
    /// </summary>
    /// <param name="dtos">The collection of DTOs to map. Cannot be null.</param>
    /// <returns>A list of mapped <see cref="WorkInformation"/> domain entities.</returns>
    internal static IEnumerable<WorkInformation> ToDomain(this IEnumerable<ListWorkInformationDto> dtos)
    {
        return dtos.Select(dto => dto.ToDomain());
    }
}
