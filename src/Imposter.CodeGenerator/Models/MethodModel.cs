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
    bool HasOverloadWithTheSameSetup
)
{
    internal bool IsGenericMethod => TypeParameters.Count > 0;

    internal bool HasTypeParameterAllowingRefStructs =>
        TypeParameters.Any(typeParameter => typeParameter.AllowsRefStructs);

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
            hasOverloadWithTheSameSetup
        );
}
