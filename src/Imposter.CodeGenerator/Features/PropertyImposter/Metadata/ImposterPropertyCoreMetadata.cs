using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata;

internal readonly ref struct ImposterPropertyCoreMetadata
{
    internal readonly string Name;

    internal readonly bool HasGetter;

    internal readonly bool HasSetter;

    internal readonly string UniqueName;
    
    internal readonly TypeSyntax TypeSyntax;

    internal readonly TypeSyntax AsSystemActionType;

    internal readonly TypeSyntax AsSystemFuncType;

    internal readonly TypeSyntax AsArgType;

    internal ImposterPropertyCoreMetadata(IPropertySymbol property, string uniqueName)
    {
        UniqueName = uniqueName;
        HasGetter = property.GetMethod != null;
        HasSetter = property.SetMethod != null;
        Name = property.Name;
        TypeSyntax = SyntaxFactoryHelper.TypeSyntax(property.Type);
        AsSystemFuncType = WellKnownTypes.System.FuncOfT(TypeSyntax);
        AsSystemActionType = WellKnownTypes.System.ActionOfT(TypeSyntax);
        AsArgType = WellKnownTypes.Imposter.Abstractions.Arg(TypeSyntax);
    }
}