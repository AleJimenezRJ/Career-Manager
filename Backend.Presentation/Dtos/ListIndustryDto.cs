using UCR.ECCI.IS.VRCampus.Backend.Presentation.Visitors;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

/// <summary>
/// Data Transfer Object (DTO) that represents industry-related work information,
/// extending the base <see cref="ListWorkInformationDto"/> with the industry's name.
/// </summary>
/// <param name="WorkInformationInternalId">The internal identifier for the work information entry.</param>
/// <param name="InformationDescription">A string with a description of the industry.</param>
/// <param name="Name">The name of the industry.</param>
/// <param name="CSRelated">Indicates whether the industry is related to computer science.</param>
public record class ListIndustryDto(
    int WorkInformationInternalId,
    string InformationDescription,
    string Name,
    bool CSRelated) : ListWorkInformationDto(WorkInformationInternalId, InformationDescription)
{
    public override TResult Accept<TResult>(IListWorkInformationDtoVisitor<TResult> visitor) =>
        visitor.Visit(this);
}