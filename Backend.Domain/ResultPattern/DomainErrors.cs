namespace UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;

/// <summary>
/// Provides a centralized collection of domain-specific error factory methods grouped by domain area.
/// </summary>
public static class DomainErrors
{
    /// <summary>
    /// Contains error definitions.
    /// </summary>
    public static class FoundErrors
    {
        /// <summary>
        /// Returns an error indicating that an entity with the specified name was not found.
        /// </summary>
        /// <param name="name">The name of the entity.</param>
        /// <returns>An <see cref="Error"/> indicating the entity was not found.</returns>
        public static Error NotFound(string name) =>
            Error.NotFound("Entity.NotFound", $"The requested information with name {name} was not found.");


        /// <summary>
        /// Creates an error indicating that the requested data was not found in the system.
        /// </summary>
        /// <remarks>This method is used for empty lists.</remarks>
        /// <returns>An <see cref="Error"/> instance representing a "not found" error, with a predefined error code and message.</returns>
        public static Error NotFound() =>
            Error.NotFound("Data.NotFound", "There is no data in the system");

        /// <summary>
        /// Returns an error indicating that the specified name already exists.
        /// </summary>
        /// <param name="name">The name of the duplicated entity.</param>
        /// <returns>An <see cref="Error"/> representing a duplication conflict.</returns>
        public static Error DuplicatedConflict(string name) =>
            Error.DuplicatedConflict("Entity.DuplicatedConflict", $"'{name}' already exists. Use a different name please.");

        /// <summary>
        /// Returns an error indicating a concurrency conflict in the database.
        /// </summary>
        /// <returns>An <see cref="Error"/> representing a concurrency conflict.</returns>
        public static Error ConcurrencyConflict() =>
            Error.ConcurrencyConflict("Database.ConcurrencyConflict", "Concurrency conflict found in the database.");
    }

    /// <summary>
    /// Contains error definitions related to validation failures.
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// Returns a validation error indicating that a required field was not provided.
        /// </summary>
        /// <param name="fieldName">The name of the missing field.</param>
        /// <returns>An <see cref="Error"/> representing a missing required field.</returns>
        public static Error Required(string fieldName) =>
            Error.Validation("Validation.Required", $"{fieldName} is required.");

        /// <summary>
        /// Returns a validation error indicating that a field exceeds the allowed maximum length.
        /// </summary>
        /// <param name="field">The name of the field being validated.</param>
        /// <param name="max">The maximum allowed length.</param>
        /// <returns>An <see cref="Error"/> for exceeding the maximum length.</returns>
        public static Error MaxLength(string field, int max) =>
            Error.Validation($"{field}.MaxLength", $"{field} must not exceed {max} characters.");

        /// <summary>
        /// Returns a validation error indicating that a number must not be zero or negative.
        /// </summary>
        /// <param name="number">The invalid number.</param>
        /// <returns>An <see cref="Error"/> indicating an invalid number input.</returns>
        public static Error InvalidNumber(int number) =>
            Error.Validation("Validation.InvalidNumber", $"Please add a different number, not {number} (no negatives, or zero)");

        /// <summary>
        /// Returns a validation error indicating that the provided information for a field is invalid.
        /// </summary>
        /// <param name="field">The information that the user was trying to ass</param>
        /// <returns>An <see cref="Error"/> for invalid input by the user.</returns>
        public static Error InvalidInformation(string field) =>
            Error.Validation("Validation.InvalidInformation", $"The provided information for {field} is invalid.");
    }
}
