using System.Collections.Concurrent;

namespace tracer
{
    public class TraceResult
    {
        public Dictionary<string, ThreadInfo> Info;
        public TraceResult(ConcurrentDictionary<int, List<MethodInfo>> dictionary)
        {
            dictionary.TryGetValue(1, out Info);
        }
    }
}
