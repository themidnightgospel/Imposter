#if ROSLYN4_14_OR_GREATER

using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter.ImposterExtensions;

internal static class ImposterExtensionsBuilder
{
    private const string ExtensionParameterName = "imposter";
    private const string MethodName = "Imposter";
    private const string InvocationBehaviorParameterName = "invocationBehavior";

    internal static ClassDeclarationSyntax Build(
        in ImposterGenerationContext imposterGenerationContext,
        string imposterNamespaceName)
    {
        var extensionClassName = $"{imposterGenerationContext.TargetSymbol.Name}{MethodName}Extensions";
        var targetType = SyntaxFactoryHelper.TypeSyntax(imposterGenerationContext.TargetSymbol);
        var imposterType = ParseTypeName(GetImposterTypeName(imposterNamespaceName, imposterGenerationContext.Imposter.Name));

        var methods = imposterGenerationContext.Imposter.IsClass
            ? BuildClassMethods(imposterType, imposterGenerationContext)
            : [BuildParameterlessMethod(imposterType)];

        var extensionDeclaration =
#if ROSLYN_5_OR_GREATER
            ExtensionBlockDeclaration()
#else
            ExtensionDeclaration()
#endif
                .WithKeyword(Token(SyntaxKind.ExtensionKeyword))
                .WithParameterList(
                    ParameterList(
                        SingletonSeparatedList(
                            Parameter(Identifier(ExtensionParameterName))
                                .WithType(targetType))))
                .WithMembers(List(methods.Cast<MemberDeclarationSyntax>()))
                .WithOpenBraceToken(Token(SyntaxKind.OpenBraceToken))
                .WithCloseBraceToken(Token(SyntaxKind.CloseBraceToken));

        return new ClassDeclarationBuilder(extensionClassName)
            .AddPublicModifier()
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .AddMember(extensionDeclaration)
            .Build();
    }

    // TODO move to metadata
    private static string GetImposterTypeName(string namespaceName, string imposterName)
    {
        if (string.IsNullOrWhiteSpace(namespaceName))
        {
            return $"global::{imposterName}";
        }

        return $"global::{namespaceName}.{imposterName}";
    }

    private static List<MethodDeclarationSyntax> BuildClassMethods(
        TypeSyntax imposterType,
        in ImposterGenerationContext imposterGenerationContext)
    {
        var constructors = imposterGenerationContext.Imposter.AccessibleConstructors;
        var methods = new List<MethodDeclarationSyntax>(constructors.Length + 1);

        if (constructors.Any(constructor => constructor.Parameters.Length == 0))
        {
            methods.Add(BuildParameterlessMethod(imposterType));
        }

        foreach (var constructor in constructors)
        {
            methods.Add(BuildConstructorOverload(imposterType, constructor));
        }

        return methods;
    }

    private static MethodDeclarationSyntax BuildParameterlessMethod(TypeSyntax imposterType)
        => new MethodDeclarationBuilder(imposterType, MethodName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .WithExpressionBody(ArrowExpressionClause(imposterType.New()))
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildConstructorOverload(
        TypeSyntax imposterType,
        in ImposterTargetConstructorMetadata constructorMetadata)
    {
        var parameters = SyntaxFactoryHelper.ParameterSyntaxes(constructorMetadata.Parameters).ToList();
        parameters.Add(CreateInvocationBehaviorParameter());

        var arguments = new List<ArgumentSyntax>();

        if (constructorMetadata.Parameters.Length > 0)
        {
            arguments.AddRange(constructorMetadata.Parameters.Select(parameter => SyntaxFactoryHelper.ArgumentSyntax(parameter)));
        }

        arguments.Add(Argument(IdentifierName(InvocationBehaviorParameterName)));

        return new MethodDeclarationBuilder(imposterType, MethodName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .WithParameterList(SyntaxFactoryHelper.ParameterListSyntax(parameters))
            .WithExpressionBody(
                ArrowExpressionClause(
                    imposterType.New(SyntaxFactoryHelper.ArgumentListSyntax(arguments)))
            )
            .WithSemicolon()
            .Build();
    }

    private static ParameterSyntax CreateInvocationBehaviorParameter() =>
        Parameter(Identifier(InvocationBehaviorParameterName))
            .WithType(WellKnownTypes.Imposter.Abstractions.ImposterMode)
            .WithDefault(
                EqualsValueClause(
                    QualifiedName(
                        WellKnownTypes.Imposter.Abstractions.ImposterMode,
                        IdentifierName("Implicit"))));
}

#endif