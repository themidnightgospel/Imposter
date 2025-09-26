using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    private const string GeneratedCodeAttributeName = "global::System.CodeDom.Compiler.GeneratedCode";
    private static readonly AssemblyName ImposterGeneratorAssembly = typeof(SyntaxFactoryHelper).Assembly.GetName();

    internal static AttributeListSyntax GeneratedCodeAttribute() =>
        AttributeList(
            SingletonSeparatedList(
                Attribute(IdentifierName(GeneratedCodeAttributeName))
                    .WithArgumentList(
                        AttributeArgumentList(
                            SeparatedList<AttributeArgumentSyntax>(
                                new SyntaxNodeOrToken[]
                                {
                                    AttributeArgument(
                                        LiteralExpression(
                                            SyntaxKind.StringLiteralExpression,
                                            SyntaxFactory.Literal(ImposterGeneratorAssembly.Name)
                                        )
                                    ),
                                    Token(SyntaxKind.CommaToken),
                                    AttributeArgument(
                                        LiteralExpression(
                                            SyntaxKind.StringLiteralExpression,
                                            SyntaxFactory.Literal(ImposterGeneratorAssembly.Version.ToString())
                                        )
                                    )
                                }
                            )
                        )
                    )
            )
        );

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