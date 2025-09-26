using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Contexts;

internal class ImposterTargetMethod
{
    internal IMethodSymbol Symbol { get; }

    internal IReadOnlyList<IParameterSymbol> ParametersExceptOut { get; }

    internal MethodImposterType MethodImposter { get; }

    // As method names aren't unique in the class, we add a number at the end to make it so
    internal string UniqueName;

    internal bool HasReturnValue => !Symbol.ReturnsVoid;

    internal readonly bool HasOutParameters;

    internal readonly string DisplayName;

    internal string MethodInvocationHistoryClassName { get; }

    internal string InvocationSetup { get; }

    internal string InvocationsSetupInterface => $"I{InvocationSetup}";

    internal readonly string MethodInvocationVerifierInterfaceName;

    internal readonly string DelegateName;

    internal readonly string CallbackDelegateName;

    internal readonly string ExceptionGeneratorDelegateName;

    internal readonly string ArgumentsClassName;

    internal readonly string ArgumentsCriteriaClassName;

    internal ImposterTargetMethod(IMethodSymbol symbol, string uniqueName)
    {
        Symbol = symbol;
        ParametersExceptOut = symbol.Parameters.Where(it => it.RefKind is not RefKind.Out).ToList();
        UniqueName = uniqueName;
        MethodInvocationHistoryClassName = $"{uniqueName}MethodInvocationHistory";
        InvocationSetup = $"{uniqueName}MethodInvocationsSetup";
        MethodInvocationVerifierInterfaceName = $"I{uniqueName}MethodInvocationVerifier";
        DelegateName = $"{uniqueName}Delegate";
        CallbackDelegateName = $"{uniqueName}CallbackDelegate";
        ExceptionGeneratorDelegateName = $"{uniqueName}ExceptionGeneratorDelegate";
        ArgumentsClassName = $"{uniqueName}Arguments";
        ArgumentsCriteriaClassName = $"{uniqueName}ArgumentsCriteria";
        HasOutParameters = symbol.Parameters.Any(it => it.RefKind is RefKind.Out);
        MethodImposter = new MethodImposterType(this);
        DisplayName = Symbol.ToFullDisplayName();
    }
}