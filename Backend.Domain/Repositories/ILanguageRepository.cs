using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;

/// <summary>
/// Defines a contract for managing languages in the system.
/// </summary>
/// <remarks>This interface provides methods for listing available languages and adding new languages to the
/// system. Implementations of this interface should handle data persistence and validation as required.</remarks>
public interface ILanguageRepository
{
    /// <summary>
    /// List all available languages in the system asynchronously.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Language>> ListLanguagesAsync();

    /// <summary>
    /// Adds a new language to the system asynchronously.
    /// </summary>
    /// <remarks>This method performs validation on the provided <paramref name="language"/> object before
    /// adding it. Ensure that the language object contains valid data to avoid errors during the operation.</remarks>
    /// <param name="language">The language to add. Cannot be null.</param>
    /// <returns>A <see cref="Result"/> object indicating the success or failure of the operation. If the operation succeeds, the
    /// result contains relevant success information; otherwise, it contains error details.</returns>
    Task<Result> AddLanguageAsync(int workInformationInternalId, Language language);

}
