using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.IsAssignableToTests;

[Subject(nameof(TypeCriteria))]
public class IsAssignableToContravariantDerivedCriteria
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () =>
        typeCriteria = TypeCriteria.Create<ArgType.IsAssignableTo<IContravariant<DerivedClass>>>();

    It ShouldMatchWithContravariance = () =>
        typeCriteria.Matches(typeof(IContravariant<BaseClass>)).ShouldBeTrue();

    public interface IContravariant<in T>
    {
    }

    public class BaseClass
    {
    }

    public class DerivedClass : BaseClass
    {
    }
}