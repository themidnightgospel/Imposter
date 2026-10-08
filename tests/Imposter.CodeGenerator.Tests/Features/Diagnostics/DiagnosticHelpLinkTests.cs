using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Microsoft.CodeAnalysis;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.Diagnostics;

public class DiagnosticHelpLinkTests
{
    private const string DiagnosticsPageUrl =
        "https://themidnightgospel.github.io/Imposter/latest/diagnostics/";

    private static readonly DiagnosticDescriptor[] Descriptors = typeof(DiagnosticDescriptors)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .Select(it => it.GetValue(null))
        .OfType<DiagnosticDescriptor>()
        .ToArray();

    [Fact]
    public void GivenEveryDiagnostic_WhenHelpLinkIsRead_ShouldPointToItsSectionOnTheDiagnosticsPage()
    {
        Descriptors
            .Select(it => it.HelpLinkUri)
            .ShouldBe(Descriptors.Select(it => DiagnosticsPageUrl + "#" + Anchor(it)));
    }

    [Fact]
    public void GivenEveryDiagnostic_WhenDiagnosticsPageIsRead_ShouldHaveSectionWithItsAnchor()
    {
        var page = File.ReadAllText(
            Path.Join(FindRepositoryRoot(), "docs", "content", "diagnostics.md")
        );

        Descriptors
            .Where(it => !page.Contains($"{{ #{Anchor(it)} }}", StringComparison.Ordinal))
            .Select(it => it.Id)
            .ShouldBeEmpty();
    }

    private static string Anchor(DiagnosticDescriptor descriptor) =>
        descriptor.Id.ToLowerInvariant();

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (!File.Exists(Path.Join(directory.FullName, "Imposter.slnx")))
        {
            directory =
                directory.Parent ?? throw new DirectoryNotFoundException("Imposter.slnx not found");
        }

        return directory.FullName;
    }
}
