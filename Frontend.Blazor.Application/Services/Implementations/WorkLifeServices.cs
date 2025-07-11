using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Application.Services.Implementations;

/// <summary>
/// Provides services for managing work-life information, including listing and adding work-life records.
/// </summary>
/// <remarks>This class acts as a service layer for interacting with work-life data, delegating operations to an
/// underlying repository. It is designed to handle asynchronous operations for retrieving and adding work-life
/// records.</remarks>
internal class WorkLifeServices : IWorkLifeServices
{
    /// <summary>
    /// Represents the repository used to access and manage work-life-related data.
    /// </summary>
    /// <remarks>This field is read-only and is intended to store a reference to an implementation of the <see
    /// cref="IWorkLifeRepository"/> interface. It is used internally to perform operations related to work-life data
    /// management.</remarks>
    private readonly IWorkLifeRepository _workLifeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkLifeServices"/> class.
    /// </summary>
    /// <param name="workLifeRepository">The repository used to access and manage work-life data. This parameter cannot be null.</param>
    public WorkLifeServices(IWorkLifeRepository workLifeRepository)
    {
        _workLifeRepository = workLifeRepository;
    }

    /// <summary>
    /// Asynchronously retrieves a collection of all <see cref="WorkLife"/> entities.
    /// </summary>
    /// <remarks>This method returns all <see cref="WorkLife"/> records available in the repository. The
    /// returned collection may be empty if no records are found.</remarks>
    /// <returns>A task representing the asynchronous operation. The task result contains an  <see cref="IEnumerable{T}"/> of
    /// <see cref="WorkLife"/> entities.</returns>
    public async Task<IEnumerable<WorkLife>> ListWorkLifeAsync()
    {
        return await _workLifeRepository.ListAllAsync();
    }

    /// <summary>
    /// Asynchronously adds work-life information to the specified career.
    /// </summary>
    /// <param name="careerInternalId">The unique identifier of the career to which the work-life information will be added. Must be a positive
    /// integer.</param>
    /// <param name="workLife">The work-life information to add. Cannot be <see langword="null"/>.</param>
    /// <returns>A result indicating the success or failure of the operation. The result will contain an error message if the operation fails.</returns>
    public async Task<Result> AddWorkLifeAsync(int careerInternalId, WorkLife workLife)
    {
        return await _workLifeRepository.AddInformationAsync(careerInternalId, workLife);
    }
}
