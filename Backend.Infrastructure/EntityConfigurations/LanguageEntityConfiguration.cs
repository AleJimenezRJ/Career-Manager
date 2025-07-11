using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.EntityConfigurations;

/// <summary>
/// Configures the database mapping for the <see cref="Language"/> entity using Entity Framework Core.
/// </summary>
internal class LanguageEntityConfiguration : IEntityTypeConfiguration<Language>
{
    /// <summary>
    /// Configures the entity type <see cref="Language"/> for use with Entity Framework Core.
    /// </summary>
    /// <remarks>This method defines the table name, primary key, property configurations, and relationships
    /// for the <see cref="Language"/> entity. It is intended to be used within the Entity Framework Core model
    /// configuration process.</remarks>
    /// <param name="builder">An <see cref="EntityTypeBuilder{Language}"/> instance used to configure the <see cref="Language"/> entity.</param>
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.ToTable("Language");


        builder.HasKey(x => x.LanguageInternalId);

        // Configure CareerInternalId to auto-generate on insert
        builder.Property(x => x.LanguageInternalId)
            .ValueGeneratedOnAdd();

        // Configure Name as a value object with a maximum length and conversion logic
        builder.Property(x => x.LanguageValue)
            .IsRequired()
            .HasMaxLength(50)
            .HasConversion(
                 x => x.Value,
                 x => LanguageVO.FromDatabase(x));


        builder.HasOne(x => x.Recruitment)
            .WithMany(x => x.LanguageRequested)
            .HasForeignKey(x => x.RecruitmentPK);

    }
}