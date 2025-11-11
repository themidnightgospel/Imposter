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

    public class test
    {
        static void A()
        {
            var imposter = new IMethodGenericResultTypeParameterCollisionTargetImposter();
            Func<int> defaultResultGenerator = () => 10;
            var valueSetup = imposter.ReturnsGeneric<int>(5);
            var otherSetup = imposter.ReturnsWithGenerator<int>(defaultResultGenerator);
            valueSetup.Returns(5);
            otherSetup.Returns(10);

        }
    }
}