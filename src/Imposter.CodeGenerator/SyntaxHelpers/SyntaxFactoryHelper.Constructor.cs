using System.Collections.Generic;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    // A public constructor that takes a parameter for each field and stores it.
    internal static ConstructorDeclarationSyntax BuildConstructorAndInitializeMembers(
        string className,
        IEnumerable<FieldDeclarationSyntax> fields
    ) =>
        new ConstructorWithFieldInitializationBuilder(className)
            .WithModifiers(Token(SyntaxKind.PublicKeyword))
            .AddParameters(fields)
            .Build();
}
