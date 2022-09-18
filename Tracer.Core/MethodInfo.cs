using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Tracer.Core;
[Serializable]
public class MethodInfo
{
    [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
    public string MethodName { get; set; }
    [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
    public string ClassName { get; set; }
    [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
    public string PerformanceTime{ get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public List<MethodInfo> ListOfMethods{ get; set; }
    private Stopwatch _stopWatch;

    public MethodInfo()
    {
        _stopWatch = new Stopwatch();
        _stopWatch.Start();
    }

    public void EndMethod()
    {
        _stopWatch.Stop();
        PerformanceTime = _stopWatch.Elapsed.Milliseconds + "ms";
    }
    
}