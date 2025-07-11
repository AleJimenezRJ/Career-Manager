using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;

/// <summary>
/// Defines a repository for managing and retrieving opportunities data.
/// </summary>
/// <remarks>This interface extends <see cref="IWorkInformationRepository"/> and provides additional functionality
/// specific to opportunities. Implementations of this interface should handle data access and retrieval  for
/// opportunities in a consistent and efficient manner.</remarks>
public interface IOpportunityRepository : IWorkInformationRepository
{
    /// <summary>
    /// Asynchronously retrieves all opportunities.
    /// </summary>
    /// <remarks>This method returns an enumerable collection of opportunities. The caller can use the result
    /// to iterate through all available opportunities.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see
    /// cref="IEnumerable{Opportunities}"/> representing all opportunities.</returns>
    new Task<IEnumerable<Opportunity>> ListAllAsync();
}
