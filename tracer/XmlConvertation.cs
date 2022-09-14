using System.Xml.Serialization;
namespace tracer;

public class XmlConvertation
{
    public void SerializeToXml(TraceResult result, string fileName) {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(TraceResult),  
            new Type[] { typeof(MethodInfo), typeof(ThreadInfo)});
        using (Stream fStream = new FileStream(fileName, 
                   FileMode.Create, FileAccess.Write, FileShare.None))
        {
            xmlSerializer.Serialize(fStream, result);
        }
    }
}