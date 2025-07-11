using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Application.Services.Implementations;


/// <summary>
/// Provides services for managing industries, including retrieving and adding industry information.
/// </summary>
/// <remarks>This class acts as a service layer between the application and the underlying repository for
/// industries. It provides methods to list industries and add industry information associated with a specific
/// career.</remarks>
internal class IndustryServices : IIndustryServices
{
    /// <summary>
    /// Represents the repository used to access and manage industry-related data.
    /// </summary>
    private readonly IIndustryRepository _industryRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="IndustryServices"/> class.
    /// </summary>
    /// <param name="industriesRepository">The repository used to access and manage industry-related data.  This parameter cannot be null.</param>
    public IndustryServices(IIndustryRepository industriesRepository)
    {
        _industryRepository = industriesRepository;
    }

    /// <summary>
    /// Asynchronously retrieves a collection of all industries.
    /// </summary>
    /// <remarks>This method returns all industries available in the repository. The returned collection  may
    /// be empty if no industries are present. The operation is performed asynchronously.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains an  <see cref="IEnumerable{T}"/> of
    /// <see cref="Industry"/> objects representing the industries.</returns>
    public async Task<IEnumerable<Industry>> ListIndustriesAsync()
    {
        return await _industryRepository.ListAllAsync();
    }

    /// <summary>
    /// Asynchronously adds industry information associated with a specific career identifier.
    /// </summary>
    /// <param name="careerInternalId">The unique identifier of the career to associate the industry information with.</param>
    /// <param name="industries">The industry information to be added.</param>
    /// <returns>A result indicating the success or failure of the operation. The result will contain an error message if the operation fails.</returns>
    public async Task<Result> AddIndustriesAsync(int careerInternalId, Industry industries)
    {
        return await _industryRepository.AddInformationAsync(careerInternalId, industries);
    }
}