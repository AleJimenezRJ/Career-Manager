using System.Text.Json.Serialization;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Visitors;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

/// <summary>
/// Represents a base data transfer object (DTO) for adding work-related information.
/// </summary>
/// <remarks>This class serves as a polymorphic base type for specific work information DTOs, such as  <see
/// cref="AddIndustryDto"/>, <see cref="AddOpportunityDto"/>, <see cref="AddWorkLifeDto"/>, <see cref="AddRecruitmentDto"/>, and <see cref="AddEnterpriseDto">. The type discriminator
/// property <c>@odata.type</c> is used to determine the derived type during serialization and
/// deserialization.</remarks>
/// <param name="InformationDescription">Only request the description when adding the work information</param>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "@odata.type")]
[JsonDerivedType(typeof(AddIndustryDto), "#namespace.AddIndustryDto")]
[JsonDerivedType(typeof(AddOpportunityDto), "#namespace.AddOpportunityDto")]
[JsonDerivedType(typeof(AddWorkLifeDto), "#namespace.AddWorkLifeDto")]
[JsonDerivedType(typeof(AddRecruitmentDto), "#namespace.AddRecruitmentDto")]
[JsonDerivedType(typeof(AddEnterpriseDto), "#namespace.AddEnterpriseDto")]
public abstract record class AddWorkInformationDto(string InformationDescription)
{
    public abstract TResult Accept<TResult>(IAddWorkInformationDtoVisitor<TResult> visitor);
}