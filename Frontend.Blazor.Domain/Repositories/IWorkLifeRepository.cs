using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Repositories;

/// <summary>
/// Defines a repository for accessing and managing work-life data.
/// </summary>
/// <remarks>This interface extends <see cref="IWorkInformationRepository"/> to provide additional functionality 
/// specific to work-life data. Implementations of this interface should support asynchronous operations  for retrieving
/// collections of <see cref="WorkLife"/> objects.</remarks>
public interface IWorkLifeRepository : IWorkInformationRepository
{
    /// <summary>
    /// Asynchronously retrieves a collection of all <see cref="WorkLife"/> objects.
    /// </summary>
    /// <remarks>This method returns all available <see cref="WorkLife"/> instances. The caller can use the
    /// returned collection      to enumerate or process the objects. If no objects are available, the method returns an
    /// empty collection.</remarks>
    /// <returns>A task representing the asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/> of <see
    /// cref="WorkLife"/> objects.</returns>
    new Task<IEnumerable<WorkLife>> ListAllAsync();
}
