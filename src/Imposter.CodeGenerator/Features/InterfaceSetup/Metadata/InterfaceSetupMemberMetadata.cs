using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.InterfaceSetup.Metadata;

internal readonly struct InterfaceSetupMemberMetadata
{
    internal readonly InterfaceSetupMemberModel Model;
    internal readonly string SetupName;
    internal readonly TypeSyntax ReturnType;
    internal readonly IReadOnlyList<TypeParameterConstraintClauseSyntax> ImplementationConstraints;

    // True for an indexer the imposter sets up with a method named SetupName instead of its own this[...].
    internal readonly bool IsSetUpByMethod;

    // The name the view declares the member under: its own, or the setup name of an overload whose setup signature
    // another overload shares, or of an indexer with a setup method.
    internal readonly string ViewName;

    // True for a method, and for an indexer with a setup method, which the view declares as that method.
    internal readonly bool IsDeclaredAsMethod;

    internal InterfaceSetupMemberMetadata(
        InterfaceSetupMemberModel model,
        string setupName,
        TypeSyntax returnType,
        bool isSetUpByMethod = false,
        string? viewName = null,
        bool isDeclaredAsMethod = false
    )
    {
        Model = model;
        SetupName = setupName;
        ReturnType = returnType;
        IsSetUpByMethod = isSetUpByMethod;
        ViewName = viewName ?? model.Name;
        IsDeclaredAsMethod = isDeclaredAsMethod || model.Kind == InterfaceSetupMemberKind.Method;
        ImplementationConstraints =
            model.Kind == InterfaceSetupMemberKind.Method
                ? GetImplementationConstraints(model)
                : [];
    }

    // Only interface targets have setup views, and their methods always carry a setup model.
    internal InterfaceSetupMemberMetadata(in ImposterTargetMethodMetadata method)
        : this(
            method.InterfaceSetupMember!,
            method.SetupName,
            method.MethodImposter.BuilderInterface.Syntax,
            viewName: method.Model.HasOverloadWithTheSameSetup ? method.SetupName : null
        ) { }

    private static List<TypeParameterConstraintClauseSyntax> GetImplementationConstraints(
        InterfaceSetupMemberModel method
    )
    {
        var result = new List<TypeParameterConstraintClauseSyntax>();
        var nullableParameters = SyntaxFactoryHelper
            .ArgParameters(method.Parameters)
            .DescendantNodes()
            .OfType<NullableTypeSyntax>()
            .Select(nullable => nullable.ElementType)
            .OfType<IdentifierNameSyntax>()
            .Select(identifier => identifier.Identifier.ValueText)
            .ToArray();

        foreach (var parameter in method.TypeParameters)
        {
            TypeParameterConstraintSyntax? constraint = null;
            if (parameter.HasReferenceTypeConstraint)
            {
                // Explicit implementations may repeat class, but not class?. The actual
                // nullability constraint is inherited from the setup interface.
                constraint = ClassOrStructConstraint(SyntaxKind.ClassConstraint);
            }
            else if (parameter.HasValueTypeConstraint)
            {
                constraint = ClassOrStructConstraint(SyntaxKind.StructConstraint);
            }
            else if (nullableParameters.Contains(parameter.Name))
            {
                constraint = DefaultConstraint();
            }

            if (constraint is not null)
            {
                result.Add(
                    TypeParameterConstraintClause(SyntaxFactoryHelper.EscapeKeyword(parameter.Name))
                        .AddConstraints(constraint)
                );
            }
        }
        return result;
    }
}
