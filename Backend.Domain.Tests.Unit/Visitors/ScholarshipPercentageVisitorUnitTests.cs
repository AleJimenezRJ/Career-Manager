using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Visitors;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Tests.Unit.Visitors;

/// <summary>
/// Unit tests for <see cref="ScholarshipPercentageVisitor"/> class.
/// Validates percentage and scholarship amount calculations for work-related entities.
/// </summary>
public class ScholarshipPercentageVisitorTests
{
    /// <summary>
    /// Verifies that the visitor sets 50% initial percentage when Steam is true.
    /// </summary>
    [Fact]
    public void Constructor_WithSteamTrue_ShouldSetPercentageTo50()
    {
        var visitor = new ScholarshipPercentageVisitor(true);
        visitor.Percentage.Should().Be(0.5m);
    }

    /// <summary>
    /// Verifies that the visitor sets 20% initial percentage when Steam is false.
    /// </summary>
    [Fact]
    public void Constructor_WithSteamFalse_ShouldSetPercentageTo20()
    {
        var visitor = new ScholarshipPercentageVisitor(false);
        visitor.Percentage.Should().Be(0.2m);
    }

    /// <summary>
    /// Verifies that visiting an enterprise in Costa Rica sets base scholarship to 2500.
    /// </summary>
    [Fact]
    public void VisitEnterprise_CostaRica_ShouldSetBaseScholarshipTo2500()
    {
        var visitor = new ScholarshipPercentageVisitor(false);
        var enterprise = new Enterprise(
            informationDescription: Description.FromDatabase("A description"),
            name: EntityName.FromDatabase("Tech Solutions"),
            country: Country.FromDatabase("Costa Rica")
        );

        visitor.Visit(enterprise);

        visitor.BaseScholarship.Should().Be(2500m);
        visitor.Country.Should().Be("Costa Rica");
    }

    /// <summary>
    /// Verifies that visiting an enterprise outside Costa Rica sets base scholarship to 2000.
    /// </summary>
    [Fact]
    public void VisitEnterprise_OtherCountry_ShouldSetBaseScholarshipTo2000()
    {
        var visitor = new ScholarshipPercentageVisitor(false);
        var enterprise = new Enterprise(
            informationDescription: Description.FromDatabase("A foreign company"),
            name: EntityName.FromDatabase("ForeignTech"),
            country: Country.FromDatabase("Germany")
        );

        visitor.Visit(enterprise);

        visitor.BaseScholarship.Should().Be(2000m);
        visitor.Country.Should().Be("Germany");
    }

    /// <summary>
    /// Tests the behavior of the <see cref="ScholarshipPercentageVisitor"/> when visiting a  <see cref="WorkLife"/>
    /// instance with a female-majority workforce and the "isSteam" flag enabled.
    /// </summary>
    /// <remarks>This test verifies that the scholarship percentage is correctly increased by 18% in total:
    /// 10% for having a female-majority workforce and 8% for the "isSteam" flag being enabled.</remarks>
    [Fact]
    public void VisitWorkLife_FemaleMajority_WithSteam_ShouldIncreasePercentageBy18()
    {
        var visitor = new ScholarshipPercentageVisitor(true);
        var workLife = new WorkLife(
            informationDescription: Description.FromDatabase("A balanced work life"),
            amountMaleWorkers: Workers.FromDatabase(40),
            amountFemaleWorkers: Workers.FromDatabase(60)
        );

        visitor.Visit(workLife);

        // Initial 0.5 + 0.10 (female > 50%) + 0.08 (isSteam)
        visitor.Percentage.Should().Be(0.68m);
    }

    /// <summary>
    /// Tests that visiting a <see cref="WorkLife"/> instance with no female majority and no steam  does not increase
    /// the scholarship percentage.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="ScholarshipPercentageVisitor"/> maintains the initial 
    /// percentage value when visiting a <see cref="WorkLife"/> instance where the number of female  workers does not
    /// exceed the number of male workers and the steam condition is false.</remarks>
    [Fact]
    public void VisitWorkLife_NoFemaleMajority_NoSteam_ShouldNotIncreasePercentage()
    {
        var visitor = new ScholarshipPercentageVisitor(false);
        var workLife = new WorkLife(
            informationDescription: Description.FromDatabase("A balanced work life"),
            amountMaleWorkers: Workers.FromDatabase(60),
            amountFemaleWorkers: Workers.FromDatabase(40)
        );

        visitor.Visit(workLife);

        // Should remain at 0.2 (initial), no increase
        visitor.Percentage.Should().Be(0.2m);
    }

    /// <summary>
    /// Tests that visiting an <see cref="Opportunity"/> located in a different country than the associated  <see
    /// cref="Enterprise"/>, with steam enabled, increases the scholarship percentage by 10%.
    /// </summary>
    /// <remarks>This test verifies the behavior of the <see cref="ScholarshipPercentageVisitor"/> when
    /// visiting an  <see cref="Opportunity"/> in a different country than the <see cref="Enterprise"/> it is associated
    /// with.  The test ensures that the percentage is correctly updated by 10% when steam is enabled.</remarks>
    [Fact]
    public void VisitOpportunity_DifferentCountry_WithSteam_ShouldIncreasePercentageBy10()
    {
        var visitor = new ScholarshipPercentageVisitor(true);
        var enterprise = new Enterprise(
            Description.FromDatabase("A description"), 
            EntityName.FromDatabase("A name"), 
            Country.FromDatabase("Costa Rica")
        );
        visitor.Visit(enterprise); // Set country

        var opportunity = new Opportunity(
            informationDescription: Description.FromDatabase("A great opportunity"),
            country: Country.FromDatabase("Germany")
        );

        visitor.Visit(opportunity);

        visitor.Percentage.Should().Be(0.6m); // 0.5 + 0.1
    }

    /// <summary>
    /// Tests that visiting an <see cref="Opportunity"/> in the same country as the visitor,  with steam enabled, does
    /// not increase the scholarship percentage.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="ScholarshipPercentageVisitor"/> maintains the 
    /// percentage at its initial value when visiting an <see cref="Opportunity"/> located in  the same country as the
    /// visitor, and the steam flag is set to <see langword="true"/>.</remarks>
    [Fact]
    public void VisitOpportunity_SameCountry_WithSteam_ShouldNotIncreasePercentage()
    {
        var visitor = new ScholarshipPercentageVisitor(true);
        visitor.Visit(new Enterprise(
            Description.FromDatabase("A description"),
            EntityName.FromDatabase("A name"),
            Country.FromDatabase("United States")
        ));

        var opportunity = new Opportunity(
            informationDescription: Description.FromDatabase("A opportunity"),
            country: Country.FromDatabase("United States"));

        visitor.Visit(opportunity);

        visitor.Percentage.Should().Be(0.5m);
    }

    /// <summary>
    /// Tests that visiting a computer science-related industry increases the scholarship percentage by 5%.
    /// </summary>
    /// <remarks>This test verifies the behavior of the <see cref="ScholarshipPercentageVisitor"/> when
    /// visiting an  industry marked as computer science-related. The expected outcome is that the percentage increases 
    /// by 0.05 (5%) from its initial value.</remarks>
    [Fact]
    public void VisitIndustry_CSRelated_ShouldIncreasePercentageBy5()
    {
        var visitor = new ScholarshipPercentageVisitor(true);
        var industry = new Industry(
            informationDescription: Description.FromDatabase("A tech industry"),
            name: EntityName.FromDatabase("Tech Industry"),
            csRelated: true);

        visitor.Visit(industry);

        visitor.Percentage.Should().Be(0.55m); // 0.5 + 0.05
    }

    /// <summary>
    /// Tests that visiting a non-computer science-related industry does not increase the scholarship percentage.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="ScholarshipPercentageVisitor"/> correctly maintains
    /// the percentage  when visiting an industry marked as not related to computer science. The initial percentage is
    /// expected  to remain unchanged.</remarks>
    [Fact]
    public void VisitIndustry_NotCSRelated_ShouldNotIncreasePercentage()
    {
        var visitor = new ScholarshipPercentageVisitor(true);
        var industry = new Industry(
            informationDescription: Description.FromDatabase("A non-tech industry"),
            name: EntityName.FromDatabase("Not tech Industry"),
            csRelated: false);

        visitor.Visit(industry);

        visitor.Percentage.Should().Be(0.5m);
    }

    /// <summary>
    /// Tests the behavior of the <see cref="ScholarshipPercentageVisitor"/> when visiting a  <see cref="WorkLife"/>
    /// instance with no workers.
    /// </summary>
    /// <remarks>This test ensures that the scholarship percentage remains unchanged when the  <see
    /// cref="WorkLife"/> instance has zero male and female workers.</remarks>
    [Fact]
    public void VisitWorkLife_NoWorkers_ShouldNotChangePercentage()
    {
        var visitor = new ScholarshipPercentageVisitor(false);
        var workLife = new WorkLife(
            informationDescription: Description.FromDatabase("No workers"),
            amountMaleWorkers: Workers.FromDatabase(0),
            amountFemaleWorkers: Workers.FromDatabase(0)
        );

        visitor.Visit(workLife);

        visitor.Percentage.Should().Be(0.2m); // Remains unchanged
    }

    /// <summary>
    /// Tests the behavior of the <see cref="ScholarshipPercentageVisitor"/> when visiting a  <see cref="Recruitment"/>
    /// object that includes English and multiple languages, ensuring  the scholarship percentage increases by 0.15.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="ScholarshipPercentageVisitor"/> correctly applies  the
    /// percentage increase for English language proficiency and the presence of multiple  languages in the recruitment
    /// object. The expected final percentage is 0.65m, calculated  as the initial percentage (0.5) plus 0.10 for
    /// English and 0.05 for multiple languages.</remarks>
    [Fact]
    public void VisitRecruitment_EnglishAndMultipleLanguages_ShouldIncreaseBy15()
    {
        var visitor = new ScholarshipPercentageVisitor(true);

        var recruitment = new Recruitment(
            informationDescription: Description.FromDatabase("Fullstack Dev"),
            steps: Description.FromDatabase("Step 1, Step 2"),
            requisites: Description.FromDatabase("Experience in C#"),
            languages: new List<Language>
            {
            new Language(LanguageVO.FromDatabase("English")),
            new Language(LanguageVO.FromDatabase("Spanish"))
            }
        );

        visitor.Visit(recruitment);

        // Initial 0.5 + 0.10 (English) + 0.05 (multiple languages)
        visitor.Percentage.Should().Be(0.65m);
    }

    /// <summary>
    /// Tests that visiting a recruitment with one non-English language does not increase the scholarship percentage.
    /// </summary>
    /// <remarks>This test verifies the behavior of the <see cref="ScholarshipPercentageVisitor"/> when the
    /// recruitment contains  a single non-English language. The initial percentage is expected to remain
    /// unchanged.</remarks>
    [Fact]
    public void VisitRecruitment_OneNonEnglishLanguage_ShouldNotIncrease()
    {
        var visitor = new ScholarshipPercentageVisitor(true);

        var recruitment = new Recruitment(
            informationDescription: Description.FromDatabase("Frontend Dev"),
            steps: Description.FromDatabase("Step A, Step B"),
            requisites: Description.FromDatabase("Experience in Angular"),
            languages: new List<Language>
            {
            new Language(LanguageVO.FromDatabase("Spanish"))
            }
        );

        visitor.Visit(recruitment);

        // Initial isSteam = true => 0.5, no increase
        visitor.Percentage.Should().Be(0.5m);
    }

}
