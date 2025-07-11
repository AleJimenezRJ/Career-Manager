using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Utility;

/// <summary>
/// Provides extension methods for working with <see cref="Result{T}"/> objects.
/// </summary>
/// <remarks>This class contains utility methods to simplify operations on <see cref="Result{T}"/> instances, such
/// as converting between derived and base types.</remarks>
public static class ResultExtensions
{
    /// <summary>
    /// Converts a <see cref="Result{TDerived}"/> to a <see cref="Result{TBase}"/> by upcasting the value type.
    /// </summary>
    /// <remarks>This method is useful for scenarios where a result with a derived type needs to be treated as
    /// a result with a base type, preserving success or failure state and associated data.</remarks>
    /// <typeparam name="TDerived">The derived type of the result value.</typeparam>
    /// <typeparam name="TBase">The base type to which the result value will be upcast.</typeparam>
    /// <param name="result">The result containing a value of type <typeparamref name="TDerived"/>.</param>
    /// <returns>A <see cref="Result{TBase}"/> containing the upcast value if the original result was successful; otherwise, a
    /// failure result containing the same errors as the original result.</returns>
    public static Result<TBase> Upcast<TDerived, TBase>(this Result<TDerived> result)
        where TDerived : TBase
    {
        return result.IsSuccess
            ? Result<TBase>.Success(result.Value!)
            : Result<TBase>.Failure(result.Errors!);
    }
}
