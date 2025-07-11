using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;

/// <summary>
/// Defines a repository for accessing and managing industry-related data.
/// </summary>
/// <remarks>This interface extends <see cref="IWorkInformationRepository"/> and provides additional functionality
/// specific to industries. Implementations of this interface should support asynchronous operations  for retrieving
/// industry data.</remarks>
public interface IIndustryRepository : IWorkInformationRepository
{
    /// <summary>
    /// Asynchronously retrieves a collection of all industries.
    /// </summary>
    /// <remarks>This method returns all industries available in the system. The returned collection may be
    /// empty if no industries are defined.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/> of
    /// <see cref="Industry"/> objects.</returns>
    new Task<IEnumerable<Industry>> ListAllAsync();
}
