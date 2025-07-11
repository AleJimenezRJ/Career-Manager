using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern;
using static UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ResultPattern.DomainErrors;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.ValueObjects;

/// <summary>
/// Represents the description of an entity as a value object.
/// Ensures that a valid, non-empty name is provided and does not exceed 700 characters.
/// </summary>
public class Description : ValueObject
{
    public string Content { get; private set; }

    private Description(string content)
    {
        Content = content;
    }

    /// <summary>
    /// Validates the input and creates a new instance of <see cref="Description"/>.
    /// </summary>
    /// <param name="content">The content of the description provided by the user.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the created <see cref="Description"/>, or an error if validation fails.
    /// </returns>
    public static Result<Description> Create(string? content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return Result<Description>.Failure(
                Validation.Required(nameof(Description))
            );
        }

        if (content.Length > 700)
        {
            return Result<Description>.Failure(
                Validation.MaxLength(nameof(Description), 700)
            );
        }

        return Result<Description>.Success(new Description(content));
    }

    /// <summary>
    /// Creates a <see cref="Description"/> instance from a database-stored string representation.
    /// </summary>
    /// <param name="content">The string representation of the description retrieved from the database.  This value must not be null or
    /// invalid.</param>
    /// <returns>A <see cref="Description"/> instance created from the provided string representation.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the provided <paramref name="content"/> is invalid or cannot be converted into a valid <see
    /// cref="Description"/>.</exception>
    public static Description FromDatabase(string content)
    {
        var result = Create(content);
        if (result.IsFailure)
        {
            throw new InvalidOperationException(
                $"Invalid description found in database: '{content}'. Error: {result.Error?.Message}"
            );
        }
        return result.Value!;
    }

    /// <summary>
    /// Gets the equality components used to compare value object instances.
    /// </summary>
    /// <returns>A sequence of components that define object equality.</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Content;
    }
}
