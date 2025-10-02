using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxProviders;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Contexts;

internal class ImposterGenerationContext
{
    internal readonly GenerateImposterDeclaration GenerateImposterDeclaration;

    internal INamedTypeSymbol TargetSymbol => GenerateImposterDeclaration.ImposterTarget;

    internal readonly ImposterType ImposterType;

    internal readonly IReadOnlyList<ImposterTargetMethodMetadata> Methods;

    internal readonly string ImposterComponentsNamespace;

    internal ImposterGenerationContext(GenerateImposterDeclaration generateImposterDeclaration)
    {
        GenerateImposterDeclaration = generateImposterDeclaration;
        ImposterType = new ImposterType(generateImposterDeclaration.ImposterTarget);
        Methods = GetMethods(generateImposterDeclaration.ImposterTarget, new MethodUniqueNames());
        ImposterComponentsNamespace = $"Imposters.{TargetSymbol.ToDisplayString()}";
    }

    private static IReadOnlyList<ImposterTargetMethodMetadata> GetMethods(INamedTypeSymbol typeSymbol, MethodUniqueNames methodUniqueNames)
    {
        if (typeSymbol.TypeKind is TypeKind.Interface)
        {
            return typeSymbol
                .GetAllInterfaceMethods()
                .Select(method => new ImposterTargetMethodMetadata(method, methodUniqueNames.GetUniqueNameForMethod(method)))
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