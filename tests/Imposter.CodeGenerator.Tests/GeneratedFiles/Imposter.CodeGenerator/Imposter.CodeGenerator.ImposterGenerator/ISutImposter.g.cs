#pragma warning disable nullable
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using Imposter;
using Imposter.CodeGenerator.Tests.Setup;

namespace Imposters.Imposter.CodeGenerator.Tests.Setup;
// int WhenSettingUpMethodReturnValue.ISut.NoParametersWithReturnType()
public delegate int NoParametersWithReturnTypeDelegate();

public class NoParametersWithReturnTypeMethodInvocationHistory
{
    public int Result { get; }
    public System.Exception Exception { get; }

    public NoParametersWithReturnTypeMethodInvocationHistory(int result, System.Exception exception)
    {
        Result = result;
        Exception = exception;
    }
}

/// <summary>
/// invocation behaviour class
/// <code>
/// int WhenSettingUpMethodReturnValue.ISut.NoParametersWithReturnType()
/// </code>
/// </summary>
public class NoParametersWithReturnTypeInvocationSetup()
{
    private NoParametersWithReturnTypeDelegate? _callBefore;
    private NoParametersWithReturnTypeDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;
    private NoParametersWithReturnTypeDelegate? _resultGenerator;

    public NoParametersWithReturnTypeInvocationSetup CallBefore(NoParametersWithReturnTypeDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public NoParametersWithReturnTypeInvocationSetup CallAfter(NoParametersWithReturnTypeDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public NoParametersWithReturnTypeInvocationSetup Returns(NoParametersWithReturnTypeDelegate resultGenerator)
    {
       _resultGenerator = resultGenerator;
       return this;
    }

    public NoParametersWithReturnTypeInvocationSetup Returns(global::System.Int32 result)
    {
       _resultGenerator = () =>
       {

            return result;
       };
       return this;
    }


    public NoParametersWithReturnTypeInvocationSetup Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public NoParametersWithReturnTypeInvocationSetup Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches()
    {
        return true;
    }

    public global::System.Int32 Execute()
    {
        
        if(_callBefore is not null)
        {
            _callBefore();
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        var result = _resultGenerator?.Invoke() ?? default(global::System.Int32);
        
        if(_callAfter is not null)
        {
            _callAfter();
        }
        
        return result;
    }
}

/// <summary>
/// method class
/// <code>
/// int WhenSettingUpMethodReturnValue.ISut.NoParametersWithReturnType()
/// </code>
/// </summary>
public class NoParametersWithReturnTypeMethod
{
    public List<NoParametersWithReturnTypeInvocationSetup> Behaviours { get; } = new();
    public List<NoParametersWithReturnTypeMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public global::System.Int32 Invoke()
    {

        try
        {
            NoParametersWithReturnTypeInvocationSetup? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches())
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            global::System.Int32 result;
            
            if(matchingBehaviour is not null)
            {
               result = matchingBehaviour.Execute();
            }
            else
            {
                
                result = default(global::System.Int32);
            }
            
            InvocationHistory.Add(new NoParametersWithReturnTypeMethodInvocationHistory(result, null));
            return result;
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new NoParametersWithReturnTypeMethodInvocationHistory(default(global::System.Int32), ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// int WhenSettingUpMethodReturnValue.ISut.NoParametersWithReturnType()
/// </code>
/// </summary>
public class NoParametersWithReturnTypeMethodInvocationVerifier
{             
    private readonly NoParametersWithReturnTypeMethod _noParametersWithReturnTypeMethod;

    public NoParametersWithReturnTypeMethodInvocationVerifier(
        NoParametersWithReturnTypeMethod noParametersWithReturnTypeMethod
    )
    {
        this._noParametersWithReturnTypeMethod = noParametersWithReturnTypeMethod;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _noParametersWithReturnTypeMethod.InvocationHistory.Count();
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

}

// int WhenSettingUpMethodReturnValue.ISut.ParameterAndReturnType(int a, int b)
public delegate int ParameterAndReturnTypeDelegate(int a, int b);

public class ParameterAndReturnTypeArguments
{
    public int a { get; }
    public int b { get; }

    public ParameterAndReturnTypeArguments(int a, int b)
    {
        this.a = a;
        this.b = b;
    }
}

public class ParameterAndReturnTypeMethodInvocationHistory
{
    public ParameterAndReturnTypeArguments Arguments { get; }
    public int Result { get; }
    public System.Exception Exception { get; }

    public ParameterAndReturnTypeMethodInvocationHistory(ParameterAndReturnTypeArguments arguments, int result, System.Exception exception)
    {
        Arguments = arguments;
        Result = result;
        Exception = exception;
    }
}

/// <summary>
/// invocation behaviour class
/// <code>
/// int WhenSettingUpMethodReturnValue.ISut.ParameterAndReturnType(int a, int b)
/// </code>
/// </summary>
public class ParameterAndReturnTypeInvocationSetup(Arg<global::System.Int32> aArg, Arg<global::System.Int32> bArg)
{
    private ParameterAndReturnTypeDelegate? _callBefore;
    private ParameterAndReturnTypeDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;
    private ParameterAndReturnTypeDelegate? _resultGenerator;

    public ParameterAndReturnTypeInvocationSetup CallBefore(ParameterAndReturnTypeDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public ParameterAndReturnTypeInvocationSetup CallAfter(ParameterAndReturnTypeDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public ParameterAndReturnTypeInvocationSetup Returns(ParameterAndReturnTypeDelegate resultGenerator)
    {
       _resultGenerator = resultGenerator;
       return this;
    }

    public ParameterAndReturnTypeInvocationSetup Returns(global::System.Int32 result)
    {
       _resultGenerator = ( int a,  int b) =>
       {

            return result;
       };
       return this;
    }


    public ParameterAndReturnTypeInvocationSetup Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public ParameterAndReturnTypeInvocationSetup Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches( int a,  int b)
    {
        return aArg.Matches(a) && bArg.Matches(b);
    }

    public global::System.Int32 Execute( int a,  int b)
    {
        
        if(_callBefore is not null)
        {
            _callBefore( a,  b);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        var result = _resultGenerator?.Invoke( a,  b) ?? default(global::System.Int32);
        
        if(_callAfter is not null)
        {
            _callAfter( a,  b);
        }
        
        return result;
    }
}

/// <summary>
/// method class
/// <code>
/// int WhenSettingUpMethodReturnValue.ISut.ParameterAndReturnType(int a, int b)
/// </code>
/// </summary>
public class ParameterAndReturnTypeMethod
{
    public List<ParameterAndReturnTypeInvocationSetup> Behaviours { get; } = new();
    public List<ParameterAndReturnTypeMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public global::System.Int32 Invoke( int a,  int b)
    {

        try
        {
            ParameterAndReturnTypeInvocationSetup? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches( a,  b))
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            global::System.Int32 result;
            
            if(matchingBehaviour is not null)
            {
               result = matchingBehaviour.Execute( a,  b);
            }
            else
            {
                
                result = default(global::System.Int32);
            }
            
            InvocationHistory.Add(new ParameterAndReturnTypeMethodInvocationHistory(new ParameterAndReturnTypeArguments(a, b),result, null));
            return result;
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new ParameterAndReturnTypeMethodInvocationHistory(new ParameterAndReturnTypeArguments(a, b),default(global::System.Int32), ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// int WhenSettingUpMethodReturnValue.ISut.ParameterAndReturnType(int a, int b)
/// </code>
/// </summary>
public class ParameterAndReturnTypeMethodInvocationVerifier
{             
    private readonly ParameterAndReturnTypeMethod _parameterAndReturnTypeMethod;
    private readonly Arg<global::System.Int32> _aArg;
    private readonly Arg<global::System.Int32> _bArg;

    public ParameterAndReturnTypeMethodInvocationVerifier(
        ParameterAndReturnTypeMethod parameterAndReturnTypeMethod, Arg<global::System.Int32> aArg, Arg<global::System.Int32> bArg
    )
    {
        this._parameterAndReturnTypeMethod = parameterAndReturnTypeMethod;
        this._aArg = aArg;
        this._bArg = bArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _parameterAndReturnTypeMethod.InvocationHistory.Count(it => Matches(it.Arguments));
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

    public bool Matches(ParameterAndReturnTypeArguments arguments)
    {
        return _aArg.Matches(arguments.a) && _bArg.Matches(arguments.b);
    }
}

// int WhenSettingUpMethodReturnValue.ISut.MethodWithRefParameter(ref int a)
public delegate int MethodWithRefParameterDelegate(ref int a);

public class MethodWithRefParameterArguments
{
    public int a { get; }

    public MethodWithRefParameterArguments(int a)
    {
        this.a = a;
    }
}

public class MethodWithRefParameterMethodInvocationHistory
{
    public MethodWithRefParameterArguments Arguments { get; }
    public int Result { get; }
    public System.Exception Exception { get; }

    public MethodWithRefParameterMethodInvocationHistory(MethodWithRefParameterArguments arguments, int result, System.Exception exception)
    {
        Arguments = arguments;
        Result = result;
        Exception = exception;
    }
}

/// <summary>
/// invocation behaviour class
/// <code>
/// int WhenSettingUpMethodReturnValue.ISut.MethodWithRefParameter(ref int a)
/// </code>
/// </summary>
public class MethodWithRefParameterInvocationSetup(Arg<global::System.Int32> aArg)
{
    private MethodWithRefParameterDelegate? _callBefore;
    private MethodWithRefParameterDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;
    private MethodWithRefParameterDelegate? _resultGenerator;

    public MethodWithRefParameterInvocationSetup CallBefore(MethodWithRefParameterDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public MethodWithRefParameterInvocationSetup CallAfter(MethodWithRefParameterDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public MethodWithRefParameterInvocationSetup Returns(MethodWithRefParameterDelegate resultGenerator)
    {
       _resultGenerator = resultGenerator;
       return this;
    }

    public MethodWithRefParameterInvocationSetup Returns(global::System.Int32 result)
    {
       _resultGenerator = (ref int a) =>
       {

            return result;
       };
       return this;
    }


    public MethodWithRefParameterInvocationSetup Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public MethodWithRefParameterInvocationSetup Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches(ref int a)
    {
        return aArg.Matches(a);
    }

    public global::System.Int32 Execute(ref int a)
    {
        
        if(_callBefore is not null)
        {
            _callBefore(ref a);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        var result = _resultGenerator?.Invoke(ref a) ?? default(global::System.Int32);
        
        if(_callAfter is not null)
        {
            _callAfter(ref a);
        }
        
        return result;
    }
}

/// <summary>
/// method class
/// <code>
/// int WhenSettingUpMethodReturnValue.ISut.MethodWithRefParameter(ref int a)
/// </code>
/// </summary>
public class MethodWithRefParameterMethod
{
    public List<MethodWithRefParameterInvocationSetup> Behaviours { get; } = new();
    public List<MethodWithRefParameterMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public global::System.Int32 Invoke(ref int a)
    {

        try
        {
            MethodWithRefParameterInvocationSetup? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches(ref a))
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            global::System.Int32 result;
            
            if(matchingBehaviour is not null)
            {
               result = matchingBehaviour.Execute(ref a);
            }
            else
            {
                
                result = default(global::System.Int32);
            }
            
            InvocationHistory.Add(new MethodWithRefParameterMethodInvocationHistory(new MethodWithRefParameterArguments(a),result, null));
            return result;
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new MethodWithRefParameterMethodInvocationHistory(new MethodWithRefParameterArguments(a),default(global::System.Int32), ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// int WhenSettingUpMethodReturnValue.ISut.MethodWithRefParameter(ref int a)
/// </code>
/// </summary>
public class MethodWithRefParameterMethodInvocationVerifier
{             
    private readonly MethodWithRefParameterMethod _methodWithRefParameterMethod;
    private readonly Arg<global::System.Int32> _aArg;

    public MethodWithRefParameterMethodInvocationVerifier(
        MethodWithRefParameterMethod methodWithRefParameterMethod, Arg<global::System.Int32> aArg
    )
    {
        this._methodWithRefParameterMethod = methodWithRefParameterMethod;
        this._aArg = aArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _methodWithRefParameterMethod.InvocationHistory.Count(it => Matches(it.Arguments));
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

    public bool Matches(MethodWithRefParameterArguments arguments)
    {
        return _aArg.Matches(arguments.a);
    }
}

// int WhenSettingUpMethodReturnValue.ISut.MethodWithOutParameter(out int a)
public delegate int MethodWithOutParameterDelegate(out int a);

public class MethodWithOutParameterArguments
{
    public int a { get; }

    public MethodWithOutParameterArguments(int a)
    {
        this.a = a;
    }
}

public class MethodWithOutParameterMethodInvocationHistory
{
    public MethodWithOutParameterArguments Arguments { get; }
    public int Result { get; }
    public System.Exception Exception { get; }

    public MethodWithOutParameterMethodInvocationHistory(MethodWithOutParameterArguments arguments, int result, System.Exception exception)
    {
        Arguments = arguments;
        Result = result;
        Exception = exception;
    }
}

/// <summary>
/// invocation behaviour class
/// <code>
/// int WhenSettingUpMethodReturnValue.ISut.MethodWithOutParameter(out int a)
/// </code>
/// </summary>
public class MethodWithOutParameterInvocationSetup(OutArg<global::System.Int32> aArg)
{
    private MethodWithOutParameterDelegate? _callBefore;
    private MethodWithOutParameterDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;
    private MethodWithOutParameterDelegate? _resultGenerator;

    public MethodWithOutParameterInvocationSetup CallBefore(MethodWithOutParameterDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public MethodWithOutParameterInvocationSetup CallAfter(MethodWithOutParameterDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public MethodWithOutParameterInvocationSetup Returns(MethodWithOutParameterDelegate resultGenerator)
    {
       _resultGenerator = resultGenerator;
       return this;
    }

    public MethodWithOutParameterInvocationSetup Returns(global::System.Int32 result)
    {
       _resultGenerator = (out int a) =>
       {

        a = default(int);
            return result;
       };
       return this;
    }


    public MethodWithOutParameterInvocationSetup Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public MethodWithOutParameterInvocationSetup Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches()
    {
        return true;
    }

    public global::System.Int32 Execute(out int a)
    {
        
    a = default(int);
        if(_callBefore is not null)
        {
            _callBefore(out a);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        var result = _resultGenerator?.Invoke(out a) ?? default(global::System.Int32);
        
        if(_callAfter is not null)
        {
            _callAfter(out a);
        }
        
        return result;
    }
}

/// <summary>
/// method class
/// <code>
/// int WhenSettingUpMethodReturnValue.ISut.MethodWithOutParameter(out int a)
/// </code>
/// </summary>
public class MethodWithOutParameterMethod
{
    public List<MethodWithOutParameterInvocationSetup> Behaviours { get; } = new();
    public List<MethodWithOutParameterMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public global::System.Int32 Invoke(out int a)
    {

        try
        {
            MethodWithOutParameterInvocationSetup? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches())
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            global::System.Int32 result;
            
            if(matchingBehaviour is not null)
            {
               result = matchingBehaviour.Execute(out a);
            }
            else
            {
                a = default(int);

                result = default(global::System.Int32);
            }
            
            InvocationHistory.Add(new MethodWithOutParameterMethodInvocationHistory(new MethodWithOutParameterArguments(a),result, null));
            return result;
        }
        catch (Exception ex)
        {
            a = default(int);

            InvocationHistory.Add(new MethodWithOutParameterMethodInvocationHistory(new MethodWithOutParameterArguments(a),default(global::System.Int32), ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// int WhenSettingUpMethodReturnValue.ISut.MethodWithOutParameter(out int a)
/// </code>
/// </summary>
public class MethodWithOutParameterMethodInvocationVerifier
{             
    private readonly MethodWithOutParameterMethod _methodWithOutParameterMethod;
    private readonly OutArg<global::System.Int32> _aArg;

    public MethodWithOutParameterMethodInvocationVerifier(
        MethodWithOutParameterMethod methodWithOutParameterMethod, OutArg<global::System.Int32> aArg
    )
    {
        this._methodWithOutParameterMethod = methodWithOutParameterMethod;
        this._aArg = aArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _methodWithOutParameterMethod.InvocationHistory.Count();
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

}

// int WhenSettingUpMethodReturnValue.ISut.MethodWithParamsParameter(params int[] a)
public delegate int MethodWithParamsParameterDelegate(int[] a);

public class MethodWithParamsParameterArguments
{
    public int[] a { get; }

    public MethodWithParamsParameterArguments(int[] a)
    {
        this.a = a;
    }
}

public class MethodWithParamsParameterMethodInvocationHistory
{
    public MethodWithParamsParameterArguments Arguments { get; }
    public int Result { get; }
    public System.Exception Exception { get; }

    public MethodWithParamsParameterMethodInvocationHistory(MethodWithParamsParameterArguments arguments, int result, System.Exception exception)
    {
        Arguments = arguments;
        Result = result;
        Exception = exception;
    }
}

/// <summary>
/// invocation behaviour class
/// <code>
/// int WhenSettingUpMethodReturnValue.ISut.MethodWithParamsParameter(params int[] a)
/// </code>
/// </summary>
public class MethodWithParamsParameterInvocationSetup(Arg<global::System.Int32[]> aArg)
{
    private MethodWithParamsParameterDelegate? _callBefore;
    private MethodWithParamsParameterDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;
    private MethodWithParamsParameterDelegate? _resultGenerator;

    public MethodWithParamsParameterInvocationSetup CallBefore(MethodWithParamsParameterDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public MethodWithParamsParameterInvocationSetup CallAfter(MethodWithParamsParameterDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public MethodWithParamsParameterInvocationSetup Returns(MethodWithParamsParameterDelegate resultGenerator)
    {
       _resultGenerator = resultGenerator;
       return this;
    }

    public MethodWithParamsParameterInvocationSetup Returns(global::System.Int32 result)
    {
       _resultGenerator = (params  int[] a) =>
       {

            return result;
       };
       return this;
    }


    public MethodWithParamsParameterInvocationSetup Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public MethodWithParamsParameterInvocationSetup Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches(params  int[] a)
    {
        return aArg.Matches(a);
    }

    public global::System.Int32 Execute(params  int[] a)
    {
        
        if(_callBefore is not null)
        {
            _callBefore( a);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        var result = _resultGenerator?.Invoke( a) ?? default(global::System.Int32);
        
        if(_callAfter is not null)
        {
            _callAfter( a);
        }
        
        return result;
    }
}

/// <summary>
/// method class
/// <code>
/// int WhenSettingUpMethodReturnValue.ISut.MethodWithParamsParameter(params int[] a)
/// </code>
/// </summary>
public class MethodWithParamsParameterMethod
{
    public List<MethodWithParamsParameterInvocationSetup> Behaviours { get; } = new();
    public List<MethodWithParamsParameterMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public global::System.Int32 Invoke(params  int[] a)
    {

        try
        {
            MethodWithParamsParameterInvocationSetup? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches( a))
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            global::System.Int32 result;
            
            if(matchingBehaviour is not null)
            {
               result = matchingBehaviour.Execute( a);
            }
            else
            {
                
                result = default(global::System.Int32);
            }
            
            InvocationHistory.Add(new MethodWithParamsParameterMethodInvocationHistory(new MethodWithParamsParameterArguments(a),result, null));
            return result;
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new MethodWithParamsParameterMethodInvocationHistory(new MethodWithParamsParameterArguments(a),default(global::System.Int32), ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// int WhenSettingUpMethodReturnValue.ISut.MethodWithParamsParameter(params int[] a)
/// </code>
/// </summary>
public class MethodWithParamsParameterMethodInvocationVerifier
{             
    private readonly MethodWithParamsParameterMethod _methodWithParamsParameterMethod;
    private readonly Arg<global::System.Int32[]> _aArg;

    public MethodWithParamsParameterMethodInvocationVerifier(
        MethodWithParamsParameterMethod methodWithParamsParameterMethod, Arg<global::System.Int32[]> aArg
    )
    {
        this._methodWithParamsParameterMethod = methodWithParamsParameterMethod;
        this._aArg = aArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _methodWithParamsParameterMethod.InvocationHistory.Count(it => Matches(it.Arguments));
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

    public bool Matches(MethodWithParamsParameterArguments arguments)
    {
        return _aArg.Matches(arguments.a);
    }
}

// Task WhenSettingUpMethodReturnValue.ISut.ReturnsTypeTask()
public delegate global::System.Threading.Tasks.Task ReturnsTypeTaskDelegate();

public class ReturnsTypeTaskMethodInvocationHistory
{
    public global::System.Threading.Tasks.Task Result { get; }
    public System.Exception Exception { get; }

    public ReturnsTypeTaskMethodInvocationHistory(global::System.Threading.Tasks.Task result, System.Exception exception)
    {
        Result = result;
        Exception = exception;
    }
}

/// <summary>
/// invocation behaviour class
/// <code>
/// Task WhenSettingUpMethodReturnValue.ISut.ReturnsTypeTask()
/// </code>
/// </summary>
public class ReturnsTypeTaskInvocationSetup()
{
    private ReturnsTypeTaskDelegate? _callBefore;
    private ReturnsTypeTaskDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;
    private ReturnsTypeTaskDelegate? _resultGenerator;

    public ReturnsTypeTaskInvocationSetup CallBefore(ReturnsTypeTaskDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public ReturnsTypeTaskInvocationSetup CallAfter(ReturnsTypeTaskDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public ReturnsTypeTaskInvocationSetup Returns(ReturnsTypeTaskDelegate resultGenerator)
    {
       _resultGenerator = resultGenerator;
       return this;
    }

    public ReturnsTypeTaskInvocationSetup Returns(global::System.Threading.Tasks.Task result)
    {
       _resultGenerator = () =>
       {

            return result;
       };
       return this;
    }


    public ReturnsTypeTaskInvocationSetup Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public ReturnsTypeTaskInvocationSetup Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches()
    {
        return true;
    }

    public global::System.Threading.Tasks.Task Execute()
    {
        
        if(_callBefore is not null)
        {
            _callBefore();
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        var result = _resultGenerator?.Invoke() ?? default(global::System.Threading.Tasks.Task);
        
        if(_callAfter is not null)
        {
            _callAfter();
        }
        
        return result;
    }
}

/// <summary>
/// method class
/// <code>
/// Task WhenSettingUpMethodReturnValue.ISut.ReturnsTypeTask()
/// </code>
/// </summary>
public class ReturnsTypeTaskMethod
{
    public List<ReturnsTypeTaskInvocationSetup> Behaviours { get; } = new();
    public List<ReturnsTypeTaskMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public global::System.Threading.Tasks.Task Invoke()
    {

        try
        {
            ReturnsTypeTaskInvocationSetup? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches())
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            global::System.Threading.Tasks.Task result;
            
            if(matchingBehaviour is not null)
            {
               result = matchingBehaviour.Execute();
            }
            else
            {
                
                result = default(global::System.Threading.Tasks.Task);
            }
            
            InvocationHistory.Add(new ReturnsTypeTaskMethodInvocationHistory(result, null));
            return result;
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new ReturnsTypeTaskMethodInvocationHistory(default(global::System.Threading.Tasks.Task), ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// Task WhenSettingUpMethodReturnValue.ISut.ReturnsTypeTask()
/// </code>
/// </summary>
public class ReturnsTypeTaskMethodInvocationVerifier
{             
    private readonly ReturnsTypeTaskMethod _returnsTypeTaskMethod;

    public ReturnsTypeTaskMethodInvocationVerifier(
        ReturnsTypeTaskMethod returnsTypeTaskMethod
    )
    {
        this._returnsTypeTaskMethod = returnsTypeTaskMethod;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _returnsTypeTaskMethod.InvocationHistory.Count();
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

}

public class ISutImposterVerifier
{
    private readonly NoParametersWithReturnTypeMethod _noParametersWithReturnTypeMethod;
    private readonly ParameterAndReturnTypeMethod _parameterAndReturnTypeMethod;
    private readonly MethodWithRefParameterMethod _methodWithRefParameterMethod;
    private readonly MethodWithOutParameterMethod _methodWithOutParameterMethod;
    private readonly MethodWithParamsParameterMethod _methodWithParamsParameterMethod;
    private readonly ReturnsTypeTaskMethod _returnsTypeTaskMethod;

    public ISutImposterVerifier(
        NoParametersWithReturnTypeMethod noParametersWithReturnTypeMethod, ParameterAndReturnTypeMethod parameterAndReturnTypeMethod, MethodWithRefParameterMethod methodWithRefParameterMethod, MethodWithOutParameterMethod methodWithOutParameterMethod, MethodWithParamsParameterMethod methodWithParamsParameterMethod, ReturnsTypeTaskMethod returnsTypeTaskMethod
    )
    {
        this._noParametersWithReturnTypeMethod = noParametersWithReturnTypeMethod;
        this._parameterAndReturnTypeMethod = parameterAndReturnTypeMethod;
        this._methodWithRefParameterMethod = methodWithRefParameterMethod;
        this._methodWithOutParameterMethod = methodWithOutParameterMethod;
        this._methodWithParamsParameterMethod = methodWithParamsParameterMethod;
        this._returnsTypeTaskMethod = returnsTypeTaskMethod;
    }


    public NoParametersWithReturnTypeMethodInvocationVerifier NoParametersWithReturnType()
          => new(_noParametersWithReturnTypeMethod);
    public ParameterAndReturnTypeMethodInvocationVerifier ParameterAndReturnType(Arg<global::System.Int32> aArg, Arg<global::System.Int32> bArg)
          => new(_parameterAndReturnTypeMethod, aArg, bArg);
    public MethodWithRefParameterMethodInvocationVerifier MethodWithRefParameter(Arg<global::System.Int32> aArg)
          => new(_methodWithRefParameterMethod, aArg);
    public MethodWithOutParameterMethodInvocationVerifier MethodWithOutParameter(OutArg<global::System.Int32> aArg)
          => new(_methodWithOutParameterMethod, aArg);
    public MethodWithParamsParameterMethodInvocationVerifier MethodWithParamsParameter(Arg<global::System.Int32[]> aArg)
          => new(_methodWithParamsParameterMethod, aArg);
    public ReturnsTypeTaskMethodInvocationVerifier ReturnsTypeTask()
          => new(_returnsTypeTaskMethod);
}

public class ISutImposter : IHaveImposterVerifier<ISutImposterVerifier>, IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Setup.WhenSettingUpMethodReturnValue.ISut>
{
    private readonly NoParametersWithReturnTypeMethod _noParametersWithReturnTypeMethod;
    private readonly ParameterAndReturnTypeMethod _parameterAndReturnTypeMethod;
    private readonly MethodWithRefParameterMethod _methodWithRefParameterMethod;
    private readonly MethodWithOutParameterMethod _methodWithOutParameterMethod;
    private readonly MethodWithParamsParameterMethod _methodWithParamsParameterMethod;
    private readonly ReturnsTypeTaskMethod _returnsTypeTaskMethod;
    private readonly ISutImposterVerifier _verifier;
    private readonly global::Imposter.CodeGenerator.Tests.Setup.WhenSettingUpMethodReturnValue.ISut _imposterInstance;

    public ISutImposter()
    {
       _noParametersWithReturnTypeMethod = new();
       _parameterAndReturnTypeMethod = new();
       _methodWithRefParameterMethod = new();
       _methodWithOutParameterMethod = new();
       _methodWithParamsParameterMethod = new();
       _returnsTypeTaskMethod = new();
       _verifier = new(_noParametersWithReturnTypeMethod,_parameterAndReturnTypeMethod,_methodWithRefParameterMethod,_methodWithOutParameterMethod,_methodWithParamsParameterMethod,_returnsTypeTaskMethod);
       _imposterInstance = new ISutImposterInstance(this);
    }

    global::Imposter.CodeGenerator.Tests.Setup.WhenSettingUpMethodReturnValue.ISut IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Setup.WhenSettingUpMethodReturnValue.ISut>.Instance() => _imposterInstance;

    ISutImposterVerifier IHaveImposterVerifier<ISutImposterVerifier>.Verify() => _verifier;

    public NoParametersWithReturnTypeInvocationSetup NoParametersWithReturnType()
    {
        var invocationBehaviour = new NoParametersWithReturnTypeInvocationSetup();
        _noParametersWithReturnTypeMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public ParameterAndReturnTypeInvocationSetup ParameterAndReturnType(Arg<global::System.Int32> aArg, Arg<global::System.Int32> bArg)
    {
        var invocationBehaviour = new ParameterAndReturnTypeInvocationSetup(aArg, bArg);
        _parameterAndReturnTypeMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public MethodWithRefParameterInvocationSetup MethodWithRefParameter(Arg<global::System.Int32> aArg)
    {
        var invocationBehaviour = new MethodWithRefParameterInvocationSetup(aArg);
        _methodWithRefParameterMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public MethodWithOutParameterInvocationSetup MethodWithOutParameter(OutArg<global::System.Int32> aArg)
    {
        var invocationBehaviour = new MethodWithOutParameterInvocationSetup(aArg);
        _methodWithOutParameterMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public MethodWithParamsParameterInvocationSetup MethodWithParamsParameter(Arg<global::System.Int32[]> aArg)
    {
        var invocationBehaviour = new MethodWithParamsParameterInvocationSetup(aArg);
        _methodWithParamsParameterMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public ReturnsTypeTaskInvocationSetup ReturnsTypeTask()
    {
        var invocationBehaviour = new ReturnsTypeTaskInvocationSetup();
        _returnsTypeTaskMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
public class ISutImposterInstance : global::Imposter.CodeGenerator.Tests.Setup.WhenSettingUpMethodReturnValue.ISut
{
  private readonly ISutImposter _imposter;
  public ISutImposterInstance(ISutImposter imposter)
  {
      _imposter = imposter;
  }
  
      public global::System.Int32 NoParametersWithReturnType()
      {
          return _imposter._noParametersWithReturnTypeMethod.Invoke();
      }
      public global::System.Int32 ParameterAndReturnType( int a,  int b)
      {
          return _imposter._parameterAndReturnTypeMethod.Invoke( a,  b);
      }
      public global::System.Int32 MethodWithRefParameter(ref int a)
      {
          return _imposter._methodWithRefParameterMethod.Invoke(ref a);
      }
      public global::System.Int32 MethodWithOutParameter(out int a)
      {
          return _imposter._methodWithOutParameterMethod.Invoke(out a);
      }
      public global::System.Int32 MethodWithParamsParameter(params  int[] a)
      {
          return _imposter._methodWithParamsParameterMethod.Invoke( a);
      }
      public global::System.Threading.Tasks.Task ReturnsTypeTask()
      {
          return _imposter._returnsTypeTaskMethod.Invoke();
      }
    }
}
#nullable restore
