using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;

/// <summary>
/// Defines a repository interface for managing enterprise-related data.
/// </summary>
/// <remarks>This interface extends <see cref="IWorkInformationRepository"/> to provide additional functionality 
/// for retrieving and managing enterprise-specific information.</remarks>
public interface IEnterpriseRepository : IWorkInformationRepository
{
    /// <summary>
    /// Asynchronously retrieves a collection of all enterprises.
    /// </summary>
    /// <remarks>This method returns all enterprises available in the system. The caller can use the returned
    /// collection      to enumerate or process enterprise data. If no enterprises are available, the returned
    /// collection will be empty.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see
    /// cref="IEnumerable{Enterprise}"/> representing the collection of enterprises.</returns>
    new Task<IEnumerable<Enterprise>> ListAllAsync();
}
