using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;

/// <summary>
/// Defines a repository for managing recruitment information.
/// </summary>
/// <remarks>This interface extends <see cref="IWorkInformationRepository"/> and provides additional functionality
/// specific to recruitment.</remarks>
public interface IRecruitmentRepository : IWorkInformationRepository
{
    /// <summary>
    /// Asynchronously retrieves all recruitment records.
    /// </summary>
    /// <remarks>This method retrieves all recruitment records without applying any filters or pagination. 
    /// Use this method when you need to access the complete list of recruitments.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains an  IEnumerable{T} of Recruitment
    /// objects representing all recruitment records.</returns>
    new Task<IEnumerable<Recruitment>> ListAllAsync();
}
