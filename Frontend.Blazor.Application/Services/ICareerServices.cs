using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Application.Services;

/// <summary>
/// Defines the application service contract for managing <see cref="Career"/> entities.
/// This interface encapsulates business use cases related to academic careers.
/// </summary>
public interface ICareerServices
{
    /// <summary>
    /// Adds a new career to the system asynchronously.
    /// May include application-level validations or coordination logic before persisting.
    /// </summary>
    /// <param name="career">The <see cref="Career"/> entity to be added.</param>
    /// <returns>
    /// A result indicating the success or failure of the operation.
    /// </returns>
    public Task<Result> AddCareerAsync(Career career);

    /// <summary>
    /// Retrieves a list of all careers asynchronously.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="Career"/> entities.
    /// </returns>
    public Task<IEnumerable<Career>> ListCareersAsync();

    /// <summary>
    /// Retrieves a specific career by its name asynchronously.
    /// </summary>
    /// <param name="Name">The name of the career to retrieve.</param>
    /// <returns>
    /// A <see cref="Career"/> if found, or <c>null</c> if no match exists.
    /// </returns>
    public Task<Career?> ListSpecificCareerAsync(string Name);

    /// <summary>
    /// Calculates the scholarship eligibility and amount for a given career.
    /// </summary>
    /// <remarks>This method performs an asynchronous operation to determine scholarship details based on the
    /// specified career.  Ensure that the provided career name is valid and corresponds to an existing career in the
    /// system.</remarks>
    /// <param name="careerName">The name of the career for which the scholarship calculation is performed. Cannot be null or empty.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result contains a <see
    /// cref="Result"/> object  with details about the scholarship eligibility and amount. If the career is not found,
    /// the result may indicate an error.</returns>
    public Task<Result> CalculateScholarshipAsync(string careerName);

    /// <summary>
    /// Asynchronously searches for results matching the specified keyword.
    /// </summary>
    /// <param name="keyword">The keyword to search for. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains  a collection of <see
    /// cref="SearchResult"/> objects that match the specified keyword.  If no matches are found, the collection will be
    /// empty.</returns>
    public Task<IEnumerable<SearchResult>> SearchKeywordAsync(string keyword);
}
