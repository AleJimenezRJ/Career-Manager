namespace UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

/// <summary>
/// Represents the base class for value objects, which are compared by their values rather than identity.
/// </summary>
public abstract class ValueObject
{
    /// <summary>
    /// Provides the components that define equality for the value object.
    /// Derived classes must implement this to specify which fields participate in equality comparisons.
    /// </summary>
    /// <returns>A sequence of objects representing the equality components.</returns>
    protected abstract IEnumerable<object> GetEqualityComponents();

    /// <summary>
    /// Determines whether the specified object is equal to the current object based on value comparison.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <summary>
    /// Returns a hash code based on the value components of the object.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x is not null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    /// <summary>
    /// Determines whether two value objects are equal using the equality operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><c>true</c> if both objects are equal or both are null; otherwise, <c>false</c>.</returns>
    protected static bool EqualOperator(ValueObject? left, ValueObject? right)
    {
        if (left is null ^ right is null)
        {
            return false;
        }
        return ReferenceEquals(left, right) || left?.Equals(right) == false;
    }

    /// <summary>
    /// Determines whether two value objects are not equal using the inequality operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><c>true</c> if the objects are not equal; otherwise, <c>false</c>.</returns>
    protected static bool NotEqualOperator(ValueObject? left, ValueObject? right)
    {
        return !EqualOperator(left, right);
    }
}
