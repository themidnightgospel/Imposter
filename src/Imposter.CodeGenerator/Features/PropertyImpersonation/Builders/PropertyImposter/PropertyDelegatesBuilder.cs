using System.Collections.Generic;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter;

// Only a property of another ref struct type gets delegates of its own (see PropertyDelegateMetadata).
internal static class PropertyDelegatesBuilder
{
    internal static MemberDeclarationSyntax[] Build(in ImposterPropertyMetadata property)
    {
        if (property.Core.Delegates is not { } delegates)
        {
            return [];
        }

        var members = new List<MemberDeclarationSyntax>();
        if (property.Core.HasGetter)
        {
            members.Add(BuildValueDelegate(property, delegates));
            members.Add(BuildReturnHandlerDelegate(property, delegates));
        }

        if (property.Core.HasSetter)
        {
            members.Add(BuildSetterCallbackDelegate(property, delegates));
        }

        return members.ToArray();
    }

    private static DelegateDeclarationSyntax BuildValueDelegate(
        in ImposterPropertyMetadata property,
        in PropertyDelegateMetadata delegates
    ) =>
        DelegateDeclaration(
                property.Core.NullableAwareValueTypeSyntax,
                Identifier(delegates.ValueDelegateName)
            )
            .AddModifiers(Token(SyntaxKind.PublicKeyword));

    private static DelegateDeclarationSyntax BuildReturnHandlerDelegate(
        in ImposterPropertyMetadata property,
        in PropertyDelegateMetadata delegates
    ) =>
        DelegateDeclaration(
                property.Core.NullableAwareValueTypeSyntax,
                Identifier(delegates.ReturnHandlerDelegateName)
            )
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddParameterListParameters(
                SyntaxFactoryHelper.ParameterSyntax(
                    property.GetterImposterBuilder.GetMethod.BaseImplementationParameter.Type,
                    property.GetterImposterBuilder.GetMethod.BaseImplementationParameter.Name
                )
            );

    private static DelegateDeclarationSyntax BuildSetterCallbackDelegate(
        in ImposterPropertyMetadata property,
        in PropertyDelegateMetadata delegates
    ) =>
        DelegateDeclaration(WellKnownTypes.Void, Identifier(delegates.SetterCallbackDelegateName))
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddParameterListParameters(
                SyntaxFactoryHelper.ParameterSyntax(
                    property.Core.NullableAwareValueTypeSyntax,
                    property.SetterImposter.SetMethod.ValueParameter.Name
                )
            );
}
