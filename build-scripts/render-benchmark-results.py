#!/usr/bin/env python3
"""Renders the README's benchmark table from a BenchmarkDotNet full JSON report.

Writes simple-method.svg, the table as an image that follows the viewer's light or dark color scheme, and
simple-method.md, the same table as Markdown. The Benchmarks workflow publishes both to the benchmark-results branch.
"""

import argparse
import json
from datetime import datetime, timezone
from pathlib import Path
from xml.sax.saxutils import escape

LIBRARIES = ["Moq", "NSubstitute", "FakeItEasy", "Rocks", "Imposter"]
HIGHLIGHTED = "Imposter"

WIDTH = 640
ROW_HEIGHT = 24
TITLE_HEIGHT = 48
FOOTER_HEIGHT = 52
LIBRARY_X = 24
ITERATIONS_RIGHT = 300
MEAN_RIGHT = 470
ALLOCATED_RIGHT = WIDTH - 24

STYLE = """
.card { fill: #ffffff; stroke: #d0d7de; }
.head { fill: #f6f8fa; }
.highlight { fill: #ddf4ff; }
.rule { stroke: #d0d7de; }
text { font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Helvetica, Arial, sans-serif; font-size: 13px;
  fill: #1f2328; }
.number { font-family: ui-monospace, SFMono-Regular, Menlo, Consolas, monospace; font-size: 12px; }
.title { font-size: 15px; font-weight: 600; }
.bold { font-weight: 600; }
.muted { fill: #59636e; font-size: 11px; }
.accent { fill: #0969da; }
@media (prefers-color-scheme: dark) {
  .card { fill: #0d1117; stroke: #30363d; }
  .head { fill: #161b22; }
  .highlight { fill: #12233d; }
  .rule { stroke: #30363d; }
  text { fill: #e6edf3; }
  .muted { fill: #9198a1; }
  .accent { fill: #4493f8; }
}
"""


def main():
    arguments = parse_arguments()
    report = json.loads(Path(arguments.report).read_text(encoding="utf-8"))
    rows = results_of(report)
    environment = environment_of(report, arguments)

    output = Path(arguments.out)
    output.mkdir(parents=True, exist_ok=True)
    (output / "simple-method.svg").write_text(svg(rows, environment), encoding="utf-8")
    (output / "simple-method.md").write_text(markdown(rows, environment), encoding="utf-8")


def parse_arguments():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--report", required=True, help="BenchmarkDotNet *-report-full.json")
    parser.add_argument("--out", required=True, help="directory to write the table to")
    parser.add_argument("--commit", required=True, help="commit the benchmarks ran on")
    parser.add_argument("--run-url", default="", help="the workflow run, linked from the Markdown table")
    return parser.parse_args()


# One row per library and iteration count, ordered by iteration count, then by LIBRARIES.
def results_of(report):
    rows = [
        {
            "library": benchmark["Method"],
            "iterations": int(benchmark["Parameters"].split("=")[1]),
            "mean": benchmark["Statistics"]["Mean"],
            "allocated": benchmark["Memory"]["BytesAllocatedPerOperation"],
        }
        for benchmark in report["Benchmarks"]
    ]
    missing = set(LIBRARIES) - {row["library"] for row in rows}
    if missing:
        raise SystemExit(f"The report has no results for {', '.join(sorted(missing))}.")
    return sorted(rows, key=lambda row: (row["iterations"], LIBRARIES.index(row["library"])))


def environment_of(report, arguments):
    host = report["HostEnvironmentInfo"]
    return {
        "processor": f"{host['ProcessorName']}, {host['LogicalCoreCount']} logical cores",
        "os": host["OsVersion"],
        # ".NET 10.0.12 (10.0.12, 10.0.1226.42308)" -> ".NET 10.0.12"
        "runtime": host["RuntimeVersion"].split(" (")[0],
        "benchmarkdotnet": host["BenchmarkDotNetVersion"],
        "commit": arguments.commit[:8],
        "date": datetime.now(timezone.utc).strftime("%Y-%m-%d"),
        "run_url": arguments.run_url,
    }


def svg(rows, environment):
    height = TITLE_HEIGHT + ROW_HEIGHT * (len(rows) + 1) + FOOTER_HEIGHT
    lines = [
        f'<svg xmlns="http://www.w3.org/2000/svg" width="{WIDTH}" height="{height}" '
        f'viewBox="0 0 {WIDTH} {height}" role="img" aria-label="Benchmark results">',
        f"<style>{STYLE}</style>",
        f'<rect class="card" x="0.5" y="0.5" width="{WIDTH - 1}" height="{height - 1}" rx="6"/>',
        f'<text class="title" x="{LIBRARY_X}" y="30">Set up Square(int) and call it N times (lower is better)</text>',
    ]

    top = TITLE_HEIGHT
    lines.append(f'<rect class="head" x="1" y="{top}" width="{WIDTH - 2}" height="{ROW_HEIGHT}"/>')
    lines += header_text(top)

    previous_iterations = None
    for index, row in enumerate(rows, start=1):
        y = top + ROW_HEIGHT * index
        if previous_iterations is not None and row["iterations"] != previous_iterations:
            lines.append(f'<line class="rule" x1="1" y1="{y}" x2="{WIDTH - 1}" y2="{y}"/>')
        previous_iterations = row["iterations"]
        highlighted = row["library"] == HIGHLIGHTED
        if highlighted:
            lines.append(f'<rect class="highlight" x="1" y="{y}" width="{WIDTH - 2}" height="{ROW_HEIGHT}"/>')
        lines += row_text(
            y,
            row["library"],
            f'{row["iterations"]:,}',
            duration(row["mean"]),
            size(row["allocated"]),
            "bold accent" if highlighted else "",
        )

    footer = top + ROW_HEIGHT * (len(rows) + 1)
    lines += [
        f'<text class="muted" x="{LIBRARY_X}" y="{footer + 22}">'
        f'{escape(environment["processor"])} · {escape(environment["os"])}</text>',
        f'<text class="muted" x="{LIBRARY_X}" y="{footer + 38}">{escape(environment["runtime"])} · '
        f'BenchmarkDotNet {escape(environment["benchmarkdotnet"])} · '
        f'commit {escape(environment["commit"])} · {environment["date"]}</text>',
        "</svg>",
    ]
    return "\n".join(lines) + "\n"


def header_text(y):
    baseline = y + 16
    return [
        f'<text class="bold" x="{LIBRARY_X}" y="{baseline}">Library</text>',
        f'<text class="bold" x="{ITERATIONS_RIGHT}" y="{baseline}" text-anchor="end">N</text>',
        f'<text class="bold" x="{MEAN_RIGHT}" y="{baseline}" text-anchor="end">Mean time</text>',
        f'<text class="bold" x="{ALLOCATED_RIGHT}" y="{baseline}" text-anchor="end">Allocated</text>',
    ]


def row_text(y, library, iterations, mean, allocated, classes):
    baseline = y + 16
    return [
        f'<text class="{classes}" x="{LIBRARY_X}" y="{baseline}">{escape(library)}</text>',
        f'<text class="number {classes}" x="{ITERATIONS_RIGHT}" y="{baseline}" text-anchor="end">{iterations}</text>',
        f'<text class="number {classes}" x="{MEAN_RIGHT}" y="{baseline}" text-anchor="end">{mean}</text>',
        f'<text class="number {classes}" x="{ALLOCATED_RIGHT}" y="{baseline}" text-anchor="end">{allocated}</text>',
    ]


def markdown(rows, environment):
    lines = [
        "| Library | N | Mean time | Allocated |",
        "|---------|--:|----------:|----------:|",
    ]
    for row in rows:
        cells = [row["library"], f'{row["iterations"]:,}', duration(row["mean"]), size(row["allocated"])]
        if row["library"] == HIGHLIGHTED:
            cells = [f"**{cell}**" for cell in cells]
        lines.append(f"| {' | '.join(cells)} |")
    run = f" ([workflow run]({environment['run_url']}))" if environment["run_url"] else ""
    lines += [
        "",
        f"{environment['processor']}, {environment['os']}, {environment['runtime']}, "
        f"BenchmarkDotNet {environment['benchmarkdotnet']}, commit {environment['commit']}, "
        f"{environment['date']}{run}.",
    ]
    return "\n".join(lines) + "\n"


def duration(nanoseconds):
    for unit, scale in (("s", 1e9), ("ms", 1e6), ("μs", 1e3)):
        if nanoseconds >= scale:
            return f"{nanoseconds / scale:,.2f} {unit}"
    return f"{nanoseconds:,.2f} ns"


def size(bytes_allocated):
    for unit, scale in (("MB", 1024 * 1024), ("KB", 1024)):
        if bytes_allocated >= scale:
            return f"{bytes_allocated / scale:,.2f} {unit}"
    return f"{bytes_allocated:,} B"


if __name__ == "__main__":
    main()
