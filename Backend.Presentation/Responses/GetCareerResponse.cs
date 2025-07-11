using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

/// <summary>
/// Represents a response to list a career to the system.
/// </summary>
/// <param name="Career">The DTO containing all the information of the career that the user wants to see</param>
public record class GetCareerResponse(ListCareerDto Career);
