using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Shared.Builders;

internal static class FormatValueMethodBuilder
{
    internal const string Name = "FormatValue";

    internal static MethodDeclarationSyntax Build()
    {
        var valueParameter = ParameterSyntax(WellKnownTypes.Object.ToNullableType(), "value");

        return new MethodDeclarationBuilder(WellKnownTypes.String, Name)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .AddParameter(valueParameter)
            .WithBody(
                Block(
                    ReturnStatement(
                        WellKnownTypes
                            .Imposter.Abstractions.DiagnosticValue.Dot(IdentifierName("Format"))
                            .Call(Argument(IdentifierName("value")))
                    )
                )
            )
            .Build();
    }

    internal static InvocationExpressionSyntax Invocation(ExpressionSyntax value) =>
        IdentifierName(Name).Call(Argument(value));
}
