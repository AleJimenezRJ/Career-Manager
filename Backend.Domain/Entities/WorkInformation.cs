using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Visitors;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;

/// <summary>
/// Represents information about work opportunities linked to a career and an enterprise.
/// </summary>
public abstract class WorkInformation
{
    /// <summary>
    /// Accepts a visitor that processes work information using the Visitor design pattern.
    /// </summary>
    /// <param name="visitor">The visitor instance that implements <see cref="IWorkInformationVisitor"/>.  Cannot be <see langword="null"/>.</param>
    public abstract void Accept(IWorkInformationVisitor visitor);

    /// <summary>
    /// Gets or sets the internal identifier for work information.
    /// </summary>
    public int WorkInformationInternalId { get; set; }

    /// <summary>
    /// Gets or sets the description
    /// </summary>
    public Description? InformationDescription { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkInformation"/> class.
    /// </summary>
    /// <remarks>This constructor is protected and intended for use by derived classes. It allows subclasses
    /// to initialize instances of <see cref="WorkInformation"/> as part of their own construction process.</remarks>
    protected WorkInformation() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkInformation"/> class with the specified internal identifiers
    /// and required skills.
    /// </summary>
    /// <remarks>This constructor is intended for use within derived classes or internal operations where
    /// specific identifiers and skill requirements need to be explicitly set.</remarks>
    /// <param name="workInformationInternalId">The unique internal identifier for the work information. Must be a positive integer.</param>
    /// <param name="informationDescription">The description for the work.</param>
    protected WorkInformation(int workInformationInternalId, Description? informationDescription)
    {
        WorkInformationInternalId = workInformationInternalId;
        InformationDescription = informationDescription;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkInformation"/> class with the specified required skills and
    /// career identifier.
    /// </summary>
    /// <param name="informationDescription">The description for the work.</param>
    protected WorkInformation(Description? informationDescription)
    {
        InformationDescription = informationDescription;
    }
}
