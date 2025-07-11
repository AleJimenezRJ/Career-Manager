using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Utility;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Visitors;

/// <summary>
/// Provides functionality to convert various DTO types into their corresponding entity representations and upcast them
/// to <see cref="WorkInformation"/>.
/// </summary>
/// <remarks>This class implements the <see cref="IAddWorkInformationDtoVisitor{TResult}"/> interface, allowing it
/// to handle multiple types of DTOs and map them to their respective entities. Each mapping operation returns a <see
/// cref="Result{T}"/> containing the converted <see cref="WorkInformation"/> entity.</remarks>
public class AddDtoToEntityVisitor : IAddWorkInformationDtoVisitor<Result<WorkInformation>>
{
    /// <summary>
    /// Converts the provided <see cref="AddWorkLifeDto"/> to a <see cref="WorkInformation"/> entity.
    /// </summary>
    /// <param name="dto">The data transfer object containing work-life information to be mapped.</param>
    /// <returns>A <see cref="Result{WorkInformation}"/> containing the mapped work-life information.</returns>
    public Result<WorkInformation> Visit(AddWorkLifeDto dto) =>
        WorkLifeDtoMapper.ToEntity(dto).Upcast<WorkLife, WorkInformation>();

    /// <summary>
    /// Converts the provided <see cref="AddIndustryDto"/> to an <see cref="Industry"/> entity and upcasts it to <see
    /// cref="WorkInformation"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing industry information to be converted.</param>
    /// <returns>A <see cref="Result{WorkInformation}"/> containing the upcasted <see cref="WorkInformation"/> entity.</returns>
    public Result<WorkInformation> Visit(AddIndustryDto dto) =>
        IndustryDtoMapper.ToEntity(dto).Upcast<Industry, WorkInformation>();

    /// <summary>
    /// Converts the provided <see cref="AddOpportunityDto"/> to a <see cref="WorkInformation"/> result.
    /// </summary>
    /// <param name="dto">The data transfer object containing information about the opportunity to be added.</param>
    /// <returns>A <see cref="Result{WorkInformation}"/> containing the mapped work information.</returns>
    public Result<WorkInformation> Visit(AddOpportunityDto dto) =>
        OpportunityDtoMapper.ToEntity(dto).Upcast<Opportunity, WorkInformation>();

    /// <summary>
    /// Converts the provided recruitment data transfer object (DTO) into a domain entity and retrieves work
    /// information.
    /// </summary>
    /// <remarks>This method maps the input DTO to a recruitment entity and performs an upcast to obtain work
    /// information.  Ensure that the <paramref name="dto"/> contains valid data required for the mapping
    /// process.</remarks>
    /// <param name="dto">The recruitment data transfer object containing information to be mapped and processed.</param>
    /// <returns>A <see cref="Result{WorkInformation}"/> containing the work information derived from the recruitment entity.</returns>
    public Result<WorkInformation> Visit(AddRecruitmentDto dto) =>
        RecruitmentDtoMapper.ToEntity(dto).Upcast<Recruitment, WorkInformation>();

    /// <summary>
    /// Converts the provided enterprise data transfer object (DTO) into a domain entity and upcasts it to a
    /// higher-level type.
    /// </summary>
    /// <param name="dto">The enterprise DTO containing the data to be mapped and processed. Cannot be null.</param>
    /// <returns>A <see cref="Result{T}"/> containing the upcasted <see cref="WorkInformation"/> entity derived from the provided
    /// DTO.</returns>
    public Result<WorkInformation> Visit(AddEnterpriseDto dto) =>
        EnterpriseDtoMapper.ToEntity(dto).Upcast<Enterprise, WorkInformation>();
}
