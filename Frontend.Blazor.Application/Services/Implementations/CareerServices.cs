using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Application.Services.Implementations;

/// <summary>
/// Provides the concrete implementation of <see cref="ICareerServices"/>.
/// Handles application-level logic related to academic careers and delegates persistence operations to the repository layer.
/// </summary>
internal class CareerServices : ICareerServices
{
    /// <summary>
    /// Represents a repository for accessing career-related data.
    /// </summary>
    /// <remarks>This field is used internally to interact with the underlying data source for career
    /// information. It is intended to be injected via dependency injection and should not be modified
    /// directly.</remarks>
    private readonly ICareerRepository _careerRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CareerServices"/> class.
    /// </summary>
    /// <param name="careerRepository">The repository used to manage <see cref="Career"/> persistence.</param>
    public CareerServices(ICareerRepository careerRepository)
    {
        _careerRepository = careerRepository;
    }

    /// <summary>
    /// Asynchronously adds a new career to the repository.
    /// </summary>
    /// <param name="career">The career object to be added. Cannot be null.</param>
    /// <returns>A result indicating the success or failure of the operation. The result will contain an error message if the operation fails.</returns>
    public async Task<Result> AddCareerAsync(Career career)
    {
        return await _careerRepository.AddCareerAsync(career);
    }

    /// <summary>
    /// Asynchronously retrieves a collection of careers.
    /// </summary>
    /// <remarks>This method returns all careers available in the repository. The returned collection may be
    /// empty  if no careers are found. The method does not filter or modify the data; it simply retrieves it as
    /// stored.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IEnumerable{Career}"/>
    /// representing the list of careers.</returns>
    public async Task<IEnumerable<Career>> ListCareersAsync()
    {
        return await _careerRepository.ListCareersAsync();
    }

    /// <summary>
    /// Retrieves a specific career by its name.
    /// </summary>
    /// <param name="Name">The name of the career to retrieve. Cannot be null or empty.</param>
    /// <returns>A <see cref="Career"/> object representing the career with the specified name,  or <see langword="null"/> if no
    /// matching career is found.</returns>
    public async Task<Career?> ListSpecificCareerAsync(string Name)
    {
        return await _careerRepository.ListSpecificCareerAsync(Name);
    }

    /// <summary>
    /// Calculates the scholarship eligibility and details for a specified career.
    /// </summary>
    /// <remarks>This method delegates the scholarship calculation to the underlying career repository. Ensure
    /// that the provided career name matches an existing career in the repository.</remarks>
    /// <param name="careerName">The name of the career for which the scholarship calculation is performed. Cannot be null or empty.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  The task result contains a <see
    /// cref="Result"/> object with the scholarship calculation details.</returns>
    public async Task<Result> CalculateScholarshipAsync(string careerName)
    {
        return await _careerRepository.CalculateScholarshipAsync(careerName);
    }

    /// <summary>
    /// Asynchronously searches for results matching the specified keyword.
    /// </summary>
    /// <param name="keyword">The keyword to search for. Must not be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of  <see
    /// cref="SearchResult"/> objects that match the specified keyword. If no matches are found,  the collection will be
    /// empty.</returns>
    public async Task<IEnumerable<SearchResult>> SearchKeywordAsync(string keyword)
    {
        return await _careerRepository.SearchKeywordAsync(keyword);
    }
}
