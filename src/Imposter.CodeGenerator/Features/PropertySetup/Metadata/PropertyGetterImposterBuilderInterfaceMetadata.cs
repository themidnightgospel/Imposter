using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertySetup.Metadata;

internal readonly struct PropertyGetterImposterBuilderInterfaceMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly ReturnsMethodMetadata ReturnsMethod;
    
    internal readonly ThrowsMethodMetadata ThrowsMethod;
    
    internal readonly CallbackMethodMetadata CallbackMethod;
    
    internal readonly CalledMethodMetadata CalledMethod;

    internal PropertyGetterImposterBuilderInterfaceMetadata(in ImposterPropertyCoreMetadata property)
    {
        Name = $"I{property.UniqueName}PropertyGetterBuilder";
        TypeSyntax = SyntaxFactory.ParseName(Name);
        
        ReturnsMethod = new ReturnsMethodMetadata(in property, TypeSyntax);
        ThrowsMethod = new ThrowsMethodMetadata(in property, TypeSyntax);
        CallbackMethod = new CallbackMethodMetadata(in property, TypeSyntax);
        CalledMethod = new CalledMethodMetadata();
    }

    internal readonly struct ReturnsMethodMetadata
    {
        internal readonly string Name = "Returns";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata ValueParameter;

        internal readonly ParameterMetadata ValueGeneratorParameter;

        internal ReturnsMethodMetadata(in ImposterPropertyCoreMetadata property, TypeSyntax builderInterfaceTypeSyntax)
        {
            ReturnType = builderInterfaceTypeSyntax;
            ValueParameter = new ParameterMetadata("value", property.TypeSyntax);
            ValueGeneratorParameter = new ParameterMetadata("valueGenerator", property.AsSystemFuncType);
        }
    }
    
    internal readonly struct ThrowsMethodMetadata
    {
        internal readonly string Name = "Throws";
        
        internal readonly string GenericTypeParameterName = "TException";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata ExceptionParameter;

        internal ThrowsMethodMetadata(in ImposterPropertyCoreMetadata property, TypeSyntax builderInterfaceTypeSyntax)
        {
            ReturnType = builderInterfaceTypeSyntax;
            ExceptionParameter = new ParameterMetadata("exception", WellKnownTypes.System.Exception);
        }
    }
    
    internal readonly struct CallbackMethodMetadata
    {
        internal readonly string Name = "GetterCallback";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata CallbackParameter;

        internal CallbackMethodMetadata(in ImposterPropertyCoreMetadata property, TypeSyntax builderInterfaceTypeSyntax)
        {
            ReturnType = builderInterfaceTypeSyntax;
            CallbackParameter = new ParameterMetadata("callback", WellKnownTypes.System.Action);
        }
    }
    
    internal readonly struct CalledMethodMetadata
    {
        internal readonly string Name = "GetterCalled";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata CountParameter;

        public CalledMethodMetadata()
        {
            ReturnType = WellKnownTypes.Void;
            CountParameter = new ParameterMetadata("count", WellKnownTypes.Imposter.Abstractions.Count);
        }
    }
}