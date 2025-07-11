using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Visitors;

/// <summary>
/// Defines a visitor interface for processing various types of work information DTOs.
/// </summary>
/// <remarks>This interface follows the Visitor design pattern, allowing implementations to define  specific
/// behavior for each type of DTO. It is typically used to decouple operations  from the DTO types themselves.</remarks>
/// <typeparam name="TResult">The type of the result produced by the visitor methods.</typeparam>
public interface IListWorkInformationDtoVisitor<out TResult>
{
    /// <summary>
    /// Processes the specified industry data transfer object and returns a result.
    /// </summary>
    /// <param name="dto">The industry data transfer object to be processed. Cannot be null.</param>
    /// <returns>A result of type <typeparamref name="TResult"/> representing the outcome of the processing.</returns>
    TResult Visit(ListIndustryDto dto);

    /// <summary>
    /// Processes the specified <see cref="ListOpportunityDto"/> and returns a result of type <typeparamref
    /// name="TResult"/>.
    /// </summary>
    /// <param name="dto">The data transfer object representing the opportunity list to be processed. Cannot be null.</param>
    /// <returns>A result of type <typeparamref name="TResult"/> based on the processing of the provided <see
    /// cref="ListOpportunityDto"/>.</returns>
    TResult Visit(ListOpportunityDto dto);

    /// <summary>
    /// Processes the specified <see cref="ListWorkLifeDto"/> and returns a result of type <typeparamref
    /// name="TResult"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing work-life information to be processed. Cannot be null.</param>
    /// <returns>A result of type <typeparamref name="TResult"/> based on the processing of the provided <paramref name="dto"/>.</returns>
    TResult Visit(ListWorkLifeDto dto);

    /// <summary>
    /// Processes the specified recruitment data transfer object (DTO) and returns a result.
    /// </summary>
    /// <param name="dto">The recruitment DTO to be visited. Must not be null.</param>
    /// <returns>A result of type <typeparamref name="TResult"/> based on the processing of the provided DTO.</returns>
    TResult Visit(ListRecruitmentDto dto);

    /// <summary>
    /// Processes the specified enterprise data transfer object (DTO) and returns a result.
    /// </summary>
    /// <param name="dto">The enterprise DTO to be visited. Must not be null.</param>
    /// <returns>A result of type <typeparamref name="TResult"/> based on the processing of the provided DTO.</returns>
    TResult Visit(ListEnterpriseDto dto);
}
