using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;

/// <summary>
/// Provides methods for mapping between enterprise domain entities and Data Transfer Objects (DTOs).
/// </summary>
/// <remarks>This class includes methods to convert enterprise entities to DTOs for adding or listing enterprises,
/// as well as methods to map DTOs back to enterprise entities. It ensures proper validation and error handling during
/// the mapping process.</remarks>
internal static class EnterpriseDtoMapper
{
    /// <summary>
    /// Converts an <see cref="Enterprise"/> entity to an <see cref="AddEnterpriseDto"/> object.
    /// </summary>
    /// <param name="entity">The <see cref="Enterprise"/> entity to convert. Must not be <c>null</c>.</param>
    /// <returns>An <see cref="AddEnterpriseDto"/> object containing the data from the specified <see cref="Enterprise"/> entity.</returns>
    internal static AddEnterpriseDto ToDto(Enterprise entity)
    {
        return new AddEnterpriseDto(
            entity.InformationDescription!.Content,
            entity.Name!.Name,
            entity.Country!.Value
        );
    }

    /// <summary>
    /// Converts an <see cref="Enterprise"/> entity to a <see cref="ListEnterpriseDto"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Enterprise"/> instance to convert. Must not be <c>null</c>.</param>
    /// <returns>A <see cref="ListEnterpriseDto"/> containing the identifier and name of the enterprise.</returns>
    internal static ListEnterpriseDto ToListDto(Enterprise entity)
    {
        return new ListEnterpriseDto(
            entity.WorkInformationInternalId,
            entity.InformationDescription!.Content,
            entity.Name!.Name,
            entity.Country!.Value
        );
    }

    /// <summary>
    /// Converts an <see cref="AddEnterpriseDto"/> instance to an <see cref="Enterprise"/> entity.
    /// </summary>
    /// <param name="dto">The data transfer object containing the information required to create an <see cref="Enterprise"/>.</param>
    /// <returns>A <see cref="Result{T}"/> containing the created <see cref="Enterprise"/> if the conversion is successful; 
    /// otherwise, a failure result with a collection of errors describing the issues encountered.</returns>
    internal static Result<Enterprise> ToEntity(AddEnterpriseDto dto)
    {
        var errors = new List<Error>();

        var nameResult = EntityName.Create(dto.Name);
        var countryResult = Country.Create(dto.Country);
        var descriptionResult = Description.Create(dto.InformationDescription);

        if (nameResult.IsFailure) errors.Add(nameResult.Error!);
        if (countryResult.IsFailure) errors.Add(countryResult.Error!);
        if (descriptionResult.IsFailure) errors.Add(descriptionResult.Error!);

        if (errors.Any())
            return Result<Enterprise>.Failure(errors);

        var enterprise = new Enterprise(
            descriptionResult.Value!,
            nameResult.Value!,
            countryResult.Value!
        );

        return Result<Enterprise>.Success(enterprise);
    }

}
