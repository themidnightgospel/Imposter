using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Contexts;

internal readonly record struct ImposterTargetMethodMetadata
{
    internal IMethodSymbol Symbol { get; }

    internal IReadOnlyList<IParameterSymbol> ParametersExceptOut { get; }

    internal bool HasInputParameters => ParametersExceptOut.Count > 0;

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

    internal readonly InvocationHistoryTypeMetadata InvocationHistory;

    internal readonly InvocationSetupType InvocationSetupType;

    internal readonly IReadOnlyList<NameSyntax> GenericTypeArguments;

    internal readonly IReadOnlyList<IdentifierNameSyntax> TargetGenericTypeArguments;

    internal ImposterTargetMethodMetadata(IMethodSymbol symbol, string uniqueName)
    {
        Symbol = symbol;
        DisplayName = Symbol.ToFullDisplayName();

        GenericTypeArguments = Symbol
            .TypeParameters
            .Select(p => SyntaxFactory.IdentifierName(p.Name))
            .ToList();

        TargetGenericTypeArguments = Symbol
            .TypeParameters
            .Select(p => SyntaxFactory.IdentifierName(p.Name + "Target")).ToList();

        ParametersExceptOut = symbol.Parameters.Where(it => it.RefKind is not RefKind.Out).ToList();
        UniqueName = uniqueName;
        HasOutParameters = symbol.Parameters.Any(it => it.RefKind is RefKind.Out);

        var argumentsTypeName = $"{uniqueName}Arguments";
        ArgumentsType = new TypeMetadata(argumentsTypeName, SyntaxFactoryHelper.WithMethodGenericArguments(Symbol, argumentsTypeName));

        ArgumentsCriteriaType = CreateTypeMetadata(Symbol, $"{uniqueName}ArgumentsCriteria");

        var invocationHistoryTypeName = $"{uniqueName}MethodInvocationHistory";
        InvocationHistory = new InvocationHistoryTypeMetadata(
            Name: invocationHistoryTypeName,
            Interface: new TypeMetadata($"I{invocationHistoryTypeName}"),
            Collection: new InvocationHistoryTypeMetadata.CollectionMetadata($"{invocationHistoryTypeName}Collection"),
            AsField: new FieldDeclarationMetadata(invocationHistoryTypeName),
            Syntax: SyntaxFactoryHelper.WithMethodGenericArguments(Symbol, invocationHistoryTypeName)
        );

        var invocationSetupTypeName = $"{uniqueName}MethodInvocationsSetup";
        InvocationSetupType = new InvocationSetupType(
            invocationSetupTypeName, CreateTypeMetadata(Symbol, $"I{invocationSetupTypeName}"),
            SyntaxFactoryHelper.WithMethodGenericArguments(Symbol, invocationSetupTypeName));

        Delegate = CreateTypeMetadata(Symbol, $"{uniqueName}Delegate");
        CallbackDelegate = CreateTypeMetadata(Symbol, $"{uniqueName}CallbackDelegate");
        ExceptionGeneratorDelegate = CreateTypeMetadata(Symbol, $"{uniqueName}ExceptionGeneratorDelegate");
        InvocationVerifierInterface = CreateTypeMetadata(Symbol, $"{uniqueName}MethodInvocationVerifier");

        var methodImposterTypeName = $"{UniqueName}MethodImposter";
        var methodImposterBuilderInterfaceName = $"I{methodImposterTypeName}Builder";
        var methodImposterInterfaceName = $"I{methodImposterTypeName}";

        var methodImposterSyntax = SyntaxFactoryHelper.WithMethodGenericArguments(Symbol, methodImposterTypeName);
        MethodImposter = new MethodImposterType(
            Name: methodImposterTypeName,
            BuilderInterface: CreateTypeMetadata(Symbol, methodImposterBuilderInterfaceName),
            Interface: new TypeMetadata(methodImposterInterfaceName),
            GenericInterface: new MethodImposterType.GenericTypeMetadata(
                methodImposterInterfaceName,
                GenericTypeArguments,
                TargetGenericTypeArguments
            ),
            Collection: new MethodImposterType.CollectionMetadata($"{methodImposterTypeName}Collection"),
            Syntax: methodImposterSyntax,
            AsField: new FieldDeclarationMetadata(methodImposterTypeName),
            Builder: new TypeMetadata("Builder", SyntaxFactory.QualifiedName(methodImposterSyntax, SyntaxFactory.IdentifierName("Builder"))));
    }

    private static TypeMetadata CreateTypeMetadata(IMethodSymbol methodSymbol, string typeName) =>
        new(typeName, SyntaxFactoryHelper.WithMethodGenericArguments(methodSymbol, typeName));
}

internal interface ITypeMetadata
{
    internal string Name { get; }

    internal NameSyntax Syntax { get; }
}

internal readonly record struct TypeMetadata(string Name, NameSyntax Syntax) : ITypeMetadata
{
    public TypeMetadata(string Name)
        : this(Name, SyntaxFactory.IdentifierName(Name))
    {
    }
}

internal readonly record struct InvocationHistoryTypeMetadata(
    string Name,
    TypeMetadata Interface,
    InvocationHistoryTypeMetadata.CollectionMetadata Collection,
    FieldDeclarationMetadata AsField,
    NameSyntax Syntax
) : ITypeMetadata
{
    internal readonly record struct CollectionMetadata(string Name, NameSyntax Syntax, FieldDeclarationMetadata AsField)
    {
        public CollectionMetadata(string Name)
            : this(Name, SyntaxFactory.IdentifierName(Name), new FieldDeclarationMetadata(Name))
        {
        }
    }
}

internal readonly record struct InvocationSetupType(string Name, TypeMetadata Interface, NameSyntax Syntax) : ITypeMetadata
{
    public const string GetOrAddMethodSetupMethodName = "GetOrAddMethodSetup";
}

internal readonly record struct MethodImposterType(
    string Name,
    TypeMetadata BuilderInterface,
    TypeMetadata Interface,
    MethodImposterType.GenericTypeMetadata GenericInterface,
    MethodImposterType.CollectionMetadata Collection,
    NameSyntax Syntax,
    FieldDeclarationMetadata AsField,
    TypeMetadata Builder) : ITypeMetadata
{
    internal readonly record struct CollectionMetadata(string Name, NameSyntax Syntax, FieldDeclarationMetadata AsField) : ITypeMetadata
    {
        public CollectionMetadata(string Name)
            : this(Name, SyntaxFactory.IdentifierName(Name), new FieldDeclarationMetadata(Name))
        {
        }
    }

    internal readonly record struct GenericTypeMetadata(string Name, NameSyntax Syntax, NameSyntax SyntaxWithTargetGenericArguments) : ITypeMetadata
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
}

internal readonly record struct FieldDeclarationMetadata
{
    public string Name { get; }

    public FieldDeclarationMetadata(string typeName)
    {
        Name = "_" + char.ToLower(typeName[0]) + typeName[1..];
    }
}