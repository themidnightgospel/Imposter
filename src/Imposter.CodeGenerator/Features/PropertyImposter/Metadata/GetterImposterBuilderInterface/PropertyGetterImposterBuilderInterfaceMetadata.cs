using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilderInterface;

internal readonly struct PropertyGetterImposterBuilderInterfaceMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly ReturnsMethodMetadata ReturnsMethod;

    internal readonly ThrowsMethodMetadata ThrowsMethod;

    internal readonly CallbackMethodMetadata CallbackMethod;

    internal readonly CalledMethodMetadata CalledMethod;

    internal readonly ThenMethodMetadata ThenMethod;

    internal PropertyGetterImposterBuilderInterfaceMetadata(in ImposterPropertyCoreMetadata property)
    {
        Name = $"I{property.UniqueName}PropertyGetterBuilder";
        TypeSyntax = SyntaxFactory.ParseName(Name);

        ReturnsMethod = new ReturnsMethodMetadata(in property, TypeSyntax);
        ThrowsMethod = new ThrowsMethodMetadata(TypeSyntax);
        CallbackMethod = new CallbackMethodMetadata(TypeSyntax);
        CalledMethod = new CalledMethodMetadata();
        ThenMethod = new ThenMethodMetadata(TypeSyntax, TypeSyntax);
    }
}
