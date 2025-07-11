using System.Text.Json.Serialization;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Visitors;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

/// <summary>
/// Represents a base data transfer object (DTO) for work-related information,  including an internal identifier and
/// required skills.
/// </summary>
/// <remarks>This class serves as a polymorphic base type for derived DTOs that provide  specific details about
/// various types of work information. The type discriminator  property <c>@odata.type</c> is used to identify the
/// derived type during serialization  and deserialization.</remarks>
/// <param name="WorkInformationInternalId">The internal ID that was generated when the entity was added</param>
/// <param name="InformationDescription">The InformationDescription for the work stored in the entity</param>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "@odata.type")]
[JsonDerivedType(typeof(ListIndustryDto), "#namespace.ListIndustryDto")]
[JsonDerivedType(typeof(ListOpportunityDto), "#namespace.ListOpportunityDto")]
[JsonDerivedType(typeof(ListWorkLifeDto), "#namespace.ListWorkLifeDto")]
[JsonDerivedType(typeof(ListRecruitmentDto), "#namespace.ListRecruitmentDto")]
[JsonDerivedType(typeof(ListEnterpriseDto), "#namespace.ListEnterpriseDto")]
public abstract record class ListWorkInformationDto(int WorkInformationInternalId, string InformationDescription)
{
    public abstract TResult Accept<TResult>(IListWorkInformationDtoVisitor<TResult> visitor);
}
