using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.ImposterTargetMethod;

internal readonly struct ReturnTypeMetadata
{
    internal readonly bool IsTask;

    internal readonly bool IsGenericTask;

    internal readonly bool IsValueTask;

    internal readonly bool IsGenericValueTask;

    internal ReturnTypeMetadata(ITypeSymbol returnTypeSymbol)
    {
        var isTask = false;
        var isGenericTask = false;
        var isValueTask = false;
        var isGenericValueTask = false;

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
            }
        }

        IsTask = isTask;
        IsGenericTask = isGenericTask;
        IsValueTask = isValueTask;
        IsGenericValueTask = isGenericValueTask;
    }
}
