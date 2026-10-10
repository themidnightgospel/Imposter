using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.Features.Shared.BuilderInterface;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Shared;

internal readonly struct ImposterTargetMetadata
{
    internal const string IndexerMemberName = "Indexer";

    internal static string GetImposterName(string targetName) => targetName + "Imposter";

    internal readonly string Name;

    internal readonly NameSyntax ImposterTypeSyntax;

    internal readonly TypeSyntax TargetTypeSyntax;

    internal readonly bool IsClass;

    internal readonly bool HasRequiredMembers;

    internal readonly Accessibility DeclaredAccessibility;

    internal readonly ImposterTargetConstructorMetadata[] AccessibleConstructors;

    internal readonly List<ImposterTargetMethodMetadata> Methods;

    internal readonly EquatableArray<TargetMemberModel<PropertyModel>> Properties;

    internal readonly EquatableArray<TargetMemberModel<PropertyModel>> Indexers;

    internal readonly EquatableArray<TargetMemberModel<EventModel>> Events;

    internal readonly ImposterTargetTypeParametersMetadata TypeParameters;

    // The imposter's constructors and the Imposter() extension declare these parameters next to the parameters of the
    // target's constructors, so they avoid those names.
    internal readonly ParameterMetadata InvocationBehaviorParameter;

    internal readonly string ExtensionParameterName;

    // The property and indexer getters' Throws<TException>() is declared inside the imposter, so its type parameter
    // avoids the names of the target's.
    private readonly ExceptionTypeParameterMetadata _getterExceptionTypeParameter;

    // The unique names each member's generated types are named after. A name in avoidedUniqueNames is taken already,
    // so a member that would get it gets another.
    private readonly NameSet _symbolNameNamespace;

    internal ImposterTargetMetadata(
        ImposterTargetModel target,
        IEnumerable<string> avoidedUniqueNames
    )
    {
        _symbolNameNamespace = new NameSet(avoidedUniqueNames);
        Name = GetImposterName(target.Name);
        TypeParameters = new ImposterTargetTypeParametersMetadata(target.TypeParameters);
        ImposterTypeSyntax = SyntaxFactoryHelper.WithMethodGenericArguments(
            TypeParameters.TypeArguments,
            Name
        );
        TargetTypeSyntax = SyntaxFactoryHelper.TypeSyntax(target.Type);
        var memberNames = _symbolNameNamespace;
        var ownMethodSetups = OwnMethodSetups(target);
        var ownPropertyAndEventSetupNames = OwnPropertyAndEventSetupNames(target);
        var typeParameterNames = target.TypeParameters.Select(it => it.Name).ToArray();
        Methods = target
            .Methods.Select(method => new ImposterTargetMethodMetadata(
                method,
                UniqueName(method),
                typeParameterNames
            ))
            .ToList();
        _getterExceptionTypeParameter = new ExceptionTypeParameterMetadata(
            new NameSet(typeParameterNames).Use(ExceptionTypeParameterMetadata.PreferredName)
        );
        IsClass = target.IsClass;
        HasRequiredMembers = target.HasRequiredMembers;
        DeclaredAccessibility = target.DeclaredAccessibility;
        AccessibleConstructors = target
            .AccessibleConstructors.Select(constructor => new ImposterTargetConstructorMetadata(
                constructor
            ))
            .ToArray();
        Properties = target.Properties;
        Indexers = target.Indexers;
        Events = target.Events;

        var constructorParameterNames = new NameSet(
            target.AccessibleConstructors.SelectMany(constructor =>
                constructor.Parameters.Select(parameter => parameter.Name)
            )
        );
        InvocationBehaviorParameter = new ParameterMetadata(
            constructorParameterNames.Use("invocationBehavior"),
            WellKnownTypes.Imposter.Abstractions.ImposterMode,
            QualifiedName(
                WellKnownTypes.Imposter.Abstractions.ImposterMode,
                IdentifierName("Implicit")
            )
        );
        ExtensionParameterName = constructorParameterNames.Use("imposter");

        // A method set up by its unique name skips a name another setup keeps as its own when the two would clash: a
        // method's setup with the same signature, or a property's or an event's of any kind.
        string UniqueName(TargetMemberModel<MethodModel> method)
        {
            var name = memberNames.Use(method.Member.Name);
            if (!ImposterTargetMethodMetadata.NeedsNumberedSetup(method))
            {
                return name;
            }

            var signature = MethodSetupSignature.Of(method.Member);
            while (
                ownPropertyAndEventSetupNames.Contains(name)
                || ownMethodSetups.Contains((name, signature))
            )
            {
                name = memberNames.Use(method.Member.Name);
            }

            return name;
        }
    }

    private static HashSet<(string Name, string Signature)> OwnMethodSetups(
        ImposterTargetModel target
    ) =>
        [
            .. target
                .Methods.Where(method => !ImposterTargetMethodMetadata.NeedsNumberedSetup(method))
                .Select(method => (method.Member.Name, MethodSetupSignature.Of(method.Member))),
        ];

    private static HashSet<string> OwnPropertyAndEventSetupNames(ImposterTargetModel target) =>
        [
            .. target
                .Properties.Where(property => !property.RequiresExplicitInterfaceImplementation)
                .Select(property => property.Member.Name),
            .. target
                .Events.Where(@event => !@event.RequiresExplicitInterfaceImplementation)
                .Select(@event => @event.Member.Name),
        ];

    internal ImposterPropertyMetadata CreatePropertyMetadata(
        TargetMemberModel<PropertyModel> property,
        NameSet memberNameSet
    ) =>
        new(
            property.Member,
            _symbolNameNamespace.Use(property.Member.Name),
            memberNameSet,
            property.RequiresExplicitInterfaceImplementation,
            _getterExceptionTypeParameter
        );

    internal ImposterIndexerMetadata CreateIndexerMetadata(
        TargetMemberModel<PropertyModel> indexer
    ) =>
        new(
            indexer.Member,
            _symbolNameNamespace.Use(IndexerMemberName),
            indexer.RequiresExplicitInterfaceImplementation,
            _getterExceptionTypeParameter
        );

    internal ImposterEventMetadata CreateEventMetadata(TargetMemberModel<EventModel> @event) =>
        new(
            @event.Member,
            _symbolNameNamespace.Use(@event.Member.Name),
            @event.RequiresExplicitInterfaceImplementation
        );
}
