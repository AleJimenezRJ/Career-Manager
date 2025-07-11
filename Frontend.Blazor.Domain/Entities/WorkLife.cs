using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Visitors;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;

/// <summary>
/// Represents information about work-life balance or environment in a work context.
/// </summary>
public class WorkLife : WorkInformation
{
    /// <summary>
    /// Gets or sets the number of female workers in the field.
    /// </summary>
    public Workers? AmountFemaleWorkers { get; set; }

    /// <summary>
    /// Gets or sets the number of male workers in the field.
    /// </summary>
    public Workers? AmountMaleWorkers { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkLife"/> class with the specified work information ID,
    /// description, and worker counts.
    /// </summary>
    /// <param name="workInformationInternalId">The unique identifier for the work information. This value is required and must be valid.</param>
    /// <param name="informationDescription">An optional description of the work information. Can be <see langword="null"/> if no description is provided.</param>
    /// <param name="amountFemaleWorkers">An optional count of female workers associated with the work information. Can be <see langword="null"/> if the
    /// count is not specified.</param>
    /// <param name="amountMaleWorkers">An optional count of male workers associated with the work information. Can be <see langword="null"/> if the
    /// count is not specified.</param>
    public WorkLife(int workInformationInternalId, Description? informationDescription, Workers? amountFemaleWorkers, Workers? amountMaleWorkers)
        : base(workInformationInternalId, informationDescription)
    {
        AmountFemaleWorkers = amountFemaleWorkers;
        AmountMaleWorkers = amountMaleWorkers;
    }

    /// <summary>
    /// Represents the work-life information, including the description and the number of female and male workers.
    /// </summary>
    /// <param name="informationDescription">The description of the work-life information. Can be null.</param>
    /// <param name="amountFemaleWorkers">The number of female workers. Can be null.</param>
    /// <param name="amountMaleWorkers">The number of male workers. Can be null.</param>
    public WorkLife(Description? informationDescription, Workers? amountFemaleWorkers, Workers? amountMaleWorkers)
        : base(informationDescription)
    {
        AmountFemaleWorkers = amountFemaleWorkers;
        AmountMaleWorkers = amountMaleWorkers;

    }

    /// <summary>
    /// Accepts a visitor that processes or inspects the current work information instance.
    /// </summary>
    /// <remarks>This method allows the visitor to perform operations on the current instance by invoking its
    /// <c>Visit</c> method. It follows the Visitor design pattern, enabling extensibility without modifying the class
    /// structure.</remarks>
    /// <param name="visitor">The visitor that implements the <see cref="IWorkInformationVisitor"/> interface. Cannot be null.</param>
    public override void Accept(IWorkInformationVisitor visitor)
    {
        visitor.Visit(this);
    }

}
