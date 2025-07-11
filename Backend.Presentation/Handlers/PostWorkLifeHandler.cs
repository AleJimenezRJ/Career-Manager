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
/// Handles the creation of a new work life entry for a specified career.
/// </summary>
/// <remarks>This method validates the input request, maps it to a domain entity, and attempts to persist the
/// entity to the database. If the operation fails due to invalid input or a persistence error, an appropriate result is
/// returned.</remarks>
public static class PostWorkLifeHandler
{
    /// <summary>
    /// Handles the creation of a new work life entity by processing the request, validating the input,  and persisting
    /// the entity to the database.
    /// </summary>
    /// <remarks>This method performs the following steps: <list type="number"> <item>Maps the incoming DTO to
    /// a domain entity and validates the input.</item> <item>Attempts to persist the entity to the database using the
    /// provided service.</item> <item>Returns an appropriate HTTP response based on the outcome, including handling
    /// validation errors, conflicts, and unknown errors.</item> </list></remarks>
    /// <param name="workLifeService">The service responsible for managing work life entities. This is injected via dependency injection.</param>
    /// <param name="request">The request containing the data for the work life entity to be created. Must not be null.</param>
    /// <param name="careerInternalId">The internal identifier of the career associated with the work life entity.</param>
    /// <param name="enterpriseInternalId">The internal identifier of the enterprise associated with the work life entity.</param>
    /// <returns>A result containing one of the following: <list type="bullet"> <item> <description> 
    /// <see cref="Results{T1, T2, T3}.Ok"/> with a <see cref="PostResponse"/> if the work life entity is successfully created. </description>
    /// </item> <item> <description> <see cref="Results{T1, T2, T3}.Conflict"/> with an <see cref="Error"/> if a
    /// conflict occurs, such as a duplicate or concurrency issue. </description> </item> <item> <description> <see
    /// cref="Results{T1, T2, T3}.BadRequest"/> with a list of <see cref="Error"/> objects if validation errors are
    /// encountered. </description> </item> </list></returns>
    public static async Task<Results<Ok<PostResponse>, Conflict<Error>, BadRequest<List<Error>>>> HandleAsync(
        [FromServices] IWorkLifeServices workLifeService,
        [FromBody] PostWorkLifeRequest request,
        [FromRoute] int careerInternalId)
    {
        // Try to map the DTO to a domain entity
        var result = WorkLifeDtoMapper.ToEntity(request.WorkLife);

        if (result.IsFailure)
        {
            var errors = result.Errors ?? new List<Error> { result.Error! };
            return TypedResults.BadRequest(errors);
        }
        var opportunityEntity = result.Value!;

        // Persist to database
        var addResult = await workLifeService.AddWorkLifeAsync(careerInternalId, opportunityEntity);

        if (addResult.IsSuccess)
        {
            return TypedResults.Ok(new PostResponse("Work Life successfully created."));
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
        var unknownError = Error.Failure("WorkLife.PersistenceFailure", "An unknown error occurred while saving the work life.");
        return TypedResults.Conflict(unknownError);
    }
}
