using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator;

internal static class ImposterTargetValidator
{
    // IMP002, IMP004, IMP008 and IMP009 stop the target's generation; IMP006 only warns. Collisions between targets
    // (IMP007) are found once all targets are known.
    internal static (EquatableArray<DiagnosticModel> Diagnostics, bool CanGenerate) Validate(
        INamedTypeSymbol target,
        MemberAccess memberAccess
    )
    {
        var location = LocationModel.From(target);
        var targetDisplayName = target.ToDisplayString();

        if (!IsInterfaceOrNonSealedClass(target))
        {
            return (
                Single(
                    DiagnosticDescriptors.InvalidImposterTarget,
                    location,
                    targetDisplayName,
                    target.TypeKind.ToString()
                ),
                false
            );
        }

        if (target.TypeKind == TypeKind.Class && !HasAccessibleConstructor(target, memberAccess))
        {
            return (
                Single(
                    DiagnosticDescriptors.ImposterTargetMustHaveAccessibleConstructor,
                    location,
                    targetDisplayName
                ),
                false
            );
        }

        if (
            target.TypeKind == TypeKind.Class
            && FindUnoverridableAbstractMember(target, memberAccess) is { } abstractMember
        )
        {
            return (
                Single(
                    DiagnosticDescriptors.ImposterTargetHasUnoverridableAbstractMember,
                    location,
                    targetDisplayName,
                    abstractMember.ToDisplayString()
                ),
                false
            );
        }

        if (FindRefLikeMember(target, memberAccess) is { } refLikeMember)
        {
            return (
                Single(
                    DiagnosticDescriptors.ImposterTargetHasRefLikeMember,
                    location,
                    targetDisplayName,
                    refLikeMember.Member.ToDisplayString(),
                    refLikeMember.Type.ToDisplayString()
                ),
                false
            );
        }

        return IsClosedGenericType(target)
            ? (
                Single(
                    DiagnosticDescriptors.ClosedGenericImposterTarget,
                    location,
                    targetDisplayName,
                    target.ConstructUnboundGenericType().ToDisplayString()
                ),
                true
            )
            : (default, true);
    }

    internal static bool IsInterfaceOrNonSealedClass(INamedTypeSymbol typeSymbol) =>
        typeSymbol.TypeKind == TypeKind.Interface
        || typeSymbol is { TypeKind: TypeKind.Class, IsSealed: false };

    private static EquatableArray<DiagnosticModel> Single(
        DiagnosticDescriptor descriptor,
        LocationModel? location,
        params string[] arguments
    ) => new[] { DiagnosticModel.Create(descriptor, location, arguments) }.ToEquatableArray();

    private static bool IsClosedGenericType(INamedTypeSymbol typeSymbol) =>
        typeSymbol.IsGenericType
        && !SymbolEqualityComparer.Default.Equals(typeSymbol, typeSymbol.OriginalDefinition);

    // A source class always has at least its implicit constructor. A class from another assembly can show none, when
    // the build imports only public and protected metadata and every constructor is internal.
    private static bool HasAccessibleConstructor(
        INamedTypeSymbol typeSymbol,
        MemberAccess memberAccess
    ) => typeSymbol.InstanceConstructors.Any(memberAccess.IsAccessible);

    // The imposter derives from the target, so it must override every abstract member the target leaves abstract.
    private static ISymbol? FindUnoverridableAbstractMember(
        INamedTypeSymbol target,
        MemberAccess memberAccess
    )
    {
        ISymbol[] overridableMembers =
        [
            .. target.GetAllOverridableMethods(),
            .. target.GetAllOverridableProperties(),
            .. target.GetAllOverridableEvents(),
        ];

        return overridableMembers.FirstOrDefault(member =>
            member.IsAbstract && !CanOverride(member, memberAccess)
        );
    }

    // A property's accessors are overridden too, and an accessor can be less accessible than its property.
    private static bool CanOverride(ISymbol member, MemberAccess memberAccess) =>
        memberAccess.IsAccessible(member)
        && (
            member is not IPropertySymbol property
            || (
                IsAbsentOrAccessible(property.GetMethod, memberAccess)
                && IsAbsentOrAccessible(property.SetMethod, memberAccess)
            )
        );

    private static bool IsAbsentOrAccessible(IMethodSymbol? accessor, MemberAccess memberAccess) =>
        accessor is null || memberAccess.IsAccessible(accessor);

    // The imposter keeps the arguments and results of every member it impersonates in fields, delegates and Arg<T>
    // matchers, none of which can hold a ref-like value. A method's Span<T> or ReadOnlySpan<T> passed or returned by
    // value is the exception: the imposter keeps its elements in an array.
    private static (ISymbol Member, ITypeSymbol Type)? FindRefLikeMember(
        INamedTypeSymbol target,
        MemberAccess memberAccess
    )
    {
        foreach (var method in ImposterTargetModel.GetMethods(target, memberAccess))
        {
            if (FindMethodRefLikeType(method) is { } type)
            {
                return (method, type);
            }
        }

        foreach (var property in ImposterTargetModel.GetProperties(target, memberAccess))
        {
            if (
                FindRefLikeType([property.Type, .. ParameterTypes(property.Parameters)]) is { } type
            )
            {
                return (property, type);
            }
        }

        foreach (var @event in ImposterTargetModel.GetEvents(target, memberAccess))
        {
            if (
                @event.Type is INamedTypeSymbol { DelegateInvokeMethod: { } invoke }
                && FindRefLikeType([invoke.ReturnType, .. ParameterTypes(invoke.Parameters)])
                    is { } type
            )
            {
                return (@event, type);
            }
        }

        return null;
    }

    // A type parameter that allows ref structs may stand for a ref struct.
    private static ITypeSymbol? FindMethodRefLikeType(IMethodSymbol method) =>
        FindRefLikeType(UncopiedTypes(method))
        ?? method.TypeParameters.FirstOrDefault(AllowsRefStruct.AllowsRefStructs);

    private static IEnumerable<ITypeSymbol> UncopiedTypes(IMethodSymbol method)
    {
        if (SpanModel.FromReturnType(method) is null)
        {
            yield return method.ReturnType;
        }

        foreach (var parameter in method.Parameters.Where(it => SpanModel.From(it) is null))
        {
            yield return parameter.Type;
        }
    }

    private static IEnumerable<ITypeSymbol> ParameterTypes(
        IEnumerable<IParameterSymbol> parameters
    ) => parameters.Select(parameter => parameter.Type);

    private static ITypeSymbol? FindRefLikeType(IEnumerable<ITypeSymbol> types) =>
        types.FirstOrDefault(it => it.IsRefLikeType);
}
