using System.Collections.Generic;
using Imposter.Abstractions;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class AnyMethodParameterTests
    {
        private readonly IDiscountServiceSutImposter _sut =
#if USE_CSHARP14
        IDiscountServiceSut.Imposter();
#else
        new IDiscountServiceSutImposter();
#endif

        [Fact]
        public void GivenGenericAnyParameters_WhenCalled_ShouldVerifyCallCount()
        {
            // Arrange
            _sut.Calculate(
                    Arg<IEnumerable<InvoiceItem>?>.Any(),
                    Arg<IDictionary<Product, IEnumerable<History>>?>.Any()
                )
                .Returns(42);

            // Act
            _sut.Instance().Calculate(null, null);

            // Assert
            _sut.Calculate(
                    Arg<IEnumerable<InvoiceItem>?>.Any(),
                    Arg<IDictionary<Product, IEnumerable<History>>?>.Any()
                )
                .Called(Count.Once());
        }

        [Fact]
        public void GivenAnyParameters_WhenCalled_ShouldVerifyCallCount()
        {
            // Arrange
            _sut.Calculate(Arg.Any, Arg.Any).Returns(42);

            // Act
            _sut.Instance().Calculate(null, null);

            // Assert
            _sut.Calculate(Arg.Any, Arg.Any).Called(Count.Once());
        }
    }
}
