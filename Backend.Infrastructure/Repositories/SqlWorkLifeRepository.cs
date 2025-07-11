using Microsoft.EntityFrameworkCore;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Repositories;

/// <summary>
/// Provides data access functionality for retrieving and managing <see cref="WorkLife"/> entities from a SQL database
/// using Entity Framework.
/// </summary>
/// <remarks>This repository is an implementation of the <see cref="IWorkLifeRepository"/> interface and extends
/// the <see cref="SqlWorkInformationRepository"/> base class. It is designed to interact with the <see
/// cref="VRCampusDatabaseContext"/> to perform operations on the <c>WorkLives</c> table.</remarks>
internal class SqlWorkLifeRepository : SqlWorkInformationRepository, IWorkLifeRepository
{
    /// <summary>
    /// Represents the database context used for accessing and managing data in the VR Campus application.
    /// </summary>
    /// <remarks>This field is read-only and is intended to be used internally for database
    /// operations.</remarks>
    private readonly VRCampusDatabaseContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlWorkLifeRepository"/> class with the specified database context.
    /// </summary>
    /// <param name="dbContext">The <see cref="VRCampusDatabaseContext"/> used to interact with the database.  This parameter cannot be <see
    /// langword="null"/>.</param>
    public SqlWorkLifeRepository(VRCampusDatabaseContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Asynchronously retrieves all <see cref="WorkLife"/> entities from the database.
    /// </summary>
    /// <remarks>This method queries the database for all <see cref="WorkLife"/> entities and returns them as
    /// a collection. The operation is performed asynchronously to avoid blocking the calling thread.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains an  <see cref="IEnumerable{T}"/> of
    /// <see cref="WorkLife"/> objects representing all entities in the database.</returns>
    public new async Task<IEnumerable<WorkLife>> ListAllAsync()
    {
        return await _dbContext.WorkLives.ToListAsync();
    }
}
