using System.Diagnostics;
using System.Xml.Serialization;
using Tracer.Core;
namespace Tracer.Serialization;

public class XmlConvertation : ITraceResultSerializer
{
    public string Serialize(TraceResult result)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(TraceResult),
            new Type[] { typeof(MethodInfo), typeof(ThreadInfo) });
        using (FileStream fs = new FileStream("2.xml", FileMode.OpenOrCreate))
        {
            xmlSerializer.Serialize(fs, result);
        }
        using (StringWriter textWriter = new StringWriter())
        {
            xmlSerializer.Serialize(textWriter, result);
            return textWriter.ToString();
        }
        
    }
}