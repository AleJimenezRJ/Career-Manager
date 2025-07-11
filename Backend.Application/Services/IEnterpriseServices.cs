using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Services;

/// <summary>
/// Defines a contract for managing enterprises within the system.
/// </summary>
/// <remarks>This interface provides methods for adding, retrieving, and listing enterprises. Implementations of
/// this interface should handle the underlying logic for enterprise management, including data storage and
/// retrieval.</remarks>
public interface IEnterpriseServices
{
    /// <summary>
    /// Asynchronously retrieves a collection of enterprises.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an  <see
    /// cref="IEnumerable{Enterprise}"/> representing the list of enterprises.  If no enterprises are available, the
    /// collection will be empty.</returns>
    Task<IEnumerable<Enterprise>> ListEnterpriseAsync();

    /// <summary>
    /// Asynchronously adds a new enterprise to the specified career.
    /// </summary>
    /// <remarks>This method performs validation on the input parameters and ensures that the enterprise is
    /// correctly associated with the specified career. The operation may involve database interactions or other
    /// external dependencies.</remarks>
    /// <param name="careerInternalId">The unique identifier of the career to which the enterprise will be added. Must be a positive integer.</param>
    /// <param name="enterprise">The enterprise object containing the details of the enterprise to be added. Cannot be null.</param>
    /// <returns>A <see cref="Task{Result}"/> representing the asynchronous operation.  The result contains information about the
    /// success or failure of the operation.</returns>
    Task<Result> AddEnterpriseAsync(int careerInternalId, Enterprise enterprise);
}
