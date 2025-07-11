using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.EntityConfigurations;

/// <summary>
/// Configures the entity type <see cref="Recruitment"/> for use with Entity Framework Core.
/// </summary>
/// <remarks>This configuration maps the <see cref="Recruitment"/> entity to the "Recruitment" table in the
/// database and defines property constraints and conversions for specific fields. The configuration ensures that
/// properties such as <c>ContentDescription</c>, <c>Language</c>, <c>Requisites</c>, and <c>Steps</c>  are properly
/// mapped and constrained according to the application's requirements.</remarks>
internal class RecruitmentEntityConfiguration : IEntityTypeConfiguration<Recruitment>
{
    /// <summary>
    /// Configures the entity type for the <see cref="Recruitment"/> class.
    /// </summary>
    /// <remarks>This method defines the table name, property configurations, and value conversions for the   
    /// <see cref="Recruitment"/> entity. It ensures that certain properties, such as , <see cref="Recruitment.Requisites"/>, and <see
    /// cref="Recruitment.Steps"/>, have specific constraints and conversions applied.</remarks>
    /// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/> used to configure the <see cref="Recruitment"/> entity.</param>
    public void Configure(EntityTypeBuilder<Recruitment> builder)
    {
        builder.ToTable("Recruitment");

        builder.Property(x => x.Requisites)
            .HasMaxLength(700)
            .HasConversion(
                convertToProviderExpression: x => x!.Content,
                convertFromProviderExpression: x => Description.FromDatabase(x));

        builder.Property(x => x.Steps)
            .HasMaxLength(700)
            .HasConversion(
                convertToProviderExpression: x => x!.Content,
                convertFromProviderExpression: x => Description.FromDatabase(x));

        builder.HasMany(x => x.LanguageRequested)
            .WithOne(x => x.Recruitment)
            .HasForeignKey("RecruitmentPK");
    }
}
