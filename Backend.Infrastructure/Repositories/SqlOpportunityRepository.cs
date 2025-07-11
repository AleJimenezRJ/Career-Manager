using Microsoft.EntityFrameworkCore;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Repositories;

/// <summary>
/// Provides methods for accessing and managing opportunities in the database.
/// </summary>
/// <remarks>This repository is an implementation of <see cref="IOpportunityRepository"/> that uses a SQL database
/// as the underlying data store. It inherits common functionality from <see
/// cref="SqlWorkInformationRepository"/>.</remarks>
internal class SqlOpportunityRepository : SqlWorkInformationRepository, IOpportunityRepository
{
    /// <summary>
    /// Represents the database context used for accessing and managing data in the VR Campus application.
    /// </summary>
    /// <remarks>This field is read-only and is intended to be used internally for database
    /// operations.</remarks>
    private readonly VRCampusDatabaseContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlOpportunityRepository"/> class with the specified database
    /// context.
    /// </summary>
    /// <param name="dbContext">The <see cref="VRCampusDatabaseContext"/> used to access the database.  This parameter cannot be <see
    /// langword="null"/>.</param>
    public SqlOpportunityRepository(VRCampusDatabaseContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Asynchronously retrieves all opportunities from the database.
    /// </summary>
    /// <remarks>This method queries the database for all opportunities and returns them as a collection.  The
    /// caller is responsible for enumerating the returned collection.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains an  IEnumerable{T} of Opportunity
    /// objects representing all opportunities.</returns>
    public new async Task<IEnumerable<Opportunity>> ListAllAsync()
    {
        return await _dbContext.Opportunities.ToListAsync();
    }

}
