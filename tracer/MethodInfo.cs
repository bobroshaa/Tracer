using System.Collections.Concurrent;
using System.Diagnostics;

namespace tracer;

public class MethodInfo
{
    public string MethodName;
    public string ClassName;
    public int PerformanceTime;
    public List<MethodInfo> ListOfMethods;
    private Stopwatch _stopWatch;

    public MethodInfo()
    {
        _stopWatch = new Stopwatch();
        _stopWatch.Start();
    }

    public void EndMethod()
    {
        _stopWatch.Stop();
        PerformanceTime = _stopWatch.Elapsed.Milliseconds;
    }
    
}