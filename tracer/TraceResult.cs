using System.Collections.Concurrent;

namespace tracer
{
    public class TraceResult
    {
        public Dictionary<string, List<ThreadInfo>> Info { get;}
        public TraceResult(ConcurrentDictionary<int, ThreadInfo> dictionary)
        {
            Info = new Dictionary<string, List<ThreadInfo>>();
            Info.Add("threads", new List<ThreadInfo>());
            foreach (var thread in dictionary)
            {
                dictionary.TryGetValue(thread.Key, out ThreadInfo threadInfo);
                threadInfo.EndThread();
                Info["threads"].Add(threadInfo);
            }
        }
    }
}
