using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.GenericInterface;

internal static class MethodImposterGenericInterfaceBuilder
{
    internal static InterfaceDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        if (!method.Symbol.IsGenericMethod)
        {
            return null;
        }

        var genericInterfaceType = method.MethodImposter.Interface;
        var invokeMethodParameters = SyntaxFactoryHelper.ParameterListSyntax(method.Symbol.Parameters);

        if (method.SupportsBaseImplementation)
        {
            invokeMethodParameters = invokeMethodParameters.AddParameters(
                Parameter(Identifier(method.MethodImposter.InvokeMethod.BaseInvocationParameterName))
                    .WithType(method.Delegate.Syntax.ToNullableType())
                    .WithDefault(EqualsValueClause(Null)));
        }

        var invokeMethod = new MethodDeclarationBuilder(
                SyntaxFactoryHelper.TypeSyntax(method.Symbol.ReturnType),
                "Invoke")
            .WithParameterList(invokeMethodParameters)
            .WithSemicolon()
            .Build();

        var hasMatchingMethodMetadata = method.MethodImposter.HasMatchingInvocationImposterGroupMethod;

        var hasMatchingSetupMethodBuilder = new MethodDeclarationBuilder(
            hasMatchingMethodMetadata.ReturnType,
            hasMatchingMethodMetadata.Name);

        if (method.Parameters.HasInputParameters)
        {
            hasMatchingSetupMethodBuilder = hasMatchingSetupMethodBuilder
                .AddParameter(
                    Parameter(Identifier(hasMatchingMethodMetadata.ArgumentsParameterName)).WithType(method.Arguments.Syntax)
                );
        }

        var hasMatchingSetupMethod = hasMatchingSetupMethodBuilder
            .WithSemicolon()
            .Build();

        return InterfaceDeclarationBuilderFactory
            .CreateForMethod(method.Symbol, genericInterfaceType.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(method.MethodImposter.Interface.Syntax))
            .AddMember(invokeMethod)
            .AddMember(hasMatchingSetupMethod)
            .Build();
    }
}
