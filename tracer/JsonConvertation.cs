using System.Text.Json;
namespace tracer;

public class JsonConvertation
{
    public string SerializeToJson(TraceResult result) {
        return JsonSerializer.Serialize(result);
    }
}