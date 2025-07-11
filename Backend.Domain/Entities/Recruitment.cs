using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Visitors;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;

/// <summary>
/// Represents a recruitment opportunity associated with workInformation
/// </summary>
public class Recruitment : WorkInformation
{
    /// <summary>
    /// Gets or sets the steps or instructions associated with the current operation.
    /// </summary>
    public Description? Steps { get; set; }

    /// <summary>
    /// Gets or sets the requisites for the recruitment.
    /// </summary>
    public Description? Requisites { get; set; }

    /// <summary>
    /// Gets or sets the list of languages requested for the recruitment.
    /// </summary>
    public List<Language> LanguageRequested { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Recruitment"/> class with full details.
    /// </summary>
    /// <param name="workInformationInternalId">The internal identifier for the work information.</param>
    /// <param name="informationDescription">The general description for the recruitment.</param>
    /// <param name="steps">The steps involved in the recruitment process.</param>
    /// <param name="languages">The language used in the recruitment.</param>
    /// <param name="contentDescription">A description of the recruitment content.</param>
    /// <param name="requisites">The requirements for the recruitment.</param>
    public Recruitment(
        int workInformationInternalId,
        Description? informationDescription,
        Description? steps,
        Description? requisites,
        IEnumerable<Language> languages)
        : base(workInformationInternalId, informationDescription)
    {
        Steps = steps;
        Requisites = requisites;
        LanguageRequested = languages.ToList();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Recruitment"/> class without a specified ID.
    /// </summary>
    /// <param name="informationDescription">The required skills for the recruitment.</param>
    /// <param name="steps">The steps involved in the recruitment process.</param>
    /// <param name="contentDescription">A description of the recruitment content.</param>
    /// <param name="requisites">The requirements for the recruitment.</param>
    public Recruitment(
        Description? informationDescription,
        Description? steps,
        Description? requisites,
        IEnumerable<Language> languages)
        : base(informationDescription)
    {
        Steps = steps;
        Requisites = requisites;
        LanguageRequested = languages.ToList();
    }

    private Recruitment()
    {
        LanguageRequested = new List<Language>();
    }


    /// <summary>
    /// Accepts a visitor that performs operations on the current work information instance.
    /// </summary>
    /// <param name="visitor">The visitor implementing <see cref="IWorkInformationVisitor"/> to process this instance. Cannot be null.</param>
    public override void Accept(IWorkInformationVisitor visitor)
    {
        visitor.Visit(this);
    }
}
