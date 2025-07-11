using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;

/// <summary>
/// Provides methods for mapping between domain entities and Data Transfer Objects (DTOs) related to opportunities.
/// </summary>
/// <remarks>This class includes methods to convert an <see cref="Opportunity"/> entity to various DTO
/// representations and vice versa. It ensures that the mapping process handles validation and error reporting where
/// necessary.</remarks>
internal static class OpportunityDtoMapper
{
    /// <summary>
    /// Converts an <see cref="Opportunity"/> instance to an <see cref="AddOpportunityDto"/>.
    /// </summary>
    /// <param name="opportunity">The <see cref="Opportunity"/> instance to convert. Must not be null, and its <c>RequiredSkills</c> and
    /// <c>OpportunitiesDescription</c> properties must be non-null.</param>
    /// <returns>An <see cref="AddOpportunityDto"/> containing the data from the specified <see cref="Opportunity"/>.</returns>
    internal static AddOpportunityDto ToDto(Opportunity opportunity)
    {
        return new AddOpportunityDto(
            opportunity.InformationDescription!.Content,
            opportunity.Country!.Value
        );
    }

    /// <summary>
    /// Converts an <see cref="Opportunity"/> object to a <see cref="ListOpportunityDto"/> object.
    /// </summary>
    /// <param name="opportunity">The <see cref="Opportunity"/> instance to convert. This parameter cannot be null, and its <c>RequiredSkills</c>
    /// and <c>OpportunitiesDescription</c> properties must not be null.</param>
    /// <returns>A <see cref="ListOpportunityDto"/> object containing the relevant data from the specified <see
    /// cref="Opportunity"/>.</returns>
    internal static ListOpportunityDto ToListDto(Opportunity opportunity)
    {
        return new ListOpportunityDto(
            opportunity.WorkInformationInternalId,
            opportunity.InformationDescription!.Content,
            opportunity.Country!.Value
        );
    }

    /// <summary>
    /// Converts an <see cref="AddOpportunityDto"/> instance into a domain <see cref="Opportunity"/> entity.
    /// </summary>
    /// <remarks>This method validates the input data from the <paramref name="dto"/> and ensures that all
    /// required fields meet the necessary constraints. If any validation fails, the method returns a failure result
    /// with the corresponding errors.</remarks>
    /// <param name="dto">The data transfer object containing the details required to create an <see cref="Opportunity"/>.</param>
    /// <returns>A <see cref="Result{T}"/> containing the created <see cref="Opportunity"/> if the conversion is successful;
    /// otherwise, a failure result with a collection of errors describing the issues encountered.</returns>
    internal static Result<Opportunity> ToEntity(AddOpportunityDto dto)
    {
        var errors = new List<Error>();

        var descriptionResult = Description.Create(dto.InformationDescription);
        var countryResult = Country.Create(dto.Country);

        if (descriptionResult.IsFailure) errors.Add(descriptionResult.Error!);
        if (countryResult.IsFailure) errors.Add(countryResult.Error!);


        if (errors.Any())
            return Result<Opportunity>.Failure(errors);

        var opportunity = new Opportunity(
            descriptionResult.Value!,
            countryResult.Value!
        );

        return Result<Opportunity>.Success(opportunity);
    }
}
