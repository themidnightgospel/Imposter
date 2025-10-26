using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    private const string MethodImplAttributeName = "global::System.Runtime.CompilerServices.MethodImpl";
    private const string MethodImplOptionsEnumName = "global::System.Runtime.CompilerServices.MethodImplOptions";

    internal static AttributeListSyntax MethodImplAggressiveInliningAttribute() =>
        AttributeList(
            SingletonSeparatedList(
                Attribute(IdentifierName(MethodImplAttributeName))
                    .WithArgumentList(
                        AttributeArgumentList(
                            SingletonSeparatedList(
                                AttributeArgument(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName(MethodImplOptionsEnumName),
                                        IdentifierName("AggressiveInlining")
                                    )
                                )
                            )
                        )
                    )
            )
        );
}