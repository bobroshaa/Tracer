namespace tracer;

public interface ITraceResultSerializer
{
    string Format { get; }
    void Serialize(TraceResult traceResult, Stream to);
}