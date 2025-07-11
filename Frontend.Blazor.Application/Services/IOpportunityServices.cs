using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Application.Services;

/// <summary>
/// Provides methods for managing and retrieving opportunities within the system.
/// </summary>
/// <remarks>This interface defines operations for listing and adding opportunities. Implementations of this
/// interface should handle the underlying data storage and retrieval mechanisms.</remarks>
public interface IOpportunityServices
{
    /// <summary>
    /// Asynchronously retrieves a collection of opportunities.
    /// </summary>
    /// <remarks>This method returns all available opportunities. The caller can use the returned collection 
    /// to process or display the opportunities as needed. The method does not filter or paginate  the results;
    /// additional filtering or processing must be performed by the caller if required.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains an  <see
    /// cref="IEnumerable{Opportunities}"/> representing the collection of opportunities.</returns>
    Task<IEnumerable<Opportunity>> ListOpportunitiesAsync();

    /// <summary>
    /// Asynchronously adds opportunities to the specified career.
    /// </summary>
    /// <param name="careerInternalId">The unique identifier of the career to which the opportunities will be added. Must be a positive integer.</param>
    /// <param name="opportunities">The object of opportunities to add. Cannot be null.</param>
    /// <returns>A result indicating the success or failure of the operation.</returns>
    Task<Result> AddOpportunitiesAsync(int careerInternalId, Opportunity opportunities);
}
