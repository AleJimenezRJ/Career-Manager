using UCR.ECCI.IS.VRCampus.Backend.Presentation.Visitors;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

/// <summary>
/// Represents the data transfer object for adding recruitment information.
/// </summary>
/// <remarks>This DTO encapsulates details about recruitment, including a description, steps, requisites,  and
/// requested languages. It is used to provide structured data for recruitment-related operations.</remarks>
/// <param name="InformationDescription">A description of the recruitment information. This provides an overview of the recruitment process or details.</param>
/// <param name="Steps">The steps involved in the recruitment process. This should outline the sequence of actions or procedures.</param>
/// <param name="Requisites">The requisites or requirements for the recruitment process. This may include qualifications, documents, or other
/// criteria.</param>
/// <param name="LanguageRequested">A collection of languages requested for the recruitment process. Each language is represented as a <see
/// cref="LanguageDto"/>.</param>
public record class AddRecruitmentDto(
    string InformationDescription,
    string Steps,
    string Requisites,
    IReadOnlyCollection<LanguageDto> LanguageRequested) : AddWorkInformationDto(InformationDescription)
{
    public override TResult Accept<TResult>(IAddWorkInformationDtoVisitor<TResult> visitor) =>
        visitor.Visit(this);
}