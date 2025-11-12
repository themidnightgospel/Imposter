using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.MethodImposter;

internal readonly struct MethodImposterMetadata
{
    internal readonly string Name;

    internal readonly TypeMetadata BuilderInterface;

    internal readonly TypeMetadata Interface;

    internal readonly MethodImposterGenericTypeMetadata GenericInterface;

    internal readonly MethodImposterCollectionMetadata Collection;

    internal readonly NameSyntax Syntax;

    internal readonly FieldDeclarationMetadata AsField;

    internal readonly MethodImposterBuilderMetadata Builder;

    internal readonly MethodImposterInvokeMethodMetadata InvokeMethod;

    internal readonly FindMatchingInvocationImposterGroupMethodMetadata FindMatchingInvocationImposterGroupMethod;

    internal readonly HasMatchingInvocationImposterGroupMethodMetadata HasMatchingInvocationImposterGroupMethod;

    internal readonly InvocationImpostersFieldMetadata InvocationImpostersField;

    internal MethodImposterMetadata(in ImposterTargetMethodMetadata method)
    {
        Name = $"{method.UniqueName}MethodImposter";
        Syntax = SyntaxFactoryHelper.WithMethodGenericArguments(method.GenericTypeArguments, Name);

        var methodImposterBuilderInterfaceName = $"I{Name}Builder";
        BuilderInterface = TypeMetadataFactory.Create(methodImposterBuilderInterfaceName, method.GenericTypeArguments);

        var methodImposterInterfaceName = $"I{Name}";
        Interface = new TypeMetadata(methodImposterInterfaceName);
        GenericInterface = new MethodImposterGenericTypeMetadata(methodImposterInterfaceName, method.GenericTypeArguments, method.TargetGenericTypeArguments);

        Collection = new MethodImposterCollectionMetadata($"{Name}Collection");
        AsField = new FieldDeclarationMetadata(Name);
        InvokeMethod = new MethodImposterInvokeMethodMetadata(method);
        FindMatchingInvocationImposterGroupMethod = new FindMatchingInvocationImposterGroupMethodMetadata(method);
        HasMatchingInvocationImposterGroupMethod = new HasMatchingInvocationImposterGroupMethodMetadata(method);
        InvocationImpostersField = new InvocationImpostersFieldMetadata(method);
        Builder = new MethodImposterBuilderMetadata(
            Syntax,
            Collection.Syntax,
            method.ArgumentsCriteria.Syntax,
            method.MethodInvocationImposterGroup.Syntax,
            method.MethodInvocationImposterGroup.MethodInvocationImposterSyntax);
    }
}
