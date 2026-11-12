using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter.ImposterInstance;

// Own generic history at the caller boundary, before setup selection can run user predicates.
// The same boundary records calls dispatched through a compatible-type adapter without another node.
internal static class GenericMethodHistoryBuilder
{
    internal static BlockSyntax Build(
        in ImposterTargetMethodMetadata method,
        ExpressionSyntax collection,
        IReadOnlyList<ArgumentSyntax> invokeArguments,
        string imposterField
    )
    {
        var names = new NameSet(
            method
                .Model.Parameters.Select(parameter => parameter.Name)
                .Concat(method.Model.TypeParameters.Select(parameter => parameter.Name))
                .Concat(method.Model.ContainingTypeTypeParameterNames)
        );
        var sequence = IdentifierName(names.Use("invocationSequence"));
        var failure = IdentifierName(names.Use("invocationException"));
        var caught = IdentifierName(names.Use("caughtInvocationException"));
        var result = IdentifierName(names.Use("invocationResult"));
        var returned = IdentifierName(names.Use("returnedValue"));
        var captures = method.Parameters.InputParameterMetadata.ToDictionary(
            parameter => parameter.Name,
            parameter => names.Use("captured" + parameter.Name.TrimStart('@'))
        );
        var body = new List<StatementSyntax>
        {
            LocalVariableDeclarationSyntax(
                WellKnownTypes.Long,
                sequence.Identifier.Text,
                WellKnownTypes
                    .Imposter.Abstractions.InvocationSequence.Dot(IdentifierName("Next"))
                    .Call()
            ),
            LocalVariableDeclarationSyntax(
                WellKnownTypes.System.Exception.ToNullableType(),
                failure.Identifier.Text,
                Null
            ),
        };
        foreach (var parameter in method.Parameters.InputParameterMetadata)
            body.Add(
                LocalVariableDeclarationSyntax(
                    parameter.NullableAwareKeptTypeSyntax,
                    captures[parameter.Name],
                    parameter.KeptValue
                )
            );
        foreach (var parameter in method.Parameters.OutputParameters)
            body.Add(
                IdentifierName(EscapeKeyword(parameter.Name))
                    .Assign(DefaultNonNullable)
                    .ToStatementSyntax()
            );
        if (method.KeepsResult)
            body.Add(
                LocalVariableDeclarationSyntax(
                    method.ReturnType.KeptTypeSyntax,
                    result.Identifier.Text,
                    DefaultNonNullable
                )
            );

        var lookup = collection
            .Dot(
                method.WithGenericArguments(
                    MethodImposterCollectionMetadata.GetImposterWithMatchingInvocationImposterGroupMethodName
                )
            )
            .Call(
                ArgumentListSyntax(
                    method.Parameters.InputParameterMetadata.Select(parameter =>
                        Argument(parameter.KeptValue)
                    )
                )
            );
        var invocation = BuildDispatch(
            method,
            collection,
            lookup,
            invokeArguments,
            names,
            result,
            returned
        );
        var recordArguments = new List<ArgumentSyntax>();
        foreach (var parameter in method.Parameters.MatchedParameterMetadata)
            recordArguments.Add(
                Argument(
                    parameter.Model.RefKind == RefKind.Out
                        ? parameter.KeptValue
                        : IdentifierName(captures[parameter.Name])
                )
            );
        if (method.KeepsResult)
            recordArguments.Add(Argument(result));
        recordArguments.Add(Argument(failure));
        recordArguments.Add(Argument(sequence));
        body.Add(
            TryStatement(
                Block(invocation),
                SingletonList(
                    CatchClause(
                        CatchDeclaration(WellKnownTypes.System.Exception, caught.Identifier),
                        null,
                        Block(failure.Assign(caught).ToStatementSyntax(), ThrowStatement())
                    )
                ),
                FinallyClause(
                    Block(
                        IdentifierName(imposterField)
                            .Dot(IdentifierName(method.InvocationHistory.Collection.AsField.Name))
                            .Dot(IdentifierName(InvocationHistoryCollectionMetadata.AddMethodName))
                            .Call(
                                Argument(
                                    method.InvocationHistory.Syntax.New(
                                        ArgumentListSyntax(recordArguments)
                                    )
                                )
                            )
                            .ToStatementSyntax()
                    )
                )
            )
        );
        return Block(body);
    }

    private static List<StatementSyntax> BuildDispatch(
        in ImposterTargetMethodMetadata method,
        ExpressionSyntax collection,
        InvocationExpressionSyntax lookup,
        IReadOnlyList<ArgumentSyntax> invokeArguments,
        NameSet names,
        IdentifierNameSyntax result,
        IdentifierNameSyntax returned
    )
    {
        var invoke = lookup
            .Dot(IdentifierName(MethodImposterInvokeMethodMetadata.Name))
            .Call(invokeArguments);
        if (!method.Parameters.InputParameterMetadata.Any(p => p.IsSpan))
            return InvokeAndCaptureResult(method, invoke, result, returned);

        // Reuse only arrays that have never been exposed to arbitrary user predicates.
        // The separate entry-history capture remains private throughout both matcher passes.
        var lookupNames = new Dictionary<string, string>();
        var statements = new List<StatementSyntax>();
        var arguments = new List<ArgumentSyntax>();
        foreach (var parameter in method.Parameters.InputParameterMetadata)
        {
            var local = names.Use("lookup" + parameter.Name.TrimStart('@'));
            lookupNames.Add(parameter.Name, local);
            statements.Add(
                LocalVariableDeclarationSyntax(
                    parameter.NullableAwareKeptTypeSyntax,
                    local,
                    parameter.KeptValue
                )
            );
            arguments.Add(Argument(IdentifierName(local)));
        }
        var selected = IdentifierName(names.Use("selectedImposter"));
        var exact = IdentifierName(names.Use("exactImposter"));
        statements.Add(
            LocalVariableDeclarationSyntax(
                Var,
                selected.Identifier.Text,
                lookup.WithArgumentList(ArgumentListSyntax(arguments))
            )
        );
        var reuseArguments = new List<ArgumentSyntax>();
        foreach (var parameter in method.Parameters.InputParameterMetadata.Where(p => p.IsSpan))
            reuseArguments.Add(Argument(IdentifierName(lookupNames[parameter.Name])));
        reuseArguments.AddRange(invokeArguments);
        var reusedCall = (method.CanReuseCapturedSpansAcrossAdapters ? selected : exact)
            .Dot(IdentifierName(method.MethodImposter.InvokeWithCapturedSpansMethodName))
            .Call(reuseArguments);
        var normalCall = selected
            .Dot(IdentifierName(MethodImposterInvokeMethodMetadata.Name))
            .Call(invokeArguments);
        ExpressionSyntax canReuse = collection.Dot(
            IdentifierName(MethodImposterCollectionMetadata.CanReuseCapturedSpansFieldName)
        );
        if (!method.CanReuseCapturedSpansAcrossAdapters)
            canReuse = canReuse.And(
                IsPatternExpression(
                    selected,
                    DeclarationPattern(
                        method.MethodImposter.Syntax,
                        SingleVariableDesignation(exact.Identifier)
                    )
                )
            );
        statements.Add(
            IfStatement(
                canReuse,
                Block(InvokeAndCaptureResult(method, reusedCall, result, returned)),
                ElseClause(Block(InvokeAndCaptureResult(method, normalCall, result, returned)))
            )
        );
        return statements;
    }

    private static List<StatementSyntax> InvokeAndCaptureResult(
        in ImposterTargetMethodMetadata method,
        ExpressionSyntax invoke,
        IdentifierNameSyntax result,
        IdentifierNameSyntax returned
    )
    {
        if (!method.HasReturnValue)
            return [invoke.ToStatementSyntax()];
        if (!method.KeepsResult)
            return [ReturnStatement(invoke)];
        return
        [
            LocalVariableDeclarationSyntax(Var, returned.Identifier.Text, invoke),
            result.Assign(method.ReturnType.KeptValue(returned)).ToStatementSyntax(),
            ReturnStatement(returned),
        ];
    }
}
