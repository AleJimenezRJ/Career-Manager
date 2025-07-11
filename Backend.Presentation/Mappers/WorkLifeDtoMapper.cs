using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;

/// <summary>
/// Provides methods for mapping between <see cref="WorkLife"/> entities and their corresponding Data Transfer Objects
/// (DTOs).
/// </summary>
/// <remarks>This class includes methods to convert <see cref="WorkLife"/> entities to DTOs for adding or listing
/// purposes, as well as methods to create <see cref="WorkLife"/> entities from DTOs. It ensures proper validation and
/// error handling during the mapping process.</remarks>
internal static class WorkLifeDtoMapper
{
    /// <summary>
    /// Converts a <see cref="WorkLife"/> instance to an <see cref="AddWorkLifeDto"/>.
    /// </summary>
    /// <param name="workLife">The <see cref="WorkLife"/> instance to convert. Must not be null, and its <c>RequiredSkills</c> and
    /// <c>Description</c> properties must be non-null.</param>
    /// <returns>An <see cref="AddWorkLifeDto"/> containing the required skills and description from the specified <see
    /// cref="WorkLife"/> instance.</returns>
    internal static AddWorkLifeDto ToDto(WorkLife workLife)
    {
        return new AddWorkLifeDto(
            workLife.InformationDescription!.Content,
            workLife.AmountFemaleWorkers!.Number,
            workLife.AmountMaleWorkers!.Number
        );
    }

    /// <summary>
    /// Converts a <see cref="WorkLife"/> instance to a <see cref="ListWorkLifeDto"/>.
    /// </summary>
    /// <param name="workLife">The <see cref="WorkLife"/> instance to convert. This parameter cannot be null.</param>
    /// <returns>A <see cref="ListWorkLifeDto"/> containing the relevant data from the specified <see cref="WorkLife"/> instance.</returns>
    internal static ListWorkLifeDto ToListDto(WorkLife workLife)
    {
        return new ListWorkLifeDto(
            workLife.WorkInformationInternalId,
            workLife.InformationDescription!.Content,
            workLife.AmountFemaleWorkers!.Number,
            workLife.AmountMaleWorkers!.Number
        );
    }

    /// <summary>
    /// Converts an <see cref="AddWorkLifeDto"/> instance into a <see cref="Result{T}"/> containing a <see
    /// cref="WorkLife"/> entity.
    /// </summary>
    /// <remarks>The method validates the <paramref name="dto"/> by ensuring that all required fields meet the
    /// necessary constraints.  If any validation errors occur, they are returned as part of the failure
    /// result.</remarks>
    /// <param name="dto">The data transfer object containing the information required to create a <see cref="WorkLife"/> entity.</param>
    /// <returns>A <see cref="Result{T}"/> containing the created <see cref="WorkLife"/> entity if the conversion is successful; 
    /// otherwise, a failure result containing a list of validation errors.</returns>
    internal static Result<WorkLife> ToEntity(AddWorkLifeDto dto)
    {
        var errors = new List<Error>();

        var descriptionResult = Description.Create(dto.InformationDescription);
        var amountFemaleWorkersResult = Workers.Create(dto.AmountFemaleWorkers);
        var amountMaleWorkersResult = Workers.Create(dto.AmountMaleWorkers);

        if (descriptionResult.IsFailure) errors.Add(descriptionResult.Error!);
        if (amountFemaleWorkersResult.IsFailure) errors.Add(amountFemaleWorkersResult.Error!);
        if (amountMaleWorkersResult.IsFailure) errors.Add(amountMaleWorkersResult.Error!);


        if (errors.Any())
            return Result<WorkLife>.Failure(errors);

        var workLife = new WorkLife(
            descriptionResult.Value!,
            amountFemaleWorkersResult.Value!,
            amountMaleWorkersResult.Value!
        );

        return Result<WorkLife>.Success(workLife);
    }
}
