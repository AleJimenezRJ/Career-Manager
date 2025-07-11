using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Application.Services.Implementations;

/// <summary>
/// Provides services for managing recruitment operations, including listing and adding recruitment information.
/// </summary>
/// <remarks>This class acts as a service layer for recruitment-related operations, delegating data access to an 
/// <see cref="IRecruitmentRepository"/> implementation. It is designed to handle business logic and  coordinate
/// repository interactions.</remarks>
internal class RecruitmentServices : IRecruitmentServices
{
    /// <summary>
    /// Represents the repository used for accessing and managing recruitment-related data.
    /// </summary>
    /// <remarks>This field is read-only and is intended to store a dependency injected instance of  <see
    /// cref="IRecruitmentRepository"/>. It provides access to recruitment data operations  such as retrieving, adding,
    /// or updating recruitment records.</remarks>
    private readonly IRecruitmentRepository _recruitmentRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecruitmentServices"/> class.
    /// </summary>
    /// <param name="recruitmentRepository">The repository used to manage recruitment-related data. This parameter cannot be null.</param>
    public RecruitmentServices(IRecruitmentRepository recruitmentRepository)
    {
        _recruitmentRepository = recruitmentRepository;
    }

    /// <summary>
    /// Retrieves a collection of all recruitments asynchronously.
    /// </summary>
    /// <remarks>This method performs an asynchronous operation to fetch the list of recruitments  from the
    /// underlying data source. Ensure that the caller awaits the returned task  to properly handle the asynchronous
    /// execution.</remarks>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all recruitments.  The collection will be empty if no recruitments
    /// are available.</returns>
    public async Task<IEnumerable<Recruitment>> ListRecruitmentsAsync()
    {
        return await _recruitmentRepository.ListAllAsync();
    }

    /// <summary>
    /// Adds a new recruitment entry for the specified career and enterprise.
    /// </summary>
    /// <remarks>This method asynchronously adds recruitment information to the repository. Ensure that the
    /// provided identifiers are valid and that the <paramref name="recruitment"/> object contains all required
    /// data.</remarks>
    /// <param name="careerInternalId">The internal identifier of the career to associate with the recruitment.</param>
    /// <param name="recruitment">The recruitment details to be added.</param>
    /// <returns>A <see cref="Result"/> indicating the outcome of the operation.</returns>
    public async Task<Result> AddRecruitmentAsync(int careerInternalId, Recruitment recruitment)
    {
        return await _recruitmentRepository.AddInformationAsync(careerInternalId, recruitment);
    }
}