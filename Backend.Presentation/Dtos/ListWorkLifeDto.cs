using UCR.ECCI.IS.VRCampus.Backend.Presentation.Visitors;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

/// <summary>
/// Represents detailed information about work life, including required skills, a description, and worker demographics.
/// </summary>
/// <remarks>This record extends <see cref="ListWorkInformationDto"/> by adding additional details such as a
/// description and the number of male and female workers. It is used to provide comprehensive information about a
/// specific work environment.</remarks>
/// <param name="WorkInformationInternalId">The internal id for the work information</param>
/// <param name="InformationDescription">A general description</param>
/// <param name="AmountFemaleWorkers"> How many workers are women usually</param>
/// <param name="AmountMaleWorkers"> How many workers are men usually</param>
public record class ListWorkLifeDto(
    int WorkInformationInternalId,
    string InformationDescription,
    int AmountFemaleWorkers,
    int AmountMaleWorkers) : ListWorkInformationDto(WorkInformationInternalId, InformationDescription)
{
    public override TResult Accept<TResult>(IListWorkInformationDtoVisitor<TResult> visitor) =>
        visitor.Visit(this);
}
