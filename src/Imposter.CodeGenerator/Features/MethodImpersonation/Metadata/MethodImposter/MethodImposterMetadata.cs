using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;

internal readonly struct MethodImposterMetadata
{
    // A generic method imposter's nested class, which As returns to impersonate the method for other type arguments.
    internal const string AdapterName = "Adapter";

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

    // The method imposter and its collection both keep the imposter's mode in a field of this name.
    internal readonly string InvocationBehaviorFieldName;

    internal MethodImposterMetadata(in ImposterTargetMethodMetadata method)
    {
        Name = $"{method.UniqueName}MethodImposter";
        Syntax = SyntaxFactoryHelper.WithMethodGenericArguments(method.GenericTypeArguments, Name);

        var methodImposterBuilderInterfaceName = $"I{Name}Builder";
        BuilderInterface = TypeMetadataFactory.Create(
            methodImposterBuilderInterfaceName,
            method.GenericTypeArguments
        );

        var methodImposterInterfaceName = $"I{Name}";
        Interface = new TypeMetadata(methodImposterInterfaceName);
        GenericInterface = new MethodImposterGenericTypeMetadata(
            methodImposterInterfaceName,
            method.GenericTypeArguments,
            method.TargetGenericTypeArguments
        );

        Collection = new MethodImposterCollectionMetadata($"{Name}Collection", method.MemberNames);
        AsField = new FieldDeclarationMetadata(Name, method.MemberNames);
        InvocationBehaviorFieldName = method.MemberNames.Use("_invocationBehavior");
        InvokeMethod = new MethodImposterInvokeMethodMetadata(
            method.ReservedParameterNames,
            method.Delegate.Syntax
        );
        FindMatchingInvocationImposterGroupMethod =
            new FindMatchingInvocationImposterGroupMethodMetadata(
                method.ReservedParameterNames,
                method.MemberNames
            );
        HasMatchingInvocationImposterGroupMethod =
            new HasMatchingInvocationImposterGroupMethodMetadata(method.ReservedParameterNames);
        InvocationImpostersField = new InvocationImpostersFieldMetadata(
            method.ReservedParameterNames
        );
        Builder = new MethodImposterBuilderMetadata(
            Syntax,
            method.Model.IsGenericMethod ? Collection.Syntax : null,
            method.ArgumentsCriteria.Syntax,
            method.MethodInvocationImposterGroup.Syntax,
            method.MethodInvocationImposterGroup.MethodInvocationImposterSyntax
        );
    }
}
