using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Mappers;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Repositories;

/// <summary>
/// Provides methods for retrieving and managing work information associated with careers.
/// </summary>
/// <remarks>This repository interacts with an underlying API client to fetch, add, and manage work information.
/// It supports operations such as retrieving specific work information by career ID, fetching all work information, and
/// adding new work information.</remarks>
internal class KiotaWorkInformationRepository : IWorkInformationRepository
{
    /// <summary>
    /// Represents the API client used to perform HTTP requests to external services.
    /// </summary>
    /// <remarks>This field is read-only and is intended to encapsulate the functionality for interacting with
    /// external APIs. It is typically initialized during the construction of the containing class.</remarks>
    private readonly ApiClient _apiClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="KiotaWorkInformationRepository"/> class.
    /// </summary>
    /// <param name="apiClient">The API client used to interact with the underlying service. This parameter cannot be null.</param>
    public KiotaWorkInformationRepository(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }


    /// <summary>
    /// Retrieves a collection of work information associated with the specified career.
    /// </summary>
    /// <remarks>This method performs an asynchronous operation to fetch work information from an external
    /// API. Ensure that the provided <paramref name="careerInternalId"/> corresponds to a valid career.</remarks>
    /// <param name="careerInternalId">The internal identifier of the career for which to retrieve work information. Must be a valid, non-negative
    /// integer.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an  <IEnumerable{WorkInformation}>
    /// representing the work information for the specified career. If no work information is available, an empty
    /// collection is returned.</returns>
    public async Task<IEnumerable<WorkInformation>> ListSpecificInformationAsync(int careerInternalId)
    {
        var response = await _apiClient
            .Careers[careerInternalId]
            .WorkInformation
            .GetAsync();

        return response?.WorkInformations?.ToDomain() ?? Enumerable.Empty<WorkInformation>();
    }

    /// <summary>
    /// Retrieves detailed information about a specific work item by its unique identifier.
    /// </summary>
    /// <remarks>This method asynchronously fetches the work item details from an external API.  Ensure that
    /// the provided <paramref name="id"/> corresponds to a valid work item.</remarks>
    /// <param name="id">The unique identifier of the work item to retrieve. Must be a positive integer.</param>
    /// <returns>A <see cref="WorkInformation"/> object containing the details of the specified work item,  or <see
    /// langword="null"/> if the work item does not exist.</returns>
    public async Task<WorkInformation?> ListSingleWorkInformationtAsync(int id)
    {
        var response = await _apiClient
            .WorkInformation[id]
            .GetAsync();
        return response?.WorkInformation!.ToDomain();
    }

    /// <summary>
    /// Asynchronously retrieves a collection of all work information records.
    /// </summary>
    /// <remarks>This method fetches work information data from the underlying API client and converts it to
    /// domain objects. If no records are available, an empty collection is returned.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see
    /// cref="IEnumerable{WorkInformation}"/>  representing the collection of work information records. Returns an empty
    /// collection if no records are found.</returns>
    public async Task<IEnumerable<WorkInformation>> ListAllAsync()
    {
        var response = await _apiClient.WorkInformation.GetAsync();
        return response?.WorkInformations?.ToDomain() ?? Enumerable.Empty<WorkInformation>();
    }

    /// <summary>
    /// Adds work information to the specified career record asynchronously.
    /// </summary>
    /// <remarks>This method performs the operation asynchronously. Currently not implemented</remarks>
    /// <param name="careerInternalId">The unique identifier of the career record to which the work information will be added.</param>
    /// <param name="workInformation">The work information to associate with the career record. Cannot be null.</param>
    /// <returns>A <see cref="Result"/> indicating the outcome of the operation.  Returns <see langword="Success"/> if the
    /// operation completes successfully.</returns>
    public async Task<Result> AddInformationAsync(int careerInternalId, WorkInformation workInformation)
    {
        return Result.Success();
    }

}