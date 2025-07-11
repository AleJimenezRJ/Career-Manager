using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Application.Services.Implementations;

/// <summary>
/// Provides services for managing and retrieving language-related data.
/// </summary>
/// <remarks>This class acts as a service layer for language operations, including listing available languages and
/// adding new languages. It relies on an <see cref="ILanguageRepository"/> for data persistence.</remarks>
internal class LanguageServices : ILanguageServices
{
    /// <summary>
    /// Represents a repository for accessing language-related data.
    /// </summary>
    /// <remarks>This field is intended to store a reference to an implementation of the <see
    /// cref="ILanguageRepository"/> interface. It is used internally to retrieve or manage language-specific
    /// information.</remarks>
    private readonly ILanguageRepository _languageRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="LanguageServices"/> class.
    /// </summary>
    /// <remarks>The <paramref name="languageRepository"/> parameter must not be null. This class depends on
    /// the repository  to perform operations related to language management.</remarks>
    /// <param name="languageRepository">The repository used to manage language-related data.</param>
    public LanguageServices(ILanguageRepository languageRepository)
    {
        _languageRepository = languageRepository;
    }

    /// <summary>
    /// Asynchronously retrieves a collection of supported languages.
    /// </summary>
    /// <remarks>This method returns a list of languages available in the system. The caller can use the
    /// returned collection  to display language options or perform language-specific operations. The collection will be
    /// empty if no  languages are available.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see
    /// cref="IEnumerable{Language}"/> of supported languages.</returns>
    public async Task<IEnumerable<Language>> ListLanguagesAsync()
    {
        return await _languageRepository.ListLanguagesAsync();
    }

    /// <summary>
    /// Adds a new language to the repository asynchronously.
    /// </summary>
    /// <param name="language">The language to add. Cannot be null.</param>
    /// <returns>A <see cref="Result"/> object indicating the success or failure of the operation.</returns>
    public async Task<Result> AddLanguageAsync(int workInformationInternalId, Language language)
    {
        return await _languageRepository.AddLanguageAsync(workInformationInternalId, language);
    }
}
