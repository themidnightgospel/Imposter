using System.Collections.Generic;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter.Setter;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter;

internal static class SharedPropertyBuilder
{
    internal static ConstructorDeclarationSyntax Constructor(
        in ImposterPropertyMetadata property,
        string className
    )
    {
        var mode = PropertyAccessorStateMetadata.InvocationBehaviorParameter;
        var displayName = PropertyAccessorStateMetadata.PropertyDisplayNameParameter;
        var constructor = new ConstructorBuilder(className).WithModifiers(
            TokenList(Token(SyntaxKind.InternalKeyword))
        );
        var arguments = new List<ArgumentSyntax>();
        if (property.Core.KeepsValue)
        {
            var field = property.ImposterBuilder.DefaultPropertyBehaviourField;
            constructor.AddParameter(ParameterSyntax(field.Type, field.Name));
            arguments.Add(Argument(IdentifierName(field.Name)));
        }
        else
        {
            arguments.Add(Argument(Null));
        }
        constructor.AddParameter(ParameterSyntax(mode)).AddParameter(ParameterSyntax(displayName));
        arguments.Add(Argument(IdentifierName(mode.Name)));
        arguments.Add(Argument(IdentifierName(displayName.Name)));
        return constructor
            .AddInitializer(
                ConstructorInitializer(
                    SyntaxKind.BaseConstructorInitializer,
                    ArgumentListSyntax(arguments)
                )
            )
            .Build();
    }

    internal static ClassDeclarationSyntax BuildSetter(in ImposterPropertyMetadata property)
    {
        var setter = property.SetterImposter;
        var runtime = new SharedPropertyMetadata(property.Core);
        var builder = new ClassDeclarationBuilder(setter.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(runtime.SetterType))
            .AddMember(Constructor(property, setter.Name));
        if (setter.SetMethod.RequiresDirectBaseAssignment)
        {
            builder.AddMember(
                new MethodDeclarationBuilder(setter.SetMethod.ReturnType, setter.SetMethod.Name)
                    .AddModifier(Token(SyntaxKind.InternalKeyword))
                    .AddParameter(ParameterSyntax(setter.SetMethod.ValueParameter))
                    .WithBody(
                        Block(
                            ReturnStatement(
                                BaseExpression()
                                    .Dot(
                                        IdentifierName(
                                            SharedPropertyMetadata.RecordAndShouldCallBase
                                        )
                                    )
                                    .Call(
                                        Argument(
                                            IdentifierName(setter.SetMethod.ValueParameter.Name)
                                        )
                                    )
                            )
                        )
                    )
                    .Build()
            );
        }
        return builder.AddMember(SetterImposterBuilderBuilder.Build(property)).Build();
    }
}
