using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Visitors;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;

/// <summary>
/// Represents a job opportunity related to a specific work information context.
/// </summary>
public class Opportunity : WorkInformation
{
    /// <summary>
    /// Gets or sets the country associated with the entity.
    /// </summary>
    public Country? Country { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="Opportunity"/> class with the specified work information ID,
    /// required skills, opportunities description, country, and language.
    /// </summary>
    /// <param name="workInformationInternalId">The internal identifier for the work information. Must be a positive integer.</param>
    /// <param name="informationDescription">The description of the opportunities available. Can be <see langword="null"/> if no description is provided.</param>
    /// <param name="country">The country where the opportunity takes place. Can be <see langword="null"/> if unspecified.</param>
    public Opportunity(int workInformationInternalId, Description? informationDescription, Country? country)
        : base(workInformationInternalId, informationDescription)
    {
        Country = country;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Opportunity"/> class with required skills,
    /// description, country, and language, but without a specific internal work information ID.
    /// </summary>
    /// <param name="informationDescription">A description of the opportunities available. Can be <see langword="null"/> if no description is provided.</param>
    /// <param name="country">The country where the opportunity is offered. Can be <see langword="null"/> if unspecified.</param>
    public Opportunity(Description? informationDescription, Country? country)
        : base(informationDescription)
    {
        Country = country;
    }

    /// <summary>
    /// Accepts a visitor that performs operations on the current work information instance.
    /// </summary>
    /// <param name="visitor">The visitor that implements <see cref="IWorkInformationVisitor"/> to process this instance. Cannot be <see
    /// langword="null"/>.</param>
    public override void Accept(IWorkInformationVisitor visitor)
    {
        visitor.Visit(this);
    }
}
