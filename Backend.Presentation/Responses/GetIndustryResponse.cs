using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

/// <summary>
/// Represents the response containing a list of industries.
/// </summary>
/// <param name="Industries">A collection of <see cref="ListIndustryDto"/> objects, where each object represents an industry.</param>
public record class GetIndustryResponse(List<ListIndustryDto> Industries);
