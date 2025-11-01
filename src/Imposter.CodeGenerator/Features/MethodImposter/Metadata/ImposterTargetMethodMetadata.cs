using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata;

// TODO Split this similar to how property metada is
internal readonly struct ImposterTargetMethodMetadata : IParameterNameContextProvider
{
    internal IMethodSymbol Symbol { get; }

    internal readonly ImposterTargetMethodParametersMetadata Parameters;

    internal readonly MethodImposterMetadata MethodImposter;

    // As method names aren't unique in the class, we add a number at the end to make it so
    internal readonly string UniqueName;

    internal bool HasReturnValue => !Symbol.ReturnsVoid;

    internal readonly string DisplayName;

    internal readonly InvocationVerifierInterfaceMetadata InvocationVerifierInterface;

    internal readonly TypeMetadata Delegate;

    internal readonly TypeMetadata CallbackDelegate;

    internal readonly TypeMetadata ExceptionGeneratorDelegate;

    internal readonly TypeMetadata Arguments;

    internal readonly ArgumentCriteriaTypeMetadata ArgumentsCriteria;

    internal readonly InvocationHistoryTypeMetadata InvocationHistory;

    internal readonly InvocationSetupMetadata InvocationSetup;

    internal readonly TypeSyntax ReturnTypeSyntax;

    internal readonly bool IsAsync;

    // TODO: Put those into GenericTypeArgumentsMetadata
    internal readonly IReadOnlyList<NameSyntax> GenericTypeArguments;

    internal readonly TypeParameterListSyntax? GenericTypeParameterListSyntax;

    internal readonly TypeArgumentListSyntax? GenericTypeArgumentListSyntax;

    internal readonly IReadOnlyList<IdentifierNameSyntax> TargetGenericTypeArguments;

    internal readonly TypeParameterListSyntax? TargetGenericTypeParameterListSyntax;

    public NameSet CreateParameterNameContext() => new(Symbol.Parameters.Select(p => p.Name));

    internal ImposterTargetMethodMetadata(
        IMethodSymbol symbol,
        string uniqueName)
    {
        Symbol = symbol;
        DisplayName = Symbol.ToFullDisplayName();
        IsAsync = IsMethodAsync(symbol);

        UniqueName = uniqueName;
        ReturnTypeSyntax = SyntaxFactoryHelper.TypeSyntax(Symbol.ReturnType);

        Parameters = new ImposterTargetMethodParametersMetadata(Symbol.Parameters);

        GenericTypeArguments = Symbol
            .TypeParameters
            .Select(p => SyntaxFactory.IdentifierName(p.Name))
            .ToArray();

        GenericTypeParameterListSyntax = SyntaxFactoryHelper.TypeParameterListSyntax(GenericTypeArguments);
        GenericTypeArgumentListSyntax = SyntaxFactoryHelper.TypeArgumentListSyntax(GenericTypeArguments);

        TargetGenericTypeArguments = Symbol
            .TypeParameters
            .Select(p => SyntaxFactory.IdentifierName(p.Name + "Target"))
            .ToArray();

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

    // TODO improve
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

internal readonly struct InvocationVerifierInterfaceMetadata
{
    internal const string VerifyMethodArgumentsParameterName = "arguments";

    internal readonly NameSyntax Syntax;

    internal readonly CalledMethodMetadata CalledMethod;

    internal readonly string Name;

    public InvocationVerifierInterfaceMetadata(ImposterTargetMethodMetadata method)
    {
        Name = $"{method.UniqueName}MethodInvocationVerifier";
        Syntax = SyntaxFactoryHelper.WithMethodGenericArguments(method.GenericTypeArguments, Name);
        CalledMethod = new CalledMethodMetadata();
    }

    internal readonly struct CalledMethodMetadata
    {
        internal const string Name = "Called";

        internal readonly ParameterMetadata CountParameter;

        public CalledMethodMetadata()
        {
            CountParameter = new ParameterMetadata("count", WellKnownTypes.Imposter.Abstractions.Count);
        }
    }
}