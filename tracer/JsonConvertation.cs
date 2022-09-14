using Newtonsoft.Json;

namespace tracer;

public class JsonConvertation
{
    public string SerializeToJson(TraceResult result) {
        return JsonConvert.SerializeObject(result, Formatting.Indented);
    }
}