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
/// Handles the creation of a new industry for a specified career.
/// </summary>
/// <remarks>This method validates the provided industry data, maps it to a domain entity, and attempts to persist
/// it to the database. If the mapping fails, a bad request response is returned with the validation errors. If the
/// persistence operation fails, a conflict response is returned with an error indicating the failure.</remarks>
public static class PostIndustryHandler
{
    /// <summary>
    /// Handles the creation of an industry entity and persists it to the database.
    /// </summary>
    /// <remarks>This method processes the incoming request to create an industry entity, validates the input,
    /// and attempts to save the entity to the database. It returns an appropriate HTTP response based  on the outcome
    /// of the operation, including success, validation errors, or conflicts.</remarks>
    /// <param name="industryService">The service used to manage industry-related operations. This is provided via dependency injection.</param>
    /// <param name="request">The request containing the data for the industry to be created. This must include valid industry details.</param>
    /// <param name="careerInternalId">The internal identifier of the career to which the industry belongs. This must be a positive integer.</param>
    /// <returns>A <see cref="Results{T1, T2, T3}"/> object containing one of the following results: <list type="bullet"> <item>
    /// <description><see cref="Ok{T}"/>: Indicates the industry was successfully created. Returns a <see
    /// cref="PostResponse"/> with a success message.</description> </item> <item> <description><see
    /// cref="Conflict{T}"/>: Indicates a conflict occurred, such as a duplicate or concurrency issue. Returns an <see
    /// cref="Error"/> describing the conflict.</description> </item> <item> <description><see cref="BadRequest{T}"/>:
    /// Indicates validation errors in the request. Returns a list of <see cref="Error"/> objects describing the
    /// issues.</description> </item> </list></returns>
    public static async Task<Results<Ok<PostResponse>, Conflict<Error>, BadRequest<List<Error>>>> HandleAsync(
        [FromServices] IIndustryServices industryService,
        [FromBody] PostIndustryRequest request,
        [FromRoute] int careerInternalId)
    {
            // Try to map the DTO to a domain entity
        var result = IndustryDtoMapper.ToEntity(request.Industry);

        if (result.IsFailure)
        {
            var errors = result.Errors ?? new List<Error> { result.Error! };
            return TypedResults.BadRequest(errors);
        }
        var industryEntity = result.Value!;

        // Persist to database
        var addResult = await industryService.AddIndustriesAsync(careerInternalId, industryEntity);
        if (addResult.IsSuccess)
        {
            return TypedResults.Ok(new PostResponse("Industry successfully created."));
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
        var unknownError = Error.Failure("Industry.PersistenceFailure", "An unknown error occurred while saving the industry.");
        return TypedResults.Conflict(unknownError);
    }
}
