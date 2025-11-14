using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.ImposterTargetMethod;

internal readonly struct ReturnTypeMetadata
{
    internal readonly bool IsTask;

    internal readonly bool IsGenericTask;

    internal readonly bool IsValueTask;

    internal readonly bool IsGenericValueTask;

    internal readonly bool SupportsAsyncValueResult;

    internal readonly TypeSyntax? AsyncValueTypeSyntax;

    internal readonly TypeSymbolMetadata TypeSymbolMetadata;

    internal ReturnTypeMetadata(ITypeSymbol returnTypeSymbol, bool supportsNullableGenericType)
    {
        var taskLikeMetadata = returnTypeSymbol.GetTaskLikeMetadata();

        IsTask = taskLikeMetadata.IsTask;
        IsGenericTask = taskLikeMetadata.IsGenericTask;
        IsValueTask = taskLikeMetadata.IsValueTask;
        IsGenericValueTask = taskLikeMetadata.IsGenericValueTask;
        SupportsAsyncValueResult = taskLikeMetadata.SupportsAsyncValueResult;
        AsyncValueTypeSyntax = taskLikeMetadata.AsyncValueTypeSymbol is null
            ? null
            : SyntaxFactoryHelper.TypeSyntax(taskLikeMetadata.AsyncValueTypeSymbol);
        TypeSymbolMetadata = returnTypeSymbol.GetTypeSymbolMetadata(supportsNullableGenericType);
    }
}
