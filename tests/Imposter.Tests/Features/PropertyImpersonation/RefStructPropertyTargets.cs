using Imposter.Abstractions;
using Imposter.Tests.Features.PropertyImpersonation;

[assembly: GenerateImposter(typeof(IRefStructPropertySut))]
[assembly: GenerateImposter(typeof(RefStructPropertyClass))]

namespace Imposter.Tests.Features.PropertyImpersonation
{
    public ref struct Bookmark
    {
        public int Page;

        public Bookmark(int page) => Page = page;
    }

    public interface IRefStructPropertySut
    {
        Bookmark Current { get; set; }
    }

    public class RefStructPropertyClass
    {
        public int BasePage { get; private set; } = 7;

        public virtual Bookmark Current
        {
            get => new Bookmark(BasePage);
            set => BasePage = value.Page;
        }
    }
}
