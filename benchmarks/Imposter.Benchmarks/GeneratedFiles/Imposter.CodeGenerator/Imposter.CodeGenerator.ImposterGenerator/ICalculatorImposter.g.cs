#pragma warning disable nullable
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using Imposter;

namespace Imposters;
// int Benchy.ICalculator.Add(int left, int right)
public delegate int AddDelegate(int left, int right);

public class AddArguments
{
    public int left { get; }
    public int right { get; }

    public AddArguments(int left, int right)
    {
        this.left = left;
        this.right = right;
    }
}

public class AddMethodInvocationHistory
{
    public AddArguments Arguments { get; }
    public int Result { get; }
    public System.Exception Exception { get; }

    public AddMethodInvocationHistory(AddArguments arguments, int result, System.Exception exception)
    {
        Arguments = arguments;
        Result = result;
        Exception = exception;
    }
}

/// <summary>
/// invocation behaviour class
/// <code>
/// int Benchy.ICalculator.Add(int left, int right)
/// </code>
/// </summary>
public class AddInvocationSetup(Arg<global::System.Int32> leftArg, Arg<global::System.Int32> rightArg)
{
    private AddDelegate? _callBefore;
    private AddDelegate? _callAfter;
    private Func<Exception>? _exceptionGenerator;
    private AddDelegate? _resultGenerator;

    public AddInvocationSetup CallBefore(AddDelegate callBefore)
    {
       _callBefore = callBefore;
       return this;
    }
    
    public AddInvocationSetup CallAfter(AddDelegate callAfter)
    {
       _callAfter = callAfter;
       return this;
    }

    public AddInvocationSetup Returns(AddDelegate resultGenerator)
    {
       _resultGenerator = resultGenerator;
       return this;
    }

    public AddInvocationSetup Returns(global::System.Int32 result)
    {
       _resultGenerator = ( int left,  int right) =>
       {

            return result;
       };
       return this;
    }


    public AddInvocationSetup Throws<TException>() where TException : Exception, new()
    {
       _exceptionGenerator = () => new TException();
       return this;
    }

    public AddInvocationSetup Throws(Exception exception)
    {
       _exceptionGenerator = () => exception;
       return this;
    }

    public bool Matches( int left,  int right)
    {
        return leftArg.Matches(left) && rightArg.Matches(right);
    }

    public global::System.Int32 Execute( int left,  int right)
    {
        
        if(_callBefore is not null)
        {
            _callBefore( left,  right);
        }
    
        if (_exceptionGenerator != null)
        {
            throw _exceptionGenerator.Invoke();
        }
        
        var result = _resultGenerator?.Invoke( left,  right) ?? default(global::System.Int32);
        
        if(_callAfter is not null)
        {
            _callAfter( left,  right);
        }
        
        return result;
    }
}

/// <summary>
/// method class
/// <code>
/// int Benchy.ICalculator.Add(int left, int right)
/// </code>
/// </summary>
public class AddMethod
{
    public List<AddInvocationSetup> Behaviours { get; } = new();
    public List<AddMethodInvocationHistory> InvocationHistory { get; } = new();
    
    public global::System.Int32 Invoke( int left,  int right)
    {

        try
        {
            AddInvocationSetup? matchingBehaviour = null;
            foreach(var behaviour in Enumerable.Reverse(Behaviours))
            {
                if(behaviour.Matches( left,  right))
                {
                   matchingBehaviour = behaviour;
                   break;
                }
            }
            global::System.Int32 result;
            
            if(matchingBehaviour is not null)
            {
               result = matchingBehaviour.Execute( left,  right);
            }
            else
            {
                
                result = default(global::System.Int32);
            }
            
            InvocationHistory.Add(new AddMethodInvocationHistory(new AddArguments(left, right),result, null));
            return result;
        }
        catch (Exception ex)
        {
            
            InvocationHistory.Add(new AddMethodInvocationHistory(new AddArguments(left, right),default(global::System.Int32), ex));
            throw;
        }
    }
}

/// <summary>
/// invocation verifier class
/// <code>
/// int Benchy.ICalculator.Add(int left, int right)
/// </code>
/// </summary>
public class AddMethodInvocationVerifier
{             
    private readonly AddMethod _addMethod;
    private readonly Arg<global::System.Int32> _leftArg;
    private readonly Arg<global::System.Int32> _rightArg;

    public AddMethodInvocationVerifier(
        AddMethod addMethod, Arg<global::System.Int32> leftArg, Arg<global::System.Int32> rightArg
    )
    {
        this._addMethod = addMethod;
        this._leftArg = leftArg;
        this._rightArg = rightArg;
    }

    
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = _addMethod.InvocationHistory.Count(it => Matches(it.Arguments));
    
        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }

    public bool Matches(AddArguments arguments)
    {
        return _leftArg.Matches(arguments.left) && _rightArg.Matches(arguments.right);
    }
}

public class ICalculatorImposterVerifier
{
    private readonly AddMethod _addMethod;

    public ICalculatorImposterVerifier(
        AddMethod addMethod
    )
    {
        this._addMethod = addMethod;
    }


    public AddMethodInvocationVerifier Add(Arg<global::System.Int32> leftArg, Arg<global::System.Int32> rightArg)
          => new(_addMethod, leftArg, rightArg);
}

public class ICalculatorImposter : IHaveImposterVerifier<ICalculatorImposterVerifier>, IHaveImposterInstance<global::Benchy.ICalculator>
{
    private readonly AddMethod _addMethod;
    private readonly ICalculatorImposterVerifier _verifier;
    private readonly global::Benchy.ICalculator _imposterInstance;

    public ICalculatorImposter()
    {
       _addMethod = new();
       _verifier = new(_addMethod);
       _imposterInstance = new ICalculatorImposterInstance(this);
    }

    global::Benchy.ICalculator IHaveImposterInstance<global::Benchy.ICalculator>.Instance() => _imposterInstance;

    ICalculatorImposterVerifier IHaveImposterVerifier<ICalculatorImposterVerifier>.Verify() => _verifier;

    public AddInvocationSetup Add(Arg<global::System.Int32> leftArg, Arg<global::System.Int32> rightArg)
    {
        var invocationBehaviour = new AddInvocationSetup(leftArg, rightArg);
        _addMethod.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
    
public class ICalculatorImposterInstance : global::Benchy.ICalculator
{
  private readonly ICalculatorImposter _imposter;
  public ICalculatorImposterInstance(ICalculatorImposter imposter)
  {
      _imposter = imposter;
  }
  
      public global::System.Int32 Add( int left,  int right)
      {
          return _imposter._addMethod.Invoke( left,  right);
      }
    }
}
#nullable restore
