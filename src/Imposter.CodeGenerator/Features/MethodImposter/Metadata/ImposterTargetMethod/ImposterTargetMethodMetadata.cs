using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.ImposterTargetMethod;

internal readonly struct ImposterTargetMethodMetadata : IParameterNameContextProvider
{
    internal readonly IMethodSymbol Symbol;

    internal readonly ImposterTargetMethodParametersMetadata Parameters;

    internal readonly MethodInvocationImposterGroupMetadata MethodInvocationImposterGroup;

    internal readonly MethodInvocationImposterMetadata MethodInvocationImposter;

    internal readonly TypeMetadata Delegate;

    internal readonly TypeMetadata CallbackDelegate;

    internal readonly TypeMetadata ExceptionGeneratorDelegate;

    internal readonly ArgumentCriteriaTypeMetadata ArgumentsCriteria;

    internal readonly AsMethodMetadata ArgumentsCriteriaAsMethod;

    internal readonly TypeMetadata Arguments;

    internal readonly InvocationHistoryTypeMetadata InvocationHistory;

    internal readonly InvocationVerifierInterfaceMetadata InvocationVerifierInterface;

    internal readonly MethodImposterMetadata MethodImposter;

    internal readonly ReturnTypeMetadata ReturnType;

    internal readonly NameSet GenericTypeParameterNameSet;

    internal readonly bool HasReturnValue;

    internal readonly bool SupportsBaseImplementation;
    internal readonly bool SupportsNullableGenericType;

    internal readonly string UniqueName;

    internal readonly string DisplayName;

    internal readonly TypeSyntax ReturnTypeSyntax;

    internal readonly SyntaxTokenList ImposterInstanceMethodModifiers;

    internal readonly IReadOnlyList<NameSyntax> GenericTypeArguments;

    internal readonly TypeArgumentListSyntax? GenericTypeArgumentListSyntax;

    internal readonly TypeParameterListSyntax? GenericTypeParameterListSyntax;

    internal readonly IReadOnlyList<NameSyntax> TargetGenericTypeArguments;

    internal readonly TypeParameterListSyntax? TargetGenericTypeParameterListSyntax;

    internal readonly SyntaxList<TypeParameterConstraintClauseSyntax> GenericTypeConstraintClauses;

    internal readonly string Namespace;

    internal bool IsAsync { get; }

    internal ImposterTargetMethodMetadata(
        IMethodSymbol symbol,
        string uniqueName,
        bool supportsNullableGenericType)
    {
        Symbol = symbol;
        UniqueName = uniqueName;
        DisplayName = Symbol.ToFullDisplayName();
        Namespace = Symbol.ContainingNamespace.ToDisplayString();
        ReturnTypeSyntax = SyntaxFactoryHelper.TypeSyntax(Symbol.ReturnType);
        ReturnType = new ReturnTypeMetadata(Symbol.ReturnType);
        HasReturnValue = !Symbol.ReturnsVoid;
        SupportsBaseImplementation = Symbol.ContainingType?.TypeKind == TypeKind.Class && !Symbol.IsAbstract;
        SupportsNullableGenericType = supportsNullableGenericType;
        IsAsync = IsMethodAsync(symbol);

        Parameters = new ImposterTargetMethodParametersMetadata(Symbol.Parameters);
        GenericTypeParameterNameSet = new NameSet(Symbol.TypeParameters.Select(p => p.Name));
        GenericTypeArguments = Symbol.TypeParameters.Select(p => SyntaxFactory.IdentifierName(p.Name)).ToArray();
        GenericTypeArgumentListSyntax = SyntaxFactoryHelper.TypeArgumentListSyntax(GenericTypeArguments);
        GenericTypeParameterListSyntax = SyntaxFactoryHelper.TypeParameterListSyntax(GenericTypeArguments);
        GenericTypeConstraintClauses = SyntaxFactory.List(SyntaxFactoryHelper.TypeParameterConstraintClauses(Symbol));

        var targetGenericNameContext = new NameSet(Symbol.TypeParameters.Select(p => p.Name));
        TargetGenericTypeArguments = Symbol.TypeParameters
            .Select(p => SyntaxFactory.IdentifierName(targetGenericNameContext.Use($"{p.Name}Target")))
            .ToArray();
        TargetGenericTypeParameterListSyntax = SyntaxFactoryHelper.TypeParameterListSyntax(TargetGenericTypeArguments);

        Delegate = TypeMetadataFactory.Create($"{uniqueName}Delegate", GenericTypeArguments);
        CallbackDelegate = TypeMetadataFactory.Create($"{uniqueName}CallbackDelegate", GenericTypeArguments);
        ExceptionGeneratorDelegate = TypeMetadataFactory.Create($"{uniqueName}ExceptionGeneratorDelegate", GenericTypeArguments);

        var argumentsTypeName = $"{uniqueName}Arguments";
        Arguments = new TypeMetadata(argumentsTypeName, SyntaxFactoryHelper.WithMethodGenericArguments(GenericTypeArguments, argumentsTypeName));
        ArgumentsCriteria = new ArgumentCriteriaTypeMetadata(this);
        ArgumentsCriteriaAsMethod = new AsMethodMetadata(Symbol.TypeParameters, GenericTypeParameterNameSet);
        InvocationHistory = new InvocationHistoryTypeMetadata(this);
        MethodInvocationImposterGroup = new MethodInvocationImposterGroupMetadata(this);
        MethodInvocationImposter = new MethodInvocationImposterMetadata(this);
        InvocationVerifierInterface = new InvocationVerifierInterfaceMetadata(this);
        MethodImposter = new MethodImposterMetadata(this);
        ImposterInstanceMethodModifiers = ImposterInstanceModifierBuilder.For(symbol);
    }

    public NameSet CreateParameterNameContext()
    {
        var names = new List<string>(Parameters.Parameters.Select(p => p.Name))
        {
            UniqueName,
            Namespace
        };

        return new NameSet(names);
    }

    private static bool IsMethodAsync(IMethodSymbol methodSymbol) =>
        methodSymbol.ReturnsVoid == false
        && methodSymbol.ReturnType.AllInterfaces.Any(i => i.ToDisplayString() == "System.Runtime.CompilerServices.IAsyncStateMachine")
        || (methodSymbol.ReturnType is INamedTypeSymbol namedType &&
            (namedType.ToDisplayString() == "System.Threading.Tasks.Task" ||
             namedType.ToDisplayString() == "System.Threading.Tasks.ValueTask" ||
              (namedType.IsGenericType &&
               (namedType.ConstructedFrom.ToDisplayString() == "System.Threading.Tasks.Task<TResult>" ||
                namedType.ConstructedFrom.ToDisplayString() == "System.Threading.Tasks.ValueTask<TResult>"))));

    internal readonly struct AsMethodMetadata
    {
        internal readonly NameSyntax[] TargetTypeArguments;
        internal readonly TypeParameterSyntax[] TypeParameters;

        internal AsMethodMetadata(IReadOnlyList<ITypeParameterSymbol> typeParameters, NameSet nameSet)
        {
            var allocatedNames = typeParameters
                .Select(p => nameSet.Use($"{p.Name}Target"))
                .ToArray();

            TargetTypeArguments = allocatedNames
                .Select(name => (NameSyntax)SyntaxFactory.IdentifierName(name))
                .ToArray();

            TypeParameters = allocatedNames
                .Select(SyntaxFactory.TypeParameter)
                .ToArray();
        }
    }
}
