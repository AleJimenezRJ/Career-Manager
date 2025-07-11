using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using static UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern.DomainErrors;

namespace UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

/// <summary>
/// Represents the name of an entity as a value object.
/// Ensures that a valid, non-empty name is provided and does not exceed 100 characters.
/// </summary>
public class EntityName : ValueObject
{
    public string Name { get; private set; }

    private EntityName(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Validates the input and creates a new instance of <see cref="EntityName"/>.
    /// </summary>
    /// <param name="name">The name of the entity provided by the user.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the created <see cref="EntityName"/>, or an error if validation fails.
    /// </returns>
    public static Result<EntityName> Create(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<EntityName>.Failure(
                Validation.Required(nameof(EntityName))
            );
        }

        if (name.Length > 100)
        {
            return Result<EntityName>.Failure(
                Validation.MaxLength(nameof(EntityName), 100)
            );
        }

        return Result<EntityName>.Success(new EntityName(name));
    }

    /// <summary>
    /// Creates an <see cref="EntityName"/> instance from a name retrieved from the database.
    /// </summary>
    /// <param name="name">The name value retrieved from the database. Must not be null or empty.</param>
    /// <returns>An <see cref="EntityName"/> instance representing the specified name.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the provided <paramref name="name"/> is invalid or cannot be converted into an <see
    /// cref="EntityName"/>. The exception message includes details about the validation error.</exception>
    public static EntityName FromDatabase(string name)
    {
        var result = Create(name);
        if (result.IsFailure)
        {
            throw new InvalidOperationException(
                $"Invalid Name found in database: '{name}'. Error: {result.Error?.Message}"
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
        yield return Name;
    }
}
