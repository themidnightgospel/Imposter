using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;

internal readonly struct ReturnTypeMetadata
{
    internal readonly TypeSyntax? GenericAwaitableResultType;

    internal readonly TypeSymbolMetadata TypeSymbolMetadata;

    internal readonly TaskLikeMetadata TaskLikeMetadata;

    internal ReturnTypeMetadata(ITypeSymbol returnTypeSymbol, bool supportsNullableGenericType)
    {
        TaskLikeMetadata = returnTypeSymbol.GetTaskLikeMetadata();

        GenericAwaitableResultType = TaskLikeMetadata.GenericAwaitableResultType is null
            ? null
            : SyntaxFactoryHelper.TypeSyntax(TaskLikeMetadata.GenericAwaitableResultType);

        TypeSymbolMetadata = returnTypeSymbol.GetTypeSymbolMetadata(
            TaskLikeMetadata.IsAwaitable,
            supportsNullableGenericType
        );
    }
}
