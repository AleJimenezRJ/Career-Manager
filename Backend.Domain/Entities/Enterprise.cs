using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Visitors;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;

/// <summary>
/// Represents an enterprise entity, including its name, associated country, and work information.
/// </summary>
/// <remarks>The <see cref="Enterprise"/> class extends <see cref="WorkInformation"/> to provide additional
/// details specific to an enterprise. It includes properties for the enterprise's name and country, and supports
/// visitor-based operations through the <see cref="Accept"/> method.</remarks>
public class Enterprise : WorkInformation
{
    /// <summary>
    /// Gets or sets the name of the entity.
    /// </summary>
    public EntityName? Name { get; set; }

    /// <summary>
    /// Gets or sets the country associated with the enterprise.
    /// </summary>
    public Country? Country { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="Enterprise"/> class with the specified work information ID,
    /// description, name, and country.
    /// </summary>
    /// <param name="workInformationInternalId">The unique identifier for the work information associated with the enterprise.</param>
    /// <param name="informationDescription">An optional description of the enterprise's information. Can be <see langword="null"/>.</param>
    /// <param name="name">An optional name of the enterprise. Can be <see langword="null"/>.</param>
    /// <param name="country">An optional country associated with the enterprise. Can be <see langword="null"/>.</param>
    public Enterprise(int workInformationInternalId, Description? informationDescription, EntityName? name, Country? country)
        : base(workInformationInternalId, informationDescription)
    {
        Name = name;
        Country = country;
    }

    
    public Enterprise(Description? informationDescription, EntityName? name, Country? country)
        : base(informationDescription)
    {
        Name = name;
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
