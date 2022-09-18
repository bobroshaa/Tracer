namespace Tracer.Core 
{
    [Serializable]
    public class TraceResult
    {
        public TraceResult(){}
        public List<ThreadInfo> Threads { get; }
        public TraceResult(List<ThreadInfo> threads)
        {
            Threads = threads;
        }
    }
}