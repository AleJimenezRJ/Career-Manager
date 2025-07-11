using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Services.Implementations;

/// <summary>
/// Provides services for managing and retrieving opportunities.
/// </summary>
/// <remarks>This class acts as a service layer for interacting with opportunities data,  delegating operations to
/// the underlying repository. It includes methods for  listing opportunities and adding new opportunities.</remarks>
internal class OpportunityServices : IOpportunityServices
{
    /// <summary>
    /// Represents the repository used to access and manage opportunities data.
    /// </summary>
    /// <remarks>This field is a read-only instance of <see cref="IOpportunityRepository"/> and is used
    /// internally to perform operations related to opportunities, such as retrieving, updating, or deleting
    /// records.</remarks>
    private readonly IOpportunityRepository _opportunityRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="OpportunityServices"/> class.
    /// </summary>
    /// <param name="opportunitiesRepository">The repository used to manage and access opportunity data. This parameter cannot be null.</param>
    public OpportunityServices(IOpportunityRepository opportunitiesRepository)
    {
        _opportunityRepository = opportunitiesRepository;
    }

    /// <summary>
    /// Asynchronously retrieves a collection of all opportunities.
    /// </summary>
    /// <remarks>This method returns all opportunities available in the repository. The result is an
    /// enumerable collection that can be iterated over. If no opportunities exist, the returned collection will be
    /// empty.</remarks>
    /// <returns>A task representing the asynchronous operation. The task result contains an enumerable collection of  <see
    /// cref="Opportunity"/> objects.</returns>
    public async Task<IEnumerable<Opportunity>> ListOpportunitiesAsync()
    {
        return await _opportunityRepository.ListAllAsync();
    }

    /// <summary>
    /// Asynchronously adds opportunities associated with a specific career.
    /// </summary>
    /// <param name="careerInternalId">The unique identifier of the career to which the opportunities will be added. Must be a positive integer.</param>
    /// <param name="opportunities">The opportunities data to be added. Cannot be <see langword="null"/>.</param>
    /// <returns>A result indicating the success or failure of the operation. The result will contain an error message if the operation fails.</returns>
    public async Task<Result> AddOpportunitiesAsync(int careerInternalId, Opportunity opportunities)
    {
        return await _opportunityRepository.AddInformationAsync(careerInternalId, opportunities);
    }
}