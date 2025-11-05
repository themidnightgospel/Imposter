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

    internal ReturnTypeMetadata(ITypeSymbol returnTypeSymbol)
    {
        var isTask = false;
        var isGenericTask = false;
        var isValueTask = false;
        var isGenericValueTask = false;
        var supportsAsyncValueResult = false;
        TypeSyntax? asyncValueTypeSyntax = null;

        if (returnTypeSymbol is INamedTypeSymbol namedType)
        {
            var displayString = namedType.ToDisplayString();
            isTask = displayString == "System.Threading.Tasks.Task";
            isValueTask = displayString == "System.Threading.Tasks.ValueTask";

            if (namedType.IsGenericType)
            {
                var constructedFrom = namedType.ConstructedFrom.ToDisplayString();
                isGenericTask = constructedFrom == "System.Threading.Tasks.Task<TResult>";
                isGenericValueTask = constructedFrom == "System.Threading.Tasks.ValueTask<TResult>";

                if (isGenericTask)
                {
                    isTask = true;
                }

                if (isGenericValueTask)
                {
                    isValueTask = true;
                }

                if (isGenericTask || isGenericValueTask)
                {
                    supportsAsyncValueResult = true;
                    asyncValueTypeSyntax = SyntaxFactoryHelper.TypeSyntax(namedType.TypeArguments[0]);
                }
            }
        }

        IsTask = isTask;
        IsGenericTask = isGenericTask;
        IsValueTask = isValueTask;
        IsGenericValueTask = isGenericValueTask;
        SupportsAsyncValueResult = supportsAsyncValueResult;
        AsyncValueTypeSyntax = asyncValueTypeSyntax;
    }
}
