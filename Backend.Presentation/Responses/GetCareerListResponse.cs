using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

/// <summary>
/// Represents the response containing a list of career details.
/// </summary>
/// <param name="Career">A collection of career details, where each item is represented as a <see cref="ListCareerDto"/>.</param>
public record class GetCareerListResponse(List<ListCareerDto> Career);
