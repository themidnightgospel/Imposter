using System.Collections.Generic;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata;

internal static class TypeMetadataFactory
{
    internal static TypeMetadata Create(string typeName, IReadOnlyList<NameSyntax> genericArguments) =>
        new(typeName, SyntaxFactoryHelper.WithMethodGenericArguments(genericArguments, typeName));

}