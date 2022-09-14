using Tracer.Core;
namespace Tracer.Serialization;

public interface ITraceResultSerializer
{
    public string Serialize(TraceResult res);
}