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

    internal readonly InvocationSetupMetadata InvocationSetup;

    internal readonly TypeMetadata Delegate;

    internal readonly TypeMetadata CallbackDelegate;

    internal readonly TypeMetadata ExceptionGeneratorDelegate;

    internal readonly ArgumentCriteriaTypeMetadata ArgumentsCriteria;

    internal readonly TypeMetadata Arguments;

    internal readonly InvocationHistoryTypeMetadata InvocationHistory;

    internal readonly InvocationVerifierInterfaceMetadata InvocationVerifierInterface;

    internal readonly MethodImposterMetadata MethodImposter;

    internal readonly ReturnTypeMetadata ReturnType;

    internal readonly bool HasReturnValue;

    internal readonly string UniqueName;

    internal readonly string DisplayName;

    internal readonly TypeSyntax ReturnTypeSyntax;

    internal readonly IReadOnlyList<NameSyntax> GenericTypeArguments;

    internal readonly TypeArgumentListSyntax? GenericTypeArgumentListSyntax;

    internal readonly TypeParameterListSyntax? GenericTypeParameterListSyntax;

    internal readonly IReadOnlyList<NameSyntax> TargetGenericTypeArguments;

    internal readonly TypeParameterListSyntax? TargetGenericTypeParameterListSyntax;

    internal readonly string Namespace;

    internal bool IsAsync { get; }

    internal ImposterTargetMethodMetadata(
        IMethodSymbol symbol,
        string uniqueName)
    {
        Symbol = symbol;
        UniqueName = uniqueName;
        DisplayName = Symbol.ToFullDisplayName();
        Namespace = Symbol.ContainingNamespace.ToDisplayString();
        ReturnTypeSyntax = SyntaxFactoryHelper.TypeSyntax(Symbol.ReturnType);
        ReturnType = new ReturnTypeMetadata(Symbol.ReturnType);
        HasReturnValue = !Symbol.ReturnsVoid;
        IsAsync = IsMethodAsync(symbol);

        Parameters = new ImposterTargetMethodParametersMetadata(Symbol.Parameters);
        GenericTypeArguments = Symbol.TypeParameters.Select(p => SyntaxFactory.IdentifierName(p.Name)).ToArray();
        GenericTypeArgumentListSyntax = SyntaxFactoryHelper.TypeArgumentListSyntax(GenericTypeArguments);
        GenericTypeParameterListSyntax = SyntaxFactoryHelper.TypeParameterListSyntax(GenericTypeArguments);
        TargetGenericTypeArguments = Symbol.TypeParameters.Select(p => SyntaxFactory.IdentifierName($"{p.Name}Target")).ToArray();
        TargetGenericTypeParameterListSyntax = SyntaxFactoryHelper.TypeParameterListSyntax(TargetGenericTypeArguments);

        Delegate = TypeMetadataFactory.Create($"{uniqueName}Delegate", GenericTypeArguments);
        CallbackDelegate = TypeMetadataFactory.Create($"{uniqueName}CallbackDelegate", GenericTypeArguments);
        ExceptionGeneratorDelegate = TypeMetadataFactory.Create($"{uniqueName}ExceptionGeneratorDelegate", GenericTypeArguments);

        var argumentsTypeName = $"{uniqueName}Arguments";
        Arguments = new TypeMetadata(argumentsTypeName, SyntaxFactoryHelper.WithMethodGenericArguments(GenericTypeArguments, argumentsTypeName));
        ArgumentsCriteria = new ArgumentCriteriaTypeMetadata(this);
        InvocationHistory = new InvocationHistoryTypeMetadata(this);
        InvocationSetup = new InvocationSetupMetadata(this);
        InvocationVerifierInterface = new InvocationVerifierInterfaceMetadata(this);
        MethodImposter = new MethodImposterMetadata(this);
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
}
