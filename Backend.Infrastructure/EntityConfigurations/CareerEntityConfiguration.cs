using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.EntityConfigurations;

/// <summary>
/// Configures the database mapping for the <see cref="Career"/> entity using Entity Framework Core.
/// Defines how each property and value object is persisted in the database.
/// </summary>
internal class CareerEntityConfiguration : IEntityTypeConfiguration<Career>
{
    /// <summary>
    /// Configures the entity-to-table mapping and property conversions for the <see cref="Career"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the <see cref="Career"/> entity.</param>
    public void Configure(EntityTypeBuilder<Career> builder)
    {
        // Map to the "Careers" table
        builder.ToTable("Career");

        // Configure primary key
        builder.HasKey(x => x.CareerInternalId);

        // Configure CareerInternalId to auto-generate on insert
        builder.Property(x => x.CareerInternalId)
            .ValueGeneratedOnAdd();

        // Configure Name as a value object with a maximum length and conversion logic
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasConversion(
                convertToProviderExpression: x => x!.Name,
                convertFromProviderExpression: x => EntityName.FromDatabase(x));

        // Configure Description as a value object with a maximum length and conversion logic
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(700)
            .HasConversion(
                convertToProviderExpression: x => x!.Content,
                convertFromProviderExpression: x => Description.FromDatabase(x));

        // Configure SemestersNumber as a value object with conversion logic
        builder.Property(x => x.SemestersNumber)
            .IsRequired()
            .HasConversion(
                convertToProviderExpression: x => x!.Number,
                convertFromProviderExpression: x => SemestersNumber.FromDatabase(x));

        // Configure Modality as a value object with a maximum length and conversion logic
        builder.Property(x => x.Modality)
            .IsRequired()
            .HasMaxLength(50)
            .HasConversion(
                convertToProviderExpression: x => x!.Value,
                convertFromProviderExpression: x => Modality.FromDatabase(x));

        // Configure DegreeTitle as a value object with a maximum length and conversion logic
        builder.Property(x => x.DegreeTitle)
            .IsRequired()
            .HasMaxLength(50)
            .HasConversion(
                convertToProviderExpression: x => x!.Value,
                convertFromProviderExpression: x => DegreeTitle.FromDatabase(x));

        // Configure Scholarship as a double with a precision of 18 and scale of 2
        builder.Property(x => x.Scholarship)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        // Configure IsSteam as a boolean with a default value of false
        builder.Property(x => x.IsSteam)
            .IsRequired();

    }
}
