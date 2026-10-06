using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.SyntaxHelpers;

/// <summary>
/// Writes generated syntax as C# source in a single pass over its tokens, using tab indentation and LF line endings.
/// </summary>
/// <remarks>
/// Replaces <c>NormalizeWhitespace().ToFullString()</c>, which rebuilds the entire tree with new trivia before it can be
/// printed and dominated generation time. The layout follows NormalizeWhitespace: braces on their own lines, one member
/// or statement per line, a blank line after a closing brace, embedded statements and constraint clauses indented
/// one level. Existing whitespace trivia is ignored; comments and directives are preserved.
/// </remarks>
internal sealed class GeneratedCodeWriter
{
    private const char IndentationCharacter = '\t';
    private const char LineFeed = '\n';

    private readonly StringBuilder _text;
    private int _braceDepth;
    private bool _atLineStart = true;

    private GeneratedCodeWriter(int capacity)
    {
        _text = new StringBuilder(capacity);
    }

    internal static string Write(SyntaxNode root)
    {
        var writer = new GeneratedCodeWriter(root.FullSpan.Length + (root.FullSpan.Length / 2));
        writer.WriteTokens(root);
        return writer._text.ToString();
    }

    private void WriteTokens(SyntaxNode root)
    {
        SyntaxToken previous = default;

        foreach (var token in root.DescendantTokens())
        {
            if (!previous.IsKind(SyntaxKind.None))
            {
                var lineBreaks = LineBreaksBetween(previous, token);
                if (lineBreaks > 0)
                {
                    WriteLineBreaks(lineBreaks);
                }
                else if (NeedsSpace(previous, token))
                {
                    _text.Append(' ');
                }
            }

            WriteTrivia(token.LeadingTrivia, token);

            if (token.IsKind(SyntaxKind.EndOfFileToken))
            {
                WriteTrivia(token.TrailingTrivia, token);
                break;
            }

            if (token.IsKind(SyntaxKind.CloseBraceToken) && !IsInlineBrace(token))
            {
                _braceDepth--;
            }

            WriteIndentation(token);
            _text.Append(token.Text);
            _atLineStart = false;

            if (token.IsKind(SyntaxKind.OpenBraceToken) && !IsInlineBrace(token))
            {
                _braceDepth++;
            }

            WriteTrivia(token.TrailingTrivia, token);
            previous = token;
        }
    }

    private void WriteLineBreaks(int count)
    {
        // A preceding comment or directive has already ended the current line.
        _text.Append(LineFeed, _atLineStart ? count - 1 : count);
        _atLineStart = true;
    }

    private void WriteIndentation(SyntaxToken token)
    {
        if (_atLineStart)
        {
            _text.Append(IndentationCharacter, _braceDepth + ContinuationDepth(token));
        }
    }

    // Only comments and directives are kept; whitespace and line breaks are produced by the layout rules.
    private void WriteTrivia(SyntaxTriviaList triviaList, SyntaxToken token)
    {
        foreach (var trivia in triviaList)
        {
            if (trivia.IsKind(SyntaxKind.SingleLineCommentTrivia))
            {
                EnsureLineStart();
                WriteIndentation(token);
                _text.Append(trivia.ToString());
                _atLineStart = false;
                EnsureLineStart();
            }
            else if (trivia.IsDirective && trivia.GetStructure() is DirectiveTriviaSyntax directive)
            {
                EnsureLineStart();
                WriteDirective(directive);
                EnsureLineStart();
            }
        }
    }

    private void WriteDirective(DirectiveTriviaSyntax directive)
    {
        var first = true;
        foreach (var token in directive.DescendantTokens())
        {
            if (token.IsKind(SyntaxKind.EndOfDirectiveToken))
            {
                continue;
            }

            if (
                !first
                && !token.GetPreviousToken(includeDirectives: true).IsKind(SyntaxKind.HashToken)
            )
            {
                _text.Append(' ');
            }

            _text.Append(token.Text);
            first = false;
        }

        _atLineStart = false;
    }

    private void EnsureLineStart()
    {
        if (!_atLineStart)
        {
            _text.Append(LineFeed);
            _atLineStart = true;
        }
    }

    // Embedded statements without braces (if/else/foreach/... bodies) and type parameter constraint clauses
    // are indented one level deeper than the construct they belong to.
    private static int ContinuationDepth(SyntaxToken token)
    {
        var depth = 0;

        for (var node = token.Parent; node is not null; node = node.Parent)
        {
            if (node is BlockSyntax or MemberDeclarationSyntax or AccessorDeclarationSyntax)
            {
                if (
                    node
                    is BaseMethodDeclarationSyntax
                        or DelegateDeclarationSyntax
                        or TypeDeclarationSyntax
                )
                {
                    depth += IsInConstraintClause(token, node) ? 1 : 0;
                }

                break;
            }

            if (node is StatementSyntax statement && IsEmbeddedStatement(statement))
            {
                depth++;
            }
        }

        return depth;
    }

    private static bool IsInConstraintClause(SyntaxToken token, SyntaxNode declaration)
    {
        for (var node = token.Parent; node is not null && node != declaration; node = node.Parent)
        {
            if (node is TypeParameterConstraintClauseSyntax)
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsEmbeddedStatement(StatementSyntax statement) =>
        statement is not BlockSyntax
        && statement.Parent switch
        {
            IfStatementSyntax ifStatement => ifStatement.Statement == statement,
            ElseClauseSyntax elseClause => elseClause.Statement == statement
                && statement is not IfStatementSyntax,
            CommonForEachStatementSyntax forEach => forEach.Statement == statement,
            ForStatementSyntax forStatement => forStatement.Statement == statement,
            WhileStatementSyntax whileStatement => whileStatement.Statement == statement,
            DoStatementSyntax doStatement => doStatement.Statement == statement,
            UsingStatementSyntax usingStatement => usingStatement.Statement == statement
                && statement is not UsingStatementSyntax,
            LockStatementSyntax lockStatement => lockStatement.Statement == statement,
            FixedStatementSyntax fixedStatement => fixedStatement.Statement == statement,
            _ => false,
        };

    // Braces that stay on the line of the surrounding code: initializers, interpolations and accessor lists
    // whose accessors have no bodies (`{ get; set; }`).
    private static bool IsInlineBrace(SyntaxToken brace) =>
        brace.Parent is InitializerExpressionSyntax or InterpolationSyntax
        || (brace.Parent is AccessorListSyntax accessorList && HasNoAccessorBodies(accessorList));

    private static bool HasNoAccessorBodies(AccessorListSyntax accessorList)
    {
        foreach (var accessor in accessorList.Accessors)
        {
            if (accessor.Body is not null || accessor.ExpressionBody is not null)
            {
                return false;
            }
        }

        return true;
    }

    private static int LineBreaksBetween(SyntaxToken previous, SyntaxToken next)
    {
        switch (previous.Kind())
        {
            case SyntaxKind.OpenBraceToken:
                return IsInlineBrace(previous) ? 0 : 1;
            case SyntaxKind.CloseBraceToken:
                return LineBreaksAfterCloseBrace(previous, next);
            case SyntaxKind.CloseParenToken:
                return
                    (previous.Parent is StatementSyntax && next.Parent != previous.Parent)
                    || next.IsKind(SyntaxKind.OpenBraceToken) && !IsInlineBrace(next)
                    || next.IsKind(SyntaxKind.WhereKeyword)
                    ? 1
                    : 0;
            case SyntaxKind.CloseBracketToken:
                if (
                    previous.Parent is AttributeListSyntax
                    && previous.Parent.Parent is not ParameterSyntax
                )
                {
                    return 1;
                }

                break;
            case SyntaxKind.SemicolonToken:
                return LineBreaksAfterSemicolon(previous, next);
            case SyntaxKind.ElseKeyword:
                return next.IsKind(SyntaxKind.IfKeyword) ? 0 : 1;
            case SyntaxKind.ColonToken:
                if (previous.Parent is LabeledStatementSyntax or SwitchLabelSyntax)
                {
                    return 1;
                }

                break;
        }

        switch (next.Kind())
        {
            case SyntaxKind.OpenBraceToken:
            case SyntaxKind.CloseBraceToken:
                return IsInlineBrace(next) ? 0 : 1;
            case SyntaxKind.ElseKeyword:
            case SyntaxKind.FinallyKeyword:
            case SyntaxKind.CatchKeyword:
                return 1;
            case SyntaxKind.OpenBracketToken:
                return
                    next.Parent is AttributeListSyntax && next.Parent.Parent is not ParameterSyntax
                    ? 1
                    : 0;
            case SyntaxKind.WhereKeyword:
                // `where` goes on its own line after a method's parameter list (handled above) or directly after
                // a type parameter list; otherwise it stays on the declaration line.
                return previous.Parent is TypeParameterListSyntax ? 1 : 0;
        }

        return 0;
    }

    private static int LineBreaksAfterCloseBrace(SyntaxToken closeBrace, SyntaxToken next)
    {
        if (
            closeBrace.Parent is InitializerExpressionSyntax or InterpolationSyntax
            || closeBrace.Parent?.Parent is AnonymousFunctionExpressionSyntax
        )
        {
            return 0;
        }

        if (
            closeBrace.Parent is AccessorListSyntax accessorList
            && HasNoAccessorBodies(accessorList)
        )
        {
            if (next.IsKind(SyntaxKind.EqualsToken))
            {
                return 0;
            }

            // Mirrors NormalizeWhitespace: no blank line between auto-properties when the next one starts with a modifier.
            if (next.Parent is PropertyDeclarationSyntax)
            {
                return 1;
            }
        }

        return next.Kind() switch
        {
            SyntaxKind.EndOfFileToken
            or SyntaxKind.CloseBraceToken
            or SyntaxKind.CatchKeyword
            or SyntaxKind.FinallyKeyword
            or SyntaxKind.ElseKeyword => 1,
            SyntaxKind.WhileKeyword when next.Parent is DoStatementSyntax => 1,
            _ => 2,
        };
    }

    private static int LineBreaksAfterSemicolon(SyntaxToken semicolon, SyntaxToken next)
    {
        if (semicolon.Parent is ForStatementSyntax)
        {
            return 0;
        }

        if (
            semicolon.Parent
                is AccessorDeclarationSyntax { Parent: AccessorListSyntax accessorList }
            && HasNoAccessorBodies(accessorList)
        )
        {
            return 0;
        }

        if (next.IsKind(SyntaxKind.CloseBraceToken))
        {
            return 1;
        }

        if (semicolon.Parent is UsingDirectiveSyntax)
        {
            return next.Parent is UsingDirectiveSyntax ? 1 : 2;
        }

        // An expression-bodied property (or one with an initializer) is followed by a blank line.
        return semicolon.Parent is PropertyDeclarationSyntax ? 2 : 1;
    }

    private static bool NeedsSpace(SyntaxToken previous, SyntaxToken next)
    {
        var previousKind = previous.Kind();
        var nextKind = next.Kind();

        switch (nextKind)
        {
            case SyntaxKind.CommaToken:
            case SyntaxKind.SemicolonToken:
            case SyntaxKind.CloseParenToken:
            case SyntaxKind.CloseBracketToken:
            case SyntaxKind.DotToken:
            case SyntaxKind.ColonColonToken:
                return false;
        }

        switch (previousKind)
        {
            case SyntaxKind.OpenParenToken:
            case SyntaxKind.OpenBracketToken:
            case SyntaxKind.DotToken:
            case SyntaxKind.ColonColonToken:
                return false;
        }

        if (nextKind == SyntaxKind.QuestionToken && next.Parent is NullableTypeSyntax)
        {
            return false;
        }

        if (previousKind == SyntaxKind.QuestionToken && previous.Parent is NullableTypeSyntax)
        {
            // Mirrors NormalizeWhitespace, including `int? []`.
            return nextKind != SyntaxKind.GreaterThanToken;
        }

#if ROSLYN4_14_OR_GREATER
        if (previousKind == SyntaxKind.ExtensionKeyword)
        {
            // Mirrors NormalizeWhitespace: `extension <T>(...)`.
            return true;
        }
#endif

        if (
            IsTypeArgumentBracket(next)
            && nextKind is SyntaxKind.LessThanToken or SyntaxKind.GreaterThanToken
        )
        {
            return false;
        }

        if (previousKind == SyntaxKind.LessThanToken && IsTypeArgumentBracket(previous))
        {
            return false;
        }

        if (previousKind == SyntaxKind.GreaterThanToken && IsTypeArgumentBracket(previous))
        {
            return nextKind
                is not (
                    SyntaxKind.OpenParenToken
                    or SyntaxKind.OpenBracketToken
                    or SyntaxKind.GreaterThanToken
                    or SyntaxKind.QuestionToken
                );
        }

        if (
            nextKind == SyntaxKind.QuestionToken && next.Parent is ConditionalAccessExpressionSyntax
            || previousKind == SyntaxKind.QuestionToken
                && previous.Parent is ConditionalAccessExpressionSyntax
        )
        {
            return false;
        }

        if (nextKind == SyntaxKind.ExclamationToken && next.Parent is PostfixUnaryExpressionSyntax)
        {
            // Mirrors NormalizeWhitespace: `value!` but `default !`.
            return SyntaxFacts.IsKeywordKind(previousKind);
        }

        if (
            previousKind == SyntaxKind.CloseParenToken
            && previous.Parent is ParenthesizedVariableDesignationSyntax
            && nextKind == SyntaxKind.InKeyword
        )
        {
            // Mirrors NormalizeWhitespace: `foreach (var (key, value)in items)`.
            return false;
        }

        if (
            previous.Parent is PrefixUnaryExpressionSyntax
            && previous == previous.Parent.GetFirstToken()
        )
        {
            return false;
        }

        if (
            next.Parent is PostfixUnaryExpressionSyntax
            && nextKind is SyntaxKind.PlusPlusToken or SyntaxKind.MinusMinusToken
        )
        {
            return false;
        }

        if (previousKind == SyntaxKind.CloseParenToken && previous.Parent is CastExpressionSyntax)
        {
            return false;
        }

        if (nextKind == SyntaxKind.ColonToken && next.Parent is NameColonSyntax)
        {
            return false;
        }

        if (nextKind is SyntaxKind.OpenParenToken or SyntaxKind.OpenBracketToken)
        {
            return SpaceBeforeOpeningParenthesisOrBracket(previous, next);
        }

        return true;
    }

    private static bool SpaceBeforeOpeningParenthesisOrBracket(
        SyntaxToken previous,
        SyntaxToken next
    )
    {
        var previousKind = previous.Kind();

        if (previousKind is SyntaxKind.CloseParenToken or SyntaxKind.CloseBracketToken)
        {
            return false;
        }

        if (SyntaxFacts.IsKeywordKind(previousKind))
        {
            // `typeof(T)`, `default(T)`, `new()`, `this[...]`, `int[]` vs. `if (`, `return (`, `return [`
            return next.IsKind(SyntaxKind.OpenParenToken)
                ? previousKind
                    is not (
                        SyntaxKind.TypeOfKeyword
                        or SyntaxKind.SizeOfKeyword
                        or SyntaxKind.DefaultKeyword
                        or SyntaxKind.CheckedKeyword
                        or SyntaxKind.UncheckedKeyword
                        or SyntaxKind.ThisKeyword
                        or SyntaxKind.BaseKeyword
                        or SyntaxKind.NewKeyword
                    )
                : !SyntaxFacts.IsPredefinedType(previousKind)
                    && previousKind
                        is not (
                            SyntaxKind.NewKeyword
                            or SyntaxKind.ThisKeyword
                            or SyntaxKind.BaseKeyword
                        );
        }

        return previousKind
            is not (
                SyntaxKind.IdentifierToken
                or SyntaxKind.NumericLiteralToken
                or SyntaxKind.StringLiteralToken
                or SyntaxKind.CharacterLiteralToken
            );
    }

    private static bool IsTypeArgumentBracket(SyntaxToken token) =>
        token.Parent is TypeArgumentListSyntax or TypeParameterListSyntax;
}
