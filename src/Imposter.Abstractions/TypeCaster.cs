using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Imposter.Abstractions;

/// <summary>
/// Provides safe casting helpers that avoid exceptions for incompatible types.
/// </summary>
/// <remarks>
/// Generated imposters use it to pass values between a generic method's type parameters and those of its setups. It is
/// public for that generated code only and may change with the generator in the same package.
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class TypeCaster
{
    /// <summary>
    /// Attempts to cast a value of type <typeparamref name="TIn"/> to <typeparamref name="TOut"/> without throwing.
    /// Returns <see langword="true"/> when a direct cast is possible (including identity, reference, boxing or unboxing
    /// conversions), otherwise returns <see langword="false"/> and sets <paramref name="result"/> to default.
    /// </summary>
    /// <param name="input">The input value to cast.</param>
    /// <param name="result">The resulting value when the cast succeeds; otherwise default.</param>
    /// <returns><see langword="true"/> if the cast succeeds; otherwise <see langword="false"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryCast<TIn, TOut>(TIn input, out TOut result)
    {
        // Handles identity, boxing, unboxing and reference conversions. A null input never matches.
        if (input is TOut cast)
        {
            result = cast;
            return true;
        }

        result = default!;
        // A null casts to any reference type or Nullable<T>, the types whose default is null, but not to a
        // non-nullable value type such as int.
        return input is null && default(TOut) is null;
    }

    /// <summary>
    /// Performs a cast from <typeparamref name="TIn"/> to <typeparamref name="TOut"/>, throwing
    /// <see cref="InvalidCastException"/> when the cast is not possible.
    /// </summary>
    /// <param name="input">The input value to cast.</param>
    /// <returns>The cast value.</returns>
    /// <exception cref="InvalidCastException">Thrown when the cast is not possible.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Cast<TIn, TOut>(TIn input)
    {
        if (TryCast<TIn, TOut>(input, out var result))
        {
            return result;
        }

        throw new InvalidCastException(
            $"Cannot cast from type '{typeof(TIn)}' to '{typeof(TOut)}'."
        );
    }
}
