using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.EntityConfigurations;

/// <summary>
/// Provides configuration for the <see cref="Opportunity"/> entity type.
/// </summary>
/// <remarks>This class is used to configure the <see cref="Opportunity"/> entity for Entity Framework Core. It
/// defines the table name, required properties, and value conversions for the entity. This configuration is applied
/// during the model-building process.</remarks>
internal class OpportunityEntityConfiguration : IEntityTypeConfiguration<Opportunity>
{
    /// <summary>
    /// Configures the entity type for <see cref="Opportunity"/>.
    /// </summary>
    /// <remarks>This method sets up the table name, required properties, and value conversions for the <see
    /// cref="Opportunity"/> entity. It is called by the Entity Framework during model
    /// configuration.</remarks>
    /// <param name="builder">An <see cref="EntityTypeBuilder{TEntity}"/> instance used to configure the <see cref="Opportunity"/> entity.</param>
    public void Configure(EntityTypeBuilder<Opportunity> builder)
    {
        builder.ToTable("Opportunity");

        builder.Property(x => x.Country)
            .HasConversion(
                convertToProviderExpression: x => x!.Value,
                convertFromProviderExpression: x => Country.FromDatabase(x));

    }
}
