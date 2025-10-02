using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static InterfaceDeclarationBuilder InterfaceDeclarationBuilder(IMethodSymbol method, ITypeMetadata typeMetadata)
    {
        return new InterfaceDeclarationBuilder(typeMetadata.Name, TypeParameterList(method));
    }
    
    internal static ClassDeclarationBuilder ClassDeclarationBuilder(IMethodSymbol method, ITypeMetadata typeMetadata)
    {
        return new ClassDeclarationBuilder(typeMetadata.Name, TypeParameterList(method));
    }
}