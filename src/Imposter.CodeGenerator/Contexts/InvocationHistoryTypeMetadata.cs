using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Contexts;

internal readonly record struct InvocationHistoryTypeMetadata
{
    internal const string ArgumentsFieldName = "Arguments";
    
    internal const string ResultFieldName = "Result";
    
    internal const string ExceptionFieldName = "Exception";
    
    internal readonly string Name;

    internal readonly TypeMetadata Interface;

    internal readonly CollectionMetadata Collection;

    internal readonly FieldDeclarationMetadata AsField;

    internal readonly NameSyntax Syntax;

    public InvocationHistoryTypeMetadata(ImposterTargetMethodMetadata method)
    {
        Name = $"{method.UniqueName}MethodInvocationHistory";
        Interface = new TypeMetadata($"I{Name}");
        Collection = new CollectionMetadata($"{Name}Collection");
        AsField = new FieldDeclarationMetadata(Name);
        Syntax = SyntaxFactoryHelper.WithMethodGenericArguments(method.GenericTypeArguments, Name);
    }

    internal readonly struct MatchesMethod
    {
        internal const string Name = "Matches";
        
        internal const string ArgumentsCriteriaParameterName = "criteria";
    }

    internal readonly record struct CollectionMetadata
    {
        internal const string InvocationHistoryCollectionFieldName = "_invocationHistory";
        
        internal readonly string Name;

        internal readonly NameSyntax Syntax;

        internal readonly FieldDeclarationMetadata AsField;

        public CollectionMetadata(string name)
        {
            Name = name;
            Syntax = SyntaxFactory.IdentifierName(Name);
            AsField = new FieldDeclarationMetadata(Name);
        }

        internal readonly struct CountMethod
        {
            internal const string Name = "Count";

            internal const string ArgumentsCriteriaParameterName = "argumentsCriteria";
        }
    }
}