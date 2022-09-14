using System;

namespace tracer
{
    class Program
    {
        static void Main(string[] args)
        {
            ITracer tracer = new Tracer();
            Foo foo = new Foo(tracer);
            foo.M0();
            TraceResult res = tracer.GetTraceResult();
            JsonConvertation jsonConvertation = new JsonConvertation();
            Console.WriteLine(jsonConvertation.SerializeToJson(res));
            XmlConvertation xmlConvertation = new XmlConvertation();
            xmlConvertation.SerializeToXml(res, "file.xml");
        }
    }
}
