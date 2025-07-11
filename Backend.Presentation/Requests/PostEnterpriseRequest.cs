using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Requests;

/// <summary>
/// Represents a request to add a new enterprise to the system.
/// </summary>
/// <param name="Enterprise">The DTO containing all the information
/// of the Enterprise that the user wants to add</param>
public record class PostEnterpriseRequest(AddEnterpriseDto Enterprise);
