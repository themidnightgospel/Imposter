using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation.Suts;

[assembly: GenerateImposter(typeof(NullableReferenceMembersClass))]
[assembly: GenerateImposter(typeof(INullableReferenceMembers))]

namespace Imposter.Tests.Features.ClassImpersonation.Suts
{
    public class NullableReferenceMembersClass
    {
        public virtual string? Describe(string? value) => value;

        public virtual string? Name { get; set; }

        public virtual string? this[string? key]
        {
            get => key;
            set { }
        }
    }

    public interface INullableReferenceMembers
    {
        string? Find(string? key);

        Task<string?> FindAsync(string? key);
    }
}
