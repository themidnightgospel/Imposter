using System.Collections.Generic;
using Imposter.CodeGenerator.Features.Shared;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodSetup.Metadata;

internal readonly struct MethodImposterMetadata
{
    internal readonly string Name;

    internal readonly TypeMetadata BuilderInterface;

    internal readonly TypeMetadata Interface;

    internal readonly GenericTypeMetadata GenericInterface;

    internal readonly CollectionMetadata Collection;

    internal readonly NameSyntax Syntax;

    internal readonly FieldDeclarationMetadata AsField;

    internal readonly BuilderMetadata Builder;

    internal readonly InvokeMethodMetadata InvokeMethod;

    internal readonly FindMatchingSetupMethodMetadata FindMatchingSetupMethod;

    internal readonly InvocationSetupsFieldMetadata InvocationSetupsField;

    internal const string ExistingInvocationSetupFieldName = "_existingInvocationSetup";

    internal MethodImposterMetadata(in ImposterTargetMethodMetadata method)
    {
        Name = $"{method.UniqueName}MethodImposter";
        var methodImposterSyntax = SyntaxFactoryHelper.WithMethodGenericArguments(method.GenericTypeArguments, Name);
        Syntax = methodImposterSyntax;

        var methodImposterBuilderInterfaceName = $"I{Name}Builder";
        BuilderInterface = TypeMetadataFactory.Create(methodImposterBuilderInterfaceName, method.GenericTypeArguments);

        var methodImposterInterfaceName = $"I{Name}";
        Interface = new TypeMetadata(methodImposterInterfaceName);
        GenericInterface = new GenericTypeMetadata(methodImposterInterfaceName, method.GenericTypeArguments, method.TargetGenericTypeArguments);

        Collection = new CollectionMetadata($"{Name}Collection");
        AsField = new FieldDeclarationMetadata(Name);
        InvokeMethod = new InvokeMethodMetadata(method);
        FindMatchingSetupMethod = new FindMatchingSetupMethodMetadata();
        InvocationSetupsField = new InvocationSetupsFieldMetadata(method);
        Builder = new BuilderMetadata(Syntax, Collection.Syntax, method.ArgumentsCriteria.Syntax);
    }

    internal readonly struct BuilderMetadata
    {
        internal readonly string Name = "Builder";

        internal readonly TypeSyntax Syntax;

        internal readonly FieldMetadata ImposterCollectionField;

        internal readonly FieldMetadata MethodImposterField;

        internal readonly FieldMetadata ArgumentsCriteriaField;

        internal BuilderMetadata(
            NameSyntax methodImposterSyntax,
            NameSyntax methodImposterCollectionSyntax,
            NameSyntax argumentCriteriaSyntax)
        {
            Syntax = SyntaxFactory.QualifiedName(methodImposterSyntax, SyntaxFactory.IdentifierName("Builder"));
            ImposterCollectionField = new FieldMetadata("_imposterCollection", methodImposterCollectionSyntax);
            MethodImposterField = new FieldMetadata("_imposter", methodImposterSyntax);
            ArgumentsCriteriaField = new FieldMetadata("_argumentsCriteria", argumentCriteriaSyntax);
        }
    }

    internal readonly record struct CollectionMetadata(string Name, NameSyntax Syntax, FieldDeclarationMetadata AsField)
    {
        public CollectionMetadata(string Name)
            : this(Name, SyntaxFactory.IdentifierName(Name), new FieldDeclarationMetadata(Name))
        {
        }
    }

    internal readonly record struct GenericTypeMetadata(string Name, NameSyntax Syntax, NameSyntax SyntaxWithTargetGenericArguments)
    {
        public GenericTypeMetadata(
            string name,
            IReadOnlyList<NameSyntax> genericTypeArguments,
            IReadOnlyList<NameSyntax> targetGenericTypeArguments
        )
            : this(name, GetNameSyntax(name, genericTypeArguments), GetNameSyntax(name, targetGenericTypeArguments))
        {
        }

        private static NameSyntax GetNameSyntax(string name, IReadOnlyList<NameSyntax> genericTypeArguments) =>
            genericTypeArguments.Count > 0
                ? SyntaxFactory.GenericName(SyntaxFactory.Identifier(name),
                    SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList<TypeSyntax>(genericTypeArguments))
                )
                : SyntaxFactory.IdentifierName(name);
    }

    internal readonly struct InvokeMethodMetadata
    {
        internal const string Name = "Invoke";

        internal readonly string ExceptionVariableName;

        internal readonly string ResultVariableName;

        internal readonly string MatchingSetupVariableName;

        internal readonly string ArgumentsVariableName;

        public InvokeMethodMetadata(IParameterNameContextProvider parameterNameContextProvider)
        {
            var parameterNameContext = parameterNameContextProvider.CreateParameterNameContext();

            ExceptionVariableName = parameterNameContext.Use("ex");
            ResultVariableName = parameterNameContext.Use("result");
            MatchingSetupVariableName = parameterNameContext.Use("matchingSetup");
            ArgumentsVariableName = parameterNameContext.Use("arguments");
        }
    }

    internal readonly struct FindMatchingSetupMethodMetadata
    {
        internal const string Name = "FindMatchingSetup";

        public FindMatchingSetupMethodMetadata()
        {
        }
    }

    internal readonly struct InvocationSetupsFieldMetadata
    {
        internal readonly string Name;

        internal InvocationSetupsFieldMetadata(IParameterNameContextProvider parameterNameContextProvider)
        {
            var nameContext = parameterNameContextProvider.CreateParameterNameContext();
            Name = nameContext.Use("_invocationSetups");
        }
    }
}