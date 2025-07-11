using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Services;

/// <summary>
/// Defines a contract for managing and retrieving work-related information.
/// </summary>
/// <remarks>This interface provides methods for adding, retrieving, and listing work information. Implementations
/// of this interface should handle the underlying data storage and retrieval mechanisms.</remarks>
public interface IWorkInformationServices
{
    /// <summary>
    /// Asynchronously retrieves a collection of work information entries that match the specified identifier.
    /// </summary>
    /// <param name="careerInternalId">The unique identifier of the work information to filter the results by.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of  <see
    /// cref="WorkInformation"/> objects that match the specified identifier. If no entries are found, the collection
    /// will be empty.</returns>
    public Task<IEnumerable<WorkInformation>> ListSpecificInformationAsync(int careerInternalId);

    /// <summary>
    /// Retrieves detailed information about a specific work item based on its unique identifier.
    /// </summary>
    /// <remarks>This method performs an asynchronous operation to fetch the work item details. Ensure that
    /// the provided <paramref name="id"/> corresponds to a valid work item.</remarks>
    /// <param name="id">The unique identifier of the work item to retrieve. Must be a positive integer.</param>
    /// <returns>A <see cref="Task{WorkInformation}"/> representing the asynchronous operation.  The task result contains the
    /// <see cref="WorkInformation"/> object for the specified work item. If no work item is found with the given
    /// identifier, the result may be <see langword="null"/>.</returns>
    public Task<WorkInformation?> ListSingleWorkInformationtAsync(int id);

    /// <summary>
    /// Asynchronously retrieves a collection of all available work information records.
    /// </summary>
    /// <remarks>This method returns all work information records without filtering or pagination.  Use this
    /// method when you need to access the complete dataset. For large datasets,  consider potential performance
    /// implications and memory usage.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains an  <see
    /// cref="IEnumerable{WorkInformation}"/> representing the collection of work  information records. If no records
    /// are available, the collection will be empty.</returns>
    public Task<IEnumerable<WorkInformation>> ListAllAsync();
}
