using System.Collections.Generic;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    internal static InterfaceDeclarationSyntax BuildInvocationSetupInterface(in ImposterTargetMethodMetadata method)
    {
        return InterfaceDeclarationBuilderFactory
            .CreateForMethod(method.Symbol, method.MethodInvocationImposterGroup.Interface.Name)
            .AddMembers(GetMethods(method))
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .Build();

        static IReadOnlyCollection<MemberDeclarationSyntax> GetMethods(in ImposterTargetMethodMetadata method)
        {
            var methods = new List<MemberDeclarationSyntax>
            {
                new MethodDeclarationBuilder(method.MethodInvocationImposterGroup.ThrowsMethod.ReturnType, method.MethodInvocationImposterGroup.ThrowsMethod.Name)
                    .WithTypeParameters(TypeParameterList(SingletonSeparatedList(TypeParameter("TException"))))
                    .AddConstraintClause(TypeParameterConstraintClause("TException").AddConstraints(TypeConstraint(IdentifierName("Exception")), ConstructorConstraint()))
                    .WithSemicolon()
                    .Build(),
                new MethodDeclarationBuilder(method.MethodInvocationImposterGroup.ThrowsMethod.ReturnType, method.MethodInvocationImposterGroup.ThrowsMethod.Name)
                    .WithParameterList(SyntaxFactoryHelper.ParameterSyntax(method.MethodInvocationImposterGroup.ThrowsMethod.ExceptionParameter).ToSingleParameterListSyntax())
                    .WithSemicolon()
                    .Build(),
                new MethodDeclarationBuilder(method.MethodInvocationImposterGroup.ThrowsMethod.ReturnType, method.MethodInvocationImposterGroup.ThrowsMethod.Name)
                    .AddParameter(SyntaxFactoryHelper.ParameterSyntax(method.MethodInvocationImposterGroup.ThrowsMethod.ExceptionGeneratorParameter))
                    .WithSemicolon()
                    .Build(),
                new MethodDeclarationBuilder(method.MethodInvocationImposterGroup.CallbackMethod.ReturnType, method.MethodInvocationImposterGroup.CallbackMethod.Name)
                    .AddParameter(SyntaxFactoryHelper.ParameterSyntax(method.MethodInvocationImposterGroup.CallbackMethod.CallbackParameter))
                    .WithSemicolon()
                    .Build(),
            };

            if (method.HasReturnValue)
            {
                methods.AddRange([
                    new MethodDeclarationBuilder(method.MethodInvocationImposterGroup.ReturnsMethod.ReturnType, method.MethodInvocationImposterGroup.ReturnsMethod.Name)
                        .AddParameter(SyntaxFactoryHelper.ParameterSyntax(method.MethodInvocationImposterGroup.ReturnsMethod.ResultGeneratorParameter))
                        .WithSemicolon()
                        .Build(),
                    new MethodDeclarationBuilder(method.MethodInvocationImposterGroup.ReturnsMethod.ReturnType, method.MethodInvocationImposterGroup.ReturnsMethod.Name)
                        .AddParameter(SyntaxFactoryHelper.ParameterSyntax(method.MethodInvocationImposterGroup.ReturnsMethod.ValueParameter))
                        .WithSemicolon()
                        .Build()
                ]);
            }

            if (method.MethodInvocationImposterGroup.ReturnsAsyncMethod is { } returnsAsyncMethod)
            {
                methods.Add(
                    new MethodDeclarationBuilder(returnsAsyncMethod.ReturnType, returnsAsyncMethod.Name)
                        .AddParameter(SyntaxFactoryHelper.ParameterSyntax(returnsAsyncMethod.ValueParameter))
                        .WithSemicolon()
                        .Build());
            }

            if (method.MethodInvocationImposterGroup.ThrowsAsyncMethod is { } throwsAsyncMethod)
            {
                methods.Add(
                    new MethodDeclarationBuilder(throwsAsyncMethod.ReturnType, throwsAsyncMethod.Name)
                        .AddParameter(SyntaxFactoryHelper.ParameterSyntax(throwsAsyncMethod.ExceptionParameter))
                        .WithSemicolon()
                        .Build());
            }

            if (method.MethodInvocationImposterGroup.UseBaseImplementationMethod is { } useBaseImplementationMethod)
            {
                methods.Add(new MethodDeclarationBuilder(useBaseImplementationMethod.ReturnType, useBaseImplementationMethod.Name)
                    .WithSemicolon()
                    .Build());
            }

            methods.Add(new MethodDeclarationBuilder(method.MethodInvocationImposterGroup.ThenMethod.ReturnType, method.MethodInvocationImposterGroup.ThenMethod.Name)
                .WithSemicolon()
                .Build());

            return methods;
        }
    }
}
