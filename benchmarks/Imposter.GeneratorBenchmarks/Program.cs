using System.Diagnostics;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using Imposter.Abstractions;
using Imposter.CodeGenerator.CodeGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

if (args.Length != 1)
{
    Console.Error.WriteLine("Usage: Imposter.GeneratorBenchmarks <new-output-directory>");
    return 1;
}

var output = Path.GetFullPath(args[0]);
if (Directory.Exists(output))
{
    Console.Error.WriteLine($"Output directory already exists: {output}");
    return 1;
}

Directory.CreateDirectory(output);
var parseOptions = new CSharpParseOptions(LanguageVersion.Preview);
var references = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!)
    .Split(Path.PathSeparator)
    .Append(typeof(GenerateImposterAttribute).Assembly.Location)
    .Distinct(StringComparer.Ordinal)
    .Select(path => MetadataReference.CreateFromFile(path))
    .ToArray();
Console.WriteLine(
    $"{RuntimeInformation.FrameworkDescription}; {RuntimeInformation.OSDescription}; {RuntimeInformation.ProcessArchitecture}"
);
Console.WriteLine(
    $"Roslyn {typeof(CSharpCompilation).Assembly.GetName().Version}; 3 warmups, median of 15 runs; MB = 1,000,000 bytes"
);
Console.WriteLine("Parse and bind+emit: 1 warmup, median of 5 runs, Release optimization");
var measurements = new List<string> { "Scenario,MedianBytes,MedianMilliseconds" };
var compilationMeasurements = new List<string>
{
    "Scenario,GeneratedCharacters,GeneratedLines,MedianParseMilliseconds,MedianBindEmitMilliseconds,AssemblyBytes,MethodILBytes",
};

foreach (var generic in new[] { false, true })
{
    var name = generic ? "generic" : "plain";
    var targets = CSharpSyntaxTree.ParseText(Workload(generic), parseOptions, "Targets.cs");
    var unrelated = CSharpSyntaxTree.ParseText(
        "class Unrelated { int Value => 0; }",
        parseOptions,
        "Unrelated.cs"
    );
    CSharpCompilation CreateCompilation() =>
        CSharpCompilation.Create(
            "Benchmark",
            [targets, unrelated],
            references,
            new CSharpCompilationOptions(
                OutputKind.DynamicallyLinkedLibrary,
                optimizationLevel: OptimizationLevel.Release
            )
        );

    Measure(
        $"{name}-cold",
        () =>
        {
            // A fresh compilation also gives the rename cache fresh symbols on every run.
            var compilation = CreateCompilation();
            return () => NewDriver().RunGenerators(compilation);
        }
    );

    var current = CreateCompilation();
    var driver = NewDriver().RunGenerators(current);
    SaveSources(name, driver, current);
    MeasureCompilation(name, driver, current);
    MeasureCompilation($"{name}-without-generated", driver, current, includeGenerated: false);
    var edit = 0;
    Measure(
        $"{name}-edit",
        () =>
        {
            var next = CSharpSyntaxTree.ParseText(
                $"class Unrelated {{ int Value => {++edit}; }}",
                parseOptions,
                "Unrelated.cs"
            );
            current = current.ReplaceSyntaxTree(unrelated, next);
            unrelated = next;
            return () => driver = driver.RunGenerators(current);
        }
    );
    SaveSources($"{name}-edited", driver, current);
}

foreach (
    var (name, members) in new[]
    {
        ("single-property", "int Value { get; set; }"),
        ("single-method", "int Square(int value);"),
    }
)
{
    var compilation = CSharpCompilation.Create(
        name,
        [
            CSharpSyntaxTree.ParseText(
                "using Imposter.Abstractions; [assembly: GenerateImposter(typeof(ITarget))] public interface ITarget { "
                    + members
                    + " }",
                parseOptions
            ),
        ],
        references,
        new CSharpCompilationOptions(
            OutputKind.DynamicallyLinkedLibrary,
            optimizationLevel: OptimizationLevel.Release
        )
    );
    var driver = NewDriver().RunGenerators(compilation);
    SaveSources(name, driver, compilation);
    MeasureCompilation(name, driver, compilation);
}

foreach (var probe in RenameProbes.All)
{
    var compilation = CSharpCompilation.Create(
        probe.Name,
        [CSharpSyntaxTree.ParseText(probe.Source, parseOptions, "Probe.cs")],
        references,
        new CSharpCompilationOptions(
            OutputKind.DynamicallyLinkedLibrary,
            optimizationLevel: OptimizationLevel.Release
        )
    );
    SaveSources(probe.Name, NewDriver().RunGenerators(compilation), compilation);
    Console.WriteLine($"Output probe: {probe.Name}");
}

File.WriteAllLines(Path.Combine(output, "measurements.csv"), measurements);
File.WriteAllLines(Path.Combine(output, "compilation.csv"), compilationMeasurements);
return 0;

GeneratorDriver NewDriver() =>
    CSharpGeneratorDriver.Create(
        [new ImposterGenerator().AsSourceGenerator()],
        parseOptions: parseOptions
    );

void Measure(string name, Func<Func<GeneratorDriver>> prepare)
{
    var bytes = new List<long>();
    var milliseconds = new List<double>();
    for (var run = -3; run < 15; run++)
    {
        // Parsing the edit and constructing the compilation are outside the generator measurement.
        var generate = prepare();
        var before = GC.GetAllocatedBytesForCurrentThread();
        var start = Stopwatch.GetTimestamp();
        var result = generate();
        var elapsed = Stopwatch.GetElapsedTime(start).TotalMilliseconds;
        var allocated = GC.GetAllocatedBytesForCurrentThread() - before;
        AssertSuccess(result);
        if (run >= 0)
        {
            bytes.Add(allocated);
            milliseconds.Add(elapsed);
        }
    }

    bytes.Sort();
    milliseconds.Sort();
    Console.WriteLine(
        $"{name}: {bytes[7] / 1_000_000.0:F3} MB, {milliseconds[7]:F2} ms ({bytes[0]}..{bytes[^1]} bytes)"
    );
    measurements.Add(FormattableString.Invariant($"{name},{bytes[7]},{milliseconds[7]:F3}"));
}

void SaveSources(string name, GeneratorDriver driver, Compilation compilation)
{
    AssertSuccess(driver);
    var sources = driver.GetRunResult().Results.Single().GeneratedSources;
    if (sources.IsEmpty)
        throw new InvalidOperationException($"No sources generated for {name}");
    var diagnostics = compilation
        .AddSyntaxTrees(sources.Select(source => source.SyntaxTree))
        .GetDiagnostics()
        .Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
        .ToArray();
    if (diagnostics.Length != 0)
        throw new InvalidOperationException(
            string.Join(Environment.NewLine, diagnostics.AsEnumerable())
        );

    var directory = Path.Combine(output, "sources", name);
    Directory.CreateDirectory(directory);
    foreach (var source in sources)
        File.WriteAllText(
            Path.Combine(directory, source.HintName),
            source.SourceText.ToString(),
            new UTF8Encoding(false)
        );
}

// Reparse output text and create a fresh compilation for each sample: generator syntax trees and
// SaveSources' bound compilations must not warm the parse or binding measurements.
void MeasureCompilation(
    string name,
    GeneratorDriver driver,
    Compilation compilation,
    bool includeGenerated = true
)
{
    var sources = driver.GetRunResult().Results.Single().GeneratedSources;
    var texts = includeGenerated
        ? sources.Select(source => source.SourceText.ToString()).ToArray()
        : [];
    var parseTimes = new List<double>();
    var emitTimes = new List<double>();
    long assemblyBytes = 0;
    long ilBytes = 0;
    for (var run = -1; run < 5; run++)
    {
        var start = Stopwatch.GetTimestamp();
        var trees = texts.Select(text => CSharpSyntaxTree.ParseText(text, parseOptions)).ToArray();
        var parseTime = Stopwatch.GetElapsedTime(start).TotalMilliseconds;
        var fresh = CSharpCompilation.Create(
            compilation.AssemblyName!,
            compilation.SyntaxTrees.Concat(trees),
            references,
            (CSharpCompilationOptions)compilation.Options
        );
        using var stream = new MemoryStream();
        start = Stopwatch.GetTimestamp();
        var result = fresh.Emit(stream);
        var emitTime = Stopwatch.GetElapsedTime(start).TotalMilliseconds;
        if (!result.Success)
            throw new InvalidOperationException(
                string.Join(Environment.NewLine, result.Diagnostics)
            );
        assemblyBytes = stream.Length;
        stream.Position = 0;
        using var pe = new PEReader(stream);
        var metadata = pe.GetMetadataReader();
        ilBytes = metadata
            .MethodDefinitions.Select(handle =>
                metadata.GetMethodDefinition(handle).RelativeVirtualAddress
            )
            .Where(address => address != 0)
            .Sum(address => (long)pe.GetMethodBody(address).GetILBytes()!.Length);
        if (run >= 0)
        {
            parseTimes.Add(parseTime);
            emitTimes.Add(emitTime);
        }
    }
    parseTimes.Sort();
    emitTimes.Sort();
    var characters = texts.Sum(text => (long)text.Length);
    var lines = includeGenerated ? sources.Sum(source => (long)source.SourceText.Lines.Count) : 0;
    compilationMeasurements.Add(
        FormattableString.Invariant(
            $"{name},{characters},{lines},{parseTimes[2]:F3},{emitTimes[2]:F3},{assemblyBytes},{ilBytes}"
        )
    );
    Console.WriteLine(
        $"{name}: {lines} lines, {characters} chars, parse {parseTimes[2]:F2} ms, bind+emit {emitTimes[2]:F2} ms, PE {assemblyBytes} bytes, method IL {ilBytes} bytes"
    );
}

static void AssertSuccess(GeneratorDriver driver)
{
    var result = driver.GetRunResult();
    foreach (var generator in result.Results)
        if (generator.Exception is { } exception)
            throw new InvalidOperationException("Generator failed", exception);
    var errors = result
        .Diagnostics.Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
        .ToArray();
    if (errors.Length != 0)
        throw new InvalidOperationException(
            string.Join(Environment.NewLine, errors.AsEnumerable())
        );
}

static string Workload(bool generic)
{
    var source = new StringBuilder("using Imposter.Abstractions;\n");
    for (var target = 0; target < 20; target++)
        source.AppendLine(
            $"[assembly: GenerateImposter(typeof(Workload.ITarget{target}{(generic ? "<,>" : "")}), true)]"
        );
    source.AppendLine("namespace Workload {");
    for (var target = 0; target < 20; target++)
    {
        source.AppendLine(
            $"public interface ITarget{target}{(generic ? "<TEntity, TKey>" : "")} {{"
        );
        var entity = generic ? "TEntity" : "string";
        var key = generic ? "TKey" : "int";
        for (var member = 0; member < 10; member++)
        {
            source.AppendLine($"{entity} Get{member}({key} key);");
            source.AppendLine($"TResult Convert{member}<TResult>({entity} value, {key} key);");
            source.AppendLine(
                $"System.Threading.Tasks.Task<{entity}> Load{member}Async({key} key);"
            );
            source.AppendLine($"{entity} Value{member} {{ get; set; }}");
        }
        source.AppendLine(
            $"event System.Action<{entity}> Changed; {entity} this[{key} key] {{ get; set; }} }}"
        );
    }
    return source.AppendLine("}").ToString();
}
