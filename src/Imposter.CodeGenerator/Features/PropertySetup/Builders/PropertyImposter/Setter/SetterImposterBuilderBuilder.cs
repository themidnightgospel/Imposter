using Imposter.CodeGenerator.Features.PropertySetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.PropertySetup.Builders.PropertyImposter.Setter;

internal static class SetterImposterBuilderBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterPropertyMetadata property) =>
        new ClassDeclarationBuilder(property.SetterImposter.Builder.Name)
            .AddBaseType(SimpleBaseType(property.SetterImposterBuilderInterface.Syntax))
            .AddMember(SinglePrivateReadonlyVariableField(property.SetterImposter.Builder.SetterImposterField))
            .AddMember(SinglePrivateReadonlyVariableField(property.SetterImposter.Builder.CriteriaField))
            .AddMember(BuildConstructor(property))
            .AddMember(BuildCallbackMethod(property))
            .AddMember(BuildCalledMethod(property))
            .Build();

    private static ConstructorDeclarationSyntax BuildConstructor(in ImposterPropertyMetadata property) =>
        new ConstructorWithFieldInitializationBuilder(property.SetterImposter.Builder.Name)
            .WithModifiers(Token(SyntaxKind.InternalKeyword))
            .AddParameter(property.SetterImposter.Builder.SetterImposterField)
            .AddParameter(property.SetterImposter.Builder.CriteriaField)
            .Build();

    internal static MethodDeclarationSyntax BuildCalledMethod(in ImposterPropertyMetadata property) =>
        new MethodDeclarationBuilder(property.SetterImposterBuilderInterface.CalledMethod.ReturnType, property.SetterImposterBuilderInterface.CalledMethod.Name)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(property.SetterImposterBuilderInterface.Syntax))
            .AddParameter(ParameterSyntax(property.SetterImposterBuilderInterface.CalledMethod.CountParameter))
            .WithBody(Block(
                IdentifierName(property.SetterImposter.Builder.SetterImposterField.Name)
                    .Dot(IdentifierName(property.SetterImposter.CalledMethod.Name))
                    .Call(ArgumentListSyntax(
                        [
                            Argument(IdentifierName(property.SetterImposter.Builder.CriteriaField.Name)),
                            Argument(IdentifierName(property.SetterImposterBuilderInterface.CalledMethod.CountParameter.Name))
                        ])
                    )
                    .ToStatementSyntax()
            ))
            .Build();

    internal static MethodDeclarationSyntax BuildCallbackMethod(in ImposterPropertyMetadata property) =>
        new MethodDeclarationBuilder(property.SetterImposterBuilderInterface.CallbackMethod.ReturnType, property.SetterImposterBuilderInterface.CallbackMethod.Name)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(property.SetterImposterBuilderInterface.Syntax))
            .AddParameter(ParameterSyntax(property.SetterImposterBuilderInterface.CallbackMethod.CallbackParameter))
            .WithBody(Block(
                IdentifierName(property.SetterImposter.Builder.SetterImposterField.Name)
                    .Dot(IdentifierName(property.SetterImposter.CallbackMethod.Name))
                    .Call(ArgumentListSyntax(
                        [
                            Argument(IdentifierName(property.SetterImposter.Builder.CriteriaField.Name)),
                            Argument(IdentifierName(property.SetterImposterBuilderInterface.CallbackMethod.CallbackParameter.Name)),
                        ])
                    )
                    .ToStatementSyntax()
                ,
                ReturnStatement(ThisExpression())
            ))
            .Build();
}