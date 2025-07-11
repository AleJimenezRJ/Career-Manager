using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Application.Services;

/// <summary>
/// Provides methods for managing industries within a career context.
/// </summary>
/// <remarks>This interface defines operations for retrieving and adding industries associated with a specific
/// career. Implementations of this interface should ensure thread safety and proper handling of asynchronous
/// operations.</remarks>
public interface IIndustryServices
{
    /// <summary>
    /// Asynchronously retrieves a collection of industries.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an  <see cref="IEnumerable{T}"/> of
    /// <see cref="Industry"/> representing the industries.</returns>
    Task<IEnumerable<Industry>> ListIndustriesAsync();

    /// <summary>
    /// Asynchronously adds the specified industries to the career identified by the given internal ID.
    /// </summary>
    /// <param name="careerInternalId">The internal identifier of the career to which the industries will be added. Must be a positive integer.</param>
    /// <param name="industries">The industries to add to the career. Cannot be null.</param>
    /// <returns>A result indicating the success or failure of the operation.</returns>
    Task<Result> AddIndustriesAsync(int careerInternalId, Industry industries);
}
