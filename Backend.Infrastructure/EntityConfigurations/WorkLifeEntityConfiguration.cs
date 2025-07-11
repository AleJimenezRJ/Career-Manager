using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.EntityConfigurations;

/// <summary>
/// Configures the entity type <see cref="WorkLife"/> for use with Entity Framework Core.
/// </summary>
/// <remarks>This configuration defines the table name as "WorkLife" and specifies constraints and conversions for
/// the <see cref="WorkLife.WorkLifeDescription"/> property. The property is required, has a maximum length of 700 characters,
/// and uses a custom conversion to and from the database format.</remarks>
internal class WorkLifeEntityConfiguration : IEntityTypeConfiguration<WorkLife>
{
    /// <summary>
    /// Configures the entity type <see cref="WorkLife"/> for the database context.
    /// </summary>
    /// <remarks>This method sets the table name to "WorkLife" and configures the <see
    /// cref="WorkLife.WorkLifeDescription"/> property to be required, have a maximum length of 700 characters, and use a custom
    /// conversion for database storage.</remarks>
    /// <param name="builder">An <see cref="EntityTypeBuilder{WorkLife}"/> used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<WorkLife> builder)
    {
        builder.ToTable("WorkLife");

        builder.Property(x => x.AmountFemaleWorkers)
            .IsRequired()
            .HasConversion(
                convertToProviderExpression: x => x!.Number,
                convertFromProviderExpression: x => Workers.FromDatabase(x));

        builder.Property(x => x.AmountMaleWorkers)
            .IsRequired()
            .HasConversion(
                convertToProviderExpression: x => x!.Number,
                convertFromProviderExpression: x => Workers.FromDatabase(x));
    }
}
