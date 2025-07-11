using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

/// <summary>
/// Represents the response containing a collection of work information details retrieved by ID.
/// </summary>
/// <param name="WorkInformations">A list of <see cref="ListWorkInformationDto"/> objects, where each object contains details about specific work
/// information.</param>
public record class GetWorkInformationByIdResponse(List<ListWorkInformationDto> WorkInformations);
