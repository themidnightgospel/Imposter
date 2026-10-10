using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A method the imposter implements or overrides. <see cref="OverrideAccessibility"/> is the accessibility an
/// override in the imposter's assembly must declare. <see cref="HasOverloadWithTheSameSetup"/> is true when an overload
/// set up beside it, on a class's imposter or in its interface's setup view, differs from it only in what their setups
/// can't tell apart: passing a parameter by value or by in, ref or ref readonly, and the ref structs they leave out.
/// <see cref="HasAdapter"/> is true for a generic method whose imposter serves calls with other type arguments through
/// an adapter, which converts the values between them (see <see cref="NeedsAdapter"/>).
/// </summary>
internal sealed record MethodModel(
    string Name,
    string MetadataName,
    string DisplayName,
    string ContainingNamespace,
    TypeModel ContainingType,
    EquatableArray<string> ContainingTypeTypeParameterNames,
    bool IsClassMember,
    bool IsAbstract,
    bool IsAsync,
    Accessibility OverrideAccessibility,
    EquatableArray<TypeParameterModel> TypeParameters,
    EquatableArray<ParameterModel> Parameters,
    ReturnTypeModel ReturnType,
    bool HasOverloadWithTheSameSetup,
    bool HasAdapter
)
{
    internal bool IsGenericMethod => TypeParameters.Count > 0;

    internal static MethodModel From(
        IMethodSymbol method,
        MemberAccess memberAccess,
        bool hasOverloadWithTheSameSetup
    ) =>
        new(
            method.Name,
            method.MetadataName,
            method.ToFullDisplayName(),
            method.ContainingNamespace.ToDisplayString(),
            TypeModel.From(method.ContainingType),
            method.ContainingType.TypeParameters.Select(it => it.Name).ToEquatableArray(),
            method.ContainingType.TypeKind == TypeKind.Class,
            method.IsAbstract,
            method.IsMethodAsync(),
            memberAccess.GetOverrideAccessibility(method),
            method.TypeParameters.Select(TypeParameterModel.From).ToEquatableArray(),
            method.Parameters.Select(ParameterModel.From).ToEquatableArray(),
            ReturnTypeModel.From(method),
            hasOverloadWithTheSameSetup,
            NeedsAdapter(method)
        );

    // An adapter can't convert a value that may be a ref struct, so a method with a type parameter that allows ref
    // structs has none, and its setups apply to calls with the same type arguments only.
    internal static bool NeedsAdapter(IMethodSymbol method) =>
        method.IsGenericMethod && !method.TypeParameters.Any(AllowsRefStruct.AllowsRefStructs);
}
