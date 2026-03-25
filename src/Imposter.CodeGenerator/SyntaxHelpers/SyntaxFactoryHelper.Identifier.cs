using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    /// <summary>
    /// Returns the name prefixed with '@' if it is a C# keyword, otherwise returns it unchanged.
    /// Use this when converting <see cref="Microsoft.CodeAnalysis.IParameterSymbol.Name"/> (which
    /// strips the '@') back into a token that must be valid as an identifier in generated code.
    /// </summary>
    internal static string EscapeKeyword(string name) =>
        SyntaxFacts.GetKeywordKind(name) != SyntaxKind.None ? "@" + name : name;
}
