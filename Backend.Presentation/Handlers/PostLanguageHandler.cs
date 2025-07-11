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
/// Handles the creation of a new language entity by processing the provided request and persisting it to the database.
/// </summary>
/// <remarks>This method validates the input request, maps it to a domain entity, and attempts to save the entity
/// to the database. It returns an appropriate HTTP response based on the outcome of the operation, including success,
/// validation errors,  or conflict scenarios.</remarks>
public static class PostLanguageHandler
{
    /// <summary>
    /// Handles the creation of a new language entity by processing the provided request and persisting it to the
    /// database.
    /// </summary>
    /// <remarks>This method validates the input data, maps it to a domain entity, and attempts to save it to
    /// the database. Depending on the outcome, it returns an appropriate HTTP response, including success, validation
    /// errors, or conflict errors.</remarks>
    /// <param name="LanguageRepository">The repository used to persist language entities to the database.</param>
    /// <param name="internalWorkInformationId">The identifier for the internal work information associated with the language entity.</param>
    /// <param name="request">The request containing the language data to be processed and saved.</param>
    /// <returns>A <see cref="Results{T1, T2, T3}"/> object containing one of the following results: <list type="bullet"> <item>
    /// <description><see cref="Ok{T}"/>: Indicates successful creation of the language entity, with a response
    /// message.</description> </item> <item> <description><see cref="Conflict{T}"/>: Indicates a conflict occurred,
    /// such as a duplicate or concurrency issue, with an error description.</description> </item> <item>
    /// <description><see cref="BadRequest{T}"/>: Indicates validation errors or other issues with the input data, with
    /// a list of errors.</description> </item> </list></returns>
    public static async Task<Results<Ok<PostResponse>, Conflict<Error>, BadRequest<List<Error>>>> HandleAsync(
    [FromServices] ILanguageRepository LanguageRepository,
    [FromRoute] int internalWorkInformationId,
    [FromBody] PostLanguageRequest request)
    {
        // Try to map the DTO to a domain entity
        var result = LanguageDtoMapper.ToEntity(request.Dto);

        if (result.IsFailure)
        {
            var errors = result.Errors ?? new List<Error> { result.Error! };
            return TypedResults.BadRequest(errors);
        }
        var LanguageEntity = result.Value!;

        // Persist to database
        var addResult = await LanguageRepository.AddLanguageAsync(internalWorkInformationId, LanguageEntity);

        if (addResult.IsSuccess)
        {
            return TypedResults.Ok(new PostResponse("Language successfully created."));
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
        var unknownError = Error.Failure("Language.PersistenceFailure", "An unknown error occurred while saving the Language.");
        return TypedResults.Conflict(unknownError);
    }
}
