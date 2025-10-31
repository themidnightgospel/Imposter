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

    internal readonly IReadOnlyList<ImposterPropertyMetadata> Properties;

    internal ImposterTargetMetadata(INamedTypeSymbol targetSymbol)
    {
        var symbolNameNamespace = new NameSet([]);
        
        TargetSymbol = targetSymbol;
        Name = targetSymbol.Name + "Imposter";
        Methods = GetMethods(targetSymbol, symbolNameNamespace);
        Properties = GetProperties(targetSymbol, symbolNameNamespace);
    }

    private static IReadOnlyList<ImposterTargetMethodMetadata> GetMethods(INamedTypeSymbol typeSymbol, NameSet nameSet)
    {
        if (typeSymbol.TypeKind is TypeKind.Interface)
        {
            return typeSymbol
                .GetAllInterfaceMethods()
                .Select(method => new ImposterTargetMethodMetadata(method, nameSet.Use(method.Name)))
                .ToList();
        }

        // TODO Add class support.
        throw new InvalidOperationException("Only interfaces are supported");
    }

    private static IReadOnlyList<ImposterPropertyMetadata> GetProperties(INamedTypeSymbol typeSymbol, NameSet nameSet)
    {
        if (typeSymbol.TypeKind is TypeKind.Interface)
        {
            return typeSymbol
                .GetAllInterfaceProperties()
                .Select(property => new ImposterPropertyMetadata(property, nameSet.Use(property.Name)))
                .ToList();
        }

        // TODO Add class support.
        throw new InvalidOperationException("Only interfaces are supported");
    }
}