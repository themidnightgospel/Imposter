using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator;

internal static class ImposterTargetValidator
{
    // IMP002, IMP004 and IMP008 to IMP012 stop the target's generation; IMP006 only warns. Collisions between targets
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

        if (FindUnimplementedStaticAbstractMember(target) is { } staticAbstractMember)
        {
            return (
                Single(
                    DiagnosticDescriptors.ImposterTargetHasStaticAbstractMember,
                    location,
                    targetDisplayName,
                    staticAbstractMember.ToDisplayString()
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

        if (target.HasRequiredMembers() && memberAccess.LacksSetsRequiredMembersAttribute())
        {
            return (
                Single(
                    DiagnosticDescriptors.ImposterTargetRequiredMembersNeedSetsRequiredMembers,
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

        if (FindRefReturningMember(target, memberAccess) is { } refReturningMember)
        {
            return (
                Single(
                    DiagnosticDescriptors.ImposterTargetHasRefReturningMember,
                    location,
                    targetDisplayName,
                    refReturningMember.ToDisplayString()
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

    // The imposter passes its target as a type argument, which an interface can't be while one of its static abstract
    // members, or an inherited one, has no implementation in it. A class target implements them itself. Accessors are
    // skipped, so the diagnostic names their property or event.
    private static ISymbol? FindUnimplementedStaticAbstractMember(INamedTypeSymbol target) =>
        target.TypeKind == TypeKind.Interface
            ? target
                .AllInterfaces.Prepend(target)
                .SelectMany(@interface => @interface.GetMembers())
                .FirstOrDefault(member =>
                    member
                        is { IsStatic: true, IsAbstract: true }
                            and not IMethodSymbol { AssociatedSymbol: not null }
                    && target.FindImplementationForInterfaceMember(member) is null
                )
            : null;

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
    // matchers, none of which can hold a ref-like value. A Span<T> or ReadOnlySpan<T> is the exception where the
    // imposter keeps its elements in an array: a method's span parameter or a span it returns by value, a property's
    // or indexer's span value, an indexer's span key, and an event's span parameter (see UncopiedEventTypes). A
    // method's parameter or result, a property's or indexer's value, or a sync event delegate's parameter, of another
    // ref struct type isn't kept at all, only passed through (see IsPassedThrough, ReturnsPassedThrough,
    // PassesValueThrough and UncopiedEventTypes).
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
            if (FindRefLikeType(UncopiedTypes(property)) is { } type)
            {
                return (property, type);
            }
        }

        foreach (var @event in ImposterTargetModel.GetEvents(target, memberAccess))
        {
            if (
                @event.Type is INamedTypeSymbol { DelegateInvokeMethod: { } invoke }
                && FindRefLikeType(UncopiedEventTypes(invoke)) is { } type
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
        if (SpanModel.FromReturnType(method) is null && !ReturnsPassedThrough(method))
        {
            yield return method.ReturnType;
        }

        var uncopiedParameters = method.Parameters.Where(it =>
            SpanModel.From(it) is null && !IsPassedThrough(it, method)
        );
        foreach (var parameter in uncopiedParameters)
        {
            yield return parameter.Type;
        }
    }

    // A method passes another ref struct on to its delegates, unless the ref struct's type uses the method's type
    // parameters: a generic method's adapter would have to convert it between type arguments.
    private static bool IsPassedThrough(IParameterSymbol parameter, IMethodSymbol method) =>
        ParameterModel.IsCustomRefStruct(parameter)
        && !parameter.Type.ReferencesTypeParameterOf(method);

    // A ref struct result passes back from a Returns delegate or the base implementation, on the same condition. A
    // generic method's adapter also passes by-reference arguments through locals, which the result could refer to
    // (CS8352), so it can't take any but the ref structs it forwards as they are.
    private static bool ReturnsPassedThrough(IMethodSymbol method) =>
        ReturnTypeModel.ReturnsCustomRefStruct(method)
        && !method.ReturnType.ReferencesTypeParameterOf(method)
        && !(method.IsGenericMethod && method.Parameters.Any(PassesThroughAdapterLocal));

    private static bool PassesThroughAdapterLocal(IParameterSymbol parameter) =>
        parameter.RefKind is RefKind.Ref or RefKind.In or RefKinds.RefReadOnlyParameter
        && !ParameterModel.IsCustomRefStruct(parameter);

    private static IEnumerable<ITypeSymbol> UncopiedTypes(IPropertySymbol property)
    {
        if (SpanModel.FromProperty(property) is null && !PassesValueThrough(property))
        {
            yield return property.Type;
        }

        foreach (var parameter in property.Parameters.Where(it => SpanModel.From(it) is null))
        {
            yield return parameter.Type;
        }
    }

    // A class indexer's base getter is a lambda that reads the base indexer with copies of the keys. Its value could
    // refer to the copy of a key taken by in or ref readonly (CS8347), so the lambda couldn't return a value passed
    // through.
    private static bool PassesValueThrough(IPropertySymbol property) =>
        PropertyModel.PassesValueThrough(property)
        && !(
            property.ContainingType.TypeKind == TypeKind.Class
            && property.GetMethod is { IsAbstract: false }
            && property.Parameters.Any(it =>
                it.RefKind is RefKind.In or RefKinds.RefReadOnlyParameter
            )
        );

    // An event's raise copies a span argument's elements into its history and passes the span itself on, and passes
    // another ref struct on without keeping it. An async event's raise can't take a ref struct at all, so it takes the
    // array a span covers and passes the delegates a span over it, which a ref, out or ref readonly parameter can't
    // take: it needs a span variable, which an async method can't declare.
    private static IEnumerable<ITypeSymbol> UncopiedEventTypes(IMethodSymbol invoke)
    {
        yield return invoke.ReturnType;

        var isAsync = invoke.ReturnType.IsAwaitable();
        var uncopiedParameters = invoke.Parameters.Where(it =>
            SpanModel.From(it) is null
                ? isAsync || !ParameterModel.IsCustomRefStruct(it)
                : isAsync
                    && it.RefKind is RefKind.Ref or RefKind.Out or RefKinds.RefReadOnlyParameter
        );
        foreach (var parameter in uncopiedParameters)
        {
            yield return parameter.Type;
        }
    }

    private static ITypeSymbol? FindRefLikeType(IEnumerable<ITypeSymbol> types) =>
        types.FirstOrDefault(it => it.IsRefLikeType);

    // An imposter returns the results it is set up with by value, so it can't implement or override a member that
    // returns by ref or ref readonly.
    private static ISymbol? FindRefReturningMember(
        INamedTypeSymbol target,
        MemberAccess memberAccess
    ) =>
        ImposterTargetModel
            .GetMethods(target, memberAccess)
            .Concat<ISymbol>(ImposterTargetModel.GetProperties(target, memberAccess))
            .FirstOrDefault(member =>
                member
                    is IMethodSymbol { RefKind: not RefKind.None }
                        or IPropertySymbol { RefKind: not RefKind.None }
            );
}
