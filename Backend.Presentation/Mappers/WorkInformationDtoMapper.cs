using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Visitors;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;

/// <summary>
/// Provides methods for mapping between DTOs and entities related to work information.
/// </summary>
/// <remarks>This class contains static methods for converting between data transfer objects (DTOs) and domain
/// entities. It supports mapping for various types of work information, including industry, opportunity, work life, and
/// recruitment.</remarks>
public static class WorkInformationDtoMapper
{
    /// <summary>
    /// Converts an <see cref="AddWorkInformationDto"/> instance to a <see cref="WorkInformation"/> entity.
    /// </summary>
    /// <param name="dto">The data transfer object containing the work information to be converted. Cannot be null.</param>
    /// <returns>A <see cref="Result{T}"/> containing the converted <see cref="WorkInformation"/> entity. The result may indicate
    /// success or failure depending on the conversion process.</returns>
    public static Result<WorkInformation> ToEntity(AddWorkInformationDto dto)
    {
        return dto.Accept(new AddDtoToEntityVisitor());
    }

    /// <summary>
    /// Maps a <see cref="WorkInformation"/> entity to its corresponding <see cref="ListWorkInformationDto"/>
    /// representation.
    /// </summary>
    /// <remarks>This method uses specific mappers for each supported derived type of <see
    /// cref="WorkInformation"/>. Ensure that the provided <paramref name="entity"/> is of a supported type to avoid
    /// exceptions.</remarks>
    /// <param name="entity">The <see cref="WorkInformation"/> instance to be mapped. Must be one of the supported derived types: <see
    /// cref="Industry"/>, <see cref="Opportunity"/>, <see cref="WorkLife"/>, or <see cref="Recruitment"/>.</param>
    /// <returns>A <see cref="ListWorkInformationDto"/> that represents the provided <paramref name="entity"/>.</returns>
    /// <exception cref="NotSupportedException">Thrown if the type of <paramref name="entity"/> is not supported for mapping.</exception>
    public static ListWorkInformationDto ToListDto(WorkInformation entity)
    {
        return entity switch
        {
            Industry industry => IndustryDtoMapper.ToListDto(industry),
            Opportunity opportunity => OpportunityDtoMapper.ToListDto(opportunity),
            WorkLife workLife => WorkLifeDtoMapper.ToListDto(workLife),
            Recruitment recruitment => RecruitmentDtoMapper.ToListDto(recruitment),
            Enterprise enterprise => EnterpriseDtoMapper.ToListDto(enterprise),
            _ => throw new NotSupportedException($"Mapper for type {entity.GetType().Name} is not implemented.")
        };
    }
}
