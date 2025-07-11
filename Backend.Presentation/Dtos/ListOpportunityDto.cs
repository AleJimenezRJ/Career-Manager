using UCR.ECCI.IS.VRCampus.Backend.Presentation.Visitors;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

/// <summary>
/// Data Transfer Object (DTO) that represents information about available opportunities,
/// extending the base <see cref="ListWorkInformationDto"/> class with a description of the opportunities.
/// </summary>
/// <param name="WorkInformationInternalId">The internal identifier for the work information entry.</param>
/// <param name="InformationDescription">A description providing details about the available opportunities.</param>
/// <param name="Country"> The country where the opportunity is located.</param>
public record class ListOpportunityDto(
    int WorkInformationInternalId,
    string InformationDescription,
    string Country) : ListWorkInformationDto(WorkInformationInternalId, InformationDescription)
{
    public override TResult Accept<TResult>(IListWorkInformationDtoVisitor<TResult> visitor) =>
        visitor.Visit(this);
}