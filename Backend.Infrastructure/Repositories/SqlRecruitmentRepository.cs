using Microsoft.EntityFrameworkCore;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Repositories;

/// <summary>
/// Provides data access functionality for recruitment-related operations using a SQL database.
/// </summary>
/// <remarks>This repository interacts with the <see cref="VRCampusDatabaseContext"/> to retrieve and manage
/// recruitment data. It extends <see cref="SqlWorkInformationRepository"/> and implements <see
/// cref="IRecruitmentRepository"/> to provide recruitment-specific functionality.</remarks>
internal class SqlRecruitmentRepository : SqlWorkInformationRepository, IRecruitmentRepository
{
    /// <summary>
    /// Represents the database context used to interact with the VR Campus database.
    /// </summary>
    /// <remarks>This field is read-only and is intended to be used internally for database
    /// operations.</remarks>
    private readonly VRCampusDatabaseContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlRecruitmentRepository"/> class with the specified database
    /// context.
    /// </summary>
    /// <param name="dbContext">The database context used to interact with the underlying VRCampus database. Cannot be null.</param>
    public SqlRecruitmentRepository(VRCampusDatabaseContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Asynchronously retrieves all recruitment records from the database.
    /// </summary>
    /// <remarks>This method queries the database context to retrieve all recruitment records. The returned
    /// collection  will be empty if no records are found.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains an  <IEnumerable{T}> of
    /// <Recruitment> objects representing all recruitment records.</returns>
    public new async Task<IEnumerable<Recruitment>> ListAllAsync()
    {
        return await _dbContext.Recruitments.ToListAsync();
    }

}
