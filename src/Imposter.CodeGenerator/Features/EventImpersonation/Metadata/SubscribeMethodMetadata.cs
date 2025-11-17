using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

internal readonly struct SubscribeMethodMetadata
{
    internal readonly string Name;
    internal readonly ParameterMetadata HandlerParameter;
    internal readonly ParameterMetadata? BaseImplementationParameter;

    internal SubscribeMethodMetadata(in ImposterEventCoreMetadata core)
    {
        Name = "Subscribe";
        HandlerParameter = new ParameterMetadata("handler", core.HandlerTypeSyntax);
        BaseImplementationParameter = core.SupportsBaseImplementation
            ? new ParameterMetadata(
                "baseImplementation",
                WellKnownTypes.System.Action.ToNullableType(),
                SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression)
            )
            : null;
    }
}
