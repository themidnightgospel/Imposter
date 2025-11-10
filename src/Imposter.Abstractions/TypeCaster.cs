using System.Runtime.CompilerServices;

namespace Imposter.Abstractions;

/// <summary>
/// Provides safe casting helpers that avoid exceptions for incompatible types.
/// </summary>
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
        // Handle null input first. A null can be cast to any reference type or nullable value type.
        if (input == null)
        {
            // `default(TOut) == null` is a check to see if TOut is a reference type or Nullable<T>.
            if (default(TOut) == null)
            {
                result = default!;
                return true;
            }

            // Cannot cast null to a non-nullable value type (e.g., int, bool).
            result = default!;
            return false;
        }

        // Use the 'is' operator for a fast path. This handles identity, boxing, unboxing,
        // and reference conversions efficiently.
        if (input is TOut casted)
        {
            result = casted;
            return true;
        }

        result = default!;
        return false;
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

        throw new InvalidCastException($"Cannot cast from type '{typeof(TIn)}' to '{typeof(TOut)}'.");
    }
}
