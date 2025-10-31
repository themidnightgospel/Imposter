using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertySetup.Metadata;

internal readonly struct PropertySetterImposterMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly SetterCallbacksFieldMetadata SetterCallbacksField;

    internal readonly FieldMetadata SetterInvocationHistoryField;

    internal readonly FieldMetadata DefaultPropertyBehaviourField;
    
    internal readonly CallbackMethodMetadata CallbackMethod;
    
    internal readonly CalledMethodMetadata CalledMethod;

    internal readonly SetMethodMetadata SetMethod;

    internal readonly BuilderMetadata Builder;

    public PropertySetterImposterMetadata(in ImposterPropertyCoreMetadata property, in FieldMetadata defaultPropertyBehaviourMetadata)
    {
        Name = "SetterImposter";
        TypeSyntax = SyntaxFactory.ParseName(Name);
        SetterCallbacksField = new SetterCallbacksFieldMetadata(property);
        SetterInvocationHistoryField = new FieldMetadata("_setterInvocationHistory", WellKnownTypes.System.Collections.Concurrent.ConcurrentBag(property.TypeSyntax));
        DefaultPropertyBehaviourField = defaultPropertyBehaviourMetadata;
        CallbackMethod = new CallbackMethodMetadata(property);
        CalledMethod = new CalledMethodMetadata(property);
        SetMethod = new SetMethodMetadata(property);
        Builder = new BuilderMetadata(property, TypeSyntax);
    }

    internal readonly struct BuilderMetadata
    {
        internal readonly string Name = "Builder";

        internal readonly NameSyntax TypeSyntax;

        internal readonly FieldMetadata SetterImposterField;

        internal readonly FieldMetadata CriteriaField;

        public BuilderMetadata(in ImposterPropertyCoreMetadata property, NameSyntax setterImposterTypeSyntax)
        {
            TypeSyntax = SyntaxFactory.QualifiedName(setterImposterTypeSyntax, SyntaxFactory.IdentifierName(Name));
            SetterImposterField = new FieldMetadata("_setterImposter", setterImposterTypeSyntax);
            CriteriaField = new FieldMetadata("_criteria", property.AsArgType);
        }
    }

    internal readonly struct SetterCallbacksFieldMetadata : IFieldMetadata
    {
        public string Name => "_setterCallbacks";

        public TypeSyntax Type { get; }

        internal readonly TypeSyntax TupleTypeSyntax;

        internal SetterCallbacksFieldMetadata(in ImposterPropertyCoreMetadata property)
        {
            Type = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
                TupleTypeSyntax = WellKnownTypes.System.Tuple(
                    property.AsArgType,
                    WellKnownTypes.System.ActionOfT(property.TypeSyntax)
                )
            );
        }
    }

    internal readonly struct SetMethodMetadata
    {
        internal readonly string Name = "Set";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata ValueParameter;

        internal SetMethodMetadata(in ImposterPropertyCoreMetadata property)
        {
            ReturnType = WellKnownTypes.Void;
            ValueParameter = new ParameterMetadata("value", property.TypeSyntax);
        }
    }
    
    internal readonly struct CallbackMethodMetadata
    {
        internal readonly string Name = "Callback";

        internal readonly TypeSyntax ReturnType;
        
        internal readonly ParameterMetadata CriteriaParameter;

        internal readonly ParameterMetadata CallbackParameter;

        internal CallbackMethodMetadata(in ImposterPropertyCoreMetadata property)
        {
            ReturnType = WellKnownTypes.Void;
            CallbackParameter = new ParameterMetadata("callback", WellKnownTypes.System.ActionOfT(property.TypeSyntax));
            CriteriaParameter = new ParameterMetadata("criteria", property.AsArgType);
        }
    }
    
    internal readonly struct CalledMethodMetadata
    {
        internal readonly string Name = "Called";

        internal readonly TypeSyntax ReturnType;
        
        internal readonly ParameterMetadata CriteriaParameter;

        internal readonly ParameterMetadata CountParameter;
        
        internal readonly string SetterInvocationCountVariableName = "setterInvocationCount";

        internal CalledMethodMetadata(in ImposterPropertyCoreMetadata property)
        {
            ReturnType = WellKnownTypes.Void;
            CriteriaParameter = new ParameterMetadata("criteria", property.AsArgType);
            CountParameter = new ParameterMetadata("count", WellKnownTypes.Imposter.Abstractions.Count);
        }
    }
}