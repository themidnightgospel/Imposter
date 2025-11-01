using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.Features.Shared;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertySetup.Metadata;

internal readonly struct PropertyGetterImposterBuilderMetadata
{
    internal readonly string Name;

    internal readonly TypeSyntax TypeSyntax;

    internal readonly ReturnValuesFieldMetadata ReturnValuesField;

    internal readonly CallbacksFieldMetadata CallbacksField;

    internal readonly LastReturnValueFieldMetadata LastReturnValueField;

    internal readonly InvocationCountFieldMetadata InvocationCountField;

    internal readonly FieldMetadata DefaultPropertyBehaviourField;

    internal readonly AddReturnValueMethodMetadata AddReturnValueMethod;

    internal readonly GetMethodMetadata GetMethod;

    internal PropertyGetterImposterBuilderMetadata(in ImposterPropertyCoreMetadata property, in FieldMetadata defaultPropertyBehaviourMetadata)
    {
        Name = "GetterImposterBuilder";
        TypeSyntax = SyntaxFactory.ParseName(Name);
        ReturnValuesField = new ReturnValuesFieldMetadata(property);
        CallbacksField = new CallbacksFieldMetadata();
        LastReturnValueField = new LastReturnValueFieldMetadata(property);
        InvocationCountField = new InvocationCountFieldMetadata();
        DefaultPropertyBehaviourField = defaultPropertyBehaviourMetadata;
        AddReturnValueMethod = new AddReturnValueMethodMetadata(property);
        GetMethod = new GetMethodMetadata(property);
    }

    internal readonly struct ReturnValuesFieldMetadata
    {
        internal readonly string Name = "_returnValues";

        internal readonly TypeSyntax TypeSyntax;

        internal ReturnValuesFieldMetadata(in ImposterPropertyCoreMetadata property)
        {
            TypeSyntax = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
                WellKnownTypes.System.FuncOfT(property.TypeSyntax)
            );
        }
    }

    internal readonly struct CallbacksFieldMetadata
    {
        internal readonly string Name = "_callbacks";

        internal readonly TypeSyntax TypeSyntax;

        public CallbacksFieldMetadata()
        {
            TypeSyntax = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
                WellKnownTypes.System.Action
            );
        }
    }

    internal readonly struct LastReturnValueFieldMetadata
    {
        internal readonly string Name = "_lastReturnValue";

        internal readonly TypeSyntax TypeSyntax;

        internal LastReturnValueFieldMetadata(in ImposterPropertyCoreMetadata property)
        {
            TypeSyntax = WellKnownTypes.System.FuncOfT(property.TypeSyntax);
        }
    }

    internal readonly struct InvocationCountFieldMetadata
    {
        internal readonly string Name = "_invocationCount";

        internal readonly TypeSyntax TypeSyntax = WellKnownTypes.Int;

        public InvocationCountFieldMetadata()
        {
        }
    }

    internal readonly struct AddReturnValueMethodMetadata
    {
        internal readonly string Name = "AddReturnValue";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata ValueGeneratorParameter;

        internal AddReturnValueMethodMetadata(in ImposterPropertyCoreMetadata property)
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