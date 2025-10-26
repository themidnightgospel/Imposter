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

    internal readonly TypeSyntax AsSystemActionType;

    internal readonly TypeSyntax AsSystemFuncType;

    internal readonly TypeSyntax AsArgType;

    internal readonly PropertyImposterInterfaceMetadata PropertyImposterInterface;

    internal readonly PropertyImposterMetadata PropertyImposter;

    internal readonly ReturnsMethodMetadata ReturnsMethod;

    internal readonly ThrowsMethodMetadata ThrowsMethod;

    internal readonly SetterCallbackMethodMetadata SetterCallbackMethod;

    internal readonly GetterCallbackMethodMetadata GetterCallbackMethod;

    internal readonly GetterCalledMethodMetadata GetterCalledMethod;

    internal readonly SetterCalledMethodMetadata SetterCalledMethod;

    internal readonly GetterReturnValuesFieldMetadata GetterReturnValuesField;

    internal readonly GetterCallbacksFieldMetadata GetterCallbacksField;

    internal readonly LastGetterReturnValueFieldMetadata LastGetterReturnValueField;

    internal readonly GetterInvocationCountFieldMetadata GetterInvocationCountField;

    internal readonly SetterCallbacksFieldMetadata SetterCallbacksField;

    internal readonly SetterInvocationHistoryFieldMetadata SetterInvocationHistoryField;

    internal readonly UseAutoPropertyBehaviourFieldMetadata UseAutoPropertyBehaviourField;

    internal readonly BackingFieldFieldMetadata BackingFieldField;

    internal readonly AddGetterReturnValueMethodMetadata AddGetterReturnValueMethod;

    internal readonly GetMethodMetadata GetMethod;

    internal readonly SetMethodMetadata SetMethod;

    internal readonly AsFieldMetadata AsField;

    public ImposterTargetPropertyMetadata(IPropertySymbol property, string uniqueName)
    {
        UniqueName = uniqueName;
        Symbol = property;
        HasGetter = property.GetMethod != null;
        HasSetter = property.SetMethod != null;
        TypeSyntax = SyntaxFactoryHelper.TypeSyntax(property.Type);
        PropertyImposterInterface = new PropertyImposterInterfaceMetadata(this);
        PropertyImposter = new PropertyImposterMetadata(this);

        AsSystemFuncType = WellKnownTypes.System.FuncOfT(TypeSyntax);
        AsSystemActionType = WellKnownTypes.System.ActionOfT(TypeSyntax);
        AsArgType = WellKnownTypes.Imposter.Abstractions.Arg(TypeSyntax);

        ReturnsMethod = new ReturnsMethodMetadata(this);
        ThrowsMethod = new ThrowsMethodMetadata(this);
        SetterCallbackMethod = new SetterCallbackMethodMetadata(this);
        GetterCallbackMethod = new GetterCallbackMethodMetadata(this);
        GetterCalledMethod = new GetterCalledMethodMetadata();
        SetterCalledMethod = new SetterCalledMethodMetadata(this);
        GetterReturnValuesField = new GetterReturnValuesFieldMetadata(this);
        GetterCallbacksField = new GetterCallbacksFieldMetadata(this);
        LastGetterReturnValueField = new LastGetterReturnValueFieldMetadata(this);
        GetterInvocationCountField = new GetterInvocationCountFieldMetadata(this);
        SetterCallbacksField = new SetterCallbacksFieldMetadata(this);
        SetterInvocationHistoryField = new SetterInvocationHistoryFieldMetadata(this);
        UseAutoPropertyBehaviourField = new UseAutoPropertyBehaviourFieldMetadata(this);
        BackingFieldField = new BackingFieldFieldMetadata(this);
        AddGetterReturnValueMethod = new AddGetterReturnValueMethodMetadata(this);
        GetMethod = new GetMethodMetadata(this);
        SetMethod = new SetMethodMetadata(this);
        AsField = new AsFieldMetadata(this);
    }

    internal readonly struct AsFieldMetadata
    {
        internal readonly string Name;

        public AsFieldMetadata(in ImposterTargetPropertyMetadata property)
        {
            Name = $"_{property.UniqueName}";
        }
    }

    internal readonly struct SetMethodMetadata
    {
        internal readonly string Name = "Set";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata ValueParameter;

        internal SetMethodMetadata(in ImposterTargetPropertyMetadata property)
        {
            ReturnType = WellKnownTypes.Void;
            ValueParameter = new ParameterMetadata("value", property.TypeSyntax);
        }
    }

    internal readonly struct GetMethodMetadata
    {
        internal readonly string Name = "Get";

        internal readonly TypeSyntax ReturnType;

        internal GetMethodMetadata(in ImposterTargetPropertyMetadata property)
        {
            ReturnType = property.TypeSyntax;
        }
    }

    internal readonly struct AddGetterReturnValueMethodMetadata
    {
        internal readonly string Name = "AddGetterReturnValue";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata ValueGeneratorParameter;

        internal AddGetterReturnValueMethodMetadata(in ImposterTargetPropertyMetadata property)
        {
            ReturnType = WellKnownTypes.Void;
            ValueGeneratorParameter = new ParameterMetadata("valueGenerator",
                WellKnownTypes.System.FuncOfT(property.TypeSyntax));
        }
    }

    internal readonly struct GetterCallbacksFieldMetadata
    {
        internal readonly string Name = "_getterCallbacks";

        internal readonly TypeSyntax TypeSyntax;

        internal GetterCallbacksFieldMetadata(in ImposterTargetPropertyMetadata property)
        {
            TypeSyntax = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
                WellKnownTypes.System.Action
            );
        }
    }

    internal readonly struct BackingFieldFieldMetadata
    {
        internal readonly string Name = "_backingField";

        internal readonly TypeSyntax TypeSyntax;

        internal BackingFieldFieldMetadata(in ImposterTargetPropertyMetadata property)
        {
            TypeSyntax = property.TypeSyntax;
        }
    }

    internal readonly struct UseAutoPropertyBehaviourFieldMetadata
    {
        internal readonly string Name = "_useAutoPropertyBehaviour";

        internal readonly TypeSyntax TypeSyntax;

        internal UseAutoPropertyBehaviourFieldMetadata(in ImposterTargetPropertyMetadata property)
        {
            TypeSyntax = WellKnownTypes.Bool;
        }
    }

    internal readonly struct SetterInvocationHistoryFieldMetadata
    {
        internal readonly string Name = "_setterInvocationHistory";

        internal readonly TypeSyntax TypeSyntax;

        internal SetterInvocationHistoryFieldMetadata(in ImposterTargetPropertyMetadata property)
        {
            TypeSyntax = WellKnownTypes.System.Collections.Concurrent.ConcurrentBag(property.TypeSyntax);
        }
    }

    internal readonly struct SetterCallbacksFieldMetadata
    {
        internal readonly string Name = "_setterCallbacks";

        internal readonly TypeSyntax TypeSyntax;

        internal readonly TypeSyntax TupleTypeSyntax;

        internal SetterCallbacksFieldMetadata(in ImposterTargetPropertyMetadata property)
        {
            TypeSyntax = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
                TupleTypeSyntax = WellKnownTypes.System.Tuple(
                    property.AsArgType,
                    WellKnownTypes.System.ActionOfT(property.TypeSyntax)
                )
            );
        }
    }

    internal readonly struct GetterInvocationCountFieldMetadata
    {
        internal readonly string Name = "_getterInvocationCount";

        internal readonly TypeSyntax TypeSyntax;

        internal GetterInvocationCountFieldMetadata(in ImposterTargetPropertyMetadata property)
        {
            TypeSyntax = WellKnownTypes.Int;
        }
    }

    internal readonly struct LastGetterReturnValueFieldMetadata
    {
        internal readonly string Name = "_lastGetterReturnValue";

        internal readonly TypeSyntax TypeSyntax;

        internal LastGetterReturnValueFieldMetadata(in ImposterTargetPropertyMetadata property)
        {
            TypeSyntax = WellKnownTypes.System.FuncOfT(property.TypeSyntax);
        }
    }

    internal readonly struct GetterReturnValuesFieldMetadata
    {
        internal readonly string Name = "_getterReturnValues";

        internal readonly TypeSyntax TypeSyntax;

        internal GetterReturnValuesFieldMetadata(in ImposterTargetPropertyMetadata property)
        {
            TypeSyntax = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
                WellKnownTypes.System.FuncOfT(property.TypeSyntax)
            );
        }
    }

    internal readonly struct SetterCalledMethodMetadata
    {
        internal readonly string Name = "SetterCalled";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata CountParameter;

        internal readonly string SetterInvocationCountVariableName = "setterInvocationCount";

        internal readonly ParameterMetadata CriteriaParameter;

        internal SetterCalledMethodMetadata(in ImposterTargetPropertyMetadata property)
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

    internal readonly struct GetterCallbackMethodMetadata
    {
        internal readonly string Name = "GetterCallback";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata CallbackParameter;

        internal GetterCallbackMethodMetadata(in ImposterTargetPropertyMetadata property)
        {
            ReturnType = property.PropertyImposterInterface.Syntax;
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
            ReturnType = property.PropertyImposterInterface.Syntax;
            CriteriaParameter = new ParameterMetadata("criteria", property.AsArgType);
            CallbackParameter = new ParameterMetadata("callback", property.AsSystemActionType);
        }
    }

    internal readonly struct ThrowsMethodMetadata
    {
        internal readonly string Name = "Throws";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata ExceptionParameter;

        internal ThrowsMethodMetadata(in ImposterTargetPropertyMetadata property)
        {
            ReturnType = property.PropertyImposterInterface.Syntax;
            ExceptionParameter = new ParameterMetadata("exception", WellKnownTypes.System.Exception);
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
            ReturnType = property.PropertyImposterInterface.Syntax;
            ValueParameter = new ParameterMetadata("value", property.TypeSyntax);
            ValueGeneratorParameter = new ParameterMetadata("valueGenerator", property.AsSystemFuncType);
        }
    }

    internal readonly struct PropertyImposterInterfaceMetadata
    {
        internal readonly string Name;

        internal readonly NameSyntax Syntax;

        internal PropertyImposterInterfaceMetadata(in ImposterTargetPropertyMetadata property)
        {
            Name = $"I{property.UniqueName}Property";
            Syntax = SyntaxFactory.ParseName(Name);
        }
    }

    internal readonly struct PropertyImposterMetadata
    {
        internal readonly string Name;

        internal readonly TypeSyntax Syntax;

        internal PropertyImposterMetadata(in ImposterTargetPropertyMetadata property)
        {
            Name = $"{property.UniqueName}Property";
            Syntax = SyntaxFactory.ParseName(Name);
        }
    }
}