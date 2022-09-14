using System.Xml.Schema;
using System.Xml.Serialization;

namespace tracer;

public class ThreadInfo
{
    [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
    public int ThreadIndex { get;  }
    [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
    public int PerformanceTime { get; set; }
    public List<MethodInfo> methods { get; }

    public ThreadInfo(){ }
    public ThreadInfo(int threadIndex)
    {
        ThreadIndex = threadIndex;
        methods = new List<MethodInfo>();
    }
    public void EndThread()
    {
        foreach (var method in methods)
        {
            PerformanceTime += method.PerformanceTime;
        }
    }
}