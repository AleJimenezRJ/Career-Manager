using UCR.ECCI.IS.VRCampus.Backend.Presentation.Visitors;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

/// <summary>
/// Represents the data transfer object for adding work-life information, including required skills,  a description, and
/// the number of male and female workers.
/// </summary>
/// <remarks>This record is used to encapsulate work-life-related data for operations that require detailed 
/// information about the workforce and job requirements. It extends <see cref="AddWorkInformationDto"/>  to include
/// additional properties specific to work-life details.</remarks>
/// <param name="InformationDescription"> The skills used</param>
/// <param name="AmountFemaleWorkers"> How many workers are women usually</param>
/// <param name="AmountMaleWorkers"> How many workers are men usually</param>
public record class AddWorkLifeDto(
    string InformationDescription, 
    int AmountFemaleWorkers, 
    int AmountMaleWorkers) : AddWorkInformationDto(InformationDescription)
{
    public override TResult Accept<TResult>(IAddWorkInformationDtoVisitor<TResult> visitor) => 
        visitor.Visit(this);
}