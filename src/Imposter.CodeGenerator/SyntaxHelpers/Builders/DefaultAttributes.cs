using System.Collections.Generic;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal static class DefaultAttributes
{
    private const string GeneratedCodeAttributeName =
        "global::System.CodeDom.Compiler.GeneratedCode";
    private static readonly AssemblyName ImposterGeneratorAssembly =
        typeof(SyntaxFactoryHelper).Assembly.GetName();

    internal static readonly AttributeListSyntax SetsRequiredMembersAttribute = AttributeList(
        SingletonSeparatedList(
            Attribute(ParseName("global::System.Diagnostics.CodeAnalysis.SetsRequiredMembers"))
        )
    );

    private static readonly AttributeListSyntax GeneratedCodeAttribute = AttributeList(
        SingletonSeparatedList(
            Attribute(IdentifierName(GeneratedCodeAttributeName))
                .WithArgumentList(
                    AttributeArgumentList(
                        SeparatedList<AttributeArgumentSyntax>(
                            new SyntaxNodeOrToken[]
                            {
                                AttributeArgument(ImposterGeneratorAssembly.Name.StringLiteral()),
                                Token(SyntaxKind.CommaToken),
                                AttributeArgument(
                                    ImposterGeneratorAssembly.Version.ToString().StringLiteral()
                                ),
                            }
                        )
                    )
                )
        )
    );

    // Static fields initialize in declaration order, so this one stays below GeneratedCodeAttribute.
    internal static readonly IReadOnlyList<AttributeListSyntax> DefaultTypeAttributes =
    [
        GeneratedCodeAttribute,
    ];
}
