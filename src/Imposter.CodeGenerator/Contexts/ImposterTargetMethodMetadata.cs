using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Contexts;

internal readonly record struct ImposterTargetMethodMetadata
{
    internal IMethodSymbol Symbol { get; }

    internal IReadOnlyList<IParameterSymbol> ParametersExceptOut { get; }

    internal MethodImposterType MethodImposter { get; }

    // As method names aren't unique in the class, we add a number at the end to make it so
    internal readonly string UniqueName;

    internal bool HasReturnValue => !Symbol.ReturnsVoid;

    internal readonly bool HasOutParameters;

    internal readonly string DisplayName;

    internal readonly TypeMetadata InvocationVerifierInterface;

    internal readonly TypeMetadata Delegate;

    internal readonly TypeMetadata CallbackDelegate;

    internal readonly TypeMetadata ExceptionGeneratorDelegate;

    internal readonly TypeMetadata ArgumentsType;

    internal readonly TypeMetadata ArgumentsCriteriaType;

    internal readonly TypeMetadata InvocationHistoryType;

    internal readonly InvocationSetupType InvocationSetupType;

    internal ImposterTargetMethodMetadata(IMethodSymbol symbol, string uniqueName)
    {
        Symbol = symbol;
        DisplayName = Symbol.ToFullDisplayName();

        ParametersExceptOut = symbol.Parameters.Where(it => it.RefKind is not RefKind.Out).ToList();
        UniqueName = uniqueName;
        HasOutParameters = symbol.Parameters.Any(it => it.RefKind is RefKind.Out);

        var argumentsTypeName = $"{uniqueName}Arguments";
        ArgumentsType = new TypeMetadata(argumentsTypeName, SyntaxFactoryHelper.TypeSyntaxWithGenericArguments(Symbol, argumentsTypeName));

        ArgumentsCriteriaType = CreateTypeMetadata(Symbol, $"{uniqueName}ArgumentsCriteria");
        InvocationHistoryType = CreateTypeMetadata(Symbol, $"{uniqueName}MethodInvocationHistory");

        var invocationSetupTypeName = $"{uniqueName}MethodInvocationsSetup";
        InvocationSetupType = new InvocationSetupType(
            invocationSetupTypeName, CreateTypeMetadata(Symbol, $"I{invocationSetupTypeName}"),
            SyntaxFactoryHelper.TypeSyntaxWithGenericArguments(Symbol, invocationSetupTypeName));

        Delegate = CreateTypeMetadata(Symbol, $"{uniqueName}Delegate");
        CallbackDelegate = CreateTypeMetadata(Symbol, $"{uniqueName}CallbackDelegate");
        ExceptionGeneratorDelegate = CreateTypeMetadata(Symbol, $"{uniqueName}ExceptionGeneratorDelegate");
        InvocationVerifierInterface = CreateTypeMetadata(Symbol, $"{uniqueName}MethodInvocationVerifier");

        var methodImposterTypeName = $"{UniqueName}MethodImposter";
        MethodImposter = new MethodImposterType(
            methodImposterTypeName,
            CreateTypeMetadata(Symbol, $"I{methodImposterTypeName}Builder"),
            SyntaxFactoryHelper.TypeSyntaxWithGenericArguments(Symbol, methodImposterTypeName),
            new FieldDeclarationMetadata(methodImposterTypeName),
            CreateTypeMetadata(Symbol, $"Builder"));
    }

    private static TypeMetadata CreateTypeMetadata(IMethodSymbol methodSymbol, string typeName) =>
        new TypeMetadata(typeName, SyntaxFactoryHelper.TypeSyntaxWithGenericArguments(methodSymbol, typeName));
}

internal interface ITypeMetadata
{
    internal string Name { get; }

    internal NameSyntax Syntax { get; }
}

internal readonly record struct TypeMetadata(string Name, NameSyntax Syntax) : ITypeMetadata;

internal readonly record struct InvocationSetupType(string Name, TypeMetadata Interface, NameSyntax Syntax) : ITypeMetadata;

internal readonly record struct MethodImposterType(
    string Name,
    TypeMetadata BuilderInterface,
    NameSyntax Syntax,
    FieldDeclarationMetadata AsField,
    TypeMetadata Builder) : ITypeMetadata;

internal readonly record struct FieldDeclarationMetadata
{
    public string Name { get; }

    public FieldDeclarationMetadata(string typeName)
    {
        Name = "_" + char.ToLower(typeName[0]) + typeName[1..];
    }
}