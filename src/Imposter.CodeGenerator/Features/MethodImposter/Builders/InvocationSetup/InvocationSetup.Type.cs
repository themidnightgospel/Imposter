using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterTargetMethodMetadata method)
    {
        return ClassDeclarationBuilderFactory
            .CreateForMethod(method.Symbol, method.InvocationSetup.Name)
            .AddMember(DefaultInstanceLazyInitializer(method))
            .AddMember(method.Parameters.HasInputParameters ? SyntaxFactoryHelper.ArgumentsCriteriaProperty(method.ArgumentsCriteria.Syntax) : null)
            .AddMember(InvocationImpostersFieldDeclaration(method))
            .AddMember(LastInvocationImposterFieldDeclaration(method))
            .AddMember(Constructor(method))
            .AddMember(AddInvocationImposterMethod(method))
            .AddMember(GetInvocationImposterMethod(method))
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
        var ctor = ConstructorDeclaration(method.InvocationSetup.Name)
            .AddModifiers(Token(SyntaxKind.PublicKeyword));

        if (method.Parameters.HasInputParameters)
        {
            ctor = ctor
                .AddParameterListParameters(Parameter(Identifier("argumentsCriteria")).WithType(method.ArgumentsCriteria.Syntax))
                .WithBody(Block(
                    AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName("ArgumentsCriteria"),
                            IdentifierName("argumentsCriteria"))
                        .ToStatementSyntax()));
        }
        else
        {
            ctor = ctor.WithBody(Block());
        }

        return ctor;
    }
}
