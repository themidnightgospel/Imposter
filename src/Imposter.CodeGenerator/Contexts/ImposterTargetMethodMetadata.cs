using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Contexts;

internal readonly record struct ImposterTargetMethodParametersMetadata
{
    internal IReadOnlyList<IParameterSymbol> Parameters { get; }
    
    internal IReadOnlyList<IParameterSymbol> InputParameters { get; }

    internal readonly ParameterListSyntax ParameterListSyntax;

    internal readonly ParameterListSyntax InputParameterWithoutRefKindListSyntax;

    internal readonly ArgumentListSyntax InputParametersAsArgumentListSyntaxWithoutRef;
    
    internal readonly ArgumentListSyntax InputParametersAsArgumentListSyntaxWithRef;
    
    internal readonly ArgumentListSyntax ParametersAsArgumentListSyntaxWithRef;

    internal bool HasInputParameters => InputParameters.Count > 0;

    internal readonly bool HasOutputParameters;

    public ImposterTargetMethodParametersMetadata(IReadOnlyList<IParameterSymbol> symbolParameters)
    {
        Parameters = symbolParameters;
        InputParameters = symbolParameters.Where(it => it.RefKind is not RefKind.Out).ToArray();
        HasOutputParameters = symbolParameters.Any(it => it.RefKind is RefKind.Out);

        ParameterListSyntax = SyntaxFactoryHelper.ParameterListSyntax(symbolParameters);
        InputParameterWithoutRefKindListSyntax = SyntaxFactoryHelper.ParameterListSyntax(InputParameters, includeRefKind: false);

        InputParametersAsArgumentListSyntaxWithoutRef = SyntaxFactoryHelper.ArgumenstListSyntax(InputParameters, includeRefKind: false);
        InputParametersAsArgumentListSyntaxWithRef = SyntaxFactoryHelper.ArgumenstListSyntax(InputParameters, includeRefKind: true);
        ParametersAsArgumentListSyntaxWithRef = SyntaxFactoryHelper.ArgumenstListSyntax(Parameters, includeRefKind: true);
    }
}

internal interface IParameterNameContextProvider
{
    SymbolNameNamespace CreateParameterNameContext();
}

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

    // TODO: Put those into GenericTypeArgumentsMetadata
    internal readonly IReadOnlyList<NameSyntax> GenericTypeArguments;

    internal readonly TypeParameterListSyntax? GenericTypeParameterListSyntax;

    internal readonly TypeArgumentListSyntax? GenericTypeArgumentListSyntax;

    internal readonly IReadOnlyList<IdentifierNameSyntax> TargetGenericTypeArguments;

    internal readonly TypeParameterListSyntax? TargetGenericTypeParameterListSyntax;

    public SymbolNameNamespace CreateParameterNameContext() => new(SymbolNames.GetParameterNames(this.Symbol));

    internal ImposterTargetMethodMetadata(
        IMethodSymbol symbol,
        string uniqueName)
    {
        Symbol = symbol;
        DisplayName = Symbol.ToFullDisplayName();

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