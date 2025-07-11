using UCR.ECCI.IS.VRCampus.Backend.Presentation.Visitors;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

/// <summary>
/// Represents the data transfer object for adding an opportunity, including required skills,  description, country, and
/// language information.
/// </summary>
/// <remarks>This DTO is used to encapsulate the details of an opportunity being added, such as the  required
/// skills, a description of the opportunity, the country where the opportunity is  located, and the language associated
/// with the opportunity.</remarks>
/// <param name="InformationDescription">Required skills for the opportunity</param>
/// <param name="Country"> The country where the opportunity is located</param>
public record class AddOpportunityDto(
    string InformationDescription,
    string Country) : AddWorkInformationDto(InformationDescription)
{
    public override TResult Accept<TResult>(IAddWorkInformationDtoVisitor<TResult> visitor) =>
        visitor.Visit(this);
}