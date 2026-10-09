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
    private const string MethodName = "Imposter";

    internal static ClassDeclarationSyntax Build(
        in ImposterGenerationContext imposterGenerationContext,
        string? imposterNamespaceName
    )
    {
        var extensionClassName = $"{imposterGenerationContext.Target.Name}{MethodName}Extensions";
        var targetType = imposterGenerationContext.Imposter.TargetTypeSyntax;

        var imposterType = SyntaxFactoryHelper.GlobalQualifiedName(
            imposterNamespaceName,
            imposterGenerationContext.Imposter.ImposterTypeSyntax.ToString()
        );
        var accessibilityModifiers = GetAccessibilityModifiers(
            imposterGenerationContext.Imposter.DeclaredAccessibility
        );
        var targetTypeParameters = imposterGenerationContext.Imposter.TypeParameters;

        IEnumerable<MethodDeclarationSyntax> methodDeclarations = imposterGenerationContext
            .Imposter
            .IsClass
            ? BuildClassMethods(imposterType, accessibilityModifiers, imposterGenerationContext)
            :
            [
                BuildParameterlessMethod(
                    imposterType,
                    accessibilityModifiers,
                    imposterGenerationContext.Imposter.InvocationBehaviorParameter
                ),
            ];

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
                        SyntaxFactoryHelper.ParameterSyntax(
                            targetType,
                            imposterGenerationContext.Imposter.ExtensionParameterName
                        )
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
        var invocationBehaviorParameter = imposterGenerationContext
            .Imposter
            .InvocationBehaviorParameter;
        var methods = new List<MethodDeclarationSyntax>(constructors.Length + 1);

        if (constructors.Any(constructor => constructor.Parameters.Length == 0))
        {
            methods.Add(
                BuildParameterlessMethod(
                    imposterType,
                    accessibilityModifiers,
                    invocationBehaviorParameter
                )
            );
        }

        foreach (var constructor in constructors.Where(it => it.Parameters.Length > 0))
        {
            methods.Add(
                BuildConstructorOverload(
                    imposterType,
                    accessibilityModifiers,
                    constructor,
                    invocationBehaviorParameter
                )
            );
        }

        return methods;
    }

    private static MethodDeclarationSyntax BuildParameterlessMethod(
        TypeSyntax imposterType,
        SyntaxTokenList accessibilityModifiers,
        in ParameterMetadata invocationBehaviorParameter
    ) =>
        new MethodDeclarationBuilder(imposterType, MethodName)
            .AddModifiers(accessibilityModifiers)
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .WithExpressionBody(
                ArrowExpressionClause(
                    imposterType.New(
                        ImposterModeArgument(invocationBehaviorParameter)
                            .AsSingleArgumentListSyntax()
                    )
                )
            )
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(invocationBehaviorParameter))
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildConstructorOverload(
        TypeSyntax imposterType,
        SyntaxTokenList accessibilityModifiers,
        in ImposterTargetConstructorMetadata constructorMetadata,
        in ParameterMetadata invocationBehaviorParameter
    )
    {
        var parameters = new List<ParameterSyntax>(
            SyntaxFactoryHelper.ParameterSyntaxes(constructorMetadata.Parameters)
        )
        {
            SyntaxFactoryHelper.ParameterSyntax(invocationBehaviorParameter),
        };

        var arguments = new List<ArgumentSyntax>(
            constructorMetadata.Parameters.Select(parameter =>
                SyntaxFactoryHelper.ArgumentSyntax(parameter)
            )
        )
        {
            ImposterModeArgument(invocationBehaviorParameter),
        };

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

    private static ArgumentSyntax ImposterModeArgument(
        in ParameterMetadata invocationBehaviorParameter
    ) => Argument(IdentifierName(invocationBehaviorParameter.Name));

    private static SyntaxTokenList GetAccessibilityModifiers(Accessibility targetAccessibility) =>
        targetAccessibility switch
        {
            Accessibility.Public => TokenList(Token(SyntaxKind.PublicKeyword)),
            _ => TokenList(Token(SyntaxKind.InternalKeyword)),
        };
}

#endif
