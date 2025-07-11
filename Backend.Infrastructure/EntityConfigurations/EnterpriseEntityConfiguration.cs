using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.EntityConfigurations;

/// <summary>
/// Provides configuration for the <see cref="Enterprise"/> entity type.
/// </summary>
/// <remarks>This class is used to configure the <see cref="Enterprise"/> entity within the Entity Framework Core
/// model. It defines the table name and other entity-specific settings.</remarks>
internal class EnterpriseEntityConfiguration : IEntityTypeConfiguration<Enterprise>
{
    /// <summary>
    /// Configures the entity type for <see cref="Enterprise"/>.
    /// </summary>
    /// <remarks>This method sets up the table name and required properties for the <see cref="Enterprise"/> entity.</remarks>
    /// <param name="builder">An <see cref="EntityTypeBuilder{TEntity}"/> instance used to configure the <see cref="Enterprise"/> entity.</param>
    public void Configure(EntityTypeBuilder<Enterprise> builder)
    {
        builder.ToTable("Enterprise");

        // Configure Name as a value object with a maximum length and conversion logic
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasConversion(
                convertToProviderExpression: x => x!.Name,
                convertFromProviderExpression: x => EntityName.FromDatabase(x));

        // Country (Country VO)
        builder.Property(x => x.Country)
            .HasMaxLength(100)
            .HasConversion(
                convertToProviderExpression: x => x!.Value,
                convertFromProviderExpression: x => Country.FromDatabase(x));

    }
}
