using System.Collections.Concurrent;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;

namespace Imposter.Ideation;

public class Whiteboard
{
    private readonly ConcurrentStack<int> _invocationHistory = new ConcurrentStack<IGeneric_OutParamMethodInvocationHistory>();
    
    public void A()
    {
        _invocationHistory.Count

    }
}