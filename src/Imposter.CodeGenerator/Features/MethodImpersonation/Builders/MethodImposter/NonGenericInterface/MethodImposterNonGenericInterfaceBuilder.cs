using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.NonGenericInterface;

internal static class MethodImposterNonGenericInterfaceBuilder
{
    internal static InterfaceDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        if (!method.Model.IsGenericMethod)
        {
            return null;
        }

        return new InterfaceDeclarationBuilder(method.MethodImposter.Interface.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(
                SimpleBaseType(WellKnownTypes.Imposter.Abstractions.IImposterVerificationSource)
            )
            .AddMember(
                new MethodDeclarationBuilder(
                    WellKnownTypes.System.Collections.Generic.List(WellKnownTypes.String),
                    method.MethodImposter.ConfiguredSetupsMethodName
                )
                    .WithSemicolon()
                    .Build()
            )
            .AddMember(
                new MethodDeclarationBuilder(
                    NullableType(
                        method.MethodImposter.GenericInterface.SyntaxWithTargetGenericArguments
                    ),
                    MethodImposterMetadata.AsMethodName
                )
                    .WithTypeParameters(method.TargetGenericTypeParameterListSyntax)
                    .AddConstraintClauses(method.TargetGenericTypeConstraintClauses)
                    .WithSemicolon()
                    .Build()
            )
            .Build();
    }
}
