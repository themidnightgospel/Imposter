// TODO remove
#pragma warning disable nullable
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using Imposter;
using Imposter.Playground;

namespace Imposters.Imposter.Playground;
/// <summary>
/// a
/// <code>
/// void IOrderApiService.DoWork()
/// </code>
/// </summary>
public delegate void DoWorkDelegate();

public class DoWorkArguments
{
    public DoWorkArguments()
    {
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// void IOrderApiService.DoWork()
/// </code>
/// </summary>
public class DoWorkMethodInvocationHistory
{
    public System.Exception? Exception { get; }
  public DoWorkMethodInvocationHistory(System.Exception? exception)
{
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// void IOrderApiService.DoWork()
/// </code>
/// </summary>
public class DoWorkMethodInvocationBehaviour()
{
    private DoWorkDelegate? _callBefore;
    private DoWorkDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;

    public DoWorkMethodInvocationBehaviour CallBefore(DoWorkDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public DoWorkMethodInvocationBehaviour CallAfter(DoWorkDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public DoWorkMethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public DoWorkMethodInvocationBehaviour Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches()
    {
        return true;
    }

    public void Execute()
    {
        
        if(_callBefore is not null)
        {
            _callBefore();
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        if(_callAfter is not null)
        {
            _callAfter();
        }
        
    }
}

/// <summary>
/// method class
/// <code>
/// void IOrderApiService.DoWork()
/// </code>
/// </summary>
public class DoWorkMethod
{
    public List<DoWorkMethodInvocationBehaviour> Behaviours { get; } = new();
    public List<DoWorkMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public void Invoke()
    {

        try
        {
            DoWorkMethodInvocationBehaviour? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches())
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            
            
            if(matchingBehaviour is not null)
            {
               matchingBehaviour.Execute();
            }
            else
            {
                
                
            }
            
            InvocationHistory.Add(new DoWorkMethodInvocationHistory(null));
            
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new DoWorkMethodInvocationHistory(ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// void IOrderApiService.DoWork()
/// </code>
/// </summary>
public class DoWorkMethodInvocationVerifier
{             
    private readonly DoWorkMethod _doWorkMethod;

    public DoWorkMethodInvocationVerifier(
        DoWorkMethod doWorkMethod
    )
    {
        this._doWorkMethod = doWorkMethod;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _doWorkMethod.InvocationHistory.Count();
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

}

/// <summary>
/// a
/// <code>
/// int IOrderApiService.GetNumber()
/// </code>
/// </summary>
public delegate int GetNumberDelegate();

public class GetNumberArguments
{
    public GetNumberArguments()
    {
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// int IOrderApiService.GetNumber()
/// </code>
/// </summary>
public class GetNumberMethodInvocationHistory
{
    public global::System.Int32 Result { get; }
  public System.Exception? Exception { get; }
  public GetNumberMethodInvocationHistory(global::System.Int32 result, 
System.Exception? exception)
{
  Result = result;
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// int IOrderApiService.GetNumber()
/// </code>
/// </summary>
public class GetNumberMethodInvocationBehaviour()
{
    private GetNumberDelegate? _callBefore;
    private GetNumberDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;
    private GetNumberDelegate? _resultGenerator;

    public GetNumberMethodInvocationBehaviour CallBefore(GetNumberDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public GetNumberMethodInvocationBehaviour CallAfter(GetNumberDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public GetNumberMethodInvocationBehaviour Returns(GetNumberDelegate resultGenerator)
    {
       _resultGenerator = resultGenerator;
       return this;
    }

    public GetNumberMethodInvocationBehaviour Returns(global::System.Int32 result)
    {
       _resultGenerator = () =>
       {

            return result;
       };
       return this;
    }


    public GetNumberMethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public GetNumberMethodInvocationBehaviour Throws(Exception exception)
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
/// int IOrderApiService.GetNumber()
/// </code>
/// </summary>
public class GetNumberMethod
{
    public List<GetNumberMethodInvocationBehaviour> Behaviours { get; } = new();
    public List<GetNumberMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public global::System.Int32 Invoke()
    {

        try
        {
            GetNumberMethodInvocationBehaviour? matchingBehaviour = null;
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
            
            InvocationHistory.Add(new GetNumberMethodInvocationHistory(result, null));
            return result;
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new GetNumberMethodInvocationHistory(default(global::System.Int32), ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// int IOrderApiService.GetNumber()
/// </code>
/// </summary>
public class GetNumberMethodInvocationVerifier
{             
    private readonly GetNumberMethod _getNumberMethod;

    public GetNumberMethodInvocationVerifier(
        GetNumberMethod getNumberMethod
    )
    {
        this._getNumberMethod = getNumberMethod;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _getNumberMethod.InvocationHistory.Count();
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

}

/// <summary>
/// a
/// <code>
/// void IOrderApiService.SingleInParameter(in int value)
/// </code>
/// </summary>
public delegate void SingleInParameterDelegate(in int value);

public class SingleInParameterArguments
{
    public int value { get; }

    public SingleInParameterArguments(int value)
    {
        this.value = value;
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// void IOrderApiService.SingleInParameter(in int value)
/// </code>
/// </summary>
public class SingleInParameterMethodInvocationHistory
{
    public SingleInParameterArguments Arguments { get; }
  public System.Exception? Exception { get; }
  public SingleInParameterMethodInvocationHistory(SingleInParameterArguments arguments, System.Exception? exception)
{
  Arguments = arguments;
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// void IOrderApiService.SingleInParameter(in int value)
/// </code>
/// </summary>
public class SingleInParameterMethodInvocationBehaviour(Arg<global::System.Int32> valueArg)
{
    private SingleInParameterDelegate? _callBefore;
    private SingleInParameterDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;

    public SingleInParameterMethodInvocationBehaviour CallBefore(SingleInParameterDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public SingleInParameterMethodInvocationBehaviour CallAfter(SingleInParameterDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public SingleInParameterMethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public SingleInParameterMethodInvocationBehaviour Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches(in int value)
    {
        return valueArg.Predicate(value);
    }

    public void Execute(in int value)
    {
        
        if(_callBefore is not null)
        {
            _callBefore(in value);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        if(_callAfter is not null)
        {
            _callAfter(in value);
        }
        
    }
}

/// <summary>
/// method class
/// <code>
/// void IOrderApiService.SingleInParameter(in int value)
/// </code>
/// </summary>
public class SingleInParameterMethod
{
    public List<SingleInParameterMethodInvocationBehaviour> Behaviours { get; } = new();
    public List<SingleInParameterMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public void Invoke(in int value)
    {

        try
        {
            SingleInParameterMethodInvocationBehaviour? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches(in value))
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            
            
            if(matchingBehaviour is not null)
            {
               matchingBehaviour.Execute(in value);
            }
            else
            {
                
                
            }
            
            InvocationHistory.Add(new SingleInParameterMethodInvocationHistory(new SingleInParameterArguments(value),null));
            
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new SingleInParameterMethodInvocationHistory(new SingleInParameterArguments(value),ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// void IOrderApiService.SingleInParameter(in int value)
/// </code>
/// </summary>
public class SingleInParameterMethodInvocationVerifier
{             
    private readonly SingleInParameterMethod _singleInParameterMethod;
    private readonly Arg<global::System.Int32> _valueArg;

    public SingleInParameterMethodInvocationVerifier(
        SingleInParameterMethod singleInParameterMethod, Arg<global::System.Int32> valueArg
    )
    {
        this._singleInParameterMethod = singleInParameterMethod;
        this._valueArg = valueArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _singleInParameterMethod.InvocationHistory.Count(it => Matches(it.Arguments));
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

    public bool Matches(SingleInParameterArguments arguments)
    {
        return _valueArg.Predicate(arguments.value);
    }
}

/// <summary>
/// a
/// <code>
/// void IOrderApiService.InAndNormalParameter(in int value, string name)
/// </code>
/// </summary>
public delegate void InAndNormalParameterDelegate(in int value, string name);

public class InAndNormalParameterArguments
{
    public int value { get; }
    public string name { get; }

    public InAndNormalParameterArguments(int value, string name)
    {
        this.value = value;
        this.name = name;
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// void IOrderApiService.InAndNormalParameter(in int value, string name)
/// </code>
/// </summary>
public class InAndNormalParameterMethodInvocationHistory
{
    public InAndNormalParameterArguments Arguments { get; }
  public System.Exception? Exception { get; }
  public InAndNormalParameterMethodInvocationHistory(InAndNormalParameterArguments arguments, System.Exception? exception)
{
  Arguments = arguments;
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// void IOrderApiService.InAndNormalParameter(in int value, string name)
/// </code>
/// </summary>
public class InAndNormalParameterMethodInvocationBehaviour(Arg<global::System.Int32> valueArg, Arg<global::System.String> nameArg)
{
    private InAndNormalParameterDelegate? _callBefore;
    private InAndNormalParameterDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;

    public InAndNormalParameterMethodInvocationBehaviour CallBefore(InAndNormalParameterDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public InAndNormalParameterMethodInvocationBehaviour CallAfter(InAndNormalParameterDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public InAndNormalParameterMethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public InAndNormalParameterMethodInvocationBehaviour Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches(in int value,  string name)
    {
        return valueArg.Predicate(value) && nameArg.Predicate(name);
    }

    public void Execute(in int value,  string name)
    {
        
        if(_callBefore is not null)
        {
            _callBefore(in value,  name);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        if(_callAfter is not null)
        {
            _callAfter(in value,  name);
        }
        
    }
}

/// <summary>
/// method class
/// <code>
/// void IOrderApiService.InAndNormalParameter(in int value, string name)
/// </code>
/// </summary>
public class InAndNormalParameterMethod
{
    public List<InAndNormalParameterMethodInvocationBehaviour> Behaviours { get; } = new();
    public List<InAndNormalParameterMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public void Invoke(in int value,  string name)
    {

        try
        {
            InAndNormalParameterMethodInvocationBehaviour? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches(in value,  name))
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            
            
            if(matchingBehaviour is not null)
            {
               matchingBehaviour.Execute(in value,  name);
            }
            else
            {
                
                
            }
            
            InvocationHistory.Add(new InAndNormalParameterMethodInvocationHistory(new InAndNormalParameterArguments(value, name),null));
            
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new InAndNormalParameterMethodInvocationHistory(new InAndNormalParameterArguments(value, name),ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// void IOrderApiService.InAndNormalParameter(in int value, string name)
/// </code>
/// </summary>
public class InAndNormalParameterMethodInvocationVerifier
{             
    private readonly InAndNormalParameterMethod _inAndNormalParameterMethod;
    private readonly Arg<global::System.Int32> _valueArg;
    private readonly Arg<global::System.String> _nameArg;

    public InAndNormalParameterMethodInvocationVerifier(
        InAndNormalParameterMethod inAndNormalParameterMethod, Arg<global::System.Int32> valueArg, Arg<global::System.String> nameArg
    )
    {
        this._inAndNormalParameterMethod = inAndNormalParameterMethod;
        this._valueArg = valueArg;
        this._nameArg = nameArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _inAndNormalParameterMethod.InvocationHistory.Count(it => Matches(it.Arguments));
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

    public bool Matches(InAndNormalParameterArguments arguments)
    {
        return _valueArg.Predicate(arguments.value) && _nameArg.Predicate(arguments.name);
    }
}

/// <summary>
/// a
/// <code>
/// void IOrderApiService.TwoInParameters(in int value, in double age)
/// </code>
/// </summary>
public delegate void TwoInParametersDelegate(in int value, in double age);

public class TwoInParametersArguments
{
    public int value { get; }
    public double age { get; }

    public TwoInParametersArguments(int value, double age)
    {
        this.value = value;
        this.age = age;
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// void IOrderApiService.TwoInParameters(in int value, in double age)
/// </code>
/// </summary>
public class TwoInParametersMethodInvocationHistory
{
    public TwoInParametersArguments Arguments { get; }
  public System.Exception? Exception { get; }
  public TwoInParametersMethodInvocationHistory(TwoInParametersArguments arguments, System.Exception? exception)
{
  Arguments = arguments;
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// void IOrderApiService.TwoInParameters(in int value, in double age)
/// </code>
/// </summary>
public class TwoInParametersMethodInvocationBehaviour(Arg<global::System.Int32> valueArg, Arg<global::System.Double> ageArg)
{
    private TwoInParametersDelegate? _callBefore;
    private TwoInParametersDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;

    public TwoInParametersMethodInvocationBehaviour CallBefore(TwoInParametersDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public TwoInParametersMethodInvocationBehaviour CallAfter(TwoInParametersDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public TwoInParametersMethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public TwoInParametersMethodInvocationBehaviour Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches(in int value, in double age)
    {
        return valueArg.Predicate(value) && ageArg.Predicate(age);
    }

    public void Execute(in int value, in double age)
    {
        
        if(_callBefore is not null)
        {
            _callBefore(in value, in age);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        if(_callAfter is not null)
        {
            _callAfter(in value, in age);
        }
        
    }
}

/// <summary>
/// method class
/// <code>
/// void IOrderApiService.TwoInParameters(in int value, in double age)
/// </code>
/// </summary>
public class TwoInParametersMethod
{
    public List<TwoInParametersMethodInvocationBehaviour> Behaviours { get; } = new();
    public List<TwoInParametersMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public void Invoke(in int value, in double age)
    {

        try
        {
            TwoInParametersMethodInvocationBehaviour? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches(in value, in age))
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            
            
            if(matchingBehaviour is not null)
            {
               matchingBehaviour.Execute(in value, in age);
            }
            else
            {
                
                
            }
            
            InvocationHistory.Add(new TwoInParametersMethodInvocationHistory(new TwoInParametersArguments(value, age),null));
            
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new TwoInParametersMethodInvocationHistory(new TwoInParametersArguments(value, age),ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// void IOrderApiService.TwoInParameters(in int value, in double age)
/// </code>
/// </summary>
public class TwoInParametersMethodInvocationVerifier
{             
    private readonly TwoInParametersMethod _twoInParametersMethod;
    private readonly Arg<global::System.Int32> _valueArg;
    private readonly Arg<global::System.Double> _ageArg;

    public TwoInParametersMethodInvocationVerifier(
        TwoInParametersMethod twoInParametersMethod, Arg<global::System.Int32> valueArg, Arg<global::System.Double> ageArg
    )
    {
        this._twoInParametersMethod = twoInParametersMethod;
        this._valueArg = valueArg;
        this._ageArg = ageArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _twoInParametersMethod.InvocationHistory.Count(it => Matches(it.Arguments));
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

    public bool Matches(TwoInParametersArguments arguments)
    {
        return _valueArg.Predicate(arguments.value) && _ageArg.Predicate(arguments.age);
    }
}

/// <summary>
/// a
/// <code>
/// void IOrderApiService.Modify(ref int value)
/// </code>
/// </summary>
public delegate void ModifyDelegate(ref int value);

public class ModifyArguments
{
    public int value { get; }

    public ModifyArguments(int value)
    {
        this.value = value;
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// void IOrderApiService.Modify(ref int value)
/// </code>
/// </summary>
public class ModifyMethodInvocationHistory
{
    public ModifyArguments Arguments { get; }
  public System.Exception? Exception { get; }
  public ModifyMethodInvocationHistory(ModifyArguments arguments, System.Exception? exception)
{
  Arguments = arguments;
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// void IOrderApiService.Modify(ref int value)
/// </code>
/// </summary>
public class ModifyMethodInvocationBehaviour(Arg<global::System.Int32> valueArg)
{
    private ModifyDelegate? _callBefore;
    private ModifyDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;

    public ModifyMethodInvocationBehaviour CallBefore(ModifyDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public ModifyMethodInvocationBehaviour CallAfter(ModifyDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public ModifyMethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public ModifyMethodInvocationBehaviour Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches(ref int value)
    {
        return valueArg.Predicate(value);
    }

    public void Execute(ref int value)
    {
        
        if(_callBefore is not null)
        {
            _callBefore(ref value);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        if(_callAfter is not null)
        {
            _callAfter(ref value);
        }
        
    }
}

/// <summary>
/// method class
/// <code>
/// void IOrderApiService.Modify(ref int value)
/// </code>
/// </summary>
public class ModifyMethod
{
    public List<ModifyMethodInvocationBehaviour> Behaviours { get; } = new();
    public List<ModifyMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public void Invoke(ref int value)
    {

        try
        {
            ModifyMethodInvocationBehaviour? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches(ref value))
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            
            
            if(matchingBehaviour is not null)
            {
               matchingBehaviour.Execute(ref value);
            }
            else
            {
                
                
            }
            
            InvocationHistory.Add(new ModifyMethodInvocationHistory(new ModifyArguments(value),null));
            
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new ModifyMethodInvocationHistory(new ModifyArguments(value),ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// void IOrderApiService.Modify(ref int value)
/// </code>
/// </summary>
public class ModifyMethodInvocationVerifier
{             
    private readonly ModifyMethod _modifyMethod;
    private readonly Arg<global::System.Int32> _valueArg;

    public ModifyMethodInvocationVerifier(
        ModifyMethod modifyMethod, Arg<global::System.Int32> valueArg
    )
    {
        this._modifyMethod = modifyMethod;
        this._valueArg = valueArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _modifyMethod.InvocationHistory.Count(it => Matches(it.Arguments));
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

    public bool Matches(ModifyArguments arguments)
    {
        return _valueArg.Predicate(arguments.value);
    }
}

/// <summary>
/// a
/// <code>
/// void IOrderApiService.Initialize(out int value)
/// </code>
/// </summary>
public delegate void InitializeDelegate(out int value);

public class InitializeArguments
{
    public int value { get; }

    public InitializeArguments(int value)
    {
        this.value = value;
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// void IOrderApiService.Initialize(out int value)
/// </code>
/// </summary>
public class InitializeMethodInvocationHistory
{
    public InitializeArguments Arguments { get; }
  public System.Exception? Exception { get; }
  public InitializeMethodInvocationHistory(InitializeArguments arguments, System.Exception? exception)
{
  Arguments = arguments;
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// void IOrderApiService.Initialize(out int value)
/// </code>
/// </summary>
public class InitializeMethodInvocationBehaviour(OutArg<global::System.Int32> valueArg)
{
    private InitializeDelegate? _callBefore;
    private InitializeDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;

    public InitializeMethodInvocationBehaviour CallBefore(InitializeDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public InitializeMethodInvocationBehaviour CallAfter(InitializeDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public InitializeMethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public InitializeMethodInvocationBehaviour Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches()
    {
        return true;
    }

    public void Execute(out int value)
    {
        
    value = default(int);
        if(_callBefore is not null)
        {
            _callBefore(out value);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        if(_callAfter is not null)
        {
            _callAfter(out value);
        }
        
    }
}

/// <summary>
/// method class
/// <code>
/// void IOrderApiService.Initialize(out int value)
/// </code>
/// </summary>
public class InitializeMethod
{
    public List<InitializeMethodInvocationBehaviour> Behaviours { get; } = new();
    public List<InitializeMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public void Invoke(out int value)
    {

        try
        {
            InitializeMethodInvocationBehaviour? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches())
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            
            
            if(matchingBehaviour is not null)
            {
               matchingBehaviour.Execute(out value);
            }
            else
            {
                value = default(int);

                
            }
            
            InvocationHistory.Add(new InitializeMethodInvocationHistory(new InitializeArguments(value),null));
            
        }
        catch (Exception ex)
        {
            value = default(int);

            InvocationHistory.Add(new InitializeMethodInvocationHistory(new InitializeArguments(value),ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// void IOrderApiService.Initialize(out int value)
/// </code>
/// </summary>
public class InitializeMethodInvocationVerifier
{             
    private readonly InitializeMethod _initializeMethod;
    private readonly OutArg<global::System.Int32> _valueArg;

    public InitializeMethodInvocationVerifier(
        InitializeMethod initializeMethod, OutArg<global::System.Int32> valueArg
    )
    {
        this._initializeMethod = initializeMethod;
        this._valueArg = valueArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _initializeMethod.InvocationHistory.Count();
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

}

/// <summary>
/// a
/// <code>
/// void IOrderApiService.AddNumbers(params int[] numbers)
/// </code>
/// </summary>
public delegate void AddNumbersDelegate(int[] numbers);

public class AddNumbersArguments
{
    public int[] numbers { get; }

    public AddNumbersArguments(int[] numbers)
    {
        this.numbers = numbers;
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// void IOrderApiService.AddNumbers(params int[] numbers)
/// </code>
/// </summary>
public class AddNumbersMethodInvocationHistory
{
    public AddNumbersArguments Arguments { get; }
  public System.Exception? Exception { get; }
  public AddNumbersMethodInvocationHistory(AddNumbersArguments arguments, System.Exception? exception)
{
  Arguments = arguments;
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// void IOrderApiService.AddNumbers(params int[] numbers)
/// </code>
/// </summary>
public class AddNumbersMethodInvocationBehaviour(Arg<global::System.Int32[]> numbersArg)
{
    private AddNumbersDelegate? _callBefore;
    private AddNumbersDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;

    public AddNumbersMethodInvocationBehaviour CallBefore(AddNumbersDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public AddNumbersMethodInvocationBehaviour CallAfter(AddNumbersDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public AddNumbersMethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public AddNumbersMethodInvocationBehaviour Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches(params  int[] numbers)
    {
        return numbersArg.Predicate(numbers);
    }

    public void Execute(params  int[] numbers)
    {
        
        if(_callBefore is not null)
        {
            _callBefore( numbers);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        if(_callAfter is not null)
        {
            _callAfter( numbers);
        }
        
    }
}

/// <summary>
/// method class
/// <code>
/// void IOrderApiService.AddNumbers(params int[] numbers)
/// </code>
/// </summary>
public class AddNumbersMethod
{
    public List<AddNumbersMethodInvocationBehaviour> Behaviours { get; } = new();
    public List<AddNumbersMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public void Invoke(params  int[] numbers)
    {

        try
        {
            AddNumbersMethodInvocationBehaviour? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches( numbers))
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            
            
            if(matchingBehaviour is not null)
            {
               matchingBehaviour.Execute( numbers);
            }
            else
            {
                
                
            }
            
            InvocationHistory.Add(new AddNumbersMethodInvocationHistory(new AddNumbersArguments(numbers),null));
            
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new AddNumbersMethodInvocationHistory(new AddNumbersArguments(numbers),ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// void IOrderApiService.AddNumbers(params int[] numbers)
/// </code>
/// </summary>
public class AddNumbersMethodInvocationVerifier
{             
    private readonly AddNumbersMethod _addNumbersMethod;
    private readonly Arg<global::System.Int32[]> _numbersArg;

    public AddNumbersMethodInvocationVerifier(
        AddNumbersMethod addNumbersMethod, Arg<global::System.Int32[]> numbersArg
    )
    {
        this._addNumbersMethod = addNumbersMethod;
        this._numbersArg = numbersArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _addNumbersMethod.InvocationHistory.Count(it => Matches(it.Arguments));
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

    public bool Matches(AddNumbersArguments arguments)
    {
        return _numbersArg.Predicate(arguments.numbers);
    }
}

/// <summary>
/// a
/// <code>
/// void IOrderApiService.Greet(string name = "Guest")
/// </code>
/// </summary>
public delegate void GreetDelegate(string name = "Guest");

public class GreetArguments
{
    public string name { get; }

    public GreetArguments(string name = "Guest")
    {
        this.name = name;
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// void IOrderApiService.Greet(string name = "Guest")
/// </code>
/// </summary>
public class GreetMethodInvocationHistory
{
    public GreetArguments Arguments { get; }
  public System.Exception? Exception { get; }
  public GreetMethodInvocationHistory(GreetArguments arguments, System.Exception? exception)
{
  Arguments = arguments;
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// void IOrderApiService.Greet(string name = "Guest")
/// </code>
/// </summary>
public class GreetMethodInvocationBehaviour(Arg<global::System.String> nameArg)
{
    private GreetDelegate? _callBefore;
    private GreetDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;

    public GreetMethodInvocationBehaviour CallBefore(GreetDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public GreetMethodInvocationBehaviour CallAfter(GreetDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public GreetMethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public GreetMethodInvocationBehaviour Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches( string name)
    {
        return nameArg.Predicate(name);
    }

    public void Execute( string name)
    {
        
        if(_callBefore is not null)
        {
            _callBefore( name);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        if(_callAfter is not null)
        {
            _callAfter( name);
        }
        
    }
}

/// <summary>
/// method class
/// <code>
/// void IOrderApiService.Greet(string name = "Guest")
/// </code>
/// </summary>
public class GreetMethod
{
    public List<GreetMethodInvocationBehaviour> Behaviours { get; } = new();
    public List<GreetMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public void Invoke( string name)
    {

        try
        {
            GreetMethodInvocationBehaviour? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches( name))
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            
            
            if(matchingBehaviour is not null)
            {
               matchingBehaviour.Execute( name);
            }
            else
            {
                
                
            }
            
            InvocationHistory.Add(new GreetMethodInvocationHistory(new GreetArguments(name),null));
            
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new GreetMethodInvocationHistory(new GreetArguments(name),ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// void IOrderApiService.Greet(string name = "Guest")
/// </code>
/// </summary>
public class GreetMethodInvocationVerifier
{             
    private readonly GreetMethod _greetMethod;
    private readonly Arg<global::System.String> _nameArg;

    public GreetMethodInvocationVerifier(
        GreetMethod greetMethod, Arg<global::System.String> nameArg
    )
    {
        this._greetMethod = greetMethod;
        this._nameArg = nameArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _greetMethod.InvocationHistory.Count(it => Matches(it.Arguments));
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

    public bool Matches(GreetArguments arguments)
    {
        return _nameArg.Predicate(arguments.name);
    }
}

/// <summary>
/// a
/// <code>
/// string? IOrderApiService.FindUser(string? username)
/// </code>
/// </summary>
public delegate string FindUserDelegate(string? username);

public class FindUserArguments
{
    public string username { get; }

    public FindUserArguments(string? username)
    {
        this.username = username;
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// string? IOrderApiService.FindUser(string? username)
/// </code>
/// </summary>
public class FindUserMethodInvocationHistory
{
    public FindUserArguments Arguments { get; }
  public global::System.String? Result { get; }
  public System.Exception? Exception { get; }
  public FindUserMethodInvocationHistory(FindUserArguments arguments, global::System.String? result, 
System.Exception? exception)
{
  Arguments = arguments;
  Result = result;
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// string? IOrderApiService.FindUser(string? username)
/// </code>
/// </summary>
public class FindUserMethodInvocationBehaviour(Arg<global::System.String?> usernameArg)
{
    private FindUserDelegate? _callBefore;
    private FindUserDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;
    private FindUserDelegate? _resultGenerator;

    public FindUserMethodInvocationBehaviour CallBefore(FindUserDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public FindUserMethodInvocationBehaviour CallAfter(FindUserDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public FindUserMethodInvocationBehaviour Returns(FindUserDelegate resultGenerator)
    {
       _resultGenerator = resultGenerator;
       return this;
    }

    public FindUserMethodInvocationBehaviour Returns(global::System.String? result)
    {
       _resultGenerator = ( string username) =>
       {

            return result;
       };
       return this;
    }


    public FindUserMethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public FindUserMethodInvocationBehaviour Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches( string username)
    {
        return usernameArg.Predicate(username);
    }

    public global::System.String? Execute( string username)
    {
        
        if(_callBefore is not null)
        {
            _callBefore( username);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        var result = _resultGenerator?.Invoke( username) ?? default(global::System.String?);
        
        if(_callAfter is not null)
        {
            _callAfter( username);
        }
        
        return result;
    }
}

/// <summary>
/// method class
/// <code>
/// string? IOrderApiService.FindUser(string? username)
/// </code>
/// </summary>
public class FindUserMethod
{
    public List<FindUserMethodInvocationBehaviour> Behaviours { get; } = new();
    public List<FindUserMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public global::System.String? Invoke( string username)
    {

        try
        {
            FindUserMethodInvocationBehaviour? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches( username))
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            global::System.String? result;
            
            if(matchingBehaviour is not null)
            {
               result = matchingBehaviour.Execute( username);
            }
            else
            {
                
                result = default(global::System.String?);
            }
            
            InvocationHistory.Add(new FindUserMethodInvocationHistory(new FindUserArguments(username),result, null));
            return result;
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new FindUserMethodInvocationHistory(new FindUserArguments(username),default(global::System.String?), ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// string? IOrderApiService.FindUser(string? username)
/// </code>
/// </summary>
public class FindUserMethodInvocationVerifier
{             
    private readonly FindUserMethod _findUserMethod;
    private readonly Arg<global::System.String?> _usernameArg;

    public FindUserMethodInvocationVerifier(
        FindUserMethod findUserMethod, Arg<global::System.String?> usernameArg
    )
    {
        this._findUserMethod = findUserMethod;
        this._usernameArg = usernameArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _findUserMethod.InvocationHistory.Count(it => Matches(it.Arguments));
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

    public bool Matches(FindUserArguments arguments)
    {
        return _usernameArg.Predicate(arguments.username);
    }
}

/// <summary>
/// a
/// <code>
/// (int sum, int count) IOrderApiService.Calculate(int[] numbers)
/// </code>
/// </summary>
public delegate (int sum, int count) CalculateDelegate(int[] numbers);

public class CalculateArguments
{
    public int[] numbers { get; }

    public CalculateArguments(int[] numbers)
    {
        this.numbers = numbers;
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// (int sum, int count) IOrderApiService.Calculate(int[] numbers)
/// </code>
/// </summary>
public class CalculateMethodInvocationHistory
{
    public CalculateArguments Arguments { get; }
  public (global::System.Int32 sum, global::System.Int32 count) Result { get; }
  public System.Exception? Exception { get; }
  public CalculateMethodInvocationHistory(CalculateArguments arguments, (global::System.Int32 sum, global::System.Int32 count) result, 
System.Exception? exception)
{
  Arguments = arguments;
  Result = result;
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// (int sum, int count) IOrderApiService.Calculate(int[] numbers)
/// </code>
/// </summary>
public class CalculateMethodInvocationBehaviour(Arg<global::System.Int32[]> numbersArg)
{
    private CalculateDelegate? _callBefore;
    private CalculateDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;
    private CalculateDelegate? _resultGenerator;

    public CalculateMethodInvocationBehaviour CallBefore(CalculateDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public CalculateMethodInvocationBehaviour CallAfter(CalculateDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public CalculateMethodInvocationBehaviour Returns(CalculateDelegate resultGenerator)
    {
       _resultGenerator = resultGenerator;
       return this;
    }

    public CalculateMethodInvocationBehaviour Returns((global::System.Int32 sum, global::System.Int32 count) result)
    {
       _resultGenerator = ( int[] numbers) =>
       {

            return result;
       };
       return this;
    }


    public CalculateMethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public CalculateMethodInvocationBehaviour Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches( int[] numbers)
    {
        return numbersArg.Predicate(numbers);
    }

    public (global::System.Int32 sum, global::System.Int32 count) Execute( int[] numbers)
    {
        
        if(_callBefore is not null)
        {
            _callBefore( numbers);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        var result = _resultGenerator?.Invoke( numbers) ?? default((global::System.Int32 sum, global::System.Int32 count));
        
        if(_callAfter is not null)
        {
            _callAfter( numbers);
        }
        
        return result;
    }
}

/// <summary>
/// method class
/// <code>
/// (int sum, int count) IOrderApiService.Calculate(int[] numbers)
/// </code>
/// </summary>
public class CalculateMethod
{
    public List<CalculateMethodInvocationBehaviour> Behaviours { get; } = new();
    public List<CalculateMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public (global::System.Int32 sum, global::System.Int32 count) Invoke( int[] numbers)
    {

        try
        {
            CalculateMethodInvocationBehaviour? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches( numbers))
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            (global::System.Int32 sum, global::System.Int32 count) result;
            
            if(matchingBehaviour is not null)
            {
               result = matchingBehaviour.Execute( numbers);
            }
            else
            {
                
                result = default((global::System.Int32 sum, global::System.Int32 count));
            }
            
            InvocationHistory.Add(new CalculateMethodInvocationHistory(new CalculateArguments(numbers),result, null));
            return result;
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new CalculateMethodInvocationHistory(new CalculateArguments(numbers),default((global::System.Int32 sum, global::System.Int32 count)), ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// (int sum, int count) IOrderApiService.Calculate(int[] numbers)
/// </code>
/// </summary>
public class CalculateMethodInvocationVerifier
{             
    private readonly CalculateMethod _calculateMethod;
    private readonly Arg<global::System.Int32[]> _numbersArg;

    public CalculateMethodInvocationVerifier(
        CalculateMethod calculateMethod, Arg<global::System.Int32[]> numbersArg
    )
    {
        this._calculateMethod = calculateMethod;
        this._numbersArg = numbersArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _calculateMethod.InvocationHistory.Count(it => Matches(it.Arguments));
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

    public bool Matches(CalculateArguments arguments)
    {
        return _numbersArg.Predicate(arguments.numbers);
    }
}

/// <summary>
/// a
/// <code>
/// Task IOrderApiService.SaveAsync()
/// </code>
/// </summary>
public delegate global::System.Threading.Tasks.Task SaveAsyncDelegate();

public class SaveAsyncArguments
{
    public SaveAsyncArguments()
    {
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// Task IOrderApiService.SaveAsync()
/// </code>
/// </summary>
public class SaveAsyncMethodInvocationHistory
{
    public global::System.Threading.Tasks.Task Result { get; }
  public System.Exception? Exception { get; }
  public SaveAsyncMethodInvocationHistory(global::System.Threading.Tasks.Task result, 
System.Exception? exception)
{
  Result = result;
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// Task IOrderApiService.SaveAsync()
/// </code>
/// </summary>
public class SaveAsyncMethodInvocationBehaviour()
{
    private SaveAsyncDelegate? _callBefore;
    private SaveAsyncDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;
    private SaveAsyncDelegate? _resultGenerator;

    public SaveAsyncMethodInvocationBehaviour CallBefore(SaveAsyncDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public SaveAsyncMethodInvocationBehaviour CallAfter(SaveAsyncDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public SaveAsyncMethodInvocationBehaviour Returns(SaveAsyncDelegate resultGenerator)
    {
       _resultGenerator = resultGenerator;
       return this;
    }

    public SaveAsyncMethodInvocationBehaviour Returns(global::System.Threading.Tasks.Task result)
    {
       _resultGenerator = () =>
       {

            return result;
       };
       return this;
    }


    public SaveAsyncMethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public SaveAsyncMethodInvocationBehaviour Throws(Exception exception)
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
/// Task IOrderApiService.SaveAsync()
/// </code>
/// </summary>
public class SaveAsyncMethod
{
    public List<SaveAsyncMethodInvocationBehaviour> Behaviours { get; } = new();
    public List<SaveAsyncMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public global::System.Threading.Tasks.Task Invoke()
    {

        try
        {
            SaveAsyncMethodInvocationBehaviour? matchingBehaviour = null;
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
            
            InvocationHistory.Add(new SaveAsyncMethodInvocationHistory(result, null));
            return result;
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new SaveAsyncMethodInvocationHistory(default(global::System.Threading.Tasks.Task), ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// Task IOrderApiService.SaveAsync()
/// </code>
/// </summary>
public class SaveAsyncMethodInvocationVerifier
{             
    private readonly SaveAsyncMethod _saveAsyncMethod;

    public SaveAsyncMethodInvocationVerifier(
        SaveAsyncMethod saveAsyncMethod
    )
    {
        this._saveAsyncMethod = saveAsyncMethod;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _saveAsyncMethod.InvocationHistory.Count();
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

}

/// <summary>
/// a
/// <code>
/// ValueTask<bool> IOrderApiService.TrySaveAsync()
/// </code>
/// </summary>
public delegate global::System.Threading.Tasks.ValueTask<bool> TrySaveAsyncDelegate();

public class TrySaveAsyncArguments
{
    public TrySaveAsyncArguments()
    {
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// ValueTask<bool> IOrderApiService.TrySaveAsync()
/// </code>
/// </summary>
public class TrySaveAsyncMethodInvocationHistory
{
    public global::System.Threading.Tasks.ValueTask<global::System.Boolean> Result { get; }
  public System.Exception? Exception { get; }
  public TrySaveAsyncMethodInvocationHistory(global::System.Threading.Tasks.ValueTask<global::System.Boolean> result, 
System.Exception? exception)
{
  Result = result;
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// ValueTask<bool> IOrderApiService.TrySaveAsync()
/// </code>
/// </summary>
public class TrySaveAsyncMethodInvocationBehaviour()
{
    private TrySaveAsyncDelegate? _callBefore;
    private TrySaveAsyncDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;
    private TrySaveAsyncDelegate? _resultGenerator;

    public TrySaveAsyncMethodInvocationBehaviour CallBefore(TrySaveAsyncDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public TrySaveAsyncMethodInvocationBehaviour CallAfter(TrySaveAsyncDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public TrySaveAsyncMethodInvocationBehaviour Returns(TrySaveAsyncDelegate resultGenerator)
    {
       _resultGenerator = resultGenerator;
       return this;
    }

    public TrySaveAsyncMethodInvocationBehaviour Returns(global::System.Threading.Tasks.ValueTask<global::System.Boolean> result)
    {
       _resultGenerator = () =>
       {

            return result;
       };
       return this;
    }


    public TrySaveAsyncMethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public TrySaveAsyncMethodInvocationBehaviour Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches()
    {
        return true;
    }

    public global::System.Threading.Tasks.ValueTask<global::System.Boolean> Execute()
    {
        
        if(_callBefore is not null)
        {
            _callBefore();
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        var result = _resultGenerator?.Invoke() ?? default(global::System.Threading.Tasks.ValueTask<global::System.Boolean>);
        
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
/// ValueTask<bool> IOrderApiService.TrySaveAsync()
/// </code>
/// </summary>
public class TrySaveAsyncMethod
{
    public List<TrySaveAsyncMethodInvocationBehaviour> Behaviours { get; } = new();
    public List<TrySaveAsyncMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public global::System.Threading.Tasks.ValueTask<global::System.Boolean> Invoke()
    {

        try
        {
            TrySaveAsyncMethodInvocationBehaviour? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches())
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            global::System.Threading.Tasks.ValueTask<global::System.Boolean> result;
            
            if(matchingBehaviour is not null)
            {
               result = matchingBehaviour.Execute();
            }
            else
            {
                
                result = default(global::System.Threading.Tasks.ValueTask<global::System.Boolean>);
            }
            
            InvocationHistory.Add(new TrySaveAsyncMethodInvocationHistory(result, null));
            return result;
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new TrySaveAsyncMethodInvocationHistory(default(global::System.Threading.Tasks.ValueTask<global::System.Boolean>), ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// ValueTask<bool> IOrderApiService.TrySaveAsync()
/// </code>
/// </summary>
public class TrySaveAsyncMethodInvocationVerifier
{             
    private readonly TrySaveAsyncMethod _trySaveAsyncMethod;

    public TrySaveAsyncMethodInvocationVerifier(
        TrySaveAsyncMethod trySaveAsyncMethod
    )
    {
        this._trySaveAsyncMethod = trySaveAsyncMethod;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _trySaveAsyncMethod.InvocationHistory.Count();
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

}

/// <summary>
/// a
/// <code>
/// void IOrderApiService.Log(string message)
/// </code>
/// </summary>
public delegate void LogDelegate(string message);

public class LogArguments
{
    public string message { get; }

    public LogArguments(string message)
    {
        this.message = message;
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// void IOrderApiService.Log(string message)
/// </code>
/// </summary>
public class LogMethodInvocationHistory
{
    public LogArguments Arguments { get; }
  public System.Exception? Exception { get; }
  public LogMethodInvocationHistory(LogArguments arguments, System.Exception? exception)
{
  Arguments = arguments;
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// void IOrderApiService.Log(string message)
/// </code>
/// </summary>
public class LogMethodInvocationBehaviour(Arg<global::System.String> messageArg)
{
    private LogDelegate? _callBefore;
    private LogDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;

    public LogMethodInvocationBehaviour CallBefore(LogDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public LogMethodInvocationBehaviour CallAfter(LogDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public LogMethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public LogMethodInvocationBehaviour Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches( string message)
    {
        return messageArg.Predicate(message);
    }

    public void Execute( string message)
    {
        
        if(_callBefore is not null)
        {
            _callBefore( message);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        if(_callAfter is not null)
        {
            _callAfter( message);
        }
        
    }
}

/// <summary>
/// method class
/// <code>
/// void IOrderApiService.Log(string message)
/// </code>
/// </summary>
public class LogMethod
{
    public List<LogMethodInvocationBehaviour> Behaviours { get; } = new();
    public List<LogMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public void Invoke( string message)
    {

        try
        {
            LogMethodInvocationBehaviour? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches( message))
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            
            
            if(matchingBehaviour is not null)
            {
               matchingBehaviour.Execute( message);
            }
            else
            {
                
                
            }
            
            InvocationHistory.Add(new LogMethodInvocationHistory(new LogArguments(message),null));
            
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new LogMethodInvocationHistory(new LogArguments(message),ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// void IOrderApiService.Log(string message)
/// </code>
/// </summary>
public class LogMethodInvocationVerifier
{             
    private readonly LogMethod _logMethod;
    private readonly Arg<global::System.String> _messageArg;

    public LogMethodInvocationVerifier(
        LogMethod logMethod, Arg<global::System.String> messageArg
    )
    {
        this._logMethod = logMethod;
        this._messageArg = messageArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _logMethod.InvocationHistory.Count(it => Matches(it.Arguments));
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

    public bool Matches(LogArguments arguments)
    {
        return _messageArg.Predicate(arguments.message);
    }
}

/// <summary>
/// a
/// <code>
/// void IOrderApiService.Log(string message, int level)
/// </code>
/// </summary>
public delegate void Log_0_Delegate(string message, int level);

public class Log_0_Arguments
{
    public string message { get; }
    public int level { get; }

    public Log_0_Arguments(string message, int level)
    {
        this.message = message;
        this.level = level;
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// void IOrderApiService.Log(string message, int level)
/// </code>
/// </summary>
public class Log_0_MethodInvocationHistory
{
    public Log_0_Arguments Arguments { get; }
  public System.Exception? Exception { get; }
  public Log_0_MethodInvocationHistory(Log_0_Arguments arguments, System.Exception? exception)
{
  Arguments = arguments;
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// void IOrderApiService.Log(string message, int level)
/// </code>
/// </summary>
public class Log_0_MethodInvocationBehaviour(Arg<global::System.String> messageArg, Arg<global::System.Int32> levelArg)
{
    private Log_0_Delegate? _callBefore;
    private Log_0_Delegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;

    public Log_0_MethodInvocationBehaviour CallBefore(Log_0_Delegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public Log_0_MethodInvocationBehaviour CallAfter(Log_0_Delegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public Log_0_MethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public Log_0_MethodInvocationBehaviour Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches( string message,  int level)
    {
        return messageArg.Predicate(message) && levelArg.Predicate(level);
    }

    public void Execute( string message,  int level)
    {
        
        if(_callBefore is not null)
        {
            _callBefore( message,  level);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        if(_callAfter is not null)
        {
            _callAfter( message,  level);
        }
        
    }
}

/// <summary>
/// method class
/// <code>
/// void IOrderApiService.Log(string message, int level)
/// </code>
/// </summary>
public class Log_0_Method
{
    public List<Log_0_MethodInvocationBehaviour> Behaviours { get; } = new();
    public List<Log_0_MethodInvocationHistory> InvocationHistory { get; } = new();
    
    public void Invoke( string message,  int level)
    {

        try
        {
            Log_0_MethodInvocationBehaviour? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches( message,  level))
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            
            
            if(matchingBehaviour is not null)
            {
               matchingBehaviour.Execute( message,  level);
            }
            else
            {
                
                
            }
            
            InvocationHistory.Add(new Log_0_MethodInvocationHistory(new Log_0_Arguments(message, level),null));
            
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new Log_0_MethodInvocationHistory(new Log_0_Arguments(message, level),ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// void IOrderApiService.Log(string message, int level)
/// </code>
/// </summary>
public class Log_0_MethodInvocationVerifier
{             
    private readonly Log_0_Method _log_0_Method;
    private readonly Arg<global::System.String> _messageArg;
    private readonly Arg<global::System.Int32> _levelArg;

    public Log_0_MethodInvocationVerifier(
        Log_0_Method log_0_Method, Arg<global::System.String> messageArg, Arg<global::System.Int32> levelArg
    )
    {
        this._log_0_Method = log_0_Method;
        this._messageArg = messageArg;
        this._levelArg = levelArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _log_0_Method.InvocationHistory.Count(it => Matches(it.Arguments));
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

    public bool Matches(Log_0_Arguments arguments)
    {
        return _messageArg.Predicate(arguments.message) && _levelArg.Predicate(arguments.level);
    }
}

/// <summary>
/// a
/// <code>
/// void IOrderApiService.OldMethod()
/// </code>
/// </summary>
public delegate void OldMethodDelegate();

public class OldMethodArguments
{
    public OldMethodArguments()
    {
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// void IOrderApiService.OldMethod()
/// </code>
/// </summary>
public class OldMethodMethodInvocationHistory
{
    public System.Exception? Exception { get; }
  public OldMethodMethodInvocationHistory(System.Exception? exception)
{
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// void IOrderApiService.OldMethod()
/// </code>
/// </summary>
public class OldMethodMethodInvocationBehaviour()
{
    private OldMethodDelegate? _callBefore;
    private OldMethodDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;

    public OldMethodMethodInvocationBehaviour CallBefore(OldMethodDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public OldMethodMethodInvocationBehaviour CallAfter(OldMethodDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public OldMethodMethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public OldMethodMethodInvocationBehaviour Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches()
    {
        return true;
    }

    public void Execute()
    {
        
        if(_callBefore is not null)
        {
            _callBefore();
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        if(_callAfter is not null)
        {
            _callAfter();
        }
        
    }
}

/// <summary>
/// method class
/// <code>
/// void IOrderApiService.OldMethod()
/// </code>
/// </summary>
public class OldMethodMethod
{
    public List<OldMethodMethodInvocationBehaviour> Behaviours { get; } = new();
    public List<OldMethodMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public void Invoke()
    {

        try
        {
            OldMethodMethodInvocationBehaviour? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches())
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            
            
            if(matchingBehaviour is not null)
            {
               matchingBehaviour.Execute();
            }
            else
            {
                
                
            }
            
            InvocationHistory.Add(new OldMethodMethodInvocationHistory(null));
            
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new OldMethodMethodInvocationHistory(ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// void IOrderApiService.OldMethod()
/// </code>
/// </summary>
public class OldMethodMethodInvocationVerifier
{             
    private readonly OldMethodMethod _oldMethodMethod;

    public OldMethodMethodInvocationVerifier(
        OldMethodMethod oldMethodMethod
    )
    {
        this._oldMethodMethod = oldMethodMethod;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _oldMethodMethod.InvocationHistory.Count();
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

}

/// <summary>
/// a
/// <code>
/// void IOrderApiService.InOutRef(in int input, out int output, ref int temp)
/// </code>
/// </summary>
public delegate void InOutRefDelegate(in int input, out int output, ref int temp);

public class InOutRefArguments
{
    public int input { get; }
    public int output { get; }
    public int temp { get; }

    public InOutRefArguments(int input, int output, int temp)
    {
        this.input = input;
        this.output = output;
        this.temp = temp;
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// void IOrderApiService.InOutRef(in int input, out int output, ref int temp)
/// </code>
/// </summary>
public class InOutRefMethodInvocationHistory
{
    public InOutRefArguments Arguments { get; }
  public System.Exception? Exception { get; }
  public InOutRefMethodInvocationHistory(InOutRefArguments arguments, System.Exception? exception)
{
  Arguments = arguments;
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// void IOrderApiService.InOutRef(in int input, out int output, ref int temp)
/// </code>
/// </summary>
public class InOutRefMethodInvocationBehaviour(Arg<global::System.Int32> inputArg, OutArg<global::System.Int32> outputArg, Arg<global::System.Int32> tempArg)
{
    private InOutRefDelegate? _callBefore;
    private InOutRefDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;

    public InOutRefMethodInvocationBehaviour CallBefore(InOutRefDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public InOutRefMethodInvocationBehaviour CallAfter(InOutRefDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public InOutRefMethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public InOutRefMethodInvocationBehaviour Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches(in int input, ref int temp)
    {
        return inputArg.Predicate(input) && tempArg.Predicate(temp);
    }

    public void Execute(in int input, out int output, ref int temp)
    {
        
    output = default(int);
        if(_callBefore is not null)
        {
            _callBefore(in input, out output, ref temp);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        if(_callAfter is not null)
        {
            _callAfter(in input, out output, ref temp);
        }
        
    }
}

/// <summary>
/// method class
/// <code>
/// void IOrderApiService.InOutRef(in int input, out int output, ref int temp)
/// </code>
/// </summary>
public class InOutRefMethod
{
    public List<InOutRefMethodInvocationBehaviour> Behaviours { get; } = new();
    public List<InOutRefMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public void Invoke(in int input, out int output, ref int temp)
    {

        try
        {
            InOutRefMethodInvocationBehaviour? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches(in input, ref temp))
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            
            
            if(matchingBehaviour is not null)
            {
               matchingBehaviour.Execute(in input, out output, ref temp);
            }
            else
            {
                output = default(int);

                
            }
            
            InvocationHistory.Add(new InOutRefMethodInvocationHistory(new InOutRefArguments(input, output, temp),null));
            
        }
        catch (Exception ex)
        {
            output = default(int);

            InvocationHistory.Add(new InOutRefMethodInvocationHistory(new InOutRefArguments(input, output, temp),ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// void IOrderApiService.InOutRef(in int input, out int output, ref int temp)
/// </code>
/// </summary>
public class InOutRefMethodInvocationVerifier
{             
    private readonly InOutRefMethod _inOutRefMethod;
    private readonly Arg<global::System.Int32> _inputArg;
    private readonly OutArg<global::System.Int32> _outputArg;
    private readonly Arg<global::System.Int32> _tempArg;

    public InOutRefMethodInvocationVerifier(
        InOutRefMethod inOutRefMethod, Arg<global::System.Int32> inputArg, OutArg<global::System.Int32> outputArg, Arg<global::System.Int32> tempArg
    )
    {
        this._inOutRefMethod = inOutRefMethod;
        this._inputArg = inputArg;
        this._outputArg = outputArg;
        this._tempArg = tempArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _inOutRefMethod.InvocationHistory.Count(it => Matches(it.Arguments));
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

    public bool Matches(InOutRefArguments arguments)
    {
        return _inputArg.Predicate(arguments.input) && _tempArg.Predicate(arguments.temp);
    }
}

/// <summary>
/// a
/// <code>
/// void IOrderApiService.InOutRefParam(in int input, out int output, ref int temp, params int[] parameters)
/// </code>
/// </summary>
public delegate void InOutRefParamDelegate(in int input, out int output, ref int temp, int[] parameters);

public class InOutRefParamArguments
{
    public int input { get; }
    public int output { get; }
    public int temp { get; }
    public int[] parameters { get; }

    public InOutRefParamArguments(int input, int output, int temp, int[] parameters)
    {
        this.input = input;
        this.output = output;
        this.temp = temp;
        this.parameters = parameters;
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// void IOrderApiService.InOutRefParam(in int input, out int output, ref int temp, params int[] parameters)
/// </code>
/// </summary>
public class InOutRefParamMethodInvocationHistory
{
    public InOutRefParamArguments Arguments { get; }
  public System.Exception? Exception { get; }
  public InOutRefParamMethodInvocationHistory(InOutRefParamArguments arguments, System.Exception? exception)
{
  Arguments = arguments;
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// void IOrderApiService.InOutRefParam(in int input, out int output, ref int temp, params int[] parameters)
/// </code>
/// </summary>
public class InOutRefParamMethodInvocationBehaviour(Arg<global::System.Int32> inputArg, OutArg<global::System.Int32> outputArg, Arg<global::System.Int32> tempArg, Arg<global::System.Int32[]> parametersArg)
{
    private InOutRefParamDelegate? _callBefore;
    private InOutRefParamDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;

    public InOutRefParamMethodInvocationBehaviour CallBefore(InOutRefParamDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public InOutRefParamMethodInvocationBehaviour CallAfter(InOutRefParamDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public InOutRefParamMethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public InOutRefParamMethodInvocationBehaviour Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches(in int input, ref int temp, params  int[] parameters)
    {
        return inputArg.Predicate(input) && tempArg.Predicate(temp) && parametersArg.Predicate(parameters);
    }

    public void Execute(in int input, out int output, ref int temp, params  int[] parameters)
    {
        
    output = default(int);
        if(_callBefore is not null)
        {
            _callBefore(in input, out output, ref temp,  parameters);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        if(_callAfter is not null)
        {
            _callAfter(in input, out output, ref temp,  parameters);
        }
        
    }
}

/// <summary>
/// method class
/// <code>
/// void IOrderApiService.InOutRefParam(in int input, out int output, ref int temp, params int[] parameters)
/// </code>
/// </summary>
public class InOutRefParamMethod
{
    public List<InOutRefParamMethodInvocationBehaviour> Behaviours { get; } = new();
    public List<InOutRefParamMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public void Invoke(in int input, out int output, ref int temp, params  int[] parameters)
    {

        try
        {
            InOutRefParamMethodInvocationBehaviour? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches(in input, ref temp,  parameters))
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            
            
            if(matchingBehaviour is not null)
            {
               matchingBehaviour.Execute(in input, out output, ref temp,  parameters);
            }
            else
            {
                output = default(int);

                
            }
            
            InvocationHistory.Add(new InOutRefParamMethodInvocationHistory(new InOutRefParamArguments(input, output, temp, parameters),null));
            
        }
        catch (Exception ex)
        {
            output = default(int);

            InvocationHistory.Add(new InOutRefParamMethodInvocationHistory(new InOutRefParamArguments(input, output, temp, parameters),ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// void IOrderApiService.InOutRefParam(in int input, out int output, ref int temp, params int[] parameters)
/// </code>
/// </summary>
public class InOutRefParamMethodInvocationVerifier
{             
    private readonly InOutRefParamMethod _inOutRefParamMethod;
    private readonly Arg<global::System.Int32> _inputArg;
    private readonly OutArg<global::System.Int32> _outputArg;
    private readonly Arg<global::System.Int32> _tempArg;
    private readonly Arg<global::System.Int32[]> _parametersArg;

    public InOutRefParamMethodInvocationVerifier(
        InOutRefParamMethod inOutRefParamMethod, Arg<global::System.Int32> inputArg, OutArg<global::System.Int32> outputArg, Arg<global::System.Int32> tempArg, Arg<global::System.Int32[]> parametersArg
    )
    {
        this._inOutRefParamMethod = inOutRefParamMethod;
        this._inputArg = inputArg;
        this._outputArg = outputArg;
        this._tempArg = tempArg;
        this._parametersArg = parametersArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _inOutRefParamMethod.InvocationHistory.Count(it => Matches(it.Arguments));
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

    public bool Matches(InOutRefParamArguments arguments)
    {
        return _inputArg.Predicate(arguments.input) && _tempArg.Predicate(arguments.temp) && _parametersArg.Predicate(arguments.parameters);
    }
}

/// <summary>
/// a
/// <code>
/// int IOrderApiService.MethodWithMultipleArguments(int a, int b, int c)
/// </code>
/// </summary>
public delegate int MethodWithMultipleArgumentsDelegate(int a, int b, int c);

public class MethodWithMultipleArgumentsArguments
{
    public int a { get; }
    public int b { get; }
    public int c { get; }

    public MethodWithMultipleArgumentsArguments(int a, int b, int c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// int IOrderApiService.MethodWithMultipleArguments(int a, int b, int c)
/// </code>
/// </summary>
public class MethodWithMultipleArgumentsMethodInvocationHistory
{
    public MethodWithMultipleArgumentsArguments Arguments { get; }
  public global::System.Int32 Result { get; }
  public System.Exception? Exception { get; }
  public MethodWithMultipleArgumentsMethodInvocationHistory(MethodWithMultipleArgumentsArguments arguments, global::System.Int32 result, 
System.Exception? exception)
{
  Arguments = arguments;
  Result = result;
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// int IOrderApiService.MethodWithMultipleArguments(int a, int b, int c)
/// </code>
/// </summary>
public class MethodWithMultipleArgumentsMethodInvocationBehaviour(Arg<global::System.Int32> aArg, Arg<global::System.Int32> bArg, Arg<global::System.Int32> cArg)
{
    private MethodWithMultipleArgumentsDelegate? _callBefore;
    private MethodWithMultipleArgumentsDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;
    private MethodWithMultipleArgumentsDelegate? _resultGenerator;

    public MethodWithMultipleArgumentsMethodInvocationBehaviour CallBefore(MethodWithMultipleArgumentsDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public MethodWithMultipleArgumentsMethodInvocationBehaviour CallAfter(MethodWithMultipleArgumentsDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public MethodWithMultipleArgumentsMethodInvocationBehaviour Returns(MethodWithMultipleArgumentsDelegate resultGenerator)
    {
       _resultGenerator = resultGenerator;
       return this;
    }

    public MethodWithMultipleArgumentsMethodInvocationBehaviour Returns(global::System.Int32 result)
    {
       _resultGenerator = ( int a,  int b,  int c) =>
       {

            return result;
       };
       return this;
    }


    public MethodWithMultipleArgumentsMethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public MethodWithMultipleArgumentsMethodInvocationBehaviour Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches( int a,  int b,  int c)
    {
        return aArg.Predicate(a) && bArg.Predicate(b) && cArg.Predicate(c);
    }

    public global::System.Int32 Execute( int a,  int b,  int c)
    {
        
        if(_callBefore is not null)
        {
            _callBefore( a,  b,  c);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        var result = _resultGenerator?.Invoke( a,  b,  c) ?? default(global::System.Int32);
        
        if(_callAfter is not null)
        {
            _callAfter( a,  b,  c);
        }
        
        return result;
    }
}

/// <summary>
/// method class
/// <code>
/// int IOrderApiService.MethodWithMultipleArguments(int a, int b, int c)
/// </code>
/// </summary>
public class MethodWithMultipleArgumentsMethod
{
    public List<MethodWithMultipleArgumentsMethodInvocationBehaviour> Behaviours { get; } = new();
    public List<MethodWithMultipleArgumentsMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public global::System.Int32 Invoke( int a,  int b,  int c)
    {

        try
        {
            MethodWithMultipleArgumentsMethodInvocationBehaviour? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches( a,  b,  c))
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            global::System.Int32 result;
            
            if(matchingBehaviour is not null)
            {
               result = matchingBehaviour.Execute( a,  b,  c);
            }
            else
            {
                
                result = default(global::System.Int32);
            }
            
            InvocationHistory.Add(new MethodWithMultipleArgumentsMethodInvocationHistory(new MethodWithMultipleArgumentsArguments(a, b, c),result, null));
            return result;
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new MethodWithMultipleArgumentsMethodInvocationHistory(new MethodWithMultipleArgumentsArguments(a, b, c),default(global::System.Int32), ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// int IOrderApiService.MethodWithMultipleArguments(int a, int b, int c)
/// </code>
/// </summary>
public class MethodWithMultipleArgumentsMethodInvocationVerifier
{             
    private readonly MethodWithMultipleArgumentsMethod _methodWithMultipleArgumentsMethod;
    private readonly Arg<global::System.Int32> _aArg;
    private readonly Arg<global::System.Int32> _bArg;
    private readonly Arg<global::System.Int32> _cArg;

    public MethodWithMultipleArgumentsMethodInvocationVerifier(
        MethodWithMultipleArgumentsMethod methodWithMultipleArgumentsMethod, Arg<global::System.Int32> aArg, Arg<global::System.Int32> bArg, Arg<global::System.Int32> cArg
    )
    {
        this._methodWithMultipleArgumentsMethod = methodWithMultipleArgumentsMethod;
        this._aArg = aArg;
        this._bArg = bArg;
        this._cArg = cArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _methodWithMultipleArgumentsMethod.InvocationHistory.Count(it => Matches(it.Arguments));
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

    public bool Matches(MethodWithMultipleArgumentsArguments arguments)
    {
        return _aArg.Predicate(arguments.a) && _bArg.Predicate(arguments.b) && _cArg.Predicate(arguments.c);
    }
}

/// <summary>
/// a
/// <code>
/// int IOrderApiService.MethodWithOutParameter(int a, out int b)
/// </code>
/// </summary>
public delegate int MethodWithOutParameterDelegate(int a, out int b);

public class MethodWithOutParameterArguments
{
    public int a { get; }
    public int b { get; }

    public MethodWithOutParameterArguments(int a, int b)
    {
        this.a = a;
        this.b = b;
    }
}

/// <summary>
/// invocation class for the method
/// <code>
/// int IOrderApiService.MethodWithOutParameter(int a, out int b)
/// </code>
/// </summary>
public class MethodWithOutParameterMethodInvocationHistory
{
    public MethodWithOutParameterArguments Arguments { get; }
  public global::System.Int32 Result { get; }
  public System.Exception? Exception { get; }
  public MethodWithOutParameterMethodInvocationHistory(MethodWithOutParameterArguments arguments, global::System.Int32 result, 
System.Exception? exception)
{
  Arguments = arguments;
  Result = result;
  Exception = exception;
    }
}
/// <summary>
/// invocation behaviour class
/// <code>
/// int IOrderApiService.MethodWithOutParameter(int a, out int b)
/// </code>
/// </summary>
public class MethodWithOutParameterMethodInvocationBehaviour(Arg<global::System.Int32> aArg, OutArg<global::System.Int32> bArg)
{
    private MethodWithOutParameterDelegate? _callBefore;
    private MethodWithOutParameterDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;
    private MethodWithOutParameterDelegate? _resultGenerator;

    public MethodWithOutParameterMethodInvocationBehaviour CallBefore(MethodWithOutParameterDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public MethodWithOutParameterMethodInvocationBehaviour CallAfter(MethodWithOutParameterDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public MethodWithOutParameterMethodInvocationBehaviour Returns(MethodWithOutParameterDelegate resultGenerator)
    {
       _resultGenerator = resultGenerator;
       return this;
    }

    public MethodWithOutParameterMethodInvocationBehaviour Returns(global::System.Int32 result)
    {
       _resultGenerator = ( int a, out int b) =>
       {

        b = default(int);
            return result;
       };
       return this;
    }


    public MethodWithOutParameterMethodInvocationBehaviour Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public MethodWithOutParameterMethodInvocationBehaviour Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches( int a)
    {
        return aArg.Predicate(a);
    }

    public global::System.Int32 Execute( int a, out int b)
    {
        
    b = default(int);
        if(_callBefore is not null)
        {
            _callBefore( a, out b);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        var result = _resultGenerator?.Invoke( a, out b) ?? default(global::System.Int32);
        
        if(_callAfter is not null)
        {
            _callAfter( a, out b);
        }
        
        return result;
    }
}

/// <summary>
/// method class
/// <code>
/// int IOrderApiService.MethodWithOutParameter(int a, out int b)
/// </code>
/// </summary>
public class MethodWithOutParameterMethod
{
    public List<MethodWithOutParameterMethodInvocationBehaviour> Behaviours { get; } = new();
    public List<MethodWithOutParameterMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public global::System.Int32 Invoke( int a, out int b)
    {

        try
        {
            MethodWithOutParameterMethodInvocationBehaviour? matchingBehaviour = null;
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
               result = matchingBehaviour.Execute( a, out b);
            }
            else
            {
                b = default(int);

                result = default(global::System.Int32);
            }
            
            InvocationHistory.Add(new MethodWithOutParameterMethodInvocationHistory(new MethodWithOutParameterArguments(a, b),result, null));
            return result;
        }
        catch (Exception ex)
        {
            b = default(int);

            InvocationHistory.Add(new MethodWithOutParameterMethodInvocationHistory(new MethodWithOutParameterArguments(a, b),default(global::System.Int32), ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// int IOrderApiService.MethodWithOutParameter(int a, out int b)
/// </code>
/// </summary>
public class MethodWithOutParameterMethodInvocationVerifier
{             
    private readonly MethodWithOutParameterMethod _methodWithOutParameterMethod;
    private readonly Arg<global::System.Int32> _aArg;
    private readonly OutArg<global::System.Int32> _bArg;

    public MethodWithOutParameterMethodInvocationVerifier(
        MethodWithOutParameterMethod methodWithOutParameterMethod, Arg<global::System.Int32> aArg, OutArg<global::System.Int32> bArg
    )
    {
        this._methodWithOutParameterMethod = methodWithOutParameterMethod;
        this._aArg = aArg;
        this._bArg = bArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _methodWithOutParameterMethod.InvocationHistory.Count(it => Matches(it.Arguments));
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

    public bool Matches(MethodWithOutParameterArguments arguments)
    {
        return _aArg.Predicate(arguments.a);
    }
}

public class IOrderApiServiceImposterVerifier
{
    private readonly DoWorkMethod _doWorkMethod;
    private readonly GetNumberMethod _getNumberMethod;
    private readonly SingleInParameterMethod _singleInParameterMethod;
    private readonly InAndNormalParameterMethod _inAndNormalParameterMethod;
    private readonly TwoInParametersMethod _twoInParametersMethod;
    private readonly ModifyMethod _modifyMethod;
    private readonly InitializeMethod _initializeMethod;
    private readonly AddNumbersMethod _addNumbersMethod;
    private readonly GreetMethod _greetMethod;
    private readonly FindUserMethod _findUserMethod;
    private readonly CalculateMethod _calculateMethod;
    private readonly SaveAsyncMethod _saveAsyncMethod;
    private readonly TrySaveAsyncMethod _trySaveAsyncMethod;
    private readonly LogMethod _logMethod;
    private readonly Log_0_Method _log_0_Method;
    private readonly OldMethodMethod _oldMethodMethod;
    private readonly InOutRefMethod _inOutRefMethod;
    private readonly InOutRefParamMethod _inOutRefParamMethod;
    private readonly MethodWithMultipleArgumentsMethod _methodWithMultipleArgumentsMethod;
    private readonly MethodWithOutParameterMethod _methodWithOutParameterMethod;

    public IOrderApiServiceImposterVerifier(
        DoWorkMethod doWorkMethod, GetNumberMethod getNumberMethod, SingleInParameterMethod singleInParameterMethod, InAndNormalParameterMethod inAndNormalParameterMethod, TwoInParametersMethod twoInParametersMethod, ModifyMethod modifyMethod, InitializeMethod initializeMethod, AddNumbersMethod addNumbersMethod, GreetMethod greetMethod, FindUserMethod findUserMethod, CalculateMethod calculateMethod, SaveAsyncMethod saveAsyncMethod, TrySaveAsyncMethod trySaveAsyncMethod, LogMethod logMethod, Log_0_Method log_0_Method, OldMethodMethod oldMethodMethod, InOutRefMethod inOutRefMethod, InOutRefParamMethod inOutRefParamMethod, MethodWithMultipleArgumentsMethod methodWithMultipleArgumentsMethod, MethodWithOutParameterMethod methodWithOutParameterMethod
    )
    {
        this._doWorkMethod = doWorkMethod;
        this._getNumberMethod = getNumberMethod;
        this._singleInParameterMethod = singleInParameterMethod;
        this._inAndNormalParameterMethod = inAndNormalParameterMethod;
        this._twoInParametersMethod = twoInParametersMethod;
        this._modifyMethod = modifyMethod;
        this._initializeMethod = initializeMethod;
        this._addNumbersMethod = addNumbersMethod;
        this._greetMethod = greetMethod;
        this._findUserMethod = findUserMethod;
        this._calculateMethod = calculateMethod;
        this._saveAsyncMethod = saveAsyncMethod;
        this._trySaveAsyncMethod = trySaveAsyncMethod;
        this._logMethod = logMethod;
        this._log_0_Method = log_0_Method;
        this._oldMethodMethod = oldMethodMethod;
        this._inOutRefMethod = inOutRefMethod;
        this._inOutRefParamMethod = inOutRefParamMethod;
        this._methodWithMultipleArgumentsMethod = methodWithMultipleArgumentsMethod;
        this._methodWithOutParameterMethod = methodWithOutParameterMethod;
    }


    public DoWorkMethodInvocationVerifier DoWork()
          => new(_doWorkMethod);
    public GetNumberMethodInvocationVerifier GetNumber()
          => new(_getNumberMethod);
    public SingleInParameterMethodInvocationVerifier SingleInParameter(Arg<global::System.Int32> valueArg)
          => new(_singleInParameterMethod, valueArg);
    public InAndNormalParameterMethodInvocationVerifier InAndNormalParameter(Arg<global::System.Int32> valueArg, Arg<global::System.String> nameArg)
          => new(_inAndNormalParameterMethod, valueArg, nameArg);
    public TwoInParametersMethodInvocationVerifier TwoInParameters(Arg<global::System.Int32> valueArg, Arg<global::System.Double> ageArg)
          => new(_twoInParametersMethod, valueArg, ageArg);
    public ModifyMethodInvocationVerifier Modify(Arg<global::System.Int32> valueArg)
          => new(_modifyMethod, valueArg);
    public InitializeMethodInvocationVerifier Initialize(OutArg<global::System.Int32> valueArg)
          => new(_initializeMethod, valueArg);
    public AddNumbersMethodInvocationVerifier AddNumbers(Arg<global::System.Int32[]> numbersArg)
          => new(_addNumbersMethod, numbersArg);
    public GreetMethodInvocationVerifier Greet(Arg<global::System.String> nameArg)
          => new(_greetMethod, nameArg);
    public FindUserMethodInvocationVerifier FindUser(Arg<global::System.String?> usernameArg)
          => new(_findUserMethod, usernameArg);
    public CalculateMethodInvocationVerifier Calculate(Arg<global::System.Int32[]> numbersArg)
          => new(_calculateMethod, numbersArg);
    public SaveAsyncMethodInvocationVerifier SaveAsync()
          => new(_saveAsyncMethod);
    public TrySaveAsyncMethodInvocationVerifier TrySaveAsync()
          => new(_trySaveAsyncMethod);
    public LogMethodInvocationVerifier Log(Arg<global::System.String> messageArg)
          => new(_logMethod, messageArg);
    public Log_0_MethodInvocationVerifier Log(Arg<global::System.String> messageArg, Arg<global::System.Int32> levelArg)
          => new(_log_0_Method, messageArg, levelArg);
    public OldMethodMethodInvocationVerifier OldMethod()
          => new(_oldMethodMethod);
    public InOutRefMethodInvocationVerifier InOutRef(Arg<global::System.Int32> inputArg, OutArg<global::System.Int32> outputArg, Arg<global::System.Int32> tempArg)
          => new(_inOutRefMethod, inputArg, outputArg, tempArg);
    public InOutRefParamMethodInvocationVerifier InOutRefParam(Arg<global::System.Int32> inputArg, OutArg<global::System.Int32> outputArg, Arg<global::System.Int32> tempArg, Arg<global::System.Int32[]> parametersArg)
          => new(_inOutRefParamMethod, inputArg, outputArg, tempArg, parametersArg);
    public MethodWithMultipleArgumentsMethodInvocationVerifier MethodWithMultipleArguments(Arg<global::System.Int32> aArg, Arg<global::System.Int32> bArg, Arg<global::System.Int32> cArg)
          => new(_methodWithMultipleArgumentsMethod, aArg, bArg, cArg);
    public MethodWithOutParameterMethodInvocationVerifier MethodWithOutParameter(Arg<global::System.Int32> aArg, OutArg<global::System.Int32> bArg)
          => new(_methodWithOutParameterMethod, aArg, bArg);
}

public class IOrderApiServiceImposter : IHaveImposterVerifier<IOrderApiServiceImposterVerifier>, IHaveImposterInstance<global::Imposter.Playground.IOrderApiService>
{
    private readonly DoWorkMethod _doWorkMethod;
    private readonly GetNumberMethod _getNumberMethod;
    private readonly SingleInParameterMethod _singleInParameterMethod;
    private readonly InAndNormalParameterMethod _inAndNormalParameterMethod;
    private readonly TwoInParametersMethod _twoInParametersMethod;
    private readonly ModifyMethod _modifyMethod;
    private readonly InitializeMethod _initializeMethod;
    private readonly AddNumbersMethod _addNumbersMethod;
    private readonly GreetMethod _greetMethod;
    private readonly FindUserMethod _findUserMethod;
    private readonly CalculateMethod _calculateMethod;
    private readonly SaveAsyncMethod _saveAsyncMethod;
    private readonly TrySaveAsyncMethod _trySaveAsyncMethod;
    private readonly LogMethod _logMethod;
    private readonly Log_0_Method _log_0_Method;
    private readonly OldMethodMethod _oldMethodMethod;
    private readonly InOutRefMethod _inOutRefMethod;
    private readonly InOutRefParamMethod _inOutRefParamMethod;
    private readonly MethodWithMultipleArgumentsMethod _methodWithMultipleArgumentsMethod;
    private readonly MethodWithOutParameterMethod _methodWithOutParameterMethod;
    private readonly IOrderApiServiceImposterVerifier _verifier;
    private readonly global::Imposter.Playground.IOrderApiService _imposterInstance;

    public IOrderApiServiceImposter()
    {
       _doWorkMethod = new();
       _getNumberMethod = new();
       _singleInParameterMethod = new();
       _inAndNormalParameterMethod = new();
       _twoInParametersMethod = new();
       _modifyMethod = new();
       _initializeMethod = new();
       _addNumbersMethod = new();
       _greetMethod = new();
       _findUserMethod = new();
       _calculateMethod = new();
       _saveAsyncMethod = new();
       _trySaveAsyncMethod = new();
       _logMethod = new();
       _log_0_Method = new();
       _oldMethodMethod = new();
       _inOutRefMethod = new();
       _inOutRefParamMethod = new();
       _methodWithMultipleArgumentsMethod = new();
       _methodWithOutParameterMethod = new();
       _verifier = new(_doWorkMethod,_getNumberMethod,_singleInParameterMethod,_inAndNormalParameterMethod,_twoInParametersMethod,_modifyMethod,_initializeMethod,_addNumbersMethod,_greetMethod,_findUserMethod,_calculateMethod,_saveAsyncMethod,_trySaveAsyncMethod,_logMethod,_log_0_Method,_oldMethodMethod,_inOutRefMethod,_inOutRefParamMethod,_methodWithMultipleArgumentsMethod,_methodWithOutParameterMethod);
       _imposterInstance = new IOrderApiServiceImposterInstance(this);
    }

    global::Imposter.Playground.IOrderApiService IHaveImposterInstance<global::Imposter.Playground.IOrderApiService>.Instance() => _imposterInstance;

    IOrderApiServiceImposterVerifier IHaveImposterVerifier<IOrderApiServiceImposterVerifier>.Verify() => _verifier;

    public DoWorkMethodInvocationBehaviour DoWork()
    {
        var invocationBehaviour = new DoWorkMethodInvocationBehaviour();
        _doWorkMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public GetNumberMethodInvocationBehaviour GetNumber()
    {
        var invocationBehaviour = new GetNumberMethodInvocationBehaviour();
        _getNumberMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public SingleInParameterMethodInvocationBehaviour SingleInParameter(Arg<global::System.Int32> valueArg)
    {
        var invocationBehaviour = new SingleInParameterMethodInvocationBehaviour(valueArg);
        _singleInParameterMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public InAndNormalParameterMethodInvocationBehaviour InAndNormalParameter(Arg<global::System.Int32> valueArg, Arg<global::System.String> nameArg)
    {
        var invocationBehaviour = new InAndNormalParameterMethodInvocationBehaviour(valueArg, nameArg);
        _inAndNormalParameterMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public TwoInParametersMethodInvocationBehaviour TwoInParameters(Arg<global::System.Int32> valueArg, Arg<global::System.Double> ageArg)
    {
        var invocationBehaviour = new TwoInParametersMethodInvocationBehaviour(valueArg, ageArg);
        _twoInParametersMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public ModifyMethodInvocationBehaviour Modify(Arg<global::System.Int32> valueArg)
    {
        var invocationBehaviour = new ModifyMethodInvocationBehaviour(valueArg);
        _modifyMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public InitializeMethodInvocationBehaviour Initialize(OutArg<global::System.Int32> valueArg)
    {
        var invocationBehaviour = new InitializeMethodInvocationBehaviour(valueArg);
        _initializeMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public AddNumbersMethodInvocationBehaviour AddNumbers(Arg<global::System.Int32[]> numbersArg)
    {
        var invocationBehaviour = new AddNumbersMethodInvocationBehaviour(numbersArg);
        _addNumbersMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public GreetMethodInvocationBehaviour Greet(Arg<global::System.String> nameArg)
    {
        var invocationBehaviour = new GreetMethodInvocationBehaviour(nameArg);
        _greetMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public FindUserMethodInvocationBehaviour FindUser(Arg<global::System.String?> usernameArg)
    {
        var invocationBehaviour = new FindUserMethodInvocationBehaviour(usernameArg);
        _findUserMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public CalculateMethodInvocationBehaviour Calculate(Arg<global::System.Int32[]> numbersArg)
    {
        var invocationBehaviour = new CalculateMethodInvocationBehaviour(numbersArg);
        _calculateMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public SaveAsyncMethodInvocationBehaviour SaveAsync()
    {
        var invocationBehaviour = new SaveAsyncMethodInvocationBehaviour();
        _saveAsyncMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public TrySaveAsyncMethodInvocationBehaviour TrySaveAsync()
    {
        var invocationBehaviour = new TrySaveAsyncMethodInvocationBehaviour();
        _trySaveAsyncMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public LogMethodInvocationBehaviour Log(Arg<global::System.String> messageArg)
    {
        var invocationBehaviour = new LogMethodInvocationBehaviour(messageArg);
        _logMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public Log_0_MethodInvocationBehaviour Log(Arg<global::System.String> messageArg, Arg<global::System.Int32> levelArg)
    {
        var invocationBehaviour = new Log_0_MethodInvocationBehaviour(messageArg, levelArg);
        _log_0_Method.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public OldMethodMethodInvocationBehaviour OldMethod()
    {
        var invocationBehaviour = new OldMethodMethodInvocationBehaviour();
        _oldMethodMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public InOutRefMethodInvocationBehaviour InOutRef(Arg<global::System.Int32> inputArg, OutArg<global::System.Int32> outputArg, Arg<global::System.Int32> tempArg)
    {
        var invocationBehaviour = new InOutRefMethodInvocationBehaviour(inputArg, outputArg, tempArg);
        _inOutRefMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public InOutRefParamMethodInvocationBehaviour InOutRefParam(Arg<global::System.Int32> inputArg, OutArg<global::System.Int32> outputArg, Arg<global::System.Int32> tempArg, Arg<global::System.Int32[]> parametersArg)
    {
        var invocationBehaviour = new InOutRefParamMethodInvocationBehaviour(inputArg, outputArg, tempArg, parametersArg);
        _inOutRefParamMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public MethodWithMultipleArgumentsMethodInvocationBehaviour MethodWithMultipleArguments(Arg<global::System.Int32> aArg, Arg<global::System.Int32> bArg, Arg<global::System.Int32> cArg)
    {
        var invocationBehaviour = new MethodWithMultipleArgumentsMethodInvocationBehaviour(aArg, bArg, cArg);
        _methodWithMultipleArgumentsMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
    public MethodWithOutParameterMethodInvocationBehaviour MethodWithOutParameter(Arg<global::System.Int32> aArg, OutArg<global::System.Int32> bArg)
    {
        var invocationBehaviour = new MethodWithOutParameterMethodInvocationBehaviour(aArg, bArg);
        _methodWithOutParameterMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
public class IOrderApiServiceImposterInstance : global::Imposter.Playground.IOrderApiService
{
  private readonly IOrderApiServiceImposter _imposter;
  public IOrderApiServiceImposterInstance(IOrderApiServiceImposter imposter)
  {
      _imposter = imposter;
  }
  
      public void DoWork()
      {
          _imposter._doWorkMethod.Invoke();
      }
      public global::System.Int32 GetNumber()
      {
          return _imposter._getNumberMethod.Invoke();
      }
      public void SingleInParameter(in int value)
      {
          _imposter._singleInParameterMethod.Invoke(in value);
      }
      public void InAndNormalParameter(in int value,  string name)
      {
          _imposter._inAndNormalParameterMethod.Invoke(in value,  name);
      }
      public void TwoInParameters(in int value, in double age)
      {
          _imposter._twoInParametersMethod.Invoke(in value, in age);
      }
      public void Modify(ref int value)
      {
          _imposter._modifyMethod.Invoke(ref value);
      }
      public void Initialize(out int value)
      {
          _imposter._initializeMethod.Invoke(out value);
      }
      public void AddNumbers(params  int[] numbers)
      {
          _imposter._addNumbersMethod.Invoke( numbers);
      }
      public void Greet( string name)
      {
          _imposter._greetMethod.Invoke( name);
      }
      public global::System.String? FindUser( string username)
      {
          return _imposter._findUserMethod.Invoke( username);
      }
      public (global::System.Int32 sum, global::System.Int32 count) Calculate( int[] numbers)
      {
          return _imposter._calculateMethod.Invoke( numbers);
      }
      public global::System.Threading.Tasks.Task SaveAsync()
      {
          return _imposter._saveAsyncMethod.Invoke();
      }
      public global::System.Threading.Tasks.ValueTask<global::System.Boolean> TrySaveAsync()
      {
          return _imposter._trySaveAsyncMethod.Invoke();
      }
      public void Log( string message)
      {
          _imposter._logMethod.Invoke( message);
      }
      public void Log( string message,  int level)
      {
          _imposter._log_0_Method.Invoke( message,  level);
      }
      public void OldMethod()
      {
          _imposter._oldMethodMethod.Invoke();
      }
      public void InOutRef(in int input, out int output, ref int temp)
      {
          _imposter._inOutRefMethod.Invoke(in input, out output, ref temp);
      }
      public void InOutRefParam(in int input, out int output, ref int temp, params  int[] parameters)
      {
          _imposter._inOutRefParamMethod.Invoke(in input, out output, ref temp,  parameters);
      }
      public global::System.Int32 MethodWithMultipleArguments( int a,  int b,  int c)
      {
          return _imposter._methodWithMultipleArgumentsMethod.Invoke( a,  b,  c);
      }
      public global::System.Int32 MethodWithOutParameter( int a, out int b)
      {
          return _imposter._methodWithOutParameterMethod.Invoke( a, out b);
      }
    }
}
#nullable restore
