using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;


namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Mappers;

/// <summary>
/// Provides extension methods for mapping between DTOs and domain entities related to opportunities.
/// </summary>
/// <remarks>This class contains methods to convert between <see cref="AddOpportunityDto"/>, <see
/// cref="Opportunity"/>,  and <see cref="ListOpportunityDto"/>. These methods facilitate the transformation of data
/// between  different layers of the application, ensuring consistency and encapsulation of mapping logic.</remarks>
internal static class OpportunityDtoMapper
{
    /// <summary>
    /// Maps an <see cref="AddOpportunityDto"/> instance to an <see cref="Opportunity"/> domain entity.
    /// </summary>
    /// <param name="dto">The DTO to convert. Must not be <c>null</c>.</param>
    /// <returns>A <see cref="Result{T}"/> containing the converted <see cref="Opportunity"/> or an error if mapping failed.</returns>
    internal static Result<Opportunity> ToEntity(this AddOpportunityDto dto)
    {
        var opportunity = new Opportunity(
            Description.FromDatabase(dto.InformationDescription!),
            Country.FromDatabase(dto.Country!)
        );

        return Result<Opportunity>.Success(opportunity);
    }

    /// <summary>
    /// Converts an <see cref="Opportunity"/> instance to a <see cref="ListOpportunityDto"/> representation.
    /// </summary>
    /// <param name="opportunity">The <see cref="Opportunity"/> to convert.</param>
    /// <returns>A new <see cref="ListOpportunityDto"/> populated with data from the <paramref name="opportunity"/>.</returns>
    internal static ListOpportunityDto ToListDto(this Opportunity opportunity)
    {
        return new ListOpportunityDto
        {
            WorkInformationInternalId = opportunity.WorkInformationInternalId,
            InformationDescription = opportunity.InformationDescription?.Content,
            Country = opportunity.Country?.Value 
        };
    }

    internal static AddOpportunityDto ToDto(this Opportunity opportunity)
    {
        return new AddOpportunityDto
        {
            InformationDescription = opportunity.InformationDescription?.Content,
            Country = opportunity.Country?.Value 
        };
    }

}
