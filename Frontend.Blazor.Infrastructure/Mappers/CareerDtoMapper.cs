using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;


namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Mappers;

/// <summary>
/// Provides extension methods for mapping between career-related DTOs and entity models.
/// </summary>
/// <remarks>This static class contains methods to convert between <see cref="AddCareerDto"/>, <see
/// cref="ListCareerDto"/>,  and <see cref="Career"/> objects. These methods facilitate the transformation of data for
/// use in different layers  of the application, such as mapping input DTOs to domain entities or preparing entities for
/// output as DTOs.</remarks>
internal static class CareerDtoMapper
{
    /// <summary>
    /// Converts an <see cref="AddCareerDto"/> instance to a <see cref="Career"/> entity.
    /// </summary>
    /// <remarks>This method maps the properties of <paramref name="careerDto"/> to their corresponding domain
    /// representations and constructs a <see cref="Career"/> entity. The resulting entity is wrapped in a <see
    /// cref="Result{T}"/> to indicate success.</remarks>
    /// <param name="careerDto">The data transfer object containing career information to be converted. Cannot be null.</param>
    /// <returns>A <see cref="Result{T}"/> containing the successfully created <see cref="Career"/> entity.</returns>
    internal static Result<Career> ToEntity(this AddCareerDto careerDto)
    {
        var career = new Career(
            EntityName.FromDatabase(careerDto.Name!),
            Description.FromDatabase(careerDto.Description!),
            SemestersNumber.FromDatabase(careerDto.SemestersNumber!),
            Modality.FromDatabase(careerDto.Modality!),
            DegreeTitle.FromDatabase(careerDto.DegreeTitle!),
            careerDto.IsSteam
        );

        return Result<Career>.Success(career);
    }

    /// <summary>
    /// Converts a <see cref="ListCareerDto"/> instance to a <see cref="Career"/> entity.
    /// </summary>
    /// <remarks>This method maps the properties of the <see cref="ListCareerDto"/> to the corresponding
    /// domain entity <see cref="Career"/>. It ensures that the data is transformed using domain-specific value
    /// objects.</remarks>
    /// <param name="careerDto">The data transfer object containing career information to be converted. Cannot be null.</param>
    /// <returns>A <see cref="Result{T}"/> containing the successfully created <see cref="Career"/> entity.</returns>
    internal static Result<Career> ToEntity(this ListCareerDto careerDto)
    {
        var career = new Career(
            careerDto.CareerInternalId,
            EntityName.FromDatabase(careerDto.Name!),
            Description.FromDatabase(careerDto.Description!),
            SemestersNumber.FromDatabase(careerDto.SemestersNumber!),
            Modality.FromDatabase(careerDto.Modality!),
            DegreeTitle.FromDatabase(careerDto.DegreeTitle!),
            careerDto.Scholarship,
            careerDto.IsSteam
        );

        return Result<Career>.Success(career);
    }

    /// <summary>
    /// Converts a collection of <see cref="ListCareerDto"/> objects to a collection of <see cref="Career"/> entities.
    /// </summary>
    /// <remarks>This method transforms each <see cref="ListCareerDto"/> into a corresponding <see
    /// cref="Career"/> entity by mapping its properties using the appropriate database conversion methods.</remarks>
    /// <param name="careerDtos">The collection of <see cref="ListCareerDto"/> objects to convert. Each object must have non-null values for its
    /// properties.</param>
    /// <returns>A collection of <see cref="Career"/> entities created from the provided <see cref="ListCareerDto"/> objects.</returns>
    internal static IEnumerable<Career> ToEntity(this IEnumerable<ListCareerDto> careerDtos)
    {
        return careerDtos.Select(careerDto => new Career(
            careerDto.CareerInternalId,
            EntityName.FromDatabase(careerDto.Name!),
            Description.FromDatabase(careerDto.Description!),
            SemestersNumber.FromDatabase(careerDto.SemestersNumber!),
            Modality.FromDatabase(careerDto.Modality!),
            DegreeTitle.FromDatabase(careerDto.DegreeTitle!),
            careerDto.Scholarship,
            careerDto.IsSteam
        ));
    }

    /// <summary>
    /// Converts a collection of <see cref="SearchResultDto"/> objects to a collection of <see cref="SearchResult"/>
    /// objects.
    /// </summary>
    /// <remarks>Each <see cref="SearchResultDto"/> in the input collection is transformed into a <see
    /// cref="SearchResult"/>  using its properties. Ensure that the input collection is not null and that the
    /// properties of each  <see cref="SearchResultDto"/> are properly initialized to avoid runtime
    /// exceptions.</remarks>
    /// <param name="searchResultDtos">The collection of <see cref="SearchResultDto"/> objects to convert. Cannot be null.</param>
    /// <returns>A collection of <see cref="SearchResult"/> objects, where each object is mapped from the corresponding <see
    /// cref="SearchResultDto"/>.</returns>
    internal static IEnumerable<SearchResult> ToEntity(this IEnumerable<SearchResultDto> searchResultDtos)
    {
        return searchResultDtos.Select(dto => new SearchResult(
            dto.CareerId,
            dto.CareerName!,
            dto.TableName!,
            dto.ColumnName!,
            dto.Field!
        ));
    }

    /// <summary>
    /// Converts a <see cref="Career"/> instance to a <see cref="ListCareerDto"/> representation.
    /// </summary>
    /// <param name="career">The <see cref="Career"/> instance to convert. Cannot be <see langword="null"/>.</param>
    /// <returns>A <see cref="ListCareerDto"/> object containing the mapped properties from the <see cref="Career"/> instance.</returns>
    internal static ListCareerDto ToListDto(this Career career)
    {
        return new ListCareerDto
        {
            CareerInternalId = career.CareerInternalId,
            Name = career.Name?.Name,
            Description = career.Description?.Content,
            SemestersNumber = career.SemestersNumber?.Number,
            Modality = career.Modality?.Value,
            DegreeTitle = career.DegreeTitle?.Value,
            Scholarship = (double?)career.Scholarship,
            IsSteam = career.IsSteam
        };
    }

    /// <summary>
    /// Converts a <see cref="Career"/> instance to an <see cref="AddCareerDto"/> object.
    /// </summary>
    /// <param name="career">The <see cref="Career"/> instance to convert. Cannot be null.</param>
    /// <returns>An <see cref="AddCareerDto"/> object containing the mapped properties from the <paramref name="career"/>
    /// instance.</returns>
    internal static AddCareerDto ToAddDto(this Career career)
    {
        return new AddCareerDto
        {
            Name = career.Name?.Name,
            Description = career.Description?.Content,
            SemestersNumber = career.SemestersNumber?.Number,
            Modality = career.Modality?.Value,
            DegreeTitle = career.DegreeTitle?.Value,
            IsSteam = career.IsSteam
        };
    }

}
