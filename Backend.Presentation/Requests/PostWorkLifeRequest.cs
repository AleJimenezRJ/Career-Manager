using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Requests;

/// <summary>
/// Represents a request to add a new work-life balance entry.
/// </summary>
/// <param name="WorkLife">The details of the worklife</param>
public record class PostWorkLifeRequest(AddWorkLifeDto WorkLife);
