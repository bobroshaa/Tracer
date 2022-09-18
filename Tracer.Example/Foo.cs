using Tracer.Core;
namespace Tracer.Example
{
    public class Foo
    {
        private ITracer _tracer;

        public Foo(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void M0()
        {
            M1();
            M2();
        }

        private void M1()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            Console.WriteLine("M1");
            _tracer.StopTrace();
        }

        private void M2()
        {
            _tracer.StartTrace();
            Thread.Sleep(200);
            Console.WriteLine("M2");
            _tracer.StopTrace();
        }

    }
}
