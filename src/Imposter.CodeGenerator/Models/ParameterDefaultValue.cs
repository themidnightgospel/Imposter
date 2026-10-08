using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// An explicit parameter default. The expression is kept as text so the model compares by value; null means
/// default(T), whose T is written the way the parameter's type is.
/// </summary>
internal sealed record ParameterDefaultValue(string? Expression)
{
    private static readonly ParameterDefaultValue TypeDefault = new((string?)null);

    internal static ParameterDefaultValue? From(IParameterSymbol parameter)
    {
        if (!parameter.HasExplicitDefaultValue)
        {
            return null;
        }

        if (parameter.ExplicitDefaultValue is not { } value)
        {
            return TypeDefault;
        }

        return SyntaxFactoryHelper.DefaultValueExpression(parameter.Type, value) is { } expression
            ? new ParameterDefaultValue(expression.ToFullString())
            : null;
    }
}
