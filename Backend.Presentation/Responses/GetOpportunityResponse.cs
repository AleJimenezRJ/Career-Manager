using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

/// <summary>
/// Represents the response containing a list of opportunities retrieved from a data source.
/// </summary>
/// <param name="Opportunities">The collection of <see cref="ListOpportunityDto"/> objects included in the response. This parameter cannot be null.</param>
public record class GetOpportunityResponse(List<ListOpportunityDto> Opportunities);
