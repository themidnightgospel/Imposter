using System.Collections.Generic;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    internal static InterfaceDeclarationSyntax BuildInvocationSetupInterface(in ImposterTargetMethodMetadata method)
    {
        return InterfaceDeclarationBuilderFactory
            .CreateForMethod(method.Symbol, method.InvocationSetup.Interface.Name)
            .AddMembers(GetMethods(method))
            .Build(SyntaxTokenList.Create(Token(SyntaxKind.PublicKeyword)));

        static IReadOnlyCollection<MemberDeclarationSyntax> GetMethods(in ImposterTargetMethodMetadata method)
        {
            var methods = new List<MemberDeclarationSyntax>
            {
                new MethodDeclarationBuilder(method.InvocationSetup.ThrowsMethod.ReturnType, method.InvocationSetup.ThrowsMethod.Name)
                    .WithTypeParameters(TypeParameterList(SingletonSeparatedList(TypeParameter("TException"))))
                    .AddConstraintClause(TypeParameterConstraintClause("TException").AddConstraints(TypeConstraint(IdentifierName("Exception")), ConstructorConstraint()))
                    .WithSemicolon()
                    .Build(),
                new MethodDeclarationBuilder(method.InvocationSetup.ThrowsMethod.ReturnType, method.InvocationSetup.ThrowsMethod.Name)
                    .WithParameterList(SyntaxFactoryHelper.ParameterSyntax(method.InvocationSetup.ThrowsMethod.ExceptionParameter).ToSingleParameterListSyntax())
                    .WithSemicolon()
                    .Build(),
                new MethodDeclarationBuilder(method.InvocationSetup.ThrowsMethod.ReturnType, method.InvocationSetup.ThrowsMethod.Name)
                    .AddParameter(SyntaxFactoryHelper.ParameterSyntax(method.InvocationSetup.ThrowsMethod.ExceptionGeneratorParameter))
                    .WithSemicolon()
                    .Build(),
                new MethodDeclarationBuilder(method.InvocationSetup.CallbackMethod.ReturnType, method.InvocationSetup.CallbackMethod.Name)
                    .AddParameter(SyntaxFactoryHelper.ParameterSyntax(method.InvocationSetup.CallbackMethod.CallbackParameter))
                    .WithSemicolon()
                    .Build(),
            };

            if (method.HasReturnValue)
            {
                methods.AddRange([
                    new MethodDeclarationBuilder(method.InvocationSetup.ReturnsMethod.ReturnType, method.InvocationSetup.ReturnsMethod.Name)
                        .AddParameter(SyntaxFactoryHelper.ParameterSyntax(method.InvocationSetup.ReturnsMethod.ResultGeneratorParameter))
                        .WithSemicolon()
                        .Build(),
                    new MethodDeclarationBuilder(method.InvocationSetup.ReturnsMethod.ReturnType, method.InvocationSetup.ReturnsMethod.Name)
                        .AddParameter(SyntaxFactoryHelper.ParameterSyntax(method.InvocationSetup.ReturnsMethod.ValueParameter))
                        .WithSemicolon()
                        .Build()
                ]);
            }

            methods.Add(new MethodDeclarationBuilder(method.InvocationSetup.ThenMethod.ReturnType, method.InvocationSetup.ThenMethod.Name)
                .WithSemicolon()
                .Build());

            return methods;
        }
    }
}
