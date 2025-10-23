using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertySetup.Metadata;

internal readonly struct ImposterTargetPropertyMetadata
{
    internal readonly IPropertySymbol Symbol;

    internal readonly bool HasGetter;

    internal readonly bool HasSetter;

    internal readonly TypeSyntax TypeSyntax;

    internal readonly string UniqueName;

    internal readonly InterfaceMetadata Interface;

    internal readonly ReturnsMethodMetadata ReturnsMethod;

    internal readonly TypeSyntax AsSystemActionType;

    internal readonly TypeSyntax AsSystemFuncType;

    internal readonly TypeSyntax AsArgType;

    internal readonly SetterCallbackMethodMetadata SetterCallbackMethod;

    internal readonly GetterCallbackMethodMetadata GetterCallbackMethod;

    internal readonly AutoPropertyMethodMetadata AutoPropertyMethod;

    internal readonly GetterCalledMethodMetadata GetterCalledMethod;
    
    internal readonly SetterCalledMethodMetadata SetterCalledMethod;

    public ImposterTargetPropertyMetadata(IPropertySymbol property, string uniqueName)
    {
        UniqueName = uniqueName;
        Symbol = property;
        HasGetter = property.GetMethod != null;
        HasSetter = property.SetMethod != null;
        TypeSyntax = SyntaxFactoryHelper.TypeSyntax(property.Type);
        Interface = new InterfaceMetadata(this);

        AsSystemFuncType = WellKnownTypes.System.FuncOfT(TypeSyntax);
        AsSystemActionType = WellKnownTypes.System.ActionOfT(TypeSyntax);
        AsArgType = WellKnownTypes.Imposter.Abstractions.Arg(TypeSyntax);

        ReturnsMethod = new ReturnsMethodMetadata(this);
        SetterCallbackMethod = new SetterCallbackMethodMetadata(this);
        GetterCallbackMethod = new GetterCallbackMethodMetadata(this);
        AutoPropertyMethod = new AutoPropertyMethodMetadata(this);
        GetterCalledMethod = new GetterCalledMethodMetadata();
        SetterCalledMethod = new SetterCalledMethodMetadata(this);
    }
    
    internal readonly struct SetterCalledMethodMetadata
    {
        internal readonly string Name = "SetterCalled";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata CountParameter;
        
        internal readonly ParameterMetadata CriteriaParameter;

        public SetterCalledMethodMetadata(in ImposterTargetPropertyMetadata property)
        {
            ReturnType = WellKnownTypes.Void;
            CountParameter = new ParameterMetadata("count", WellKnownTypes.Imposter.Abstractions.Count);
            CriteriaParameter = new ParameterMetadata("criteria", property.AsArgType);
        }
    }

    internal readonly struct GetterCalledMethodMetadata
    {
        internal readonly string Name = "GetterCalled";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata CountParameter;

        public GetterCalledMethodMetadata()
        {
            ReturnType = WellKnownTypes.Void;
            CountParameter = new ParameterMetadata("count", WellKnownTypes.Imposter.Abstractions.Count);
        }
    }

    internal readonly struct AutoPropertyMethodMetadata
    {
        internal readonly string Name = "AutoProperty";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata InitialValueParameter;

        internal AutoPropertyMethodMetadata(in ImposterTargetPropertyMetadata property)
        {
            ReturnType = WellKnownTypes.Void;
            InitialValueParameter = new ParameterMetadata("initialValue", property.TypeSyntax, SyntaxFactoryHelper.Default);
        }
    }

    internal readonly struct GetterCallbackMethodMetadata
    {
        internal readonly string Name = "GetterCallback";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata CallbackParameter;

        internal GetterCallbackMethodMetadata(in ImposterTargetPropertyMetadata property)
        {
            ReturnType = property.TypeSyntax;
            CallbackParameter = new ParameterMetadata("callback", WellKnownTypes.System.Action);
        }
    }

    internal readonly struct SetterCallbackMethodMetadata
    {
        internal readonly string Name = "SetterCallback";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata CriteriaParameter;

        internal readonly ParameterMetadata CallbackParameter;

        internal SetterCallbackMethodMetadata(in ImposterTargetPropertyMetadata property)
        {
            ReturnType = property.TypeSyntax;
            CriteriaParameter = new ParameterMetadata("criteria", property.AsArgType);
            CallbackParameter = new ParameterMetadata("callback", property.AsSystemActionType);
        }
    }

    internal readonly struct ReturnsMethodMetadata
    {
        internal readonly string Name = "Returns";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata ValueParameter;

        internal readonly ParameterMetadata ValueGeneratorParameter;

        internal ReturnsMethodMetadata(in ImposterTargetPropertyMetadata property)
        {
            ReturnType = property.Interface.TypeSyntax;
            ValueParameter = new ParameterMetadata("value", property.TypeSyntax);
            ValueGeneratorParameter = new ParameterMetadata("valueGenerator", property.AsSystemFuncType);
        }
    }

    internal readonly struct InterfaceMetadata
    {
        internal readonly string Name;

        internal readonly TypeSyntax TypeSyntax;

        public InterfaceMetadata(in ImposterTargetPropertyMetadata property)
        {
            Name = $"I{property.UniqueName}Property";
            TypeSyntax = SyntaxFactory.ParseTypeName(Name);
        }
    }
}