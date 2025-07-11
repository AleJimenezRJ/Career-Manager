using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Requests;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;


namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

/// <summary>
/// Handles the creation of a new career entity by validating the request, checking for conflicts, and persisting the
/// entity to the database.
/// </summary>
public static class PostCareerHandler
{
    /// <summary>
    /// Handles the creation of a career entity by processing the request, validating the input,  and persisting the
    /// entity to the database.
    /// </summary>
    /// <remarks>This method performs the following steps: <list type="number"> <item>Maps the incoming
    /// request to a domain entity and validates the input.</item> <item>Attempts to persist the entity to the database
    /// using the provided repository.</item> <item>Returns an appropriate HTTP response based on the outcome of the
    /// operation, including handling validation errors, conflicts, and unknown errors.</item> </list></remarks>
    /// <param name="careerRepository">The repository used to persist the career entity. This service is injected via dependency injection.</param>
    /// <param name="request">The request containing the career data to be created. This is provided in the request body.</param>
    /// <returns>A result containing one of the following: <list type="bullet"> <item> <description> <see cref="Results{T1, T2,
    /// T3}.Ok"/> with a <see cref="PostResponse"/> if the career is successfully created. </description> </item>
    /// <item> <description> <see cref="Results{T1, T2, T3}.Conflict"/> with an <see cref="Error"/> if a conflict
    /// occurs, such as a duplicate or concurrency issue. </description> </item> <item> <description> <see
    /// cref="Results{T1, T2, T3}.BadRequest"/> with a list of <see cref="Error"/> objects if validation fails or the
    /// input is invalid. </description> </item> </list></returns>
    public static async Task<Results<Ok<PostResponse>, Conflict<Error>, BadRequest<List<Error>>>> HandleAsync(
    [FromServices] ICareerRepository careerRepository,
    [FromBody] PostCareerRequest request)
    {
        // Try to map the DTO to a domain entity
        var result = CareerDtoMapper.ToEntity(request.Career);

        if (result.IsFailure)
        {
            var errors = result.Errors ?? new List<Error> { result.Error! };
            return TypedResults.BadRequest(errors);
        }
        var careerEntity = result.Value!;

        // Persist to database
        var addResult = await careerRepository.AddCareerAsync(careerEntity);

        if (addResult.IsSuccess)
        {
            return TypedResults.Ok(new PostResponse("Career successfully created."));
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
                    TypedResults.Conflict(addResult.Error) // Default to conflict for unknown errors
            };
        }

        // Fallback: unknown error
        var unknownError = Error.Failure("Career.PersistenceFailure", "An unknown error occurred while saving the career.");
        return TypedResults.Conflict(unknownError);
    }
}
