using Imposter.CodeGenerator.SyntaxProviders;

namespace Imposter.CodeGenerator.Descriptors;

internal class InterfaceTargetImposterDescriptorBuilder
{
    private readonly InterfaceTargetImposterDescriptor _interfaceTargetImposterDescriptor;

    internal InterfaceTargetImposterDescriptorBuilder(GenerateImposterDeclaration generateImposterDeclaration)
    {
        _interfaceTargetImposterDescriptor = new InterfaceTargetImposterDescriptor(generateImposterDeclaration);
    }

    internal InterfaceTargetImposterDescriptor Build()
    {
        return _interfaceTargetImposterDescriptor;
    }
}