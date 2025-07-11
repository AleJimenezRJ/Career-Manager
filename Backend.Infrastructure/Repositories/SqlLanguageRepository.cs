using Microsoft.EntityFrameworkCore;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Repositories;

/// <summary>
/// Provides methods for managing languages in the context of recruitments stored in a SQL database.
/// </summary>
/// <remarks>This repository interacts with the underlying database to retrieve, add, and manage language-related
/// data. It is designed to work with the <see cref="VRCampusDatabaseContext"/> and adheres to the <see
/// cref="ILanguageRepository"/> interface.</remarks>
internal class SqlLanguageRepository : ILanguageRepository
{
    /// <summary>
    /// Represents the database context used to interact with the VR Campus database.
    /// </summary>
    /// <remarks>This field is read-only and is intended to provide access to the underlying database context
    /// for performing data operations within the VR Campus application.</remarks>
    private readonly VRCampusDatabaseContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlLanguageRepository"/> class using the specified database
    /// context.
    /// </summary>
    /// <param name="dbContext">The database context used to interact with the underlying data store. Cannot be null.</param>
    public SqlLanguageRepository(VRCampusDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Asynchronously retrieves a list of all available languages from the database.
    /// </summary>
    /// <remarks>The method returns an enumerable collection of <see cref="Language"/> objects, representing
    /// the languages stored in the database. The query is executed with no tracking, meaning the returned entities are
    /// not tracked by the database context.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/> of
    /// <see cref="Language"/> objects.</returns>
    public async Task<IEnumerable<Language>> ListLanguagesAsync()
    {
        return await _dbContext.Languages
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// Adds a language to the specified recruitment's list of requested languages.
    /// </summary>
    /// <remarks>This method performs the following validations: <list type="bullet">
    /// <item><description>Ensures the recruitment exists for the given <paramref
    /// name="workInformationInternalId"/>.</description></item> <item><description>Checks for duplicate languages in
    /// the recruitment's list of requested languages.</description></item> </list> If the recruitment is not found or
    /// the language already exists, the method returns a failure result with an appropriate error message.</remarks>
    /// <param name="workInformationInternalId">The internal ID of the recruitment's work information. This ID is used to locate the recruitment.</param>
    /// <param name="language">The language to add to the recruitment's list of requested languages. Cannot be <see langword="null"/>.</param>
    /// <returns>A <see cref="Result"/> indicating the outcome of the operation. Returns <see cref="Result.Success"/> if the
    /// language is successfully added. Returns <see cref="Result.Failure"/> if the recruitment is not found, the
    /// language is <see langword="null"/>, or the language already exists in the recruitment's list.</returns>
    public async Task<Result> AddLanguageAsync(int workInformationInternalId, Language language)
    {
        if (language is null)
        {
            return Result.Failure(
                Error.Validation("Language.Null", "The language to add cannot be null."));
        }

        // Find the recruitment by its internal ID
        var recruitment = await _dbContext.Recruitments
            .Include(x => x.LanguageRequested)
            .FirstOrDefaultAsync(x => x.WorkInformationInternalId == workInformationInternalId);

        if (recruitment is null)
        {
            return Result.Failure(
                Error.NotFound("Recruitment.NotFound", $"Recruitment with ID {workInformationInternalId} not found."));
        }

        // Check for duplicates in the recruitment's language list
        if (recruitment.LanguageRequested.Any(x => x.LanguageValue.Value == language.LanguageValue.Value))
        {
            return Result.Failure(
                Error.DuplicatedConflict("Language.Duplicate", $"The language '{language.LanguageValue.Value}' already exists for this recruitment."));
        }

        // Add the language directly to the list
        recruitment.LanguageRequested.Add(language);

        await _dbContext.SaveChangesAsync();

        return Result.Success();
    }
}
