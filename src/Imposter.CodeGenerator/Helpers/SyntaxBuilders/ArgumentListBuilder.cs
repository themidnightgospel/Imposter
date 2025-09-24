using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Helpers.SyntaxBuilders;

internal readonly struct ArgumentListBuilder
{
    private readonly List<ArgumentSyntax> _arguments = new();

    public ArgumentListBuilder()
    {
    }

    internal ArgumentListBuilder AddArgument(ArgumentSyntax argumentSyntax)
    {
        _arguments.Add(argumentSyntax);
        return this;
    }

    internal ArgumentListBuilder AddArgumentIf(bool condition, Func<ArgumentSyntax> argumentGenerator)
    {
        if (condition)
        {
            _arguments.Add(argumentGenerator());
        }

        return this;
    }

    internal ArgumentListSyntax Build() => ArgumentList(SeparatedList(_arguments));
}