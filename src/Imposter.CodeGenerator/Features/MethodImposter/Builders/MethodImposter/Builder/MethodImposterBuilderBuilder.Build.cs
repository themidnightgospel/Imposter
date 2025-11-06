using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterTargetMethodMetadata method)
    {
        var fields = GetFields(method).ToArray();

        var builderClass = new ClassDeclarationBuilder(method.MethodImposter.Builder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(method.MethodImposter.BuilderInterface.Syntax))
            .AddMembers(fields)
            .AddMember(SinglePrivateReadonlyVariableField(method.MethodImposter.Builder.InvocationImposterGroupField.Type, method.MethodImposter.Builder.InvocationImposterGroupField.Name))
            .AddMember(SingleVariableField(
                method.MethodImposter.Builder.CurrentInvocationImposterField.Type,
                method.MethodImposter.Builder.CurrentInvocationImposterField.Name,
                TokenList(Token(SyntaxKind.PrivateKeyword))));

        var constructor = BuildConstructorAndInitializeMembers(method.MethodImposter.Builder.Name, fields);
        constructor = constructor.WithBody(
            constructor.Body!.AddStatements(BuildInvocationSetupInitializationStatements(method).ToArray()));

        return builderClass
            .AddMember(constructor)
            .AddMembers(ImplementInvocationSetupBuilderInterface(method))
            .AddMember(BuildCalledMethod(method))
            .Build();
    }
}
