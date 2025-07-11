using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.EntityConfigurations;

/// <summary>
/// Configures the entity type settings for the <see cref="Industry"/> entity.
/// </summary>
/// <remarks>This configuration defines the table name as "Industries" and specifies constraints and conversions
/// for the <see cref="Industry.Name"/> property. The property is required, has a maximum length of 100 characters,
/// and uses a custom conversion for database storage and retrieval.</remarks>
internal class IndustryEntityConfiguration : IEntityTypeConfiguration<Industry>
{
    /// <summary>
    /// Configures the entity type for <see cref="Industry"/> in the database context.
    /// </summary>
    /// <remarks>This method sets the table name to "Industries" and configures the <c>Name</c> property to be
    /// required, have a maximum length of 100 characters, and use a custom conversion for database storage and
    /// retrieval.</remarks>
    /// <param name="builder">An <see cref="EntityTypeBuilder{TEntity}"/> used to configure the <see cref="Industry"/> entity.</param>
    public void Configure(EntityTypeBuilder<Industry> builder)
    {
        builder.ToTable("Industry");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasConversion(
                convertToProviderExpression: x => x!.Name,
                convertFromProviderExpression: x => EntityName.FromDatabase(x));

        builder.Property(x => x.CSRelated)
            .IsRequired();
    }
}
