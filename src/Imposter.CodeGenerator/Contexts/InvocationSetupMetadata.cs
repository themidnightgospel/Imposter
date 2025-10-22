using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Contexts;

internal readonly record struct InvocationSetupMetadata
{
    internal const string GetOrAddMethodSetupMethodName = "GetOrAddMethodSetup";

    internal readonly string Name;

    internal readonly TypeMetadata Interface;

    internal readonly NameSyntax Syntax;

    internal readonly ReturnsMethodMetadata ReturnsMethod;

    internal readonly InvokeMethoMetadata InvokeMethod;

    internal readonly GetNextSetupMethodMetadata GetNextSetupMethod;

    internal readonly ThrowsMethodMetadata ThrowsMethod;
    
    internal readonly CallBeforeCallbackMethodMetadata CallBeforeCallbackMethod;
    
    internal readonly CallAfterCallbackMethodMetadata CallAfterCallbackMethod;

    internal InvocationSetupMetadata(ImposterTargetMethodMetadata method)
    {
        Name = $"{method.UniqueName}MethodInvocationsSetup";
        Interface = TypeMetadataFactory.Create($"I{Name}", method.GenericTypeArguments);
        Syntax = SyntaxFactoryHelper.WithMethodGenericArguments(method.GenericTypeArguments, Name);
        ReturnsMethod = new ReturnsMethodMetadata(method, method.ReturnTypeSyntax, Interface.Syntax, method.Delegate.Syntax);
        InvokeMethod = new InvokeMethoMetadata(method);
        GetNextSetupMethod = new GetNextSetupMethodMetadata(method);
        ThrowsMethod = new ThrowsMethodMetadata(method, method.ExceptionGeneratorDelegate.Syntax, Interface.Syntax);
        CallBeforeCallbackMethod = new CallBeforeCallbackMethodMetadata(method, Interface.Syntax, method.CallbackDelegate.Syntax);
        CallAfterCallbackMethod = new CallAfterCallbackMethodMetadata(method, Interface.Syntax, method.CallbackDelegate.Syntax);
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

    internal readonly struct DefaultInvocationSetupMethod
    {
        public const string Name = "DefaultInvocationSetup";
    }
    
    internal readonly struct CallAfterCallbackMethodMetadata
    {
        internal readonly string Name = "CallAfter";
        
        internal readonly TypeSyntax ReturnType;
        
        internal readonly ParameterMetadata CallbackParameter;

        public CallAfterCallbackMethodMetadata(
            IParameterNameContextProvider parameterNameContextProvider,
            NameSyntax interfaceSyntax,
            TypeSyntax callbackTypeSyntax)
        {
            var nameContext = parameterNameContextProvider.CreateParameterNameContext();
            ReturnType = interfaceSyntax;
            CallbackParameter = new ParameterMetadata(nameContext.Use("callback"), callbackTypeSyntax);
        }
    }


    internal readonly struct CallBeforeCallbackMethodMetadata
    {
        internal readonly string Name = "CallBefore";
        
        internal readonly TypeSyntax ReturnType;
        
        internal readonly ParameterMetadata CallbackParameter;

        public CallBeforeCallbackMethodMetadata(
            IParameterNameContextProvider parameterNameContextProvider,
            NameSyntax interfaceSyntax,
            TypeSyntax callbackTypeSyntax)
        {
            var nameContext = parameterNameContextProvider.CreateParameterNameContext();
            ReturnType = interfaceSyntax;
            CallbackParameter = new ParameterMetadata(nameContext.Use("callback"), callbackTypeSyntax);
        }
    }

    internal readonly struct GetNextSetupMethodMetadata
    {
        internal readonly string Name;

        public GetNextSetupMethodMetadata(IParameterNameContextProvider parameterNameContextProvider)
        {
            var nameContext = parameterNameContextProvider.CreateParameterNameContext();
            Name = nameContext.Use("GetNextSetup");
        }
    }

    internal readonly struct InvokeMethoMetadata
    {
        internal readonly string Name = "Invoke";


        internal readonly string ResultVariableName;

        internal readonly string NextSetupVariableName;

        public InvokeMethoMetadata(IParameterNameContextProvider parameterNameContextProvider)
        {
            var nameContext = parameterNameContextProvider.CreateParameterNameContext();
            ResultVariableName = nameContext.Use("result");
            NextSetupVariableName = nameContext.Use("nextSetup");
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
}