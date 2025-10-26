using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.Features.PropertySetup.Metadata;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Features.Shared;

internal readonly struct ImposterTargetMetadata
{
    internal readonly string Name;

    internal readonly INamedTypeSymbol TargetSymbol;

    internal readonly IReadOnlyList<ImposterTargetMethodMetadata> Methods;

    internal readonly IReadOnlyList<ImposterTargetPropertyMetadata> Properties;

    internal ImposterTargetMetadata(INamedTypeSymbol targetSymbol)
    {
        var symbolNameNamespace = new SymbolNameNamespace([]);
        
        TargetSymbol = targetSymbol;
        Name = targetSymbol.Name + "Imposter";
        Methods = GetMethods(targetSymbol, symbolNameNamespace);
        Properties = GetProperties(targetSymbol, symbolNameNamespace);
    }

    private static IReadOnlyList<ImposterTargetMethodMetadata> GetMethods(INamedTypeSymbol typeSymbol, SymbolNameNamespace symbolNameNamespace)
    {
        if (typeSymbol.TypeKind is TypeKind.Interface)
        {
            return typeSymbol
                .GetAllInterfaceMethods()
                .Select(method => new ImposterTargetMethodMetadata(method, symbolNameNamespace.Use(method.Name)))
                .ToList();
        }

        // TODO Add class support.
        throw new InvalidOperationException("Only interfaces are supported");
    }

    private static IReadOnlyList<ImposterTargetPropertyMetadata> GetProperties(INamedTypeSymbol typeSymbol, SymbolNameNamespace symbolNameNamespace)
    {
        if (typeSymbol.TypeKind is TypeKind.Interface)
        {
            return typeSymbol
                .GetAllInterfaceProperties()
                .Select(property => new ImposterTargetPropertyMetadata(property, symbolNameNamespace.Use(property.Name)))
                .ToList();
        }

        // TODO Add class support.
        throw new InvalidOperationException("Only interfaces are supported");
    }
}