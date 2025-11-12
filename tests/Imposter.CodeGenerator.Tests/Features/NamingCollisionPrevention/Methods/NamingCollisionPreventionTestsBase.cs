using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Methods;

public abstract class NamingCollisionPreventionTestsBase
{
    internal const string Source = /*lang=csharp*/"""
                                                 using System.Threading.Tasks;
                                                 using Imposter.Abstractions;

                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IMethodGroupedTypeCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IMethodOverloadCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IMethodOperationNameCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IMethodHelperNameCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IMethodImposterMemberCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IMethodParameterLocalNameCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IMethodParameterInvocationSetupCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IMethodParameterArgumentsCriteriaCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IMethodParameterHistoryCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IMethodParameterAdapterCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IMethodParameterBaseImplementationCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IMethodGenericTypeParameterCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IMethodGenericExceptionTypeParameterCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IMethodGenericResultTypeParameterCollisionTarget))]

                                                 namespace Sample.NamingCollision
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
                                                         void HasMatchingInvocationImposterGroup();
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
                                                 """;

    private const string BaseSourceFileName = "NamingCollisionGeneratorInput.cs";
    private const string SnippetFileName = "NamingCollisionSnippet.cs";

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(NamingCollisionPreventionTestsBase));

    protected static async Task<ImmutableArray<Diagnostic>> CompileSnippet(string snippet)
    {
        var context = await TestContextTask.ConfigureAwait(false);
        return context.CompileSnippet(snippet);
    }

    protected static void AssertNoDiagnostics(ImmutableArray<Diagnostic> diagnostics) =>
        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
}

