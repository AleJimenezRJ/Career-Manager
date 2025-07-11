using Microsoft.EntityFrameworkCore;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for managing <see cref="Industry"/> entities using SQL database.
/// Inherits from <see cref="SqlWorkInformationRepository"/> to allow shared data access patterns.
/// </summary>
internal class SqlIndustryRepository : SqlWorkInformationRepository, IIndustryRepository
{
    /// <summary>
    /// Represents the database context used for accessing and managing data in the VR Campus application.
    /// </summary>
    /// <remarks>This field is read-only and is intended to be used internally for database
    /// operations.</remarks>
    private readonly VRCampusDatabaseContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlIndustryRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context used to access the data store.</param>
    public SqlIndustryRepository(VRCampusDatabaseContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Retrieves all <see cref="Industry"/> records from the database.
    /// </summary>
    /// <returns>A collection of all <see cref="Industry"/> entries.</returns>
    public new async Task<IEnumerable<Industry>> ListAllAsync()
    {
        return await _dbContext.Industries.ToListAsync();
    }
}
