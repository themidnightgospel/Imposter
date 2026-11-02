#if ROSLYN4_4_OR_GREATER
using System.Linq;
using System.Threading;
using Imposter.Abstractions;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;

internal static partial class GenerateImposterDeclarationsProvider
{
    private static readonly string GenerateImposterAttribute =
        typeof(GenerateImposterAttribute).FullName!;

    // The transform runs again after every edit, but it produces symbol-free declarations that compare by value, so
    // the outputs of unchanged declarations are reused.
    internal static IncrementalValuesProvider<GenerateImposterDeclaration> GetGenerateImposterDeclarations(
        this in IncrementalGeneratorInitializationContext context
    )
    {
        return context
            .SyntaxProvider.ForAttributeWithMetadataName(
                GenerateImposterAttribute,
                predicate: static (_, _) => true,
                transform: static (ctx, token) => GetDeclarations(ctx, token)
            )
            .SelectMany(static (declarations, _) => declarations)
            .Collect()
            .SelectMany(
                static (declarations, _) =>
                    ImposterTypeCollisions.Mark(declarations.Distinct().ToArray())
            )
            .WithTrackingName("GenerateImposterDeclarations");
    }

    private static EquatableArray<GenerateImposterDeclaration> GetDeclarations(
        in GeneratorAttributeSyntaxContext context,
        in CancellationToken token
    )
    {
        token.ThrowIfCancellationRequested();

        var memberAccess = new MemberAccess(context.SemanticModel.Compilation.Assembly);

        return context
            .Attributes.Select(attribute =>
                (Attribute: attribute, Target: RegisteredTarget(attribute))
            )
            .Where(it => it.Target is not null)
            .Select(it =>
                GenerateImposterDeclaration.From(
                    it.Target!,
                    PutInTheSameNamespace(it.Attribute),
                    memberAccess
                )
            )
            .Distinct()
            .ToEquatableArray();
    }
}
#endif
