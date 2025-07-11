using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

/// <summary>
/// Handles the calculation of scholarships for a specified career and enterprise.
/// </summary>
/// <remarks>This method retrieves scholarship information for a given career and enterprise by interacting with
/// the provided <see cref="ICareerRepository"/> service. It returns a result indicating the success or failure of the
/// operation.</remarks>
public static class GetScholarshipHandler
{
    /// <summary>
    /// Handles the scholarship calculation for a specified career and enterprise.
    /// </summary>
    /// <remarks>This method performs asynchronous operations and relies on the provided <paramref
    /// name="careerRepository"/>  to calculate the scholarship. Ensure that the repository is properly configured and
    /// accessible.</remarks>
    /// <param name="careerRepository">The repository used to perform operations related to career data.</param>
    /// <param name="careerName">The name of the career for which the scholarship calculation is performed. Must not be null or empty.</param>
    /// <returns>A result containing one of the following outcomes: <list type="bullet"> <item> <description> <see cref="Ok{T}"/>
    /// with a <see cref="PostResponse"/> indicating the scholarship calculation was successful. </description> </item>
    /// <item> <description> <see cref="Conflict{T}"/> with an <see cref="Error"/> indicating an unknown error occurred
    /// during the calculation. </description> </item> <item> <description> <see cref="BadRequest{T}"/> with a list of
    /// <see cref="Error"/> objects if the input parameters are invalid. </description> </item> </list></returns>
    public static async Task<Results<Ok<PostResponse>, Conflict<Error>, BadRequest<List<Error>>>> HandleAsync(
        [FromServices] ICareerRepository careerRepository,
        [FromRoute] string careerName)
    {
        var careerEntity =  await careerRepository.CalculateScholarshipAsync(careerName);
        if (careerEntity.IsSuccess)
        {
            return TypedResults.Ok(new PostResponse("Successfully calculated the scholarship."));
        }
        else {
            var unknownError = Error.Failure("Career.Calculus error", "An unknown error occurred while calculating the scholarship for the career.");
            return TypedResults.Conflict(unknownError);
        }
    }
}
