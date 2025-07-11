using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

/// <summary>
/// Handles a request to search for a keyword across various data sources.
/// </summary>
public static class GetKeywordHandler
{
    /// <summary>
    /// Executes the keyword search operation and returns all matches across the system.
    /// </summary>
    /// <param name="careerRepository">The career repository used to perform the search.</param>
    /// <param name="keyword">The keyword to search for.</param>
    /// <returns>An HTTP result containing the list of matched results or an empty list.</returns>
    public static async Task<Results<Ok<SearchKeywordResponse>, BadRequest<List<string>>>> HandleAsync(
        [FromServices] ICareerRepository careerRepository,
        [FromRoute] string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return TypedResults.BadRequest(new List<string> { "The keyword must not be null or empty." });
        }

        var results = await careerRepository.SearchKeywordAsync(keyword);

        var dtoResults = results.Select(r =>
            new SearchResultDto(
                r.CareerId,
                r.CareerName,
                r.TableName,
                r.ColumnName,
                r.Field
            )).ToList();

        var response = new SearchKeywordResponse(dtoResults);
        return TypedResults.Ok(response);
    }
}
