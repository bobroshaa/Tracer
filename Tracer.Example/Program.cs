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
            Thread thread = new Thread(foo.M0);
            thread.Start();
            thread.Join();
            TraceResult res = tracer.GetTraceResult();
            JsonConvertation jsonConvertation = new JsonConvertation();
            string jsonRes = jsonConvertation.Serialize(res);
            XmlConvertation xmlConvertation = new XmlConvertation();
            string xmlRes = xmlConvertation.Serialize(res);
            IOutputSerialization output = new OutputSerialization();
            using (Stream stream = new FileStream("1.json", FileMode.OpenOrCreate))
            {
                output.Output(jsonRes, stream);
            }
            using (Stream stream = new FileStream("1.xml", FileMode.OpenOrCreate))
            {
                output.Output(xmlRes, stream);
            }
            output.Output(jsonRes, Console.OpenStandardOutput());
            output.Output(xmlRes, Console.OpenStandardOutput());
        }
    }
}
