using System.Collections.Generic;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    internal static InterfaceDeclarationSyntax BuildInvocationSetupInterface(in ImposterTargetMethodMetadata method)
    {
        return SyntaxFactoryHelper
            .InterfaceDeclarationBuilder(method.Symbol, method.InvocationSetup.Interface.Name)
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
                new MethodDeclarationBuilder(method.InvocationSetup.CallBeforeCallbackMethod.ReturnType, method.InvocationSetup.CallBeforeCallbackMethod.Name)
                    .AddParameter(SyntaxFactoryHelper.ParameterSyntax(method.InvocationSetup.CallBeforeCallbackMethod.CallbackParameter))
                    .WithSemicolon()
                    .Build(),
                new MethodDeclarationBuilder(method.InvocationSetup.CallAfterCallbackMethod.ReturnType, method.InvocationSetup.CallAfterCallbackMethod.Name)
                    .AddParameter(SyntaxFactoryHelper.ParameterSyntax(method.InvocationSetup.CallAfterCallbackMethod.CallbackParameter))
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

            return methods;
        }
    }
}