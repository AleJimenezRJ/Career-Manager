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
/// Handles the creation of a new enterprise entity.
/// </summary>
/// <remarks>This method processes the incoming request to create an enterprise entity, validates the input, maps
/// the data transfer object (DTO) to a domain entity, and persists the entity to the database. It returns an
/// appropriate HTTP response based on the outcome of the operation, including success, validation errors, or conflict
/// scenarios.</remarks>
public static class PostEnterpriseHandler
{
    /// <summary>
    /// Handles the creation of an enterprise entity by processing the request, validating input,  and persisting the
    /// entity to the database.
    /// </summary>
    /// <remarks>This method validates the input request, maps the enterprise data to a domain entity, and
    /// attempts to persist  the entity to the database. It handles various error scenarios, including validation
    /// errors, conflicts, and  unknown persistence failures, and maps them to appropriate HTTP responses.</remarks>
    /// <param name="enterpriseService">The enterprise service used to perform operations related to enterprise entities.</param>
    /// <param name="request">The request containing the enterprise data to be created. Must not be null.</param>
    /// <param name="careerInternalId">The internal identifier of the career associated with the enterprise. Must be a positive integer.</param>
    /// <returns>A <see cref="Results{T1, T2, T3}"/> object containing one of the following results: <list type="bullet"> <item>
    /// <description> <see cref="Ok{T}"/>: Indicates the enterprise was successfully created. Contains a <see
    /// cref="PostResponse"/>  with a success message. </description> </item> <item> <description> <see
    /// cref="Conflict{T}"/>: Indicates a conflict occurred, such as a duplicated or concurrency issue.  Contains an
    /// <see cref="Error"/> describing the conflict. </description> </item> <item> <description> <see
    /// cref="BadRequest{T}"/>: Indicates the request was invalid, such as validation errors.  Contains a list of <see
    /// cref="Error"/> objects describing the issues. </description> </item> </list></returns>
    public static async Task<Results<Ok<PostResponse>, Conflict<Error>, BadRequest<List<Error>>>> HandleAsync(
        [FromServices] IEnterpriseServices enterpriseService,
        [FromBody] PostEnterpriseRequest request,
        [FromRoute] int careerInternalId)
    {
        // Try to map the DTO to a domain entity
        var result = EnterpriseDtoMapper.ToEntity(request.Enterprise);

        if (result.IsFailure)
        {
            var errors = result.Errors ?? new List<Error> { result.Error! };
            return TypedResults.BadRequest(errors);
        }
        var entity = result.Value!;

        // Persist to database
        var addResult = await enterpriseService.AddEnterpriseAsync(careerInternalId, entity);
        if (addResult.IsSuccess)
        {
            return TypedResults.Ok(new PostResponse("Enterprise successfully created."));
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
        var unknownError = Error.Failure("Enterprise.PersistenceFailure", "An unknown error occurred while saving the enterprise.");
        return TypedResults.Conflict(unknownError);
    }
}
