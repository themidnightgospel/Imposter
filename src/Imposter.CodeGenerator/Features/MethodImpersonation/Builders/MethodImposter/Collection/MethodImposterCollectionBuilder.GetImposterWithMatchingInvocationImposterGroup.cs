using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Shared;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Collection;

internal static partial class MethodImposterCollectionBuilder
{
    private static MethodDeclarationSyntax BuildGetImposterWithMatchingInvocationImposterGroup(
        in ImposterTargetMethodMetadata method
    )
    {
        var hasMatchingMethod = method.MethodImposter.HasMatchingInvocationImposterGroupMethod;

        var localNames = new NameSet(
            method
                .Model.TypeParameters.Select(typeParameter => typeParameter.Name)
                .Concat(method.Parameters.InputParameterMetadata.Select(p => p.Name))
        );
        var storedImposterIdentifier = Identifier(localNames.Use("storedImposter"));
        var typedImposterName = IdentifierName(localNames.Use("typedImposter"));

        var methodBuilder = new MethodDeclarationBuilder(
            method.MethodImposter.GenericInterface.Syntax,
            MethodImposterCollectionMetadata.GetImposterWithMatchingInvocationImposterGroupMethodName
        )
            // Own the lookup snapshot. In particular, a predicate may mutate the caller's ref/in storage while
            // candidates are inspected; subsequent candidates must still see the originally supplied values.
            .WithParameterList(CapturedArgumentsBuilder.ValueParameters(method))
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithTypeParameters(method.GenericTypeParameterListSyntax)
            .AddConstraintClauses(method.GenericTypeConstraintClauses)
            .WithBody(
                Block(
                    ForEachStatement(
                        Var,
                        storedImposterIdentifier,
                        IdentifierName(MethodImposterCollectionMetadata.ImpostersFieldName),
                        Block(
                            LocalVariableDeclarationSyntax(
                                Var,
                                typedImposterName.Identifier.Text,
                                IdentifierName(storedImposterIdentifier)
                                    .Dot(
                                        GenericName(
                                            Identifier("As"),
                                            TypeArguments(method.GenericTypeArguments)
                                        )
                                    )
                                    .Call()
                            ),
                            IfStatement(
                                typedImposterName
                                    .IsNotNull()
                                    .And(
                                        typedImposterName
                                            .Dot(IdentifierName(hasMatchingMethod.Name))
                                            .Call(CapturedArgumentsBuilder.Arguments(method))
                                    ),
                                ReturnStatement(typedImposterName)
                            )
                        )
                    ),
                    // Without a matching configured setup, the invocation is served by a transient imposter that
                    // records history but is not stored, so unconfigured invocations do not accumulate.
                    ReturnStatement(NewMethodImposterExpression(method))
                )
            );

        return methodBuilder.Build();
    }
}
