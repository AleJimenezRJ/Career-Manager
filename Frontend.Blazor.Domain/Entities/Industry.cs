using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Visitors;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;

/// <summary>
/// Represents an industry associated with a work information record.
/// </summary>
public class Industry : WorkInformation
{
    /// <summary>
    /// Gets or sets the name of the industry.
    /// </summary>
    public EntityName? Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the industry is related to computer science (CS).
    /// </summary>
    public bool? CSRelated { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Industry"/> class with the specified work information ID,
    /// required skills, career ID, and name.
    /// </summary>
    /// <param name="workInformationInternalId">The unique identifier for the work information associated with the industry.</param>
    /// <param name="informationDescription">The description for the industry.</param>
    /// <param name="name">The name of the industry. Can be <see langword="null"/> if the industry name is not specified.</param>
    public Industry(int workInformationInternalId, Description? informationDescription, EntityName? name, bool? csRelated)
        : base(workInformationInternalId, informationDescription)
    {
        Name = name;
        CSRelated = csRelated;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Industry"/> class with the specified required skills, career ID,
    /// and name.
    /// </summary>
    /// <param name="informationDescription">The description of the required skills for the industry. Can be <see langword="null"/>.</param>
    /// <param name="name">The name of the industry. Can be <see langword="null"/>.</param>
    public Industry(Description? informationDescription, EntityName? name, bool? csRelated)
        : base(informationDescription)
    {
        Name = name;
        CSRelated = csRelated;
    }

    private Industry() : base() { }

    /// <summary>
    /// Accepts a visitor that processes or inspects the current work information instance.
    /// </summary>
    /// <param name="visitor">The visitor that implements <see cref="IWorkInformationVisitor"/> to perform operations on this instance. Cannot
    /// be null.</param>
    public override void Accept(IWorkInformationVisitor visitor)
    {
        visitor.Visit(this);
    }
}
