using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodSetup.Metadata;

internal interface IFieldMetadata
{
    string Name { get; }
    
    TypeSyntax Type { get; }
}

internal readonly record struct FieldMetadata(string Name, TypeSyntax Type) : IFieldMetadata
{
}