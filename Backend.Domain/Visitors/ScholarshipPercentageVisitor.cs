using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Visitors;

/// <summary>
/// Represents a visitor that calculates the scholarship percentage based on various work information criteria.
/// </summary>
/// <remarks>This class implements the <see cref="IWorkInformationVisitor"/> interface to evaluate different
/// aspects of work information, such as work-life balance, opportunities, industry characteristics, and recruitment
/// details, to determine the scholarship percentage. The percentage is initialized based on whether the enterprise is
/// part of Steam and is adjusted based on specific conditions.</remarks>
public class ScholarshipPercentageVisitor : IWorkInformationVisitor
{
    /// <summary>
    /// Indicates whether the career is STEAM or not
    /// </summary>
    private readonly bool _isSteam;

    /// <summary>
    /// Gets the base scholarship amount awarded to a student.
    /// </summary>
    public decimal BaseScholarship { get; private set; } = 2000m;


    /// <summary>
    /// Gets the percentage value represented as a decimal.
    /// </summary>
    public decimal Percentage { get; private set; }

    /// <summary>
    /// Gets the name of the country associated with the current instance.
    /// </summary>
    public string Country { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ScholarshipPercentageVisitor"/> class,  setting the scholarship
    /// percentage based on the enterprise's platform and country.
    /// </summary>
    /// <param name="isSteam">A value indicating whether the enterprise operates on the Steam platform.  If <see langword="true"/>, the
    /// scholarship percentage is set to 50%; otherwise, it is set to 20%.</param>
    public ScholarshipPercentageVisitor(bool isSteam)
    {
        _isSteam = isSteam;
        Percentage = isSteam ? 0.5m : 0.2m;
    }

    /// <summary>
    /// Updates the base scholarship amount based on the specified enterprise's country.
    /// </summary>
    /// <remarks>If the enterprise's country is "Costa Rica", the base scholarship is set to 2500. Otherwise,
    /// it is set to 2000. Ensure that the <paramref name="enterprise"/> parameter is properly initialized and its
    /// <c>Country</c> property is set before calling this method.</remarks>
    /// <param name="enterprise">The enterprise whose country determines the scholarship amount. Must not be <see langword="null"/>.</param>
    public void Visit(Enterprise enterprise)
    {
        BaseScholarship = (enterprise.Country!.Value == "Costa Rica") ? 2500m : 2000m;
        Country = enterprise.Country!.Value;
    }


    /// <summary>
    /// Updates the <see cref="Percentage"/> property based on the gender distribution of workers  in the provided
    /// <paramref name="workLife"/> instance.
    /// </summary>
    /// <remarks>This method calculates the percentage of female workers relative to the total number of
    /// workers  and adjusts the <see cref="Percentage"/> property accordingly. If the percentage of female workers 
    /// exceeds 50%, an additional increment is applied. If the internal state indicates that steam is active,  another
    /// increment is added.</remarks>
    /// <param name="workLife">An instance of <see cref="WorkLife"/> containing the number of female and male workers.  Both <see
    /// cref="WorkLife.AmountFemaleWorkers"/> and <see cref="WorkLife.AmountMaleWorkers"/>  must be non-null for the
    /// calculation to proceed.</param>
    public void Visit(WorkLife workLife)
    {
        if (workLife.AmountFemaleWorkers is not null && workLife.AmountMaleWorkers is not null)
        {
            int total = workLife.AmountFemaleWorkers.Number + workLife.AmountMaleWorkers.Number;
            if (total > 0)
            {
                decimal femalePercent = (decimal)workLife.AmountFemaleWorkers.Number / total;
                if (femalePercent > 0.5m)
                    Percentage += 0.10m;
                if (_isSteam)
                    Percentage += 0.08m;
            }
        }
    }

    /// <summary>
    /// Updates the percentage value based on the specified opportunity's country and enterprise conditions.
    /// </summary>
    /// <remarks>If the opportunity's country is not null, differs from the enterprise country, and the steam
    /// condition is active,  the percentage is increased by 10%.</remarks>
    /// <param name="opportunity">The opportunity to evaluate. Must not be null.</param>
    public void Visit(Opportunity opportunity)
    {
        if (opportunity.Country is not null && opportunity.Country.Value != Country && _isSteam)
            Percentage += 0.10m;
    }

    /// <summary>
    /// Updates the percentage value based on the characteristics of the specified industry.
    /// </summary>
    /// <remarks>If the <see cref="Industry.CSRelated"/> property of the specified industry is <see
    /// langword="true"/>,  the percentage is increased by 0.05. This method does not modify the <paramref
    /// name="industry"/> object.</remarks>
    /// <param name="industry">The industry to evaluate. Must not be null.</param>
    public void Visit(Industry industry)
    {
        if (industry.CSRelated)
            Percentage += 0.05m;
    }

    /// <summary>
    /// Updates the percentage value based on the specified recruitment criteria.
    /// </summary>
    /// <remarks>This method adjusts the <see cref="Percentage"/> property based on the following conditions:
    /// <list type="bullet"> <item> <description>If any requested language is English (case-insensitive), 10% is added
    /// to the percentage.</description> </item> <item> <description>If more than one language is requested, an
    /// additional 5% is added to the percentage.</description> </item> </list> The method does not modify the <paramref
    /// name="recruitment"/> object itself.</remarks>
    /// <param name="recruitment">The recruitment object containing the requested languages and other relevant information.</param>
    public void Visit(Recruitment recruitment)
    {
        if (recruitment.LanguageRequested is { Count: > 0 })
        {
            // Add 10% if any language is English (case-insensitive)
            if (recruitment.LanguageRequested.Any(
                    x => string.Equals(x.LanguageValue.Value, "English", StringComparison.OrdinalIgnoreCase)))
            {
                Percentage += 0.10m;
            }

            // Add 5% if more than one language is requested
            if (recruitment.LanguageRequested.Count > 1)
            {
                Percentage += 0.05m;
            }
        }
    }
}