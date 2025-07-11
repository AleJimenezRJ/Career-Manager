using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Visitors;

/// <summary>
/// Defines a visitor interface for processing different types of "Add Work Information" DTOs.
/// </summary>
/// <remarks>This interface follows the Visitor design pattern, allowing implementations to handle specific DTO
/// types (`AddIndustryDto`, `AddOpportunityDto`, `AddWorkLifeDto`, and `AddRecruitmentDto`) in a type-safe manner. Each
/// method processes a specific DTO and returns a result of type <typeparamref name="TResult"/>.</remarks>
/// <typeparam name="TResult">The type of the result produced by the visitor methods.</typeparam>
public interface IAddWorkInformationDtoVisitor<out TResult>
{
    /// <summary>
    /// Processes the specified <see cref="AddIndustryDto"/> instance and returns a result.
    /// </summary>
    /// <param name="dto">The data transfer object containing information about the industry to be added. Cannot be <see
    /// langword="null"/>.</param>
    /// <returns>A result of type <typeparamref name="TResult"/> representing the outcome of the processing.</returns>
    TResult Visit(AddIndustryDto dto);

    /// <summary>
    /// Processes the specified <see cref="AddOpportunityDto"/> and returns a result of type <typeparamref
    /// name="TResult"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing information about the opportunity to be added. Cannot be <see
    /// langword="null"/>.</param>
    /// <returns>A result of type <typeparamref name="TResult"/> representing the outcome of the visit operation.</returns>
    TResult Visit(AddOpportunityDto dto);

    /// <summary>
    /// Processes the specified <see cref="AddWorkLifeDto"/> instance and returns a result.
    /// </summary>
    /// <param name="dto">The <see cref="AddWorkLifeDto"/> instance to be processed. Cannot be <see langword="null"/>.</param>
    /// <returns>A result of type <typeparamref name="TResult"/> based on the processing of the <paramref name="dto"/>.</returns>
    TResult Visit(AddWorkLifeDto dto);

    /// <summary>
    /// Processes the specified recruitment data transfer object and returns a result.
    /// </summary>
    /// <param name="dto">The recruitment data transfer object to be processed. Cannot be null.</param>
    /// <returns>A result of type <typeparamref name="TResult"/> representing the outcome of the visit operation.</returns>
    TResult Visit(AddRecruitmentDto dto);

    /// <summary>
    /// Processes the specified <see cref="AddEnterpriseDto"/> instance and returns a result.
    /// </summary>
    /// <param name="dto">The data transfer object containing information about the enterprise to be added. Cannot be <see
    /// langword="null"/>.</param>
    /// <returns>A result of type <typeparamref name="TResult"/> representing the outcome of the operation.</returns>
    TResult Visit(AddEnterpriseDto dto);
}