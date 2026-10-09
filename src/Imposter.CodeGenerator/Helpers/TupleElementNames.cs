using System;
using System.Linq;

namespace Imposter.CodeGenerator.Helpers;

// C# allows Item1, Item2, ... as tuple element names only at their own position (CS8125), and never allows a few
// System.ValueTuple member names (CS8126).
internal static class TupleElementNames
{
    private static readonly string[] Disallowed =
    [
        "CompareTo",
        "Deconstruct",
        "Equals",
        "GetHashCode",
        "Rest",
        "ToString",
    ];

    // Item names count as reserved at every position, so one name works in tuples where the element sits at
    // different positions.
    internal static bool IsReserved(string name) =>
        Array.IndexOf(Disallowed, name) >= 0 || IsItemName(name);

    private static bool IsItemName(string name) =>
        name.Length > 4
        && name.StartsWith("Item", StringComparison.Ordinal)
        && name[4] != '0'
        && name.Skip(4).All(character => character is >= '0' and <= '9');
}
