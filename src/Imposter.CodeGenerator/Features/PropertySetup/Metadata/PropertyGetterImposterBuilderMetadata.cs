using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertySetup.Metadata;

internal readonly struct PropertyGetterImposterBuilderMetadata
{
    internal readonly string Name;

    internal readonly TypeSyntax TypeSyntax;

    internal readonly GetterReturnValuesFieldMetadata GetterReturnValuesField;

    internal readonly GetterCallbacksFieldMetadata GetterCallbacksField;

    internal readonly LastGetterReturnValueFieldMetadata LastGetterReturnValueField;

    internal readonly GetterInvocationCountFieldMetadata GetterInvocationCountField;

    internal readonly FieldMetadata DefaultPropertyBehaviourField;

    internal readonly AddGetterReturnValueMethodMetadata AddGetterReturnValueMethod;

    internal readonly GetMethodMetadata GetMethod;

    internal PropertyGetterImposterBuilderMetadata(in ImposterPropertyCoreMetadata property, in FieldMetadata defaultPropertyBehaviourMetadata)
    {
        Name = "GetterImposterBuilder";
        TypeSyntax = SyntaxFactory.ParseName(Name);
        GetterReturnValuesField = new GetterReturnValuesFieldMetadata(property);
        GetterCallbacksField = new GetterCallbacksFieldMetadata();
        LastGetterReturnValueField = new LastGetterReturnValueFieldMetadata(property);
        GetterInvocationCountField = new GetterInvocationCountFieldMetadata();
        DefaultPropertyBehaviourField = defaultPropertyBehaviourMetadata;
        AddGetterReturnValueMethod = new AddGetterReturnValueMethodMetadata(property);
        GetMethod = new GetMethodMetadata(property);
    }

    internal readonly struct GetterReturnValuesFieldMetadata
    {
        internal readonly string Name = "_getterReturnValues";

        internal readonly TypeSyntax TypeSyntax;

        internal GetterReturnValuesFieldMetadata(in ImposterPropertyCoreMetadata property)
        {
            TypeSyntax = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
                WellKnownTypes.System.FuncOfT(property.TypeSyntax)
            );
        }
    }

    internal readonly struct GetterCallbacksFieldMetadata
    {
        internal readonly string Name = "_getterCallbacks";

        internal readonly TypeSyntax TypeSyntax;

        public GetterCallbacksFieldMetadata()
        {
            TypeSyntax = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
                WellKnownTypes.System.Action
            );
        }
    }

    internal readonly struct LastGetterReturnValueFieldMetadata
    {
        internal readonly string Name = "_lastGetterReturnValue";

        internal readonly TypeSyntax TypeSyntax;

        internal LastGetterReturnValueFieldMetadata(in ImposterPropertyCoreMetadata property)
        {
            TypeSyntax = WellKnownTypes.System.FuncOfT(property.TypeSyntax);
        }
    }

    internal readonly struct GetterInvocationCountFieldMetadata
    {
        internal readonly string Name = "_getterInvocationCount";

        internal readonly TypeSyntax TypeSyntax = WellKnownTypes.Int;

        public GetterInvocationCountFieldMetadata()
        {
        }
    }

    internal readonly struct AddGetterReturnValueMethodMetadata
    {
        internal readonly string Name = "AddGetterReturnValue";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata ValueGeneratorParameter;

        internal AddGetterReturnValueMethodMetadata(in ImposterPropertyCoreMetadata property)
        {
            ReturnType = WellKnownTypes.Void;
            ValueGeneratorParameter = new ParameterMetadata("valueGenerator",
                WellKnownTypes.System.FuncOfT(property.TypeSyntax));
        }
    }

    internal readonly struct GetMethodMetadata
    {
        internal readonly string Name = "Get";

        internal readonly TypeSyntax ReturnType;

        internal GetMethodMetadata(in ImposterPropertyCoreMetadata property)
        {
            ReturnType = property.TypeSyntax;
        }
    }
}