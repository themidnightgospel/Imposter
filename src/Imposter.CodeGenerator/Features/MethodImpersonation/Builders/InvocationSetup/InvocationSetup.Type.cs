using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterTargetMethodMetadata method)
    {
        return ClassDeclarationBuilderFactory
            .CreateForMethod(method.Model, method.MethodInvocationImposterGroup.Name)
            .AddMember(DefaultInstanceLazyInitializer(method))
            .AddMember(
                method.Parameters.HasInputParameters
                    ? SyntaxFactoryHelper.ArgumentsCriteriaProperty(method.ArgumentsCriteria.Syntax)
                    : null
            )
            .AddMember(InvocationImpostersFieldDeclaration())
            .AddMember(LastInvocationImposterFieldDeclaration())
            .AddMember(Constructor(method))
            .AddMember(AddInvocationImposterMethod(method))
            .AddMember(GetInvocationImposterMethod())
            .AddMember(InvokeMethodDeclarationSyntax(method))
            .AddMember(MethodInvocationImposterType(method))
            .Build()
#if DEBUG
            .WithLeadingTriviaComment(method.DisplayName)
#endif
        ;
    }

    private static ConstructorDeclarationSyntax Constructor(in ImposterTargetMethodMetadata method)
    {
        var ctorBuilder = new ConstructorBuilder(
            method.MethodInvocationImposterGroup.Name
        ).WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)));

        if (method.Parameters.HasInputParameters)
        {
            ctorBuilder = ctorBuilder
                .AddParameter(
                    SyntaxFactoryHelper.ParameterSyntax(
                        method.ArgumentsCriteria.Syntax,
                        "argumentsCriteria"
                    )
                )
                .WithBody(
                    Block(
                        IdentifierName("ArgumentsCriteria")
                            .Assign(IdentifierName("argumentsCriteria"))
                            .ToStatementSyntax()
                    )
                );
        }
        else
        {
            ctorBuilder = ctorBuilder.WithBody(Block());
        }

        return ctorBuilder.Build();
    }
}
