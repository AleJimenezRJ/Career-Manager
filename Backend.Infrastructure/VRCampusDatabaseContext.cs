using Microsoft.EntityFrameworkCore;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Infrastructure.EntityConfigurations;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure;

/// <summary>
/// Represents the Entity Framework Core database context for the VR Campus application.
/// Manages the entity sets and applies entity configurations for persistence.
/// </summary>
internal class VRCampusDatabaseContext : DbContext
{
    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}"/> representing the Careers table.
    /// </summary>
    public virtual DbSet<Career> Careers { get; set; } = null!;

    /// <summary>
    /// Gets or sets the database set for managing <see cref="WorkInformation"/> entities.
    /// </summary>
    public virtual DbSet<WorkInformation> WorkInformations { get; set; } = null!;

    /// <summary>
    /// Gets or sets the database set representing the collection of industries.
    /// </summary>
    public virtual DbSet<Industry> Industries { get; set; } = null!;

    /// <summary>
    /// Gets or sets the database set for managing <see cref="WorkLife"/> entities.
    /// </summary>
    public virtual DbSet<WorkLife> WorkLives { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of opportunities in the database.
    /// </summary>
    public virtual DbSet<Opportunity> Opportunities { get; set; } = null!;

    /// <summary>
    /// Gets or sets the database set for managing <see cref="Enterprise"/> entities.
    /// </summary>
    public virtual DbSet<Enterprise> Enterprises { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of recruitments in the database context.
    /// </summary>
    public virtual DbSet<Recruitment> Recruitments { get; set; } = null!;

    /// <summary>
    /// Gets or sets the database set representing the collection of languages.
    /// </summary>
    public virtual DbSet<Language> Languages { get; set; } = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="VRCampusDatabaseContext"/> class using the specified options.
    /// </summary>
    /// <param name="options">The options to configure the context.</param>
    public VRCampusDatabaseContext(DbContextOptions<VRCampusDatabaseContext> options) : base(options)
    {
    }

    /// <summary>
    /// Configures the model using the Fluent API.
    /// Applies entity configurations such as property mappings, constraints, and relationships.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the model for the context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply the configuration for all entities
        modelBuilder.ApplyConfiguration(new CareerEntityConfiguration());
        modelBuilder.ApplyConfiguration(new WorkInformationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new IndustryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new WorkLifeEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OpportunityEntityConfiguration());
        modelBuilder.ApplyConfiguration(new EnterpriseEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RecruitmentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new LanguageEntityConfiguration());
        modelBuilder.Entity<SearchResult>().HasNoKey().ToView(null); // Not mapped to a real table or view

    }

    [Obsolete("Only meant to be used by Moq library. Do not use in code.")]
    internal VRCampusDatabaseContext()
    {
        // This constructor is only for mocking purposes and should not be used in production code.
        // It allows the creation of a VRCampusDatabaseContext instance without any configuration.
    }
}
