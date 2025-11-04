using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.MethodImposter;

internal readonly record struct MethodImposterGenericTypeMetadata(string Name, NameSyntax Syntax, NameSyntax SyntaxWithTargetGenericArguments)
{
    public MethodImposterGenericTypeMetadata(
        string name,
        IReadOnlyList<NameSyntax> genericTypeArguments,
        IReadOnlyList<NameSyntax> targetGenericTypeArguments)
        : this(name, GetNameSyntax(name, genericTypeArguments), GetNameSyntax(name, targetGenericTypeArguments))
    {
    }

    private static NameSyntax GetNameSyntax(string name, IReadOnlyList<NameSyntax> genericTypeArguments) =>
        genericTypeArguments.Count > 0
            ? SyntaxFactory.GenericName(SyntaxFactory.Identifier(name),
                SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList<TypeSyntax>(genericTypeArguments)))
            : SyntaxFactory.IdentifierName(name);
}
