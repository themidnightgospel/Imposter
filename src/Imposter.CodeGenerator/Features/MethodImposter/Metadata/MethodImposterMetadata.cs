using System.Collections.Generic;
using Imposter.CodeGenerator.Features.Shared;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata;

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

    internal readonly FindMatchingInvocationImposterGroupMethodMetadata FindMatchingInvocationImposterGroupMethod;

    internal readonly InvocationImpostersFieldMetadata InvocationImpostersField;

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
        FindMatchingInvocationImposterGroupMethod = new FindMatchingInvocationImposterGroupMethodMetadata(method);
        InvocationImpostersField = new InvocationImpostersFieldMetadata(method);
        Builder = new BuilderMetadata(
            Syntax,
            Collection.Syntax,
            method.ArgumentsCriteria.Syntax,
            method.InvocationSetup.Syntax,
            method.InvocationSetup.MethodInvocationImposterSyntax);
    }

    internal readonly struct BuilderMetadata
    {
        internal readonly string Name = "Builder";

        internal readonly TypeSyntax Syntax;

        internal readonly FieldMetadata ImposterCollectionField;

        internal readonly FieldMetadata MethodImposterField;

        internal readonly FieldMetadata ArgumentsCriteriaField;

        internal readonly FieldMetadata InvocationImposterGroupField;

        internal readonly FieldMetadata CurrentInvocationImposterField;

        internal BuilderMetadata(
            NameSyntax methodImposterSyntax,
            NameSyntax methodImposterCollectionSyntax,
            NameSyntax argumentCriteriaSyntax,
            NameSyntax invocationImposterGroupType,
            NameSyntax methodInvocationImposterType)
        {
            Syntax = SyntaxFactory.QualifiedName(methodImposterSyntax, SyntaxFactory.IdentifierName("Builder"));
            ImposterCollectionField = new FieldMetadata("_imposterCollection", methodImposterCollectionSyntax);
            MethodImposterField = new FieldMetadata("_imposter", methodImposterSyntax);
            ArgumentsCriteriaField = new FieldMetadata("_argumentsCriteria", argumentCriteriaSyntax);
            InvocationImposterGroupField = new FieldMetadata("_invocationImposterGroup", invocationImposterGroupType);
            CurrentInvocationImposterField = new FieldMetadata("_currentInvocationImposter", methodInvocationImposterType);
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

        internal readonly string MatchingInvocationImposterGroupVariableName;

        internal readonly string ArgumentsVariableName;

        public InvokeMethodMetadata(IParameterNameContextProvider parameterNameContextProvider)
        {
            var parameterNameContext = parameterNameContextProvider.CreateParameterNameContext();

            ExceptionVariableName = parameterNameContext.Use("ex");
            ResultVariableName = parameterNameContext.Use("result");
            MatchingInvocationImposterGroupVariableName = parameterNameContext.Use("matchingInvocationImposterGroup");
            ArgumentsVariableName = parameterNameContext.Use("arguments");
        }
    }

    internal readonly struct FindMatchingInvocationImposterGroupMethodMetadata
    {
        internal readonly string Name;

        internal readonly string SetupVariableName;

        public FindMatchingInvocationImposterGroupMethodMetadata(IParameterNameContextProvider parameterNameContextProvider)
        {
            Name = "FindMatchingInvocationImposterGroup";
            var nameContext = parameterNameContextProvider.CreateParameterNameContext();
            SetupVariableName = nameContext.Use("invocationImposterGroup");
        }
    }

    internal readonly struct InvocationImpostersFieldMetadata
    {
        internal readonly string Name;

        internal InvocationImpostersFieldMetadata(IParameterNameContextProvider parameterNameContextProvider)
        {
            var nameContext = parameterNameContextProvider.CreateParameterNameContext();
            Name = nameContext.Use("_invocationImposters");
        }
    }
}
