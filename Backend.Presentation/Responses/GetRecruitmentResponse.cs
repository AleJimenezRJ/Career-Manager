using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

/// <summary>
/// Represents the response containing a collection of recruitment data.
/// </summary>
/// <param name="Recruitments">A list of recruitment data transfer objects, where each item represents a recruitment entry.</param>
public record class GetRecruitmentResponse(List<ListRecruitmentDto> Recruitments);