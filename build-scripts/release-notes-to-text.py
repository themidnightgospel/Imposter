#!/usr/bin/env python3
"""Turns release notes written in Markdown into the plain text nuget.org shows.

nuget.org displays release notes verbatim, so this drops the markup the release bodies use: heading markers, bold,
code spans and link syntax. List markers, tables and everything else stay as written, so a code identifier comes out
exactly as it was typed. The pack script runs the GitHub release body through it before the notes go into the package.
"""

import re
import sys

MARKUP = [
    (re.compile(r"^#{1,6}[ \t]+", re.MULTILINE), ""),
    (re.compile(r"\*\*(.+?)\*\*"), r"\1"),
    (re.compile(r"\[([^\]]+)\]\([^)]+\)"), r"\1"),
    (re.compile(r"`([^`]*)`"), r"\1"),
]


def to_text(markdown):
    text = markdown.replace("\r\n", "\n")
    for pattern, replacement in MARKUP:
        text = pattern.sub(replacement, text)
    return text


if __name__ == "__main__":
    sys.stdout.buffer.write(to_text(sys.stdin.buffer.read().decode("utf-8")).encode("utf-8"))
