using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Visitors;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Repositories;

/// <summary>
/// Provides methods for managing career-related data in the database, including adding careers, retrieving career
/// information, and calculating scholarships based on predefined rules.
/// </summary>
/// <remarks>This repository interacts with the underlying database context to perform CRUD operations and
/// implements business logic for career-related operations. It also collaborates with other repositories, such as <see
/// cref="IWorkInformationRepository"/>, to retrieve additional data required for calculations and validations.</remarks>
internal class SqlCareerRepository : ICareerRepository
{
    private readonly VRCampusDatabaseContext _dbContext;

    private readonly IWorkInformationRepository _workInformationRepository;

    public SqlCareerRepository(VRCampusDatabaseContext dbContext, IWorkInformationRepository workInformationRepository)
    {
        _dbContext = dbContext;
        _workInformationRepository = workInformationRepository;
    }

    /// <summary>
    /// Adds a new career to the database asynchronously.
    /// </summary>
    /// <remarks>This method checks if a career with the same name already exists in the database before
    /// adding the new career. If a duplicate is found, the operation fails. The method also handles various exceptions
    /// that may occur during the database operation, such as concurrency conflicts, operation cancellations, or general
    /// database update errors.</remarks>
    /// <param name="career">The career entity to be added to the database. Cannot be null.</param>
    /// <returns>A <see cref="Result"/> object indicating the outcome of the operation.  Returns <see cref="Result.Success"/> if
    /// the career is successfully added. Returns <see cref="Result.Failure"/> with an appropriate error if the
    /// operation fails due to a duplicate career, database update issues, concurrency conflicts, operation
    /// cancellation, or unexpected errors.</returns>
    public async Task<Result> AddCareerAsync(Career career)
    {
        var careerExists = await _dbContext.Careers.AnyAsync(x => x.Name == career.Name);
        if (careerExists)
        {
            return Result.Failure(DomainErrors.FoundErrors.DuplicatedConflict(career.Name!.Name));
        }
        try
        {
            await _dbContext.Careers.AddAsync(career);
            var changes = await _dbContext.SaveChangesAsync();

            if (changes == 0)
            {
                // No changes were made, treat as a failure
                return Result.Failure(
                    Error.Failure("Database.NoChanges", "No changes were made to the database.")
                );
            }

            return Result.Success();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Concurrency conflict
            return Result.Failure(DomainErrors.FoundErrors.ConcurrencyConflict());
        }
        catch (OperationCanceledException)
        {
            return Result.Failure(Error.Failure("Operation.Canceled", "The operation was canceled."));
        }
        catch (DbUpdateException ex)
        {
            // General database update error
            return Result.Failure(
                Error.Failure("Database.UpdateError", $"A database error occurred: {ex.Message}")
            );
        }
        catch (Exception ex)
        {
            // Unexpected error
            return Result.Failure(
                Error.Failure("General.Unexpected", $"An unexpected error occurred: {ex.Message}")
            );
        }
    }

    /// <summary>
    /// Asynchronously retrieves a list of all careers from the database.
    /// </summary>
    /// <remarks>The returned collection will be empty if no careers are found in the database.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains an  <see cref="IEnumerable{T}"/> of
    /// <see cref="Career"/> objects representing the careers.</returns>
    public async Task<IEnumerable<Career>> ListCareersAsync()
    {
        return await _dbContext.Careers
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a specific career entity by its name.
    /// </summary>
    /// <param name="Name">The name of the career to retrieve. This value cannot be null or empty.</param>
    /// <returns>A <see cref="Career"/> object representing the career with the specified name,  or <see langword="null"/> if no
    /// matching career is found.</returns>
    public async Task<Career?> ListSpecificCareerAsync(string Name)
    {
        var entityName = EntityName.FromDatabase(Name);
        return await _dbContext.Careers
            .FirstOrDefaultAsync(x => x.Name! == entityName);
    }

    /// <summary>
    /// Calculates the scholarship amount for a given career and enterprise.
    /// </summary>
    /// <remarks>This method determines the scholarship amount based on the career's attributes, the
    /// enterprise's country, and specific work information associated with the career and enterprise. The calculated
    /// scholarship is stored in the career entity and saved to the database.</remarks>
    /// <param name="careerName">The name of the career for which the scholarship is being calculated. Cannot be null or empty.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the operation. Returns a failure result if the
    /// specified career or enterprise is not found.</returns>
    public async Task<Result> CalculateScholarshipAsync(string careerName)
    {
        // Get career and enterprise
        var entityName = EntityName.FromDatabase(careerName);

        var career = await _dbContext.Careers.FirstOrDefaultAsync(x => x.Name! == entityName);

        if (career is null)
            return Result.Failure(Error.NotFound("Career.NotFound", $"Career '{careerName}' not found."));

        // Get all work information for this career and enterprise
        var workInformations = await _workInformationRepository.ListSpecificInformationAsync(career.CareerInternalId);

        // Use the visitor to calculate the percentage
        var visitor = new ScholarshipPercentageVisitor(career.IsSteam);
        foreach (var workInfo in workInformations)
        {
            workInfo.Accept(visitor);
        }
        var baseCalculus = visitor.BaseScholarship;
        var percentage = visitor.Percentage;

        decimal calculatedScholarship = baseCalculus + (baseCalculus * percentage);

        career.Scholarship = calculatedScholarship;
        await _dbContext.SaveChangesAsync();

        return Result.Success();
    }

    /// <summary>
    /// Asynchronously searches for records matching the specified keyword across all relevant tables.
    /// </summary>
    /// <remarks>This method executes a stored procedure named "SearchAllTables" in the database, passing the 
    /// specified keyword as a parameter. Ensure that the database connection is properly configured  and the stored
    /// procedure exists before calling this method.</remarks>
    /// <param name="keyword">The keyword to search for. This parameter cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of  <see
    /// cref="SearchResult"/> objects representing the matching records. If no matches are found,  the collection will
    /// be empty.</returns>
    /// <exception cref="InvalidOperationException">Thrown if an error occurs while executing the search operation.</exception>
    public async Task<IEnumerable<SearchResult>> SearchKeywordAsync(string keyword)
    {
        try
        {
            var parameter = new SqlParameter("@keyword", keyword);

            var results = await _dbContext
                .Set<SearchResult>()
                .FromSqlRaw("EXEC SearchAllTables @keyword", parameter)
                .ToListAsync();

            return results;
        }
        catch (Exception ex)
        {
            // Log exception if you have logging in place
            throw new InvalidOperationException("An error occurred while executing the search.", ex);
        }
    }

}
