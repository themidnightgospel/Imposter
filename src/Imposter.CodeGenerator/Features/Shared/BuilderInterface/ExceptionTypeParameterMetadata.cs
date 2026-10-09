using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Shared.BuilderInterface;

// The TException of a builder's generic Throws<TException>(): an Exception with a parameterless constructor, so the
// imposter can create the exception it throws.
internal readonly struct ExceptionTypeParameterMetadata
{
    internal const string PreferredName = "TException";

    internal readonly string Name;

    internal readonly TypeParameterListSyntax TypeParameterList;

    internal readonly TypeParameterConstraintClauseSyntax ConstraintClause;

    internal ExceptionTypeParameterMetadata(string name)
    {
        Name = name;
        TypeParameterList = TypeParameterList(SingletonSeparatedList(TypeParameter(name)));
        ConstraintClause = TypeParameterConstraintClause(name)
            .AddConstraints(
                TypeConstraint(WellKnownTypes.System.Exception),
                ConstructorConstraint()
            );
    }
}
