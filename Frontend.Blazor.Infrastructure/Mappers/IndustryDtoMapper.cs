using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;


namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Mappers;

/// <summary>
/// Provides extension methods to map between <see cref="ListIndustryDto"/> and <see cref="Industry"/> domain entities.
/// </summary>
internal static  class IndustryDtoMapper
{
    /// <summary>
    /// Maps a <see cref="ListIndustryDto"/> instance to an <see cref="Industry"/> domain entity.
    /// </summary>
    /// <param name="industryDto">The DTO to convert. Must not be <c>null</c>.</param>
    /// <returns>An instance of <see cref="Industry"/> with data copied from the DTO.</returns>
    internal static Result<Industry> ToEntity(this AddIndustryDto industryDto)
    {
        var industry = new Industry(
            Description.FromDatabase(industryDto.InformationDescription!),
            EntityName.FromDatabase(industryDto.Name!),
            industryDto.CsRelated
        );

        return Result<Industry>.Success(industry);
    }

    /// <summary>
    /// Converts an <see cref="Industry"/> instance to a <see cref="ListIndustryDto"/> representation.
    /// </summary>
    /// <param name="industry">The <see cref="Industry"/> instance to convert. Cannot be <see langword="null"/>.</param>
    /// <returns>A <see cref="ListIndustryDto"/> object containing the mapped properties from the <paramref name="industry"/>
    /// instance.</returns>
    internal static ListIndustryDto ToListDto(this Industry industry)
    {
        return new ListIndustryDto
        {
            WorkInformationInternalId = industry.WorkInformationInternalId,
            InformationDescription = industry.InformationDescription?.Content,
            Name = industry.Name?.Name,
            CsRelated = industry.CSRelated
        };
    }

    /// <summary>
    /// Converts an <see cref="Industry"/> instance to an <see cref="AddIndustryDto"/>.
    /// </summary>
    /// <remarks>This method maps the <see cref="Industry.RequiredSkills"/>, <see cref="Industry.Name"/>, and
    /// <see cref="Industry.CSRelated"/> properties to their corresponding fields in the <see
    /// cref="AddIndustryDto"/>.</remarks>
    /// <param name="industry">The <see cref="Industry"/> instance to convert. Cannot be null.</param>
    /// <returns>An <see cref="AddIndustryDto"/> containing the mapped properties from the <paramref name="industry"/> instance.</returns>
    internal static AddIndustryDto ToDto(this Industry industry)
    {
        return new AddIndustryDto
        {
            InformationDescription = industry.InformationDescription?.Content,
            Name = industry.Name?.Name,
            CsRelated = industry.CSRelated
        };
    }
}
