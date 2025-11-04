using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata;

internal readonly record struct InvocationSetupMetadata
{
    internal const string MethodInvocationImposterTypeName = "MethodInvocationImposter";

    internal readonly string Name;

    internal readonly TypeMetadata Interface;

    internal readonly NameSyntax Syntax;

    internal readonly NameSyntax MethodInvocationImposterSyntax;

    internal readonly ReturnsMethodMetadata ReturnsMethod;

    internal readonly ThrowsMethodMetadata ThrowsMethod;

    internal readonly CallbackMethodMetadata CallbackMethod;

    internal readonly ThenMethodMetadata ThenMethod;

    internal readonly DefaultInvocationSetupFieldMetadata DefaultInvocationSetupField;

    internal readonly DefaultResultGeneratorMethodMetadata DefaultResultGeneratorMethod;

    internal InvocationSetupMetadata(in ImposterTargetMethodMetadata method)
    {
        Name = $"{method.UniqueName}MethodInvocationImposterGroup";
        Interface = TypeMetadataFactory.Create($"I{method.UniqueName}MethodInvocationImposterBuilder", method.GenericTypeArguments);
        Syntax = SyntaxFactoryHelper.WithMethodGenericArguments(method.GenericTypeArguments, Name);
        MethodInvocationImposterSyntax = SyntaxFactory.QualifiedName(Syntax, SyntaxFactory.IdentifierName(MethodInvocationImposterTypeName));
        ReturnsMethod = new ReturnsMethodMetadata(method, method.ReturnTypeSyntax, Interface.Syntax, method.Delegate.Syntax);
        ThrowsMethod = new ThrowsMethodMetadata(method, method.ExceptionGeneratorDelegate.Syntax, Interface.Syntax);
        CallbackMethod = new CallbackMethodMetadata(method, Interface.Syntax, method.CallbackDelegate.Syntax);
        ThenMethod = new ThenMethodMetadata(Interface.Syntax);
        DefaultInvocationSetupField = new DefaultInvocationSetupFieldMetadata();
        DefaultResultGeneratorMethod = new DefaultResultGeneratorMethodMetadata(method.ReturnTypeSyntax);
    }

    internal readonly struct DefaultResultGeneratorMethodMetadata
    {
        internal readonly string Name = "DefaultResultGenerator";
        
        internal readonly TypeSyntax ReturnType;

        internal DefaultResultGeneratorMethodMetadata(TypeSyntax methodReturnTypeSyntax)
        {
            ReturnType = methodReturnTypeSyntax;
        }
    }

    internal readonly struct ThrowsMethodMetadata
    {
        internal readonly string Name = "Throws";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata ExceptionParameter;

        internal readonly ParameterMetadata ExceptionGeneratorParameter;

        public ThrowsMethodMetadata(
            IParameterNameContextProvider parameterNameContextProvider,
            NameSyntax exceptionGeneratorDelegateSyntax,
            TypeSyntax interfaceTypeSyntax)
        {
            ReturnType = interfaceTypeSyntax;
            var nameContext = parameterNameContextProvider.CreateParameterNameContext();
            ExceptionParameter = new ParameterMetadata(nameContext.Use("exception"), WellKnownTypes.System.Exception);
            ExceptionGeneratorParameter = new ParameterMetadata(nameContext.Use("exceptionGenerator"), exceptionGeneratorDelegateSyntax);
        }
    }
    
    internal readonly struct DefaultInvocationSetupFieldMetadata
    {
        internal readonly string Name;

        public DefaultInvocationSetupFieldMetadata()
        {
            Name = "Default";
        }
    }

    internal readonly struct ReturnsMethodMetadata
    {
        internal readonly string Name = "Returns";

        internal readonly ParameterMetadata ValueParameter;

        internal readonly ParameterMetadata ResultGeneratorParameter;

        internal readonly TypeSyntax ReturnType;

        public ReturnsMethodMetadata(
            IParameterNameContextProvider parameterNameContextProvider,
            TypeSyntax imposterMethodReturnType,
            NameSyntax interfaceSyntax,
            TypeSyntax methodDelegateTypeSyntax)
        {
            ReturnType = interfaceSyntax;
            var nameContext = parameterNameContextProvider.CreateParameterNameContext();
            ValueParameter = new ParameterMetadata(nameContext.Use("value"), imposterMethodReturnType);
            ResultGeneratorParameter = new ParameterMetadata(nameContext.Use("resultGenerator"), methodDelegateTypeSyntax);
        }
    }

    internal readonly struct CallbackMethodMetadata
    {
        internal readonly string Name = "Callback";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata CallbackParameter;

        public CallbackMethodMetadata(
            IParameterNameContextProvider parameterNameContextProvider,
            NameSyntax interfaceSyntax,
            TypeSyntax callbackTypeSyntax)
        {
            var nameContext = parameterNameContextProvider.CreateParameterNameContext();
            ReturnType = interfaceSyntax;
            CallbackParameter = new ParameterMetadata(nameContext.Use("callback"), callbackTypeSyntax);
        }
    }

    internal readonly struct ThenMethodMetadata
    {
        internal readonly string Name = "Then";

        internal readonly TypeSyntax ReturnType;

        public ThenMethodMetadata(NameSyntax interfaceSyntax)
        {
            ReturnType = interfaceSyntax;
        }
    }
}
