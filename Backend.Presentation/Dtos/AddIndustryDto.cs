using UCR.ECCI.IS.VRCampus.Backend.Presentation.Visitors;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

/// <summary>
/// Represents data required to add a new industry, including its name, required skills, and whether it is related to
/// computer science.
/// </summary>
/// <remarks>This record is used to encapsulate information about an industry being added, extending <see
/// cref="AddWorkInformationDto"/>. It includes details such as the industry's name, required skills, and whether it is
/// related to computer science.</remarks>
/// <param name="InformationDescription">Required skills in the industry</param>
/// <param name="Name"> The name of the industry</param>
/// <param name="CSRelated">If the industry is related to computer science</param>
public record class AddIndustryDto(
    string InformationDescription,
    string Name,
    bool CSRelated) : AddWorkInformationDto(InformationDescription)
{
    public override TResult Accept<TResult>(IAddWorkInformationDtoVisitor<TResult> visitor) =>
        visitor.Visit(this);
}