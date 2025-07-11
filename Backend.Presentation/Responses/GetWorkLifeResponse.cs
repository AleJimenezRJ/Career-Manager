using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

/// <summary>
/// Represents the response containing a collection of work-life data.
/// </summary>
/// <param name="WorkLives">A list of <see cref="ListWorkLifeDto"/> objects, where each object represents a unit of work-life data.</param>
public record class GetWorkLifeResponse(List<ListWorkLifeDto> WorkLives);
