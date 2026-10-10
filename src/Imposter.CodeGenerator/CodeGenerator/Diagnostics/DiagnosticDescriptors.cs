using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator.Diagnostics;

public static class DiagnosticDescriptors
{
    // Each diagnostic links to its own section, whose anchor is the lower-case diagnostic ID.
    private const string HelpUrl =
        "https://themidnightgospel.github.io/Imposter/latest/diagnostics/";

    public static readonly DiagnosticDescriptor InvalidImposterTarget = new(
        "IMP002",
        "Imposter target must be an interface or non-sealed class",
        "Apply [GenerateImposter] to an interface or make '{0}' non-sealed (currently a '{1}')",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true,
        description: "Unsupported target type. Imposters can target interfaces or extensible classes only.",
        helpLinkUri: HelpUrl + "#imp002"
    );

    public static readonly DiagnosticDescriptor NotSupportedCSharpVersion = new(
        "IMP003",
        "C# version not supported",
        "Current C# version '{0}' is not supported, version '{1}' or higher is required",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true,
        description: "The generator relies on C# 9.0 features, so it generates no imposters for older language versions.",
        helpLinkUri: HelpUrl + "#imp003"
    );

    public static readonly DiagnosticDescriptor ImposterTargetMustHaveAccessibleConstructor = new(
        "IMP004",
        "Imposter target must expose an accessible constructor",
        "Declare a public, internal, or protected constructor on '{0}' that the generated imposter can call",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true,
        description: "Accessible constructor required so generated imposters can instantiate the target.",
        helpLinkUri: HelpUrl + "#imp004"
    );

    public static readonly DiagnosticDescriptor ClosedGenericImposterTarget = new(
        "IMP006",
        "Closed generic imposter target",
        "Register the open generic type 'typeof({1})' instead of '{0}'; the generated imposter keeps the type parameters and ignores these type arguments",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Warning,
        true,
        description: "A closed generic target generates the same generic imposter as its open type, with type parameters that have no effect.",
        helpLinkUri: HelpUrl + "#imp006"
    );

    public static readonly DiagnosticDescriptor ImposterTypeNameCollision = new(
        "IMP007",
        "Imposter type name collision",
        "The imposters of '{0}' and '{1}' would both be '{2}'; register one of them with putInTheSameNamespace: false to generate it in its own namespace",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true,
        description: "Two registered targets, such as same-named types nested in different classes, would generate the same imposter type, so neither is generated.",
        helpLinkUri: HelpUrl + "#imp007"
    );

    public static readonly DiagnosticDescriptor ImposterTargetHasUnoverridableAbstractMember = new(
        "IMP008",
        "Imposter target has abstract members the project cannot override",
        "'{0}' has the abstract member '{1}', which this project cannot override, so only its own assembly or one it grants InternalsVisibleTo can derive from it",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true,
        description: "An imposter derives from its target class, so it must override every abstract member, which an internal or private protected member of another assembly does not allow.",
        helpLinkUri: HelpUrl + "#imp008"
    );

    public static readonly DiagnosticDescriptor ImposterTargetHasRefLikeMember = new(
        "IMP009",
        "Imposter target has a member with a ref-like type",
        "'{0}' has the member '{1}', whose signature uses the ref-like type '{2}', which an imposter cannot store or match",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true,
        description: "An imposter keeps arguments and results in fields, delegates and argument matchers, none of which can hold a ref-like value. It keeps Span<T> and ReadOnlySpan<T> parameters, results, property values, indexer keys and values, and event arguments as arrays of their elements, except a span returned by reference, and a span an async event takes by ref, out or ref readonly. Any other ref struct, or a type parameter that allows ref structs, still means no imposter is generated.",
        helpLinkUri: HelpUrl + "#imp009"
    );

    public static readonly DiagnosticDescriptor ImposterTargetRequiredMembersNeedSetsRequiredMembers =
        new(
            "IMP010",
            "Imposter target has required members, and SetsRequiredMembersAttribute is missing",
            "'{0}' has required members, which its imposter can only leave unset with System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute; target .NET 7 or later, or declare the attribute in this project",
            DiagnosticCategories.Imposter,
            DiagnosticSeverity.Error,
            true,
            description: "An imposter creates its instance without setting the target's required members, which its constructors allow with SetsRequiredMembersAttribute. The attribute is built into .NET 7 and later, so a project on an older framework has to declare it, and no imposter is generated without it.",
            helpLinkUri: HelpUrl + "#imp010"
        );

    public static readonly DiagnosticDescriptor ImposterTargetHasRefReturningMember = new(
        "IMP011",
        "Imposter target has a member that returns by reference",
        "'{0}' has the member '{1}', which returns by reference, so an imposter cannot impersonate it",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true,
        description: "An imposter returns the results it is set up with by value, so it cannot implement or override a method, property or indexer that returns by ref or ref readonly, and no imposter is generated.",
        helpLinkUri: HelpUrl + "#imp011"
    );

    public static readonly DiagnosticDescriptor ImposterTargetHasStaticAbstractMember = new(
        "IMP012",
        "Imposter target has a static abstract member",
        "'{0}' has the static abstract member '{1}', so it can't be the type argument its imposter needs",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true,
        description: "An interface whose static abstract member has no implementation in the interface can't be a type argument, and its imposter passes it as one, so no imposter is generated.",
        helpLinkUri: HelpUrl + "#imp012"
    );

    public static readonly DiagnosticDescriptor GeneratorCrash = new(
        "IMP005",
        "Generator crash",
        "Unhandled exception while generating imposters: '{0}'",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true,
        description: "An unexpected exception bubbled out of the source generator.",
        helpLinkUri: HelpUrl + "#imp005"
    );

    public static readonly DiagnosticDescriptor GeneratorLog = new(
        "IMPLOG001",
        "Imposter generator log",
        "{0}",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Info,
        true,
        description: "Generator log message, written when the IMPOSTER_LOG MSBuild property is true.",
        helpLinkUri: HelpUrl + "#implog001"
    );
}
