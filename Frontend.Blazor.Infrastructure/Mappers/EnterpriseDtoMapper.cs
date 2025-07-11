using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;


namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Mappers;


/// <summary>
/// Provides extension methods for mapping between enterprise-related DTOs and domain entities.
/// </summary>
/// <remarks>This static class contains methods to convert between <see cref="AddEnterpriseDto"/>, <see
/// cref="ListEnterpriseDto"/>,  and <see cref="Enterprise"/> objects. These methods facilitate the transformation of
/// data between different layers  of the application, ensuring consistency and simplifying the mapping
/// process.</remarks>
internal static class EnterpriseDtoMapper
{
    /// <summary>
    /// Converts an <see cref="AddEnterpriseDto"/> instance to an <see cref="Enterprise"/> entity.
    /// </summary>
    /// <param name="enterpriseDto">The data transfer object containing enterprise details. Cannot be null.</param>
    /// <returns>A <see cref="Result{T}"/> containing the successfully created <see cref="Enterprise"/> entity.</returns>
    internal static Result<Enterprise> ToEntity(this AddEnterpriseDto enterpriseDto)
    {
        var enterprise = new Enterprise(
            Description.FromDatabase(enterpriseDto.InformationDescription!),
            EntityName.FromDatabase(enterpriseDto.Name!),
            Country.FromDatabase(enterpriseDto.Country!)
        );

        return Result<Enterprise>.Success(enterprise);
    }

    /// <summary>
    /// Converts an <see cref="Enterprise"/> instance to a <see cref="ListEnterpriseDto"/>.
    /// </summary>
    /// <param name="enterprise">The <see cref="Enterprise"/> instance to convert. Cannot be <see langword="null"/>.</param>
    /// <returns>A <see cref="ListEnterpriseDto"/> containing the mapped properties from the specified <see cref="Enterprise"/>.</returns>
    internal static ListEnterpriseDto ToListDto(this Enterprise enterprise)
    {
        return new ListEnterpriseDto
        {
            InformationDescription = enterprise.InformationDescription?.Content,
            Name = enterprise.Name?.Name,
            Country = enterprise.Country?.Value
        };
    }

    /// <summary>
    /// Converts an <see cref="Enterprise"/> instance to an <see cref="AddEnterpriseDto"/> object.
    /// </summary>
    /// <param name="enterprise">The <see cref="Enterprise"/> instance to convert. Cannot be null.</param>
    /// <returns>An <see cref="AddEnterpriseDto"/> object containing the name, country, and description extracted from the
    /// specified <see cref="Enterprise"/> instance.</returns>
    internal static AddEnterpriseDto ToAddDto(this Enterprise enterprise)
    {
        return new AddEnterpriseDto
        {
            InformationDescription = enterprise.InformationDescription?.Content,
            Name = enterprise.Name?.Name,
            Country = enterprise.Country?.Value
        };
    }
}
