using Newtonsoft.Json;
using Tracer.Core;

namespace Tracer.Serialization;

public class JsonConvertation : ITraceResultSerializer
{
    public string Serialize(TraceResult result) {
        return JsonConvert.SerializeObject(result, Formatting.Indented);
    }
}