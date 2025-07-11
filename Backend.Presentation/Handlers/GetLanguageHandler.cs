using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

/// <summary>
/// Handles the retrieval of available languages and returns the result as a response.
/// </summary>
/// <remarks>This method queries the provided <see cref="ILanguageRepository"/> to retrieve a list of languages.
/// If no languages are found, it returns a "Not Found" result with an appropriate error. Otherwise, it maps the
/// retrieved languages to DTOs and returns them in a successful response.</remarks>
public static class GetLanguageHandler
{
    /// <summary>
    /// Handles the retrieval of language data and returns the result as an HTTP response.
    /// </summary>
    /// <remarks>This method queries the provided <see cref="ILanguageRepository"/> for a list of languages,
    /// maps them to DTOs,  and returns an appropriate HTTP response. If no languages are found, a "Not Found" response
    /// is returned with an error.  Otherwise, an "OK" response containing the language data is returned.</remarks>
    /// <param name="languageRepository">The repository used to retrieve language data. Must not be null.</param>
    /// <returns>A <see cref="Results{T1, T2}"/> containing either: <list type="bullet"> <item> <description>An "OK" result with
    /// a <see cref="GetLanguageResponse"/> containing the list of languages.</description> </item> <item>
    /// <description>A "Not Found" result with an <see cref="Error"/> indicating that no languages were
    /// found.</description> </item> </list></returns>
    public static async Task<Results<Ok<GetLanguageResponse>, NotFound<Error>>> HandleAsync(
        [FromServices] ILanguageRepository languageRepository)
    {
        var languages = await languageRepository.ListLanguagesAsync();
        var languageDto = languages
            .Select(LanguageDtoMapper.ToDto)
            .ToList();
        if (!languageDto.Any())
        {
            var notFoundError = DomainErrors.FoundErrors.NotFound();
            return TypedResults.NotFound(notFoundError);
        }

        var response = new GetLanguageResponse(languageDto);
        return TypedResults.Ok(response);
    }
}
