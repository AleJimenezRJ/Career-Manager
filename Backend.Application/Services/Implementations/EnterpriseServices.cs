using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Services.Implementations;

/// <summary>
/// Provides services for managing enterprises, including listing, adding, and retrieving specific enterprises.
/// </summary>
/// <remarks>This class acts as a service layer for enterprise-related operations, delegating the actual data
/// access to an underlying repository implementation. It is designed to handle asynchronous operations for
/// scalability.</remarks>
internal class EnterpriseServices : IEnterpriseServices
{
    /// <summary>
    /// Represents the repository used to interact with enterprise-related data.
    /// </summary>
    /// <remarks>This field is intended to store a reference to an implementation of <see
    /// cref="IEnterpriseRepository"/>,  which provides methods for accessing and managing enterprise data.</remarks>
    private readonly IEnterpriseRepository _enterpriseRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnterpriseServices"/> class.
    /// </summary>
    /// <param name="enterpriseRepository">The repository used to manage enterprise-related data. This parameter cannot be null.</param>
    public EnterpriseServices(IEnterpriseRepository enterpriseRepository)
    {
        _enterpriseRepository = enterpriseRepository;
    }

    /// <summary>
    /// Asynchronously retrieves a collection of all enterprises.
    /// </summary>
    /// <remarks>This method fetches all enterprises from the underlying data source. The returned  collection
    /// may be empty if no enterprises are available.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains  a collection of <see
    /// cref="Enterprise"/> objects representing all enterprises.</returns>
    public async Task<IEnumerable<Enterprise>> ListEnterpriseAsync()
    {
        return await _enterpriseRepository.ListAllAsync();
    }

    /// <summary>
    /// Asynchronously adds enterprise information associated with a specific career identifier.
    /// </summary>
    /// <param name="careerInternalId">The unique identifier of the career to which the enterprise information will be added. Must be a positive
    /// integer.</param>
    /// <param name="enterprise">The enterprise object containing the information to be added.  Cannot be <see langword="null"/>.</param>
    /// <returns>A <see cref="Result"/> object indicating the outcome of the operation.  The result may include success or
    /// failure details.</returns>
    public async Task<Result> AddEnterpriseAsync(int careerInternalId, Enterprise enterprise)
    {
        return await _enterpriseRepository.AddInformationAsync(careerInternalId, enterprise);
    }
}
