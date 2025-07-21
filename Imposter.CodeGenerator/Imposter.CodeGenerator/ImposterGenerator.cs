using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Imposter.CodeGenerator;

[Generator]
public class ImposterGenerator : IIncrementalGenerator
{
    private const string GenerateImposterAttributeSourceText =
        """
        using System;
                
        namespace Imposter;
        
        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly)]
        public class GenerateImposterAttribute(Type Type) : Attribute
        using System;
                
        namespace Imposter;
        
        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly)]
        public class GenerateImposterAttribute(Type Type) : Attribute
        { }
        """;

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(
            ctx => ctx.AddSource("GenerateImposterAttribute.g.cs", SourceText.From(GenerateImposterAttributeSourceText, Encoding.UTF8)));
    }
}