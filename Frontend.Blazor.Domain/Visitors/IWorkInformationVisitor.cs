using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Entities;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Visitors;

/// <summary>
/// Defines a visitor interface for processing various types of work-related information.
/// </summary>
/// <remarks>This interface follows the Visitor design pattern, allowing implementations to define  specific
/// operations for different types of work-related information, such as  <see cref="WorkLife"/>, <see
/// cref="Opportunity"/>, <see cref="Industry"/>, and <see cref="Recruitment"/>. Implementations of this interface
/// should provide logic for handling each type by overriding the corresponding method.</remarks>
public interface IWorkInformationVisitor
{
    /// <summary>
    /// Processes the specified <see cref="WorkLife"/> instance.
    /// </summary>
    /// <remarks>This method performs operations on the provided <see cref="WorkLife"/> object. Ensure that
    /// the object is properly initialized before calling this method.</remarks>
    /// <param name="workLife">The <see cref="WorkLife"/> instance to be visited. Cannot be <see langword="null"/>.</param>
    void Visit(WorkLife workLife);

    /// <summary>
    /// Processes the specified opportunity by applying the visitor's logic.
    /// </summary>
    /// <remarks>This method allows the visitor to perform operations on the provided opportunity object.
    /// Ensure that the <paramref name="opportunity"/> parameter is not null before calling this method.</remarks>
    /// <param name="opportunity">The opportunity to be visited. Cannot be null.</param>
    void Visit(Opportunity opportunity);

    /// <summary>
    /// Processes the specified <see cref="Industry"/> instance.
    /// </summary>
    /// <remarks>This method performs an operation on the provided <see cref="Industry"/> instance.  Ensure
    /// that the <paramref name="industry"/> parameter is not <see langword="null"/> before calling this
    /// method.</remarks>
    /// <param name="industry">The <see cref="Industry"/> to be visited. Cannot be <see langword="null"/>.</param>
    void Visit(Industry industry);

    /// <summary>
    /// Processes the specified recruitment instance, performing necessary operations based on its state or data.
    /// </summary>
    /// <param name="recruitment">The recruitment instance to be processed. Cannot be null.</param>
    void Visit(Recruitment recruitment);

    /// <summary>
    /// Processes the specified enterprise by performing a visit operation.
    /// </summary>
    /// <remarks>This method is intended to handle operations related to visiting an enterprise.  Ensure that
    /// the <paramref name="enterprise"/> parameter is not null before calling this method.</remarks>
    /// <param name="enterprise">The enterprise to be visited. Cannot be null.</param>
    void Visit(Enterprise enterprise);
}