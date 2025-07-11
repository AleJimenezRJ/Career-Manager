using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Requests;

/// <summary>
/// Represents a request to create a new opportunity.
/// </summary>
/// <remarks>This record encapsulates the data required to add a new opportunity, as defined by the <see
/// cref="AddOpportunityDto"/>.</remarks>
/// <param name="Opportunity">The details of the opportunity to be added. This parameter cannot be null.</param>
public record class PostOpportunityRequest(AddOpportunityDto Opportunity);
