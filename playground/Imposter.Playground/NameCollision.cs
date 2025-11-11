using System;
using Imposter.Abstractions;
using Imposter.Playground;

[assembly: GenerateImposter(typeof(IMethodGroupedTypeCollisionTarget))]
[assembly: GenerateImposter(typeof(IMethodOverloadCollisionTarget))]
[assembly: GenerateImposter(typeof(IMethodOperationNameCollisionTarget))]
[assembly: GenerateImposter(typeof(IMethodHelperNameCollisionTarget))]
[assembly: GenerateImposter(typeof(IMethodImposterMemberCollisionTarget))]
[assembly: GenerateImposter(typeof(IMethodParameterLocalNameCollisionTarget))]
[assembly: GenerateImposter(typeof(IMethodParameterInvocationSetupCollisionTarget))]
[assembly: GenerateImposter(typeof(IMethodParameterArgumentsCriteriaCollisionTarget))]
[assembly: GenerateImposter(typeof(IMethodParameterHistoryCollisionTarget))]
[assembly: GenerateImposter(typeof(IMethodParameterAdapterCollisionTarget))]
[assembly: GenerateImposter(typeof(IMethodParameterBaseImplementationCollisionTarget))]
[assembly: GenerateImposter(typeof(IMethodGenericTypeParameterCollisionTarget))]
[assembly: GenerateImposter(typeof(IMethodGenericExceptionTypeParameterCollisionTarget))]
[assembly: GenerateImposter(typeof(IMethodGenericResultTypeParameterCollisionTarget))]


[assembly: GenerateImposter(typeof(Sample.NamingCollision.IPropertyBuilderOperationCollisionTarget))]
[assembly: GenerateImposter(typeof(Sample.NamingCollision.PropertyBuilderOperationClassCollisionTarget))]
[assembly: GenerateImposter(typeof(Sample.NamingCollision.IPropertyImposterMemberCollisionTarget))]
[assembly: GenerateImposter(typeof(Sample.NamingCollision.IPropertyBackingFieldCollisionTarget))]
[assembly: GenerateImposter(typeof(Sample.NamingCollision.IPropertyGetterBuilderNameCollisionTarget))]
[assembly: GenerateImposter(typeof(Sample.NamingCollision.PropertyGetterBuilderClassCollisionTarget))]
[assembly: GenerateImposter(typeof(Sample.NamingCollision.IPropertySetterBuilderNameCollisionTarget))]
[assembly: GenerateImposter(typeof(Sample.NamingCollision.PropertySetterBuilderClassCollisionTarget))]
[assembly: GenerateImposter(typeof(Sample.NamingCollision.IPropertyBuilderParameterCollisionTarget))]
[assembly: GenerateImposter(typeof(Sample.NamingCollision.IPropertyCommonNameCollisionTarget))]
[assembly: GenerateImposter(typeof(Sample.NamingCollision.IPropertyDuplicateAccessorCollisionTarget))]
[assembly: GenerateImposter(typeof(Sample.NamingCollision.IPropertyDuplicateAccessorWithSetterCollisionTarget))]
[assembly: GenerateImposter(typeof(Sample.NamingCollision.PropertyUseBaseImplementationGetterCollisionTarget))]
[assembly: GenerateImposter(typeof(Sample.NamingCollision.PropertyUseBaseImplementationSetterCollisionTarget))]
[assembly: GenerateImposter(typeof(Sample.NamingCollision.PropertyUseBaseImplementationCombinedCollisionTarget))]

namespace Imposter.Playground
{
    public interface IMethodGroupedTypeCollisionTarget
    {
        void MethodImposter(int value);
        void MethodInvocationImposterGroup(string label);
        void MethodInvocationHistory();
        void ArgumentsCriteria(int left, int right);
        void Arguments(string label);
        void Delegate(int value);
        void CallbackDelegate(int first, int second);
        void ExceptionGeneratorDelegate(System.Exception error);
        void Collection(int count);
    }

    public interface IMethodOverloadCollisionTarget
    {
        int Duplicate();
        int Duplicate(int value);
        int Duplicate(string name, int value);
    }

    public interface IMethodOperationNameCollisionTarget
    {
        int Returns();
        System.Threading.Tasks.Task<int> ReturnsAsync();
        void Throws();
        System.Threading.Tasks.Task ThrowsAsync();
        void Callback();
        void Then();
        void UseBaseImplementation();
        int DefaultResultGenerator();
        int Default();
    }

    public interface IMethodHelperNameCollisionTarget
    {
        void Adapter();
        void HasMatchingSetup();
        void As(string hint);
        void Invoke(int value);
    }

    public interface IMethodImposterMemberCollisionTarget
    {
        void Instance();
        void ImposterTargetInstance();
        void _imposterInstance();
        void _invocationBehavior();
        void InitializeOutParametersWithDefaultValues(ref int seed, out int doubled);
    }

    public interface IMethodParameterLocalNameCollisionTarget
    {
        int InvokeWithLocals(
            int ex,
            int result,
            int matchingInvocationImposterGroup,
            int arguments,
            int baseImplementation);
    }

    public interface IMethodParameterInvocationSetupCollisionTarget
    {
        int SetupNames(
            int value,
            System.Func<int> resultGenerator,
            System.Exception exception,
            System.Func<System.Exception> exceptionGenerator,
            System.Action callback);
    }

    public interface IMethodParameterArgumentsCriteriaCollisionTarget
    {
        void CriteriaNames(int As, int Matches);
    }

    public interface IMethodParameterHistoryCollisionTarget
    {
        void HistoryNames(ref int Arguments, out int Result, out System.Exception Exception);
    }

    public interface IMethodParameterAdapterCollisionTarget
    {
        int AdapterNames(int result, string arguments, object target, object _target);
    }

    public interface IMethodParameterBaseImplementationCollisionTarget
    {
        int BaseImplementationName(int baseImplementation);
    }

    public interface IMethodGenericTypeParameterCollisionTarget
    {
        void GenericMethod<T, U, TTarget, UTarget>(T value, U other, TTarget target, UTarget otherTarget);
    }

    public interface IMethodGenericExceptionTypeParameterCollisionTarget
    {
        void ThrowsGeneric<TException>() where TException : System.Exception;
    }

    public interface IMethodGenericResultTypeParameterCollisionTarget
    {
        TResult ReturnsGeneric<TResult>(TResult value);
        TResult ReturnsWithGenerator<TResult>(System.Func<TResult> defaultResultGenerator);
    }

}

namespace Sample.NamingCollision
{
    public interface IPropertyBuilderOperationCollisionTarget
    {
        int Returns { get; set; }
        int Throws { get; set; }
        int Callback { get; set; }
        int Then { get; set; }
        int Called { get; set; }
    }

    public class PropertyBuilderOperationClassCollisionTarget
    {
        public virtual int UseBaseImplementation { get; set; }
    }

    public interface IPropertyImposterMemberCollisionTarget
    {
        object Instance { get; set; }
        object ImposterTargetInstance { get; set; }
        object _imposterInstance { get; set; }
        string _invocationBehavior { get; set; }
        int InitializeOutParametersWithDefaultValues { get; set; }
    }

    public interface IPropertyBackingFieldCollisionTarget
    {
        int _defaultPropertyBehaviour { get; set; }
        int _MyProperty { get; set; }
        int Prop { get; set; }
        int _Prop { get; set; }
    }

    public interface IPropertyGetterBuilderNameCollisionTarget
    {
        int IWeirdPropertyGetterBuilder { get; set; }
        int IWeirdPropertyGetterOutcomeBuilder { get; set; }
        int IWeirdPropertyGetterContinuationBuilder { get; set; }
        int IWeirdPropertyGetterCallbackBuilder { get; set; }
        int IWeirdPropertyGetterVerifier { get; set; }
        int IWeirdPropertyGetterFluentBuilder { get; set; }
    }

    public class PropertyGetterBuilderClassCollisionTarget
    {
        public virtual int IWeirdPropertyGetterUseBaseImplementationBuilder { get; set; }
    }

    public interface IPropertySetterBuilderNameCollisionTarget
    {
        int IWeirdPropertySetterBuilder { get; set; }
        int IWeirdPropertySetterFluentBuilder { get; set; }
        int IWeirdPropertySetterCallbackBuilder { get; set; }
        int IWeirdPropertySetterContinuationBuilder { get; set; }
        int IWeirdPropertySetterVerifier { get; set; }
    }

    public class PropertySetterBuilderClassCollisionTarget
    {
        public virtual int IWeirdPropertySetterUseBaseImplementationBuilder { get; set; }
    }

    public interface IPropertyBuilderParameterCollisionTarget
    {
        int value { get; set; }
        int valueGenerator { get; set; }
        int exception { get; set; }
        int callback { get; set; }
    }

    public interface IPropertyCommonNameCollisionTarget
    {
        int Count { get; set; }
        int Default { get; set; }
    }

    public interface IPropertyDuplicateAccessorCollisionTarget
    {
        int Reused { get; }
    }

    public interface IPropertyDuplicateAccessorWithSetterCollisionTarget
    {
        int Reused { get; set; }
    }

    public class PropertyUseBaseImplementationGetterCollisionTarget
    {
        public virtual int UseBaseImplementation
        {
            get => 0;
        }
    }

    public class PropertyUseBaseImplementationSetterCollisionTarget
    {
        public virtual int UseBaseImplementation { get; set; }
    }

    public class PropertyUseBaseImplementationCombinedCollisionTarget
    {
        public virtual int Then { get; set; }
        public virtual int UseBaseImplementation { get; set; }
    }
    
    public class test
    {
        static void A()
        {
            var imposter = new IMethodParameterInvocationSetupCollisionTargetImposter();
            var value = imposter.SetupNames(
                value: 1,
                resultGenerator: Arg<Func<int>>.Any(), 
                exception: new InvalidOperationException(),
                exceptionGenerator: Arg<Func<Exception>>.Any(), 
                callback: Arg<Action>.Any());

            _ = value;
        }
    }
}