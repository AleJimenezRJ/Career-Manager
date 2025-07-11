using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Services.Implementations;

/// <summary>
/// Provides services for retrieving work information, including listing all records,  retrieving a single record by its
/// identifier, and filtering records based on specific criteria.
/// </summary>
/// <remarks>This service acts as a layer between the application and the underlying repository,  enabling
/// asynchronous operations for querying work information.</remarks>
internal class WorkInformationServices : IWorkInformationServices
{
    /// <summary>
    /// Represents a repository for accessing and managing work-related information.
    /// </summary>
    /// <remarks>This field is intended to store a reference to an implementation of the <see
    /// cref="IWorkInformationRepository"/> interface. It is used internally to perform operations related to work
    /// information.</remarks>
    private readonly IWorkInformationRepository _workInformationRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkInformationServices"/> class.
    /// </summary>
    /// <param name="workInformationRepository">The repository used to manage and retrieve work information data. This parameter cannot be null.</param>
    public WorkInformationServices(IWorkInformationRepository workInformationRepository)
    {
        _workInformationRepository = workInformationRepository;
    }

    /// <summary>
    /// Asynchronously retrieves all work information records.
    /// </summary>
    /// <remarks>This method returns an enumerable collection of <see cref="WorkInformation"/> objects 
    /// representing all available work information records. The collection will be empty if no records exist.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains an  <see
    /// cref="IEnumerable{WorkInformation}"/> of all work information records.</returns>
    public async Task<IEnumerable<WorkInformation>> ListAllAsync()
    {
        return await _workInformationRepository.ListAllAsync();
    }

    /// <summary>
    /// Retrieves detailed information about a specific work item by its unique identifier.
    /// </summary>
    /// <remarks>This method is asynchronous and should be awaited. It queries the underlying repository to
    /// fetch the work item details.</remarks>
    /// <param name="id">The unique identifier of the work item to retrieve. Must be a positive integer.</param>
    /// <returns>A <see cref="WorkInformation"/> object containing the details of the specified work item. Returns <see
    /// langword="null"/> if no work item with the given identifier exists.</returns>
    public async Task<WorkInformation?> ListSingleWorkInformationtAsync(int id)
    {
        return await _workInformationRepository.ListSingleWorkInformationtAsync(id);
    }

    /// <summary>
    /// Asynchronously retrieves a collection of work information associated with the specified career identifier.
    /// </summary>
    /// <param name="careerInternalId">The unique identifier of the career for which work information is to be retrieved. Must be a positive integer.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of  <see
    /// cref="WorkInformation"/> objects associated with the specified career identifier. Returns an empty  collection
    /// if no work information is found.</returns>
    public async Task<IEnumerable<WorkInformation>> ListSpecificInformationAsync(int careerInternalId)
    {
        return await _workInformationRepository.ListSpecificInformationAsync(careerInternalId);
    }

}
