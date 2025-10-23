
using Imposter.Abstractions;
using Shouldly;
using System;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Shared;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodSetup.Throw
{
    public class ThrowTests
    {
        private readonly IMethodSetupFeatureSutImposter _sut = new IMethodSetupFeatureSutImposter();

        #region VoidNoParams Tests

        [Fact]
        public void VoidNoParams_WhenSetupToThrowGenericException_ThrowsException()
        {
            _sut
                .VoidNoParams()
                .Throws<InvalidOperationException>();

            Should.Throw<InvalidOperationException>(() => _sut.Instance().VoidNoParams());
        }

        [Fact]
        public void VoidNoParams_WhenSetupToThrowSpecificException_ThrowsException()
        {
            var expectedException = new ArgumentException("Test message");
            
            _sut
                .VoidNoParams()
                .Throws(expectedException);

            var thrownException = Should.Throw<ArgumentException>(() => _sut.Instance().VoidNoParams());
            thrownException.ShouldBe(expectedException);
            thrownException.Message.ShouldBe("Test message");
        }

        #endregion

        #region IntNoParams Tests

        [Fact]
        public void IntNoParams_WhenSetupToThrowGenericException_ThrowsException()
        {
            _sut
                .IntNoParams()
                .Throws<NotImplementedException>();

            Should.Throw<NotImplementedException>(() => _sut.Instance().IntNoParams());
        }

        [Fact]
        public void IntNoParams_WhenSetupToThrowSpecificException_ThrowsException()
        {
            var expectedException = new InvalidOperationException("Custom error");
            
            _sut
                .IntNoParams()
                .Throws(expectedException);

            var thrownException = Should.Throw<InvalidOperationException>(() => _sut.Instance().IntNoParams());
            thrownException.ShouldBe(expectedException);
            thrownException.Message.ShouldBe("Custom error");
        }

        [Fact]
        public void IntNoParams_WhenSetupToThrowAfterReturns_ThrowsOnSecondCall()
        {
            _sut
                .IntNoParams()
                .Returns(42)
                .Throws<ArgumentException>();

            _sut.Instance().IntNoParams().ShouldBe(42);
            Should.Throw<ArgumentException>(() => _sut.Instance().IntNoParams());
        }

        #endregion

        #region IntSingleParam Tests

        [Fact]
        public void IntSingleParam_WhenSetupToThrowGenericException_ThrowsException()
        {
            _sut
                .IntSingleParam(Arg<int>.Is(42))
                .Throws<DivideByZeroException>();

            Should.Throw<DivideByZeroException>(() => _sut.Instance().IntSingleParam(42));
        }

        [Fact]
        public void IntSingleParam_WhenSetupToThrowSpecificException_ThrowsException()
        {
            var expectedException = new ArgumentOutOfRangeException("age", "Age cannot be negative");
            
            _sut
                .IntSingleParam(Arg<int>.Is(x => x < 0))
                .Throws(expectedException);

            var thrownException = Should.Throw<ArgumentOutOfRangeException>(() => _sut.Instance().IntSingleParam(-1));
            thrownException.ShouldBe(expectedException);
            thrownException.ParamName.ShouldBe("age");
        }

        [Fact]
        public void IntSingleParam_WhenSetupToThrowWithAnyArg_ThrowsForAnyValue()
        {
            _sut
                .IntSingleParam(Arg<int>.Any())
                .Throws<NotSupportedException>();

            Should.Throw<NotSupportedException>(() => _sut.Instance().IntSingleParam(10));
            Should.Throw<NotSupportedException>(() => _sut.Instance().IntSingleParam(100));
            Should.Throw<NotSupportedException>(() => _sut.Instance().IntSingleParam(-5));
        }

        [Fact]
        public void IntSingleParam_WhenSetupToThrowWithPredicate_ThrowsOnlyForMatchingValues()
        {
            _sut
                .IntSingleParam(Arg<int>.Is(x => x > 100))
                .Throws<OverflowException>();

            Should.Throw<OverflowException>(() => _sut.Instance().IntSingleParam(101));
            Should.Throw<OverflowException>(() => _sut.Instance().IntSingleParam(1000));
            
            // Should not throw for non-matching values
            _sut.Instance().IntSingleParam(50).ShouldBe(default);
            _sut.Instance().IntSingleParam(100).ShouldBe(default);
        }

        #endregion

        #region IntParams Tests

        [Fact]
        public void IntParams_WhenSetupToThrowGenericException_ThrowsException()
        {
            _sut
                .IntParams(Arg<int>.Any(), Arg<string>.Any(), Arg<Regex>.Any())
                .Throws<InvalidDataException>();

            Should.Throw<InvalidDataException>(() => 
                _sut.Instance().IntParams(1, "test", new Regex(".*")));
        }

        [Fact]
        public void IntParams_WhenSetupToThrowSpecificException_ThrowsException()
        {
            var expectedException = new FormatException("Invalid format provided");
            
            _sut
                .IntParams(Arg<int>.Is(0), Arg<string>.Any(), Arg<Regex>.Any())
                .Throws(expectedException);

            var thrownException = Should.Throw<FormatException>(() => 
                _sut.Instance().IntParams(0, "invalid", new Regex("test")));
            thrownException.ShouldBe(expectedException);
        }

        [Fact]
        public void IntParams_WhenSetupToThrowWithSpecificArgs_ThrowsOnlyForMatchingArgs()
        {
            _sut
                .IntParams(Arg<int>.Is(999), Arg<string>.Is("error"), Arg<Regex>.Any())
                .Throws<ApplicationException>();

            Should.Throw<ApplicationException>(() => 
                _sut.Instance().IntParams(999, "error", new Regex(".*")));
            
            // Should not throw for non-matching args
            _sut.Instance().IntParams(999, "success", new Regex(".*")).ShouldBe(default);
            _sut.Instance().IntParams(1, "error", new Regex(".*")).ShouldBe(default);
        }

        #endregion

        #region IntOutParam Tests

        [Fact]
        public void IntOutParam_WhenSetupToThrowGenericException_ThrowsException()
        {
            _sut
                .IntOutParam(OutArg<int>.Any())
                .Throws<UnauthorizedAccessException>();

            Should.Throw<UnauthorizedAccessException>(() => _sut.Instance().IntOutParam(out var outVal));
        }

        [Fact]
        public void IntOutParam_WhenSetupToThrowSpecificException_ThrowsException()
        {
            var expectedException = new TimeoutException("Operation timed out");
            
            _sut
                .IntOutParam(OutArg<int>.Any())
                .Throws(expectedException);

            var thrownException = Should.Throw<TimeoutException>(() => _sut.Instance().IntOutParam(out var outVal));
            thrownException.ShouldBe(expectedException);
            thrownException.Message.ShouldBe("Operation timed out");
        }

        #endregion

        #region IntRefParam Tests

        [Fact]
        public void IntRefParam_WhenSetupToThrowGenericException_ThrowsException()
        {
            _sut
                .IntRefParam(Arg<int>.Any())
                .Throws<NullReferenceException>();

            var refValue = 42;
            Should.Throw<NullReferenceException>(() => _sut.Instance().IntRefParam(ref refValue));
        }

        [Fact]
        public void IntRefParam_WhenSetupToThrowSpecificException_ThrowsException()
        {
            var expectedException = new InvalidCastException("Cannot cast value");
            
            _sut
                .IntRefParam(Arg<int>.Is(x => x < 0))
                .Throws(expectedException);

            var refValue = -10;
            var thrownException = Should.Throw<InvalidCastException>(() => _sut.Instance().IntRefParam(ref refValue));
            thrownException.ShouldBe(expectedException);
        }

        #endregion

        #region IntInParam Tests

        [Fact]
        public void IntInParam_WhenSetupToThrowGenericException_ThrowsException()
        {
            _sut
                .IntInParam(Arg<string>.Any())
                .Throws<FileNotFoundException>();

            Should.Throw<FileNotFoundException>(() => _sut.Instance().IntInParam("test"));
        }

        [Fact]
        public void IntInParam_WhenSetupToThrowSpecificException_ThrowsException()
        {
            var expectedException = new DirectoryNotFoundException("Path not found");
            
            _sut
                .IntInParam(Arg<string>.Is(s => s.StartsWith("missing")))
                .Throws(expectedException);

            var thrownException = Should.Throw<DirectoryNotFoundException>(() => _sut.Instance().IntInParam("missing_file"));
            thrownException.ShouldBe(expectedException);
        }

        #endregion

        #region IntParamsParam Tests

        [Fact]
        public void IntParamsParam_WhenSetupToThrowGenericException_ThrowsException()
        {
            _sut
                .IntParamsParam(Arg<string[]>.Any())
                .Throws<OutOfMemoryException>();

            Should.Throw<OutOfMemoryException>(() => _sut.Instance().IntParamsParam("a", "b", "c"));
        }

        [Fact]
        public void IntParamsParam_WhenSetupToThrowSpecificException_ThrowsException()
        {
            var expectedException = new IndexOutOfRangeException("Index was out of range");
            
            _sut
                .IntParamsParam(Arg<string[]>.Is(arr => arr.Length > 5))
                .Throws(expectedException);

            var thrownException = Should.Throw<IndexOutOfRangeException>(() => 
                _sut.Instance().IntParamsParam("1", "2", "3", "4", "5", "6"));
            thrownException.ShouldBe(expectedException);
        }

        #endregion

        #region IntAllRefKinds Tests

        [Fact]
        public void IntAllRefKinds_WhenSetupToThrowGenericException_ThrowsException()
        {
            _sut
                .IntAllRefKinds(OutArg<int>.Any(), Arg<int>.Any(), Arg<int>.Any(), Arg<string>.Any(), Arg<string[]>.Any())
                .Throws<StackOverflowException>();

            var refValue = 100;
            int inValue = 50;
            Should.Throw<StackOverflowException>(() => 
                _sut.Instance().IntAllRefKinds(out var outVal, ref refValue, in inValue, "test", "a", "b"));
        }

        [Fact]
        public void IntAllRefKinds_WhenSetupToThrowSpecificException_ThrowsException()
        {
            var expectedException = new AccessViolationException("Memory access violation");
            
            _sut
                .IntAllRefKinds(OutArg<int>.Any(), Arg<int>.Is(0), Arg<int>.Any(), Arg<string>.Any(), Arg<string[]>.Any())
                .Throws(expectedException);

            var refValue = 0;
            int inValue = 50;
            var thrownException = Should.Throw<AccessViolationException>(() => 
                _sut.Instance().IntAllRefKinds(out var outVal, ref refValue, in inValue, "error", "x", "y"));
            thrownException.ShouldBe(expectedException);
        }

        #endregion

        #region Generic Method Tests

        [Fact]
        public void GenericSingleParam_WhenSetupToThrowGenericException_ThrowsException()
        {
            _sut
                .GenericSingleParam<string>(Arg<string>.Any())
                .Throws<ArgumentNullException>();

            Should.Throw<ArgumentNullException>(() => _sut.Instance().GenericSingleParam<string>("test"));
        }

        [Fact]
        public void GenericSingleParam_WhenSetupToThrowSpecificException_ThrowsException()
        {
            var expectedException = new NotImplementedException("Feature not implemented");
            
            _sut
                .GenericSingleParam<int>(Arg<int>.Is(x => x == 999))
                .Throws(expectedException);

            var thrownException = Should.Throw<NotImplementedException>(() => _sut.Instance().GenericSingleParam<int>(999));
            thrownException.ShouldBe(expectedException);
        }

        [Fact]
        public void GenericOutParam_WhenSetupToThrowGenericException_ThrowsException()
        {
            _sut
                .GenericOutParam<string, int>(OutArg<string>.Any())
                .Throws<InvalidOperationException>();

            Should.Throw<InvalidOperationException>(() => _sut.Instance().GenericOutParam<string, int>(out var outVal));
        }

        [Fact]
        public void GenericOutParam_WhenSetupToThrowSpecificException_ThrowsException()
        {
            var expectedException = new ObjectDisposedException("Object has been disposed");
            
            _sut
                .GenericOutParam<Cat, bool>(OutArg<Cat>.Any())
                .Throws(expectedException);

            var thrownException = Should.Throw<ObjectDisposedException>(() => _sut.Instance().GenericOutParam<Cat, bool>(out var outVal));
            thrownException.ShouldBe(expectedException);
        }

        [Fact]
        public void GenericRefParam_WhenSetupToThrowGenericException_ThrowsException()
        {
            _sut
                .GenericRefParam<string, int>(Arg<string>.Any())
                .Throws<FormatException>();

            var refValue = "test";
            Should.Throw<FormatException>(() => _sut.Instance().GenericRefParam<string, int>(ref refValue));
        }

        [Fact]
        public void GenericRefParam_WhenSetupToThrowSpecificException_ThrowsException()
        {
            var expectedException = new UriFormatException("Invalid URI format");
            
            _sut
                .GenericRefParam<string, Uri>(Arg<string>.Is(s => !s.StartsWith("http")))
                .Throws(expectedException);

            var refValue = "invalid-uri";
            var thrownException = Should.Throw<UriFormatException>(() => _sut.Instance().GenericRefParam<string, Uri>(ref refValue));
            thrownException.ShouldBe(expectedException);
        }

        [Fact]
        public void GenericParamsParam_WhenSetupToThrowGenericException_ThrowsException()
        {
            _sut
                .GenericParamsParam<string, int>(Arg<string[]>.Any())
                .Throws<ArgumentException>();

            Should.Throw<ArgumentException>(() => _sut.Instance().GenericParamsParam<string, int>("a", "b"));
        }

        [Fact]
        public void GenericParamsParam_WhenSetupToThrowSpecificException_ThrowsException()
        {
            var expectedException = new InvalidEnumArgumentException("Invalid enum value");
            
            _sut
                .GenericParamsParam<int, string>(Arg<int[]>.Is(arr => arr.Length == 0))
                .Throws(expectedException);

            var thrownException = Should.Throw<InvalidEnumArgumentException>(() => _sut.Instance().GenericParamsParam<int, string>());
            thrownException.ShouldBe(expectedException);
        }

        [Fact]
        public void GenericAllRefKind_WhenSetupToThrowGenericException_ThrowsException()
        {
            _sut
                .GenericAllRefKind<Cat, IAnimal, Dog, Tiger, string>(
                    OutArg<Cat>.Any(),
                    Arg<IAnimal>.Any(),
                    Arg<Dog>.Any(),
                    Arg<Tiger[]>.Any()
                )
                .Throws<NotSupportedException>();

            IAnimal refAnimal = new Cat("fluffy");
            var inDog = new Dog("buddy");
            var tigers = new Tiger[] { new Tiger("tiger1") };

            Should.Throw<NotSupportedException>(() => 
                _sut.Instance().GenericAllRefKind<Cat, IAnimal, Dog, Tiger, string>(
                    out var outCat, ref refAnimal, in inDog, tigers));
        }

        #endregion

        #region Exception Chaining Tests

        [Fact]
        public void IntSingleParam_WhenChainedThrows_ThrowsInSequence()
        {
            _sut
                .IntSingleParam(Arg<int>.Any())
                .Throws<ArgumentException>()
                .Throws<InvalidOperationException>()
                .Throws<NotImplementedException>();

            Should.Throw<ArgumentException>(() => _sut.Instance().IntSingleParam(1));
            Should.Throw<InvalidOperationException>(() => _sut.Instance().IntSingleParam(2));
            Should.Throw<NotImplementedException>(() => _sut.Instance().IntSingleParam(3));
            Should.Throw<NotImplementedException>(() => _sut.Instance().IntSingleParam(4)); // Repeats last
        }

        [Fact]
        public void IntNoParams_WhenChainedThrowsWithSpecificExceptions_ThrowsInSequence()
        {
            var exception1 = new ArgumentException("First error");
            var exception2 = new InvalidOperationException("Second error");
            
            _sut
                .IntNoParams()
                .Throws(exception1)
                .Throws(exception2);

            var thrown1 = Should.Throw<ArgumentException>(() => _sut.Instance().IntNoParams());
            thrown1.ShouldBe(exception1);
            
            var thrown2 = Should.Throw<InvalidOperationException>(() => _sut.Instance().IntNoParams());
            thrown2.ShouldBe(exception2);
            
            var thrown3 = Should.Throw<InvalidOperationException>(() => _sut.Instance().IntNoParams());
            thrown3.ShouldBe(exception2); // Repeats last
        }

        [Fact]
        public void IntSingleParam_WhenMixedReturnsAndThrows_BehavesCorrectly()
        {
            _sut
                .IntSingleParam(Arg<int>.Any())
                .Returns(100)
                .Throws<ArgumentException>()
                .Returns(200);

            _sut.Instance().IntSingleParam(1).ShouldBe(100);
            Should.Throw<ArgumentException>(() => _sut.Instance().IntSingleParam(2));
            _sut.Instance().IntSingleParam(3).ShouldBe(200);
            _sut.Instance().IntSingleParam(4).ShouldBe(200); // Repeats last
        }

        #endregion

        #region Custom Exception Tests

        [Fact]
        public void IntSingleParam_WhenSetupToThrowCustomException_ThrowsCustomException()
        {
            _sut
                .IntSingleParam(Arg<int>.Any())
                .Throws<CustomTestException>();

            Should.Throw<CustomTestException>(() => _sut.Instance().IntSingleParam(42));
        }

        [Fact]
        public void IntSingleParam_WhenSetupToThrowCustomExceptionWithMessage_ThrowsWithMessage()
        {
            var customException = new CustomTestException("Custom error message", 404);
            
            _sut
                .IntSingleParam(Arg<int>.Is(404))
                .Throws(customException);

            var thrownException = Should.Throw<CustomTestException>(() => _sut.Instance().IntSingleParam(404));
            thrownException.ShouldBe(customException);
            thrownException.Message.ShouldBe("Custom error message");
            thrownException.ErrorCode.ShouldBe(404);
        }

        #endregion

        #region Exception Inheritance Tests

        [Fact]
        public void IntSingleParam_WhenSetupToThrowDerivedType_ThrowsDerivedType()
        {
            _sut
                .IntSingleParam(Arg<int>.Any())
                .Throws<ArgumentNullException>();

            var thrownException = Should.Throw<ArgumentNullException>(() => _sut.Instance().IntSingleParam(42));
            thrownException.ShouldBeOfType<ArgumentNullException>();
            thrownException.ShouldBeAssignableTo<ArgumentException>();
        }

        [Fact]
        public void IntSingleParam_WhenSetupToThrowBaseAndDerived_ThrowsCorrectTypes()
        {
            _sut.IntSingleParam(Arg<int>.Is(1)).Throws<ArgumentException>();
            _sut.IntSingleParam(Arg<int>.Is(2)).Throws<ArgumentNullException>();

            var exception1 = Should.Throw<ArgumentException>(() => _sut.Instance().IntSingleParam(1));
            exception1.ShouldBeOfType<ArgumentException>();
            
            var exception2 = Should.Throw<ArgumentNullException>(() => _sut.Instance().IntSingleParam(2));
            exception2.ShouldBeOfType<ArgumentNullException>();
        }

        #endregion

        #region Error Cases

        [Fact]
        public void IntSingleParam_WhenSetupOverridden_LastSetupWins()
        {
            _sut
                .IntSingleParam(Arg<int>.Is(42))
                .Throws<ArgumentException>();

            _sut
                .IntSingleParam(Arg<int>.Is(42))
                .Throws<InvalidOperationException>();

            Should.Throw<InvalidOperationException>(() => _sut.Instance().IntSingleParam(42));
        }

        [Fact]
        public void IntSingleParam_WhenNoMatchingSetupForException_ReturnsDefault()
        {
            _sut
                .IntSingleParam(Arg<int>.Is(42))
                .Throws<ArgumentException>();

            // Should not throw for non-matching value
            _sut.Instance().IntSingleParam(43).ShouldBe(default);
        }

        #endregion
        
        
        [Fact]
        public async Task AsyncTaskIntNoParams_ThrowsException_PropagatesCorrectly()
        {
            var testException = new InvalidOperationException("Test async exception");
            
            _sut
                .AsyncTaskIntNoParams()
                .Throws(testException);

            var exception = await Should.ThrowAsync<InvalidOperationException>(
                async () => await _sut.Instance().AsyncTaskIntNoParams()
            );
            
            exception.Message.ShouldBe("Test async exception");
        }

        [Fact]
        public async Task AsyncTaskIntNoParams_ThrowsGenericException_PropagatesCorrectly()
        {
            _sut
                .AsyncTaskIntNoParams()
                .Throws<ArgumentNullException>();

            await Should.ThrowAsync<ArgumentNullException>(
                async () => await _sut.Instance().AsyncTaskIntNoParams()
            );
        }

        [Fact]
        public async Task AsyncTaskIntNoParams_ThrowsWithExceptionGenerator_PropagatesCorrectly()
        {
            _sut
                .AsyncTaskIntNoParams()
                .Throws(() => new TimeoutException("Generated exception"));

            var exception = await Should.ThrowAsync<TimeoutException>(
                async () => await _sut.Instance().AsyncTaskIntNoParams()
            );
            
            exception.Message.ShouldBe("Generated exception");
        }
    }

    // Custom exception for testing
    public class CustomTestException : Exception
    {
        public int ErrorCode { get; }

        public CustomTestException() { }
        
        public CustomTestException(string message) : base(message) { }
        
        public CustomTestException(string message, int errorCode) : base(message) 
        {
            ErrorCode = errorCode;
        }
    }
}