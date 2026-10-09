using System.Collections.Generic;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;

internal readonly record struct MethodImposterGenericTypeMetadata(
    string Name,
    NameSyntax Syntax,
    NameSyntax SyntaxWithTargetGenericArguments
)
{
    public MethodImposterGenericTypeMetadata(
        string name,
        IReadOnlyList<NameSyntax> genericTypeArguments,
        IReadOnlyList<NameSyntax> targetGenericTypeArguments
    )
        : this(
            name,
            SyntaxFactoryHelper.WithMethodGenericArguments(genericTypeArguments, name),
            SyntaxFactoryHelper.WithMethodGenericArguments(targetGenericTypeArguments, name)
        ) { }
}
