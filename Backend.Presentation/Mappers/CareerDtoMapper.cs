using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;

/// <summary>
/// Provides methods for mapping between <see cref="Career"/> entities and their corresponding Data Transfer Objects
/// (DTOs).
/// </summary>
/// <remarks>This class contains static methods to convert <see cref="Career"/> entities to DTOs used for adding
/// or listing careers, as well as methods to map DTOs back to <see cref="Career"/> entities. It is intended for
/// internal use within the application to facilitate data transformation between domain models and DTOs.</remarks>
internal static class CareerDtoMapper
{
    /// <summary>
    /// Converts a <see cref="Career"/> entity to an <see cref="AddCareerDto"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Career"/> entity to convert. Must not be null, and its properties must be properly initialized.</param>
    /// <returns>An <see cref="AddCareerDto"/> containing the mapped data from the specified <see cref="Career"/> entity.</returns>
    internal static AddCareerDto ToDto(Career entity)
    {
        return new AddCareerDto(
            entity.Name!.Name,
            entity.Description!.Content,
            entity.SemestersNumber!.Number,
            entity.Modality!.Value,
            entity.DegreeTitle!.Value,
            entity.IsSteam
        );
    }

    /// <summary>
    /// Converts a <see cref="Career"/> entity to a <see cref="ListCareerDto"/> object.
    /// </summary>
    /// <param name="entity">The <see cref="Career"/> entity to convert. Must not be <see langword="null"/>.</param>
    /// <returns>A <see cref="ListCareerDto"/> object containing the mapped data from the specified <see cref="Career"/> entity.</returns>
    internal static ListCareerDto ToListDto(Career entity)
    {
        return new ListCareerDto(
            entity.CareerInternalId,
            entity.Name!.Name,
            entity.Description!.Content,
            entity.SemestersNumber!.Number,
            entity.Modality!.Value,
            entity.DegreeTitle!.Value,
            entity.Scholarship!,
            entity.IsSteam
        );
    }

    /// <summary>
    /// Converts an <see cref="AddCareerDto"/> instance into a <see cref="Career"/> entity.
    /// </summary>
    /// <remarks>This method validates the input data from the <paramref name="dto"/> and ensures that all
    /// required fields meet  the necessary constraints. If any validation fails, the method returns a failure result
    /// with the corresponding errors.</remarks>
    /// <param name="dto">The data transfer object containing the information required to create a <see cref="Career"/> entity.</param>
    /// <returns>A <see cref="Result{T}"/> containing the created <see cref="Career"/> entity if the conversion is successful; 
    /// otherwise, a failure result containing a list of validation errors.</returns>
    internal static Result<Career> ToEntity(AddCareerDto dto)
    {
        var errors = new List<Error>();

        var nameResult = EntityName.Create(dto.Name);
        var descriptionResult = Description.Create(dto.Description);
        var semestersResult = SemestersNumber.Create(dto.SemestersNumber);
        var modalityResult = Modality.Create(dto.Modality);
        var degreeResult = DegreeTitle.Create(dto.DegreeTitle);

        if (nameResult.IsFailure) errors.Add(nameResult.Error!);
        if (descriptionResult.IsFailure) errors.Add(descriptionResult.Error!);
        if (semestersResult.IsFailure) errors.Add(semestersResult.Error!);
        if (modalityResult.IsFailure) errors.Add(modalityResult.Error!);
        if (degreeResult.IsFailure) errors.Add(degreeResult.Error!);

        if (errors.Any())
            return Result<Career>.Failure(errors);

        var career = new Career(
            nameResult.Value!,
            descriptionResult.Value!,
            semestersResult.Value!,
            modalityResult.Value!,
            degreeResult.Value!,
            dto.IsSteam
        );

        return Result<Career>.Success(career);
    }

}
