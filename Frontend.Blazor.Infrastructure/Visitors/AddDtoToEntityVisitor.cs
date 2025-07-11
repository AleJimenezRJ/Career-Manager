using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Mappers;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Presentation.Utility;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Visitors;

/// <summary>
/// Provides functionality to convert various DTO types into their corresponding entity representations and upcast them
/// to <see cref="WorkInformation"/>.
/// </summary>
/// <remarks>This class implements the <see cref="IAddWorkInformationDtoVisitor{TResult}"/> interface, allowing
/// the conversion of specific DTO types, such as <see cref="AddIndustryDto"/>, <see cref="AddOpportunityDto"/>, and
/// others, into their respective entity types. The resulting entities are upcast to the base type <see
/// cref="WorkInformation"/>.</remarks>
internal class AddDtoToEntityVisitor : IAddWorkInformationDtoVisitor<Result<WorkInformation>>
{
    public Result<WorkInformation> Visit(AddIndustryDto dto) =>
        IndustryDtoMapper.ToEntity(dto).Upcast<Industry, WorkInformation>();

    public Result<WorkInformation> Visit(AddOpportunityDto dto) =>
        OpportunityDtoMapper.ToEntity(dto).Upcast<Opportunity, WorkInformation>();

    public Result<WorkInformation> Visit(AddWorkLifeDto dto) =>
        WorkLifeDtoMapper.ToEntity(dto).Upcast<WorkLife, WorkInformation>();

    public Result<WorkInformation> Visit(AddRecruitmentDto dto) =>
        RecruitmentDtoMapper.ToEntity(dto).Upcast<Recruitment, WorkInformation>();

    public Result<WorkInformation> Visit(AddEnterpriseDto dto) =>
        EnterpriseDtoMapper.ToEntity(dto).Upcast<Enterprise, WorkInformation>();
}