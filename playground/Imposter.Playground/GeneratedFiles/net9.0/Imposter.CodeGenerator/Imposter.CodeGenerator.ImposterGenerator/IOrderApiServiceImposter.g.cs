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
public class IOrderApiServiceImposterVerifier
{

    public IOrderApiServiceImposterVerifier(
        
    )
    {
    }


}

public class IOrderApiServiceImposter : IHaveImposterVerifier<IOrderApiServiceImposterVerifier>, IHaveImposterInstance<global::Imposter.Playground.IOrderApiService>
{
    private readonly IOrderApiServiceImposterVerifier _verifier;
    private readonly global::Imposter.Playground.IOrderApiService _imposterInstance;

    public IOrderApiServiceImposter()
    {
       _verifier = new();
       _imposterInstance = new IOrderApiServiceImposterInstance(this);
    }

    global::Imposter.Playground.IOrderApiService IHaveImposterInstance<global::Imposter.Playground.IOrderApiService>.Instance() => _imposterInstance;

    IOrderApiServiceImposterVerifier IHaveImposterVerifier<IOrderApiServiceImposterVerifier>.Verify() => _verifier;

public class IOrderApiServiceImposterInstance : global::Imposter.Playground.IOrderApiService
{
  private readonly IOrderApiServiceImposter _imposter;
  public IOrderApiServiceImposterInstance(IOrderApiServiceImposter imposter)
  {
      _imposter = imposter;
  }
  
    }
}
#nullable restore
