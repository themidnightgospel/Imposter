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
public class TestInterfaceImposterVerifier
{

    public TestInterfaceImposterVerifier(
        
    )
    {
    }


}

public class TestInterfaceImposter : IHaveImposterVerifier<TestInterfaceImposterVerifier>, IHaveImposterInstance<global::Imposter.Playground.TestInterface>
{
    private readonly TestInterfaceImposterVerifier _verifier;
    private readonly global::Imposter.Playground.TestInterface _imposterInstance;

    public TestInterfaceImposter()
    {
       _verifier = new();
       _imposterInstance = new TestInterfaceImposterInstance(this);
    }

    global::Imposter.Playground.TestInterface IHaveImposterInstance<global::Imposter.Playground.TestInterface>.Instance() => _imposterInstance;

    TestInterfaceImposterVerifier IHaveImposterVerifier<TestInterfaceImposterVerifier>.Verify() => _verifier;

public class TestInterfaceImposterInstance : global::Imposter.Playground.TestInterface
{
  private readonly TestInterfaceImposter _imposter;
  public TestInterfaceImposterInstance(TestInterfaceImposter imposter)
  {
      _imposter = imposter;
  }
  
    }
}
#nullable restore
