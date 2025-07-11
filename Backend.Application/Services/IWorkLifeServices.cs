using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Services;

/// <summary>
/// Defines a contract for managing work-life services, including retrieving and adding work-related information for
/// careers.
/// </summary>
/// <remarks>This interface provides methods for interacting with work-life data associated with careers.
/// Implementations of this interface are expected to handle asynchronous operations and ensure thread safety where
/// applicable.</remarks>
public interface IWorkLifeServices
{
    /// <summary>
    /// Asynchronously retrieves a collection of work-related information for the current career.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an  <see
    /// cref="IEnumerable{WorkLife}"/> representing the career's work-related details. If no information is
    /// available, the collection will be empty.</returns>
    Task<IEnumerable<WorkLife>> ListWorkLifeAsync();

    /// <summary>
    /// Asynchronously adds a work-life entry to the specified career.
    /// </summary>
    /// <param name="careerInternalId">The unique identifier of the career to which the work-life entry will be added. Must be a positive integer.</param>
    /// <param name="workLife">The work-life entry to add. Cannot be <see langword="null"/>.</param>
    /// <returns>A result indicating the success or failure of the operation.</returns>
    Task<Result> AddWorkLifeAsync(int careerInternalId, WorkLife workLife);

}
