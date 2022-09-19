using System.Xml.Schema;
using System.Xml.Serialization;

namespace Tracer.Core;
[Serializable]
public class ThreadInfo
{
    [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
    public int ThreadIndex { get; }
    [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
    public string PerformanceTime { get; set; }
    public List<MethodInfo> methods { get; set; }

    public ThreadInfo(){ }
    public ThreadInfo(int threadIndex)
    {
        ThreadIndex = threadIndex;
        methods = new List<MethodInfo>();
    }
    public void EndThread()
    {
        int temp = 0;
        foreach (var method in methods)
        {
            temp += Convert.ToInt32(method.PerformanceTime.Substring(0, method.PerformanceTime.Length - 2));
        }

        PerformanceTime = temp + "ms";
    }
}