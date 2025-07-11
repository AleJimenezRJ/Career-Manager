using UCR.ECCI.IS.VRCampus.Backend.Presentation.Visitors;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

/// <summary>
/// Represents an enterprise with associated work information, including its name and country.
/// </summary>
/// <remarks>This data transfer object is used to encapsulate information about an enterprise,  including its work
/// information, description, name, and country. It extends  <see cref="ListWorkInformationDto"/> to provide additional
/// enterprise-specific details.</remarks>
public record class ListEnterpriseDto(
    int WorkInformationInternalId,
    string InformationDescription,
    string Name,
    string Country) : ListWorkInformationDto(WorkInformationInternalId, InformationDescription)
{
    public override TResult Accept<TResult>(IListWorkInformationDtoVisitor<TResult> visitor) =>
        visitor.Visit(this);
}