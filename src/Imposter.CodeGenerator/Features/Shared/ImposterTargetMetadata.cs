using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
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

    private readonly NameSet _symbolNameNamespace = new([]);

    internal ImposterTargetMetadata(
        ImposterTargetModel target,
        in SupportedCSharpFeatures supportedCSharpFeatures
    )
    {
        Name = GetImposterName(target.Name);
        TypeParameters = new ImposterTargetTypeParametersMetadata(target.TypeParameters);
        ImposterTypeSyntax = SyntaxFactoryHelper.WithMethodGenericArguments(
            TypeParameters.TypeArguments,
            Name
        );
        TargetTypeSyntax = SyntaxFactoryHelper.TypeSyntax(target.Type);
        var memberNames = _symbolNameNamespace;
        var supportsNullableGenericType = supportedCSharpFeatures.SupportsNullableGenericType;
        var ownSetupNames = OwnSetupNames(target);
        Methods = target
            .Methods.Select(method => new ImposterTargetMethodMetadata(
                method,
                UniqueName(method),
                supportsNullableGenericType
            ))
            .ToList();
        IsClass = target.IsClass;
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

        // A method set up by its unique name skips the names other setups keep as their own.
        string UniqueName(TargetMemberModel<MethodModel> method)
        {
            var name = memberNames.Use(method.Member.Name);
            while (
                ImposterTargetMethodMetadata.NeedsNumberedSetup(method)
                && ownSetupNames.Contains(name)
            )
            {
                name = memberNames.Use(method.Member.Name);
            }

            return name;
        }
    }

    // The imposter's setup members named after their target members, which no unique name may take.
    private static HashSet<string> OwnSetupNames(ImposterTargetModel target) =>
        [
            .. target
                .Methods.Where(method => !ImposterTargetMethodMetadata.NeedsNumberedSetup(method))
                .Select(method => method.Member.Name),
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
            property.RequiresExplicitInterfaceImplementation
        );

    internal ImposterIndexerMetadata CreateIndexerMetadata(
        TargetMemberModel<PropertyModel> indexer
    ) =>
        new(
            indexer.Member,
            _symbolNameNamespace.Use(IndexerMemberName),
            indexer.RequiresExplicitInterfaceImplementation
        );

    internal ImposterEventMetadata CreateEventMetadata(TargetMemberModel<EventModel> @event) =>
        new(
            @event.Member,
            _symbolNameNamespace.Use(@event.Member.Name),
            @event.RequiresExplicitInterfaceImplementation
        );
}
