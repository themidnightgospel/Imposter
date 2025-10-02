using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.IsAssignableToTests;

[Subject(nameof(TypeCriteria))]
public class IsAssignableToContravariantBaseCriteria
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () =>
        typeCriteria = TypeCriteria.Create<ArgType.IsAssignableTo<IContravariant<BaseClass>>>();

    It ShouldNotMatchInverseContravariance = () =>
        typeCriteria.Matches(typeof(IContravariant<DerivedClass>)).ShouldBeFalse();

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