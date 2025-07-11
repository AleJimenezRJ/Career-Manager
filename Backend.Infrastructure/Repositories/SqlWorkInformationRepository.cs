using Microsoft.EntityFrameworkCore;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for managing <see cref="WorkInformation"/> entities using SQL database.
/// </summary>
internal class SqlWorkInformationRepository : IWorkInformationRepository
{
    private readonly VRCampusDatabaseContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlWorkInformationRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context used to access the data store.</param>
    public SqlWorkInformationRepository(VRCampusDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Retrieves all <see cref="WorkInformation"/> records from the database.
    /// </summary>
    /// <returns>A collection of all <see cref="WorkInformation"/> entries.</returns>
    public async Task<IEnumerable<WorkInformation>> ListAllAsync()
    {
        return await _dbContext.WorkInformations.ToListAsync();
    }

    /// <summary>
    /// Adds a new work information record to the database and associates it with the specified career and enterprise.
    /// </summary>
    /// <remarks>This method verifies the existence of the specified career and enterprise before adding the
    /// work information. If either the career or enterprise does not exist, the method returns a failure result. The
    /// method also sets shadow properties to associate the work information with the provided career and enterprise
    /// IDs.</remarks>
    /// <param name="careerInternalId">The internal identifier of the career to associate with the work information. Must correspond to an existing
    /// career in the database.</param>
    /// <param name="workInformation">The <see cref="WorkInformation"/> object containing the details of the work information to be added. Cannot be
    /// null.</param>
    /// <returns>A <see cref="Result"/> indicating the outcome of the operation. Returns <see cref="Result.Success"/> if the work
    /// information is successfully added. Returns <see cref="Result.Failure"/> with an appropriate error if the
    /// operation fails.</returns>
    public async Task<Result> AddInformationAsync(int careerInternalId, WorkInformation workInformation)
    {
        // Check if the Career exists
        var careerExists = await _dbContext.Careers.AnyAsync(x => x.CareerInternalId == careerInternalId);
        if (!careerExists)
        {
            return Result.Failure(
                Error.NotFound("Career.NotFound", $"Career with ID {careerInternalId} does not exist.")
            );
        }

        try
        {
            await _dbContext.WorkInformations.AddAsync(workInformation);

            // Set shadow properties
            _dbContext.Entry(workInformation).Property("CareerInternalId").CurrentValue = careerInternalId;
            var changes = await _dbContext.SaveChangesAsync();

            if (changes == 0)
            {
                return Result.Failure(
                    Error.Failure("Database.NoChanges", "No changes were made to the database.")
                );
            }
            return Result.Success();
        }
        // Catch concurrency conflicts when multiple users try to update the same record simultaneously
        catch (DbUpdateConcurrencyException)
        {
            return Result.Failure(DomainErrors.FoundErrors.ConcurrencyConflict());
        }
        // Catch other database update exceptions
        catch (DbUpdateException ex)
        {
            return Result.Failure(Error.Failure("Database.UpdateError", ex.Message));
        }
        catch (OperationCanceledException)
        {             
            return Result.Failure(Error.Failure("Operation.Canceled", "The operation was canceled."));
        }
    }

    /// <summary>
    /// Retrieves <see cref="WorkInformation"/> records associated with a specific career.
    /// </summary>
    /// <param name="careerInternalId">The internal ID of the career.</param>
    /// <param name="enterpriseInternalId">The internal ID of the enterprise.</param>
    /// <returns>A collection of matching <see cref="WorkInformation"/> entries.</returns>
    public async Task<IEnumerable<WorkInformation>> ListSpecificInformationAsync(int careerInternalId)
    {
        return await _dbContext.WorkInformations
            .Where(x => EF.Property<int>(x, "CareerInternalId") == careerInternalId)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a single <see cref="WorkInformation"/> entry by its internal ID.
    /// </summary>
    /// <param name="id">The internal ID of the work information.</param>
    /// <returns>The <see cref="WorkInformation"/> entry if found; otherwise, <c>null</c>.</returns>
    public async Task<WorkInformation?> ListSingleWorkInformationtAsync(int id)
    {
        return await _dbContext.WorkInformations.FirstOrDefaultAsync(x => x.WorkInformationInternalId == id);
    }
}
