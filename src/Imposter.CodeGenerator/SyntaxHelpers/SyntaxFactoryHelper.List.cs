using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    public static GenericNameSyntax List(TypeSyntax itemTypeSyntax) =>
        GenericName(Identifier("List")).WithTypeArgumentList(TypeArgumentList(SingletonSeparatedList(itemTypeSyntax)));
}