using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

/// <summary>
/// Represents the response containing a list of enterprises details.
/// </summary>
/// <param name="Enterprise">A collection of Enterprise details, 
/// where each item is represented as a <see cref="ListEnterpriseDto"/>.</param>
public record class GetEnterpriseListResponse(List<ListEnterpriseDto> Enterprise);
