using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Repositories;

/// <summary>
/// Defines methods for managing and retrieving work information in a repository.
/// </summary>
/// <remarks>This interface provides asynchronous methods for adding, retrieving, and listing work information.
/// Implementations of this interface should handle data persistence and retrieval.</remarks>
public interface IWorkInformationRepository
{
    /// <summary>
    /// Asynchronously adds the specified work information to the system for the given identifier.  
    /// </summary>
    /// <param name="careerInternalId">The unique identifier associated with the entity to which the work information will be added. Must be a positive
    /// integer.</param>
    /// <param name="workInformation">The work information to add. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result"/> indicating
    /// if the operation was successful or if any errors occurred.</returns>
    Task<Result> AddInformationAsync(int careerInternalId, WorkInformation workInformation);

    /// <summary>
    /// Asynchronously retrieves a collection of work information associated with the specified identifier.
    /// </summary>
    /// <param name="careerInternalId">The unique identifier used to filter the work information. Must be a positive integer.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of  <see
    /// cref="WorkInformation"/> objects associated with the specified identifier. Returns an empty collection  if no
    /// matching information is found.</returns>
    Task<IEnumerable<WorkInformation>> ListSpecificInformationAsync(int careerInternalId);

    /// <summary>
    /// Asynchronously retrieves a single work information record by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier used to filter the specific type of work information. Must be a positive integer.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an single object of <see
    /// cref="WorkInformation"/> associated with the specified identifier.</returns>
    Task<WorkInformation?> ListSingleWorkInformationtAsync(int id);

    /// <summary>
    /// Asynchronously retrieves a collection of all work information records.
    /// </summary>
    /// <remarks>This method returns all available work information records. The caller can use the returned 
    /// collection to process or display the data. If no records are available, the method returns  an empty
    /// collection.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains an  <see
    /// cref="IEnumerable{WorkInformation}"/> representing the collection of work information records.</returns>
    Task<IEnumerable<WorkInformation>> ListAllAsync();
}
