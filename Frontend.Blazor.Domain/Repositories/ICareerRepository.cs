using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Repositories;

/// <summary>
/// Defines the contract for managing <see cref="Career"/> entities within the domain.
/// </summary>
public interface ICareerRepository
{
    /// <summary>
    /// Persists a new <see cref="Career"/> asynchronously.
    /// </summary>
    /// <param name="career">The career entity to add.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result"/> indicating
    /// if the operation was successful or if any errors occurred.</returns>
    Task<Result> AddCareerAsync(Career career);

    /// <summary>
    /// Retrieves all available career entities asynchronously.
    /// </summary>
    /// <returns>
    /// A collection of all <see cref="Career"/> instances.
    /// </returns>
    Task<IEnumerable<Career>> ListCareersAsync();

    /// <summary>
    /// Retrieves a specific <see cref="Career"/> by its name.
    /// </summary>
    /// <param name="Name">The name of the career to retrieve.</param>
    /// <returns>
    /// A matching <see cref="Career"/> instance, or <c>null</c> if not found.
    /// </returns>
    Task<Career?> ListSpecificCareerAsync(string Name);

    /// <summary>
    /// Calculates the scholarship eligibility and amount for a given career.
    /// </summary>
    /// <remarks>Use this method to determine scholarship details for a specific career.  The calculation may
    /// involve external data sources or complex business rules, so ensure proper error handling for potential
    /// failures.</remarks>
    /// <param name="careerName">The name of the career for which the scholarship calculation is performed. Cannot be null or empty.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  The result contains a <see
    /// cref="Result"/> object with details about the scholarship eligibility and amount.</returns>
    Task<Result> CalculateScholarshipAsync(string careerName);

    /// <summary>
    /// Searches across multiple tables for matches to the specified keyword.
    /// </summary>
    /// <param name="keyword">The search keyword to match against various fields.</param>
    /// <returns>
    /// A list of search results with matched content, table, and column info.
    /// </returns>
    Task<IEnumerable<SearchResult>> SearchKeywordAsync(string keyword);

}
