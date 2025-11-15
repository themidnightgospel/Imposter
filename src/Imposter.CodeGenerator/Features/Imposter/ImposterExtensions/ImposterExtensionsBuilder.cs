#if ROSLYN4_14_OR_GREATER

using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
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
        string imposterNamespaceName
    )
    {
        var extensionClassName =
            $"{imposterGenerationContext.TargetSymbol.Name}{MethodName}Extensions";
        var targetType = imposterGenerationContext.Imposter.TargetTypeSyntax;

        var imposterType = SyntaxFactoryHelper.GlobalQualifiedName(
            imposterNamespaceName,
            imposterGenerationContext.Imposter.ImposterTypeSyntax.ToString()
        );
        var accessibilityModifiers = GetAccessibilityModifiers(
            imposterGenerationContext.TargetSymbol
        );
        var targetTypeParameters = imposterGenerationContext.Imposter.TypeParameters;

        IEnumerable<MethodDeclarationSyntax> methodDeclarations = imposterGenerationContext
            .Imposter
            .IsClass
            ? BuildClassMethods(imposterType, accessibilityModifiers, imposterGenerationContext)
            : [BuildParameterlessMethod(imposterType, accessibilityModifiers)];

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
                        Parameter(Identifier(ExtensionParameterName)).WithType(targetType)
                    )
                )
            )
            .WithMembers(List(methodDeclarations.Cast<MemberDeclarationSyntax>()))
            .WithOpenBraceToken(Token(SyntaxKind.OpenBraceToken))
            .WithCloseBraceToken(Token(SyntaxKind.CloseBraceToken));

        if (targetTypeParameters.TypeParameterListSyntax is not null)
        {
            extensionDeclaration = extensionDeclaration.WithTypeParameterList(
                targetTypeParameters.TypeParameterListSyntax
            );
        }

        if (targetTypeParameters.ConstraintClauses.Count > 0)
        {
            extensionDeclaration = extensionDeclaration.WithConstraintClauses(
                List(targetTypeParameters.ConstraintClauses)
            );
        }

        var classDeclarationBuilder = new ClassDeclarationBuilder(extensionClassName);
        foreach (var modifier in accessibilityModifiers)
        {
            classDeclarationBuilder = classDeclarationBuilder.AddModifier(modifier);
        }

        return classDeclarationBuilder
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .AddMember(extensionDeclaration)
            .Build();
    }

    private static List<MethodDeclarationSyntax> BuildClassMethods(
        TypeSyntax imposterType,
        SyntaxTokenList accessibilityModifiers,
        in ImposterGenerationContext imposterGenerationContext
    )
    {
        var constructors = imposterGenerationContext.Imposter.AccessibleConstructors;
        var methods = new List<MethodDeclarationSyntax>(constructors.Length + 1);

        if (constructors.Any(constructor => constructor.Parameters.Length == 0))
        {
            methods.Add(BuildParameterlessMethod(imposterType, accessibilityModifiers));
        }

        foreach (var constructor in constructors)
        {
            methods.Add(
                BuildConstructorOverload(imposterType, accessibilityModifiers, constructor)
            );
        }

        return methods;
    }

    private static MethodDeclarationSyntax BuildParameterlessMethod(
        TypeSyntax imposterType,
        SyntaxTokenList accessibilityModifiers
    ) =>
        new MethodDeclarationBuilder(imposterType, MethodName)
            .AddModifiers(accessibilityModifiers)
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .WithExpressionBody(ArrowExpressionClause(imposterType.New()))
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildConstructorOverload(
        TypeSyntax imposterType,
        SyntaxTokenList accessibilityModifiers,
        in ImposterTargetConstructorMetadata constructorMetadata
    )
    {
        var parameters = SyntaxFactoryHelper
            .ParameterSyntaxes(constructorMetadata.Parameters)
            .ToList();
        parameters.Add(CreateInvocationBehaviorParameter());

        var arguments = new List<ArgumentSyntax>();

        if (constructorMetadata.Parameters.Length > 0)
        {
            arguments.AddRange(
                constructorMetadata.Parameters.Select(parameter =>
                    SyntaxFactoryHelper.ArgumentSyntax(parameter)
                )
            );
        }

        arguments.Add(Argument(IdentifierName(InvocationBehaviorParameterName)));

        return new MethodDeclarationBuilder(imposterType, MethodName)
            .AddModifiers(accessibilityModifiers)
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .WithParameterList(SyntaxFactoryHelper.ParameterListSyntax(parameters))
            .WithExpressionBody(
                ArrowExpressionClause(
                    imposterType.New(SyntaxFactoryHelper.ArgumentListSyntax(arguments))
                )
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
                        IdentifierName("Implicit")
                    )
                )
            );

    private static SyntaxTokenList GetAccessibilityModifiers(INamedTypeSymbol targetSymbol) =>
        targetSymbol.DeclaredAccessibility switch
        {
            Accessibility.Public => TokenList(Token(SyntaxKind.PublicKeyword)),
            _ => TokenList(Token(SyntaxKind.InternalKeyword)),
        };
}

#endif
