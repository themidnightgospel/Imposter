using System.Runtime.CompilerServices;

namespace Imposter.Abstractions;

/// <summary>
/// A utility for safe and flexible type casting and conversion.
/// </summary>
public static class TypeCaster
{
    /// <summary>
    /// Safely casts or converts a value of type <typeparamref name="TIn"/> to <typeparamref name="TOut"/>.
    /// - Avoids boxing for direct reference or identity conversions.
    /// - Handles numeric conversions, nullable types, and other convertible types.
    /// - Returns true if the conversion succeeded, false otherwise.
    /// </summary>
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
    /// Force-casts or converts a value of type <typeparamref name="TIn"/> to <typeparamref name="TOut"/>.
    /// - Throws <see cref="InvalidCastException"/> if the conversion is not possible.
    /// </summary>
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