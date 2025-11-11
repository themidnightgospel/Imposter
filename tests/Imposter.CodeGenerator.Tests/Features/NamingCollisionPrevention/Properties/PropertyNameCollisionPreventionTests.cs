using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Properties;

public class PropertyNameCollisionPreventionTests : PropertyNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task Given_PropertyBuilderOperationNames_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
using System;
using Imposter.Abstractions;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var interfaceImposter = new IPropertyBuilderOperationCollisionTargetImposter();
            interfaceImposter.Returns.Getter().Returns(1).Then().Returns(() => 2);

            interfaceImposter.Throws.Getter().Throws<InvalidOperationException>();
            interfaceImposter.Throws.Getter().Throws(new InvalidOperationException());

            interfaceImposter.Callback.Getter().Callback(() => { });

            interfaceImposter.Then.Getter().Returns(5).Then();
            interfaceImposter.Then.Setter(Arg<int>.Any()).Callback(_ => { }).Then();

            interfaceImposter.Called.Getter().Called(Count.AtLeast(1));
            interfaceImposter.Called.Setter(Arg<int>.Any()).Called(Count.AtLeast(1));

            var classImposter = new PropertyBuilderOperationClassCollisionTargetImposter();
            classImposter.UseBaseImplementation.Getter().Returns(10).Then().UseBaseImplementation();
            classImposter.UseBaseImplementation.Setter(Arg<int>.Any()).Then().UseBaseImplementation();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_ImposterMemberNamedProperties_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
using Imposter.Abstractions;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IPropertyImposterMemberCollisionTargetImposter();
            var instance = ((IHaveImposterInstance<IPropertyImposterMemberCollisionTarget>)imposter).Instance();
            _ = instance;

            imposter.Instance.Getter().Returns(new object());
            imposter.ImposterTargetInstance.Getter().Returns(new object());
            imposter._imposterInstance.Getter().Returns(new object());
            imposter._invocationBehavior.Getter().Returns("custom");
            imposter.InitializeOutParametersWithDefaultValues.Getter().Returns(42);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_PropertiesMatchingGeneratedFields_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
using Imposter.Abstractions;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IPropertyBackingFieldCollisionTargetImposter();
            imposter._defaultPropertyBehaviour.Getter().Returns(1);
            imposter._MyProperty.Getter().Returns(2);
            imposter.Prop.Getter().Returns(3);
            imposter.Prop.Setter(Arg<int>.Any()).Callback(_ => { });
            imposter._Prop.Getter().Returns(4);
            imposter._Prop.Setter(Arg<int>.Any()).Callback(_ => { });
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_CommonPropertyNames_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
using Imposter.Abstractions;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IPropertyCommonNameCollisionTargetImposter();
            imposter.Count.Getter().Returns(1);
            imposter.Count.Getter().Called(Count.AtLeast(1));
            imposter.Count.Setter(Arg<int>.Any()).Callback(_ => { });
            imposter.Default.Getter().Returns(2).Then().Returns(() => 3);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_ReusedPropertyNamesAcrossAccessors_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
using Imposter.Abstractions;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var getterOnly = new IPropertyDuplicateAccessorCollisionTargetImposter();
            getterOnly.Reused.Getter().Returns(1);

            var getterSetter = new IPropertyDuplicateAccessorWithSetterCollisionTargetImposter();
            getterSetter.Reused.Getter().Returns(2);
            getterSetter.Reused.Setter(Arg<int>.Any()).Callback(_ => { });
            getterSetter.Reused.Setter(Arg<int>.Any()).Called(Count.AtLeast(1));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }
}


