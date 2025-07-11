using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.EntityConfigurations;

/// <summary>
/// Configures the entity type <see cref="WorkInformation"/> for use with Entity Framework Core.
/// </summary>
/// <remarks>This configuration defines the table name, primary key, and property constraints for the <see
/// cref="WorkInformation"/> entity. It also specifies a conversion for the <c>RequiredSkills</c> property and
/// establishes the relationships with the <see cref="Career"/> and the <see cref="Enterprise"/> entities.</remarks>
internal class WorkInformationEntityConfiguration : IEntityTypeConfiguration<WorkInformation>
{
    /// <summary>
    /// Configures the entity type <see cref="WorkInformation"/> for use with Entity Framework Core.
    /// </summary>
    /// <remarks>This method defines the table name, primary key, property configurations, and relationships
    /// for the <see cref="WorkInformation"/> entity. It is intended to be used within the Entity Framework Core model
    /// configuration process.</remarks>
    /// <param name="builder">An <see cref="EntityTypeBuilder{TEntity}"/> instance used to configure the <see cref="WorkInformation"/> entity.</param>
    public void Configure(EntityTypeBuilder<WorkInformation> builder)
    {
        builder.ToTable("WorkInformation");

        builder.HasKey(x => x.WorkInformationInternalId);

        builder.Property(x => x.WorkInformationInternalId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.InformationDescription)
            .IsRequired()
            .HasMaxLength(700)
            .HasConversion(
                convertToProviderExpression: x => x!.Content,
                convertFromProviderExpression: x => Description.FromDatabase(x));

        builder.HasOne<Career>()
            .WithMany(x => x.WorkInformations)
            .HasForeignKey("CareerInternalId");

    }
}
