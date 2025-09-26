using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Contexts;

internal class ImposterGenerationContext
{
    internal readonly INamedTypeSymbol TargetSymbol;

    internal readonly ImposterType ImposterType;

    internal readonly IReadOnlyList<ImposterTargetMethod> Methods;

    internal readonly string Namespace;

    internal ImposterGenerationContext(INamedTypeSymbol targetSymbol)
    {
        TargetSymbol = targetSymbol;
        ImposterType = new ImposterType(targetSymbol);
        Methods = GetMethods(targetSymbol, new MethodUniqueNames());
        Namespace = $"Imposters.{TargetSymbol.ToDisplayString()}";
    }

    private static IReadOnlyList<ImposterTargetMethod> GetMethods(INamedTypeSymbol typeSymbol, MethodUniqueNames methodUniqueNames)
    {
        if (typeSymbol.TypeKind is TypeKind.Interface)
        {
            return typeSymbol
                .GetAllInterfaceMethods()
                .Select(method => new ImposterTargetMethod(method, methodUniqueNames.GetUniqueNameForMethod(method)))
                .ToList();
        }

        throw new InvalidOperationException("Only interfaces are supported");
        // TODO Add class support.
        /*
        if (typeSymbol.TypeKind is TypeKind.Class && typeSymbol.IsSealed is false)
        { }
    */
    }
}