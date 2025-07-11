using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Requests;

/// <summary>
/// Represents a request to add a new career to the system.
/// </summary>
/// <param name="Career">The DTO containing all the information of the career that the user wants to add</param>
public record class PostCareerRequest(AddCareerDto Career);