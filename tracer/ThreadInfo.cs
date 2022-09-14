namespace tracer;

public class ThreadInfo
{
    public int ThreadIndex { get; set; }
    public int PerformanceTime { get; set; }
    public List<MethodInfo> methods { get; set; }

    public ThreadInfo(int threadIndex)
    {
        ThreadIndex = threadIndex;
        methods = new List<MethodInfo>();
    }
    public void EndThread()
    {
        foreach (var method in methods)
        {
            PerformanceTime += method.PerformanceTime;
        }
    }
}