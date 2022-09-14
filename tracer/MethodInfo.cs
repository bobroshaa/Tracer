using System.Diagnostics;
using Newtonsoft.Json;

namespace tracer;

public class MethodInfo
{
    public string MethodName { get; set; }
    public string ClassName { get; set; }
    public int PerformanceTime{ get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public List<MethodInfo> ListOfMethods{ get; set; }
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