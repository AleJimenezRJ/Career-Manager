using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Mappers;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Repositories;

/// <summary>
/// Implements the <see cref="ICareerRepository"/> interface using a Kiota-generated API client to communicate with the backend.
/// </summary>
/// <remarks>
/// This repository is responsible for performing operations related to career data, such as adding new careers, listing careers,
/// retrieving a specific career, and calculating scholarships. It interacts with an external API defined by the backend server.
/// </remarks>
internal class KiotaCareerRepository : ICareerRepository
{
    private readonly ApiClient _apiClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="KiotaCareerRepository"/> class, which provides access to
    /// career-related data through the specified API client.
    /// </summary>
    /// <param name="apiClient">The API client used to interact with the remote service. Cannot be null.</param>
    public KiotaCareerRepository(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    /// <summary>
    /// Adds a new career asynchronously.
    /// </summary>
    /// <remarks>This method sends the provided career data to an external API for processing. Ensure that the
    /// <paramref name="career"/> object is properly populated before calling this method.</remarks>
    /// <param name="career">The career to be added. Cannot be null.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
    public async Task<Result> AddCareerAsync(Career career)
    {

        var addCareerDto = CareerDtoMapper.ToAddDto(career);
        var request = new PostCareerRequest
        {
            Career = addCareerDto
        };

        await _apiClient.AddCareer.PostAsPostResponseAsync(request);
        return Result.Success();
    }

    /// <summary>
    /// Asynchronously retrieves a list of available careers.
    /// </summary>
    /// <remarks>This method fetches career data from an external API and maps the results to <see
    /// cref="Career"/> entities. Ensure that the API client is properly configured before calling this
    /// method.</remarks>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Career"/> objects representing the available careers. Returns an
    /// empty collection if no careers are available or if the response is null.</returns>
    public async Task<IEnumerable<Career>> ListCareersAsync()
    {
        var response = await _apiClient.ListCareers.GetAsync();
        if (response is null || response.Career is null)
        {
            return Enumerable.Empty<Career>();
        }
        return CareerDtoMapper.ToEntity(response.Career);
    }

    /// <summary>
    /// Retrieves a specific career by its name asynchronously.
    /// </summary>
    /// <remarks>This method performs an asynchronous operation to search for a career by name using an
    /// external API client. If the career is found, it is mapped to a <see cref="Career"/> entity before being
    /// returned.</remarks>
    /// <param name="Name">The name of the career to search for. Cannot be null or empty.</param>
    /// <returns>A <see cref="Career"/> object representing the career with the specified name,  or <see langword="null"/> if no
    /// matching career is found.</returns>
    public async Task<Career?> ListSpecificCareerAsync(string Name)
    {
        await _apiClient.CalculateScholarship[Name].GetAsync();
        var dto = await _apiClient.SearchCareerByName[Name].GetAsync();
        if (dto?.Career is null)
        {
            return null;
        }
        ListCareerDto career = dto.Career;
        var result = CareerDtoMapper.ToEntity(career);
        return result.Value;
    }

    /// <summary>
    /// Calculates the scholarship eligibility for a given career asynchronously.
    /// </summary>
    /// <remarks>This method performs an asynchronous operation to calculate scholarship eligibility for the
    /// specified career. Ensure that the provided career name is valid and non-empty.</remarks>
    /// <param name="careerName">The name of the career for which the scholarship eligibility is to be calculated. Must not be null or empty.</param>
    /// <returns>A <see cref="Result"/> object indicating the success of the operation.</returns>
    public async Task<Result> CalculateScholarshipAsync(string careerName)
    {
        if (string.IsNullOrWhiteSpace(careerName))
        {
            return Result.Failure();
        }
        await _apiClient.CalculateScholarship[careerName].GetAsync();

        return Result.Success();
    }

    /// <summary>
    /// Asynchronously searches for results matching the specified keyword.
    /// </summary>
    /// <param name="keyword">The keyword to search for. Must not be null or empty.</param>
    /// <returns>A collection of <see cref="SearchResult"/> objects that match the specified keyword. Returns an empty collection
    /// if no results are found.</returns>
    public async Task<IEnumerable<SearchResult>> SearchKeywordAsync(string keyword)
    {
        var response = await _apiClient.SearchKeyword[keyword].GetAsync();
        if (response?.Results is null)
        {
            return Enumerable.Empty<SearchResult>();
        }
        return CareerDtoMapper.ToEntity(response.Results);
    }
}
