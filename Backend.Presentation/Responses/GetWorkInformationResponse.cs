using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

/// <summary>
/// Represents the response containing a collection of work information details.
/// </summary>
/// <remarks>This response is typically returned by an API endpoint or service method that retrieves work-related
/// information. The <see cref="WorkInformations"/> property contains the details of the work information grouped into
/// lists.</remarks>
/// <param name="WorkInformations"></param>
public record class GetWorkInformationResponse(List<ListWorkInformationDto> WorkInformations);
