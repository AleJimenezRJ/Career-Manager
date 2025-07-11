using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;


namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

/// <summary>
/// Represents the response containing detailed information about a single work item.
/// </summary>
/// <param name="WorkInformation">The detailed information about the work item. This parameter must not be null.</param>
public record class GetSingleWorkInformationResponse(ListWorkInformationDto WorkInformation);
