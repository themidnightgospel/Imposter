using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata;

internal readonly struct PropertySetterImposterBuilderInterfaceMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax Syntax;
    
    internal readonly CalledMethodMetadata CalledMethod;
    
    internal readonly CallbackMethodMetadata CallbackMethod;

    internal PropertySetterImposterBuilderInterfaceMetadata(in ImposterPropertyCoreMetadata property)
    {
        Name = $"I{property.UniqueName}PropertySetterBuilder";
        Syntax = SyntaxFactory.ParseName(Name);
        CalledMethod = new CalledMethodMetadata();
        CallbackMethod = new CallbackMethodMetadata(property, Syntax);
    }
    
    internal readonly struct CalledMethodMetadata
    {
        internal readonly string Name = "Called";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata CountParameter;

        internal readonly string SetterInvocationCountVariableName = "setterInvocationCount";

        public CalledMethodMetadata()
        {
            ReturnType = WellKnownTypes.Void;
            CountParameter = new ParameterMetadata("count", WellKnownTypes.Imposter.Abstractions.Count);
        }
    }

    internal readonly struct CallbackMethodMetadata
    {
        internal readonly string Name = "Callback";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata CallbackParameter;

        internal CallbackMethodMetadata(in ImposterPropertyCoreMetadata property, TypeSyntax setterBuilderInterfaceTypeSyntax)
        {
            ReturnType = setterBuilderInterfaceTypeSyntax;
            CallbackParameter = new ParameterMetadata("callback", property.AsSystemActionType);
        }
    }
}