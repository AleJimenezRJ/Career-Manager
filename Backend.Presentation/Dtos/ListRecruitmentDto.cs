using UCR.ECCI.IS.VRCampus.Backend.Presentation.Visitors;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;


/// <summary>
/// Represents detailed recruitment information, including required skills, steps, language, content description, and
/// requisites.
/// </summary>
/// <remarks>This record is used to encapsulate recruitment-specific details for a work item. It extends <see
/// cref="ListWorkInformationDto"/> to include additional recruitment-related fields.</remarks>
/// <param name="WorkInformationInternalId">The internal identifier for the work information entry.</param>
/// <param name="Steps">A textual description of the steps in the recruitment process.</param>
/// <param name="LanguageRequested">The language relevant to the recruitment.</param>
/// <param name="Requisites">A list or summary of the requisites for applying.</param>
public record class ListRecruitmentDto(
    int WorkInformationInternalId,
    string InformationDescription,
    string Steps,
    string Requisites,
    IReadOnlyCollection<LanguageDto> LanguageRequested) : ListWorkInformationDto(WorkInformationInternalId, InformationDescription)
{
    public override TResult Accept<TResult>(IListWorkInformationDtoVisitor<TResult> visitor) =>
        visitor.Visit(this);
}