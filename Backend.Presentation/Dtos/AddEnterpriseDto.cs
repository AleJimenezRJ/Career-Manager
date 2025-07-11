using UCR.ECCI.IS.VRCampus.Backend.Presentation.Visitors;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

/// <summary>
/// Represents the data required to add a new enterprise.
/// </summary>
/// <remarks>This record is used to encapsulate the details of an enterprise, including its name, country,  and a
/// description. It is typically used as a data transfer object in operations that create or  register a new
/// enterprise.</remarks>
/// <param name="Name">The name of the enterprise. This value cannot be null or empty.</param>
/// <param name="Country">The country where the enterprise is located. This value cannot be null or empty.</param>
/// <param name="InformationDescription">A brief description of the enterprise. This value can be null or empty if no description is provided.</param>
public record class AddEnterpriseDto(
    string InformationDescription,
    string Name,
    string Country) : AddWorkInformationDto(InformationDescription)
{
    public override TResult Accept<TResult>(IAddWorkInformationDtoVisitor<TResult> visitor) =>
        visitor.Visit(this);
}