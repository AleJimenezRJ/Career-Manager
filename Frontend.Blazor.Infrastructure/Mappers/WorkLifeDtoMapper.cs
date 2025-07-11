using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;


namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Mappers;

/// <summary>
/// Provides extension methods for mapping between WorkLife domain entities and their corresponding DTOs.
/// </summary>
/// <remarks>This static class contains methods to convert between <see cref="WorkLife"/> entities and various DTO
/// types, such as <see cref="AddWorkLifeDto"/> and <see cref="ListWorkLifeDto"/>. These methods facilitate the
/// transformation of data for use in different layers of the application, such as the database or API.</remarks>
internal static class WorkLifeDtoMapper
{
    /// <summary>
    /// Converts an <see cref="AddWorkLifeDto"/> instance to a <see cref="WorkLife"/> entity.
    /// </summary>
    /// <param name="workLifeDto">The data transfer object containing the information required to create a <see cref="WorkLife"/> entity. Cannot
    /// be null.</param>
    /// <returns>A <see cref="Result{T}"/> containing the successfully created <see cref="WorkLife"/> entity.</returns>
    internal static Result<WorkLife> ToEntity(this AddWorkLifeDto workLifeDto)
    {
        var workLife = new WorkLife(
            Description.FromDatabase(workLifeDto.InformationDescription!),
            Workers.FromDatabase(workLifeDto.AmountFemaleWorkers!),
            Workers.FromDatabase(workLifeDto.AmountMaleWorkers!)
        );

        return Result<WorkLife>.Success(workLife);
    }

    /// <summary>
    /// Converts a <see cref="WorkLife"/> instance to a <see cref="ListWorkLifeDto"/> object.
    /// </summary>
    /// <param name="workLife">The <see cref="WorkLife"/> instance to convert. Cannot be <see langword="null"/>.</param>
    /// <returns>A <see cref="ListWorkLifeDto"/> object containing the mapped properties from the <paramref name="workLife"/>
    /// instance.</returns>
    internal static ListWorkLifeDto ToListDto(this WorkLife workLife)
    {
        return new ListWorkLifeDto
        {
            WorkInformationInternalId = workLife.WorkInformationInternalId,
            InformationDescription = workLife.InformationDescription?.Content,
            AmountFemaleWorkers = workLife.AmountFemaleWorkers?.Number,
            AmountMaleWorkers = workLife.AmountMaleWorkers?.Number
        };
    }

    /// <summary>
    /// Converts a <see cref="WorkLife"/> instance to an <see cref="AddWorkLifeDto"/> object.
    /// </summary>
    /// <remarks>This method maps specific properties from the <see cref="WorkLife"/> object to the
    /// corresponding fields in the <see cref="AddWorkLifeDto"/> object. Null values in the source object are handled
    /// gracefully.</remarks>
    /// <param name="workLife">The <see cref="WorkLife"/> instance to convert. Must not be null.</param>
    /// <returns>An <see cref="AddWorkLifeDto"/> object containing the mapped properties from the <paramref name="workLife"/>
    /// instance.</returns>
    internal static AddWorkLifeDto ToAddDto(this WorkLife workLife)
    {
        return new AddWorkLifeDto
        {
            InformationDescription = workLife.InformationDescription?.Content,
            AmountFemaleWorkers = workLife.AmountFemaleWorkers?.Number,
            AmountMaleWorkers = workLife.AmountMaleWorkers?.Number
        };
    }
}
