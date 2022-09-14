using Tracer.Core;
using Tracer.Serialization;

namespace Tracer.Example
{
    class Program
    {
        
        static void Main(string[] args)
        {
            ITracer tracer = new Core.Tracer();
            Foo foo = new Foo(tracer);
            foo.M0();
            TraceResult res = tracer.GetTraceResult();
            JsonConvertation jsonConvertation = new JsonConvertation();
            Console.WriteLine(jsonConvertation.Serialize(res));
            XmlConvertation xmlConvertation = new XmlConvertation();
            Console.WriteLine(xmlConvertation.Serialize(res));
        }
    }
}
