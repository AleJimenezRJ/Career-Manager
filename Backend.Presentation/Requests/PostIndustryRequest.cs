using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Requests;

/// <summary>
/// Represents a request to add a new industry.
/// </summary>
/// <param name="Industry">The details of the industry to be added.</param>
public record class PostIndustryRequest(AddIndustryDto Industry);
