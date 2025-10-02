using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.IsCriteriaTests;

[Subject(nameof(TypeCriteria))]
public class IsMultiArgumentGenericTypeArgument
{
    public static TypeCriteria typeCriteria;

    Because TypeCriteriaCreated = () => 
        typeCriteria = TypeCriteria.Create<ICurrentInterface<Dictionary<IDisposable, ArgType.Is<int>>>>();

    It ShouldMatchWhenKeyAndValueAreExact = () => 
        typeCriteria.Matches(typeof(ICurrentInterface<Dictionary<IDisposable, int>>)).ShouldBeTrue();
    
    It ShouldFailWhenKeyIsWrongType = () => 
        typeCriteria.Matches(typeof(ICurrentInterface<Dictionary<BaseClass, int>>)).ShouldBeFalse();

    It ShouldFailWhenValueIsWrongType = () => 
        typeCriteria.Matches(typeof(ICurrentInterface<Dictionary<IDisposable, string>>)).ShouldBeFalse();
    
    public class BaseClass
    {
    }

    public class DerivedClass : BaseClass, IDisposable
    {
        public void Dispose()
        {
        }
    }

    public class UnrelatedClass
    {
    }
    
    public interface ICurrentInterface<T>
    {
    }

}
