using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Services;

/// <summary>
/// Defines methods for managing recruitment information, including listing and adding recruitments.
/// </summary>
/// <remarks>This interface provides asynchronous methods to interact with recruitment data, allowing clients to
/// retrieve  a list of opportunities or add new recruitment entries. Implementations should handle data persistence and
/// validation as appropriate.</remarks>
public interface IRecruitmentServices
{
    /// <summary>
    /// Asynchronously retrieves a collection of all active recruitments.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an  <see cref="IEnumerable{T}"/> of
    /// <see cref="Recruitment"/> objects representing the active recruitments.</returns>
    Task<IEnumerable<Recruitment>> ListRecruitmentsAsync();

    /// <summary>
    /// Adds a new recruitment entry for the specified career and enterprise.
    /// </summary>
    /// <param name="careerInternalId">The internal identifier of the career to associate with the recruitment.</param>
    /// <param name="recruitment">The recruitment details to be added.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result contains a <see
    /// cref="Result"/>  indicating the success or failure of the operation.</returns>
    Task<Result> AddRecruitmentAsync(int careerInternalId, Recruitment recruitment);
}
