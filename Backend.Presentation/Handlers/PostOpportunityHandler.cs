using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Requests;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;


namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

/// <summary>
/// Handles the creation of a new opportunity by processing the request, validating the input,  and persisting the
/// opportunity to the database.
/// </summary>
/// <remarks>This method validates the input request by mapping the provided DTO to a domain entity. If the
/// mapping fails,  a bad request result is returned with the associated validation errors. If the mapping succeeds, the
/// opportunity  is persisted to the database. If the persistence operation fails, a conflict result is returned.
/// Otherwise,  a success response is returned.</remarks>
public static class PostOpportunityHandler
{
    /// <summary>
    /// Handles the creation of an opportunity by mapping the request data to a domain entity, validating it,  and
    /// persisting it to the database. Returns an appropriate HTTP response based on the outcome.
    /// </summary>
    /// <remarks>This method performs the following steps: <list type="bullet"> <item>Maps the incoming
    /// request data to a domain entity.</item> <item>Validates the mapped entity and returns a <see
    /// langword="BadRequest"/> response if validation fails.</item> <item>Attempts to persist the entity to the
    /// database and returns a <see langword="Conflict"/> or <see langword="BadRequest"/> response for specific error
    /// cases.</item> <item>Returns an <see langword="Ok"/> response if the operation succeeds.</item> </list></remarks>
    /// <param name="opportunityService">The service used to manage opportunities.</param>
    /// <param name="request">The request containing the opportunity data to be created.</param>
    /// <param name="careerInternalId">The internal identifier of the career associated with the opportunity.</param>
    /// <returns>A typed result containing one of the following: <list type="bullet"> <item><see langword="Ok"/> with a <see
    /// cref="PostResponse"/> indicating successful creation.</item> <item><see langword="Conflict"/> with an <see
    /// cref="Error"/> if a conflict occurs (e.g., duplicate or concurrency issues).</item> <item><see
    /// langword="BadRequest"/> with a list of <see cref="Error"/> objects if validation fails.</item> </list></returns>
    public static async Task<Results<Ok<PostResponse>, Conflict<Error>, BadRequest<List<Error>>>> HandleAsync(
        [FromServices] IOpportunityServices opportunityService,
        [FromBody] PostOpportunityRequest request,
        [FromRoute] int careerInternalId)
    {
        // Try to map the DTO to a domain entity
        var result = OpportunityDtoMapper.ToEntity(request.Opportunity);

        if (result.IsFailure)
        {
            var errors = result.Errors ?? new List<Error> { result.Error! };
            return TypedResults.BadRequest(errors);
        }
        var opportunityEntity = result.Value!;

        // Persist to database
        var addResult = await opportunityService.AddOpportunitiesAsync(careerInternalId, opportunityEntity);

        if (addResult.IsSuccess)
        {
            return TypedResults.Ok(new PostResponse("Opportunity successfully created."));
        }

        // Handle multiple errors (validation, etc.)
        if (addResult.Errors is { Count: > 0 })
        {
            return TypedResults.BadRequest(addResult.Errors);
        }

        // Handle single error
        if (addResult.Error is not null)
        {
            // Map domain error types to HTTP responses
            return addResult.Error.ErrorType switch
            {
                ErrorType.DuplicatedConflict or ErrorType.ConcurrencyConflict =>
                    TypedResults.Conflict(addResult.Error),
                ErrorType.Validation =>
                    TypedResults.BadRequest(new List<Error> { addResult.Error }),
                _ =>
                    TypedResults.Conflict(addResult.Error)
            };
        }

        // Fallback: unknown error
        var unknownError = Error.Failure("Opportunity.PersistenceFailure", "An unknown error occurred while saving the opportunity.");
        return TypedResults.Conflict(unknownError);
    }
}
