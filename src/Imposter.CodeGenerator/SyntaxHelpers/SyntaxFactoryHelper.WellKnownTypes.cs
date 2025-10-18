using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static GenericNameSyntax ConcurrentStack(TypeSyntax itemType)
        => GenericName(Identifier("ConcurrentStack"), TypeArgumentList(SingletonSeparatedList(itemType)));
    
    public static GenericNameSyntax List(TypeSyntax itemTypeSyntax) =>
        GenericName(Identifier("List")).WithTypeArgumentList(TypeArgumentList(SingletonSeparatedList(itemTypeSyntax)));
}