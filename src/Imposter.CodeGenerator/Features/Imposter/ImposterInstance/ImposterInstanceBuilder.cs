using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Imposter.CodeGenerator.Features.PropertyImposter.Metadata;
using Imposter.CodeGenerator.Features.Shared;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter.ImposterInstance;

internal readonly ref struct ImposterInstanceBuilder
{
    // TODO this might collide. Move it to Metadata
    private const string ImposterFieldName = "_imposter";

    private readonly ClassDeclarationBuilder _imposterInstanceBuilder;

    private ImposterInstanceBuilder(ClassDeclarationBuilder imposterInstanceBuilder)
    {
        _imposterInstanceBuilder = imposterInstanceBuilder;
    }

    internal ImposterInstanceBuilder AddImposterProperty(in ImposterPropertyMetadata property)
    {
        _imposterInstanceBuilder
            .AddMember(SyntaxFactoryHelper
                .PropertyDeclarationSyntax(
                    property.Core.TypeSyntax,
                    property.Core.Name,
                    property.Core.HasGetter
                        ? Block(
                            ReturnStatement(
                                IdentifierName("_imposter")
                                    .Dot(IdentifierName(property.AsField.Name))
                                    .Dot(IdentifierName("_getterImposterBuilder"))
                                    .Dot(IdentifierName("Get"))
                                    .Call()
                            ))
                        : null,
                    property.Core.HasSetter
                        ? Block(
                            IdentifierName("_imposter")
                                .Dot(IdentifierName(property.AsField.Name))
                                .Dot(IdentifierName("_setterImposter"))
                                .Dot(IdentifierName("Set"))
                                .Call(Argument(IdentifierName("value")))
                                .ToStatementSyntax())
                        : null
                ));
        
        return this;
    }
    
    internal ClassDeclarationSyntax Build() => _imposterInstanceBuilder.Build();

    internal static ImposterInstanceBuilder Create(in ImposterGenerationContext imposterGenerationContext, string name)
    {
        var fields = GetFields(imposterGenerationContext);

        var imposterClassBuilder = new ClassDeclarationBuilder(name)
            .AddBaseType(SimpleBaseType(SyntaxFactoryHelper.TypeSyntax(imposterGenerationContext.TargetSymbol)))
            .AddMembers(fields)
            .AddMember(SyntaxFactoryHelper.BuildConstructorAndInitializeMembers(name, fields))
            .AddMembers(ImposterMethods(imposterGenerationContext));

        return new ImposterInstanceBuilder(imposterClassBuilder);
    }

    private static IReadOnlyList<FieldDeclarationSyntax> GetFields(ImposterGenerationContext imposterGenerationContext) =>
    [
        SyntaxFactoryHelper.SingleVariableField(IdentifierName(imposterGenerationContext.Imposter.Name), ImposterFieldName)
    ];

    private static IEnumerable<MethodDeclarationSyntax> ImposterMethods(in ImposterGenerationContext imposterGenerationContext)
    {
        return imposterGenerationContext.Imposter.Methods.Select(imposterMethod =>
        {
            var invokeMethodInvocationExpression = GetImposterWithMatchingSetupExpression(imposterMethod)
                .Dot(IdentifierName("Invoke"))
                .Call(SyntaxFactoryHelper.ArgumentListSyntax(imposterMethod.Symbol.Parameters, includeRefKind: true));

            return new MethodDeclarationBuilder(SyntaxFactoryHelper.TypeSyntax(imposterMethod.Symbol.ReturnType), imposterMethod.Symbol.Name)
                .AddTypeParameters(SyntaxFactoryHelper.TypeParametersSyntax(imposterMethod.Symbol))
                .AddParameters(SyntaxFactoryHelper.ParameterSyntaxes(imposterMethod.Symbol.Parameters))
                .WithBody(Block(
                    imposterMethod.HasReturnValue
                        ? ReturnStatement(invokeMethodInvocationExpression)
                        : ExpressionStatement(invokeMethodInvocationExpression))
                )
                .AddModifier(Token(SyntaxKind.PublicKeyword))
                .Build();
        });

        ExpressionSyntax GetImposterWithMatchingSetupExpression(in ImposterTargetMethodMetadata method)
        {
            if (method.Symbol.IsGenericMethod)
            {
                return IdentifierName("_imposter")
                    .Dot(IdentifierName(method.MethodImposter.Collection.AsField.Name))
                    .Dot(GenericName(Identifier("GetImposterWithMatchingSetup"), method.GenericTypeArguments.AsTypeArguments()))
                    .Call(GetGetImposterWithMatchingSetupArguments(method));
            }

            return IdentifierName("_imposter")
                .Dot(IdentifierName(method.MethodImposter.AsField.Name));

            static ArgumentListSyntax? GetGetImposterWithMatchingSetupArguments(in ImposterTargetMethodMetadata method)
            {
                if (method.Parameters.HasInputParameters)
                {
                    return Argument(
                            method.Arguments.Syntax
                                .New(SyntaxFactoryHelper.ArgumentListSyntax(method.Parameters.InputParameters, includeRefKind: false))
                        )
                        .AsSingleArgumentListSyntax();
                }

                return default;
            }
        }
    }
}