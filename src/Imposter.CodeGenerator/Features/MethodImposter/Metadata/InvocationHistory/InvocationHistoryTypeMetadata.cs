using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.InvocationHistory;

internal readonly record struct InvocationHistoryTypeMetadata
{
    internal const string ArgumentsFieldName = "Arguments";

    internal const string ResultFieldName = "Result";

    internal const string ExceptionFieldName = "Exception";

    internal readonly string Name;

    internal readonly TypeMetadata Interface;

    internal readonly InvocationHistoryCollectionMetadata Collection;

    internal readonly FieldDeclarationMetadata AsField;

    internal readonly NameSyntax Syntax;

    public InvocationHistoryTypeMetadata(ImposterTargetMethodMetadata method)
    {
        Name = $"{method.UniqueName}MethodInvocationHistory";
        Interface = new TypeMetadata($"I{Name}");
        Collection = new InvocationHistoryCollectionMetadata($"{Name}Collection");
        AsField = new FieldDeclarationMetadata(Name);
        Syntax = SyntaxFactoryHelper.WithMethodGenericArguments(method.GenericTypeArguments, Name);
    }
}
