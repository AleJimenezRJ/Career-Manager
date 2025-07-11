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
/// Handles the creation of a recruitment entity by processing the request, validating the input,  and persisting the
/// entity using the recruitment service.
/// </summary>
/// <remarks>This method maps the incoming DTO to a domain entity, validates the input, and attempts to persist
/// the entity using the provided service. Known errors, such as validation failures or conflicts, are returned as
/// appropriate HTTP responses.</remarks>
public static class PostRecruitmentHandler
{
    /// <summary>
    /// Handles the creation of a recruitment entity by processing the request, validating the input,  and persisting
    /// the entity using the recruitment service.
    /// </summary>
    /// <remarks>This method maps the incoming request to a domain entity, validates the input, and attempts
    /// to persist the entity. It handles known errors such as validation failures, duplicate conflicts, and concurrency
    /// conflicts, returning appropriate HTTP responses for each case. If an unknown error occurs, a generic conflict
    /// response is returned.</remarks>
    /// <param name="recruitmentService">The recruitment service used to persist the recruitment entity.</param>
    /// <param name="request">The request containing the recruitment data to be created.</param>
    /// <param name="careerInternalId">The internal identifier of the career associated with the recruitment.</param>
    /// <returns>A <see cref="Results{T1, T2, T3}"/> object that represents the outcome of the operation: <list type="bullet">
    /// <item> <description><see cref="Ok{T}"/> with a <see cref="PostResponse"/> if the recruitment is successfully
    /// created.</description> </item> <item> <description><see cref="Conflict{T}"/> with an <see cref="Error"/> if a
    /// conflict occurs, such as a duplicate or concurrency issue.</description> </item> <item> <description><see
    /// cref="BadRequest{T}"/> with a list of <see cref="Error"/> objects if validation errors occur.</description>
    /// </item> </list></returns>
    public static async Task<Results<Ok<PostResponse>, Conflict<Error>, BadRequest<List<Error>>>> HandleAsync(
        [FromServices] IRecruitmentServices recruitmentService,
        [FromBody] PostRecruitmentRequest request,
        [FromRoute] int careerInternalId)
    {
        // Map DTO to domain entity
        var result = RecruitmentDtoMapper.ToEntity(request.Recruitment);

        if (result.IsFailure)
        {
            var errors = result.Errors ?? new List<Error> { result.Error! };
            return TypedResults.BadRequest(errors);
        }

        var recruitmentEntity = result.Value!;

        // Call service to persist the entity
        var addResult = await recruitmentService.AddRecruitmentAsync(careerInternalId, recruitmentEntity);

        if (addResult.IsSuccess)
        {
            return TypedResults.Ok(new PostResponse("Recruitment successfully created."));
        }

        // Handle known validation or conflict errors
        if (addResult.Errors is { Count: > 0 })
        {
            return TypedResults.BadRequest(addResult.Errors);
        }

        if (addResult.Error is not null)
        {
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

        // Fallback unknown error
        var unknownError = Error.Failure("Recruitment.PersistenceFailure", "An unknown error occurred while saving the recruitment.");
        return TypedResults.Conflict(unknownError);
    }
}