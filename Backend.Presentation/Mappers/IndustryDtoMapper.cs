using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;

/// <summary>
/// Provides methods for mapping between domain entities and Data Transfer Objects (DTOs) related to the <see
/// cref="Industry"/> type.
/// </summary>
/// <remarks>This class includes methods for converting an <see cref="Industry"/> entity to various DTO
/// representations and for creating an <see cref="Industry"/> entity from a DTO. It ensures proper validation and error
/// handling during the mapping process.</remarks>
internal static class IndustryDtoMapper
{
    /// <summary>
    /// Converts an <see cref="Industry"/> object to an <see cref="AddIndustryDto"/> object.
    /// </summary>
    /// <param name="industry">The <see cref="Industry"/> instance to convert. Must not be null, and its <c>RequiredSkills</c> and <c>Name</c>
    /// properties must be non-null.</param>
    /// <returns>An <see cref="AddIndustryDto"/> instance containing the converted data from the specified <see
    /// cref="Industry"/>.</returns>
    internal static AddIndustryDto ToDto(Industry industry)
    {
        return new AddIndustryDto(
            industry.InformationDescription!.Content,
            industry.Name!.Name,
            industry.CSRelated
        );
    }

    /// <summary>
    /// Converts an <see cref="Industry"/> object to a <see cref="ListIndustryDto"/>.
    /// </summary>
    /// <param name="industry">The <see cref="Industry"/> instance to convert. Must not be <see langword="null"/>.</param>
    /// <returns>A <see cref="ListIndustryDto"/> containing the relevant data from the specified <see cref="Industry"/>.</returns>
    internal static ListIndustryDto ToListDto(Industry industry)
    {
        return new ListIndustryDto(
            industry.WorkInformationInternalId,
            industry.InformationDescription!.Content,
            industry.Name!.Name,
            industry.CSRelated
        );
    }

    /// <summary>
    /// Converts an <see cref="AddIndustryDto"/> instance into a domain <see cref="Industry"/> entity.
    /// </summary>
    /// <remarks>This method validates the input data from the <paramref name="dto"/> before creating the <see
    /// cref="Industry"/> entity. If any validation errors occur, they are returned as part of the failure
    /// result.</remarks>
    /// <param name="dto">The data transfer object containing the information required to create an <see cref="Industry"/> entity.</param>
    /// <returns>A <see cref="Result{T}"/> containing the created <see cref="Industry"/> entity if the conversion is successful;
    /// otherwise, a failure result containing a list of validation errors.</returns>
    internal static Result<Industry> ToEntity(AddIndustryDto dto)
    {
        var errors = new List<Error>();

        var descriptionResult = Description.Create(dto.InformationDescription);
        var nameResult = EntityName.Create(dto.Name);

        if (descriptionResult.IsFailure) errors.Add(descriptionResult.Error!);
        if (nameResult.IsFailure) errors.Add(nameResult.Error!);

        if (errors.Any())
            return Result<Industry>.Failure(errors);

        var industry = new Industry(
            descriptionResult.Value!,
            nameResult.Value!,
            dto.CSRelated
        );

        return Result<Industry>.Success(industry);
    }
}
