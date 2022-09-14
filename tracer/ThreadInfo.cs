namespace tracer;

public class ThreadInfo
{
    public int ThreadIndex;
    public int PerformanceTime;
    public List<MethodInfo> methods;

    public void EndThread()
    {
        foreach (var method in methods)
        {
            PerformanceTime += method.PerformanceTime;
        }
    }
}