using Tracer.Core;
namespace Tracer.Example
{
    public class Foo
    {
        private ITracer _tracer;

        internal Foo(ITracer tracer)
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
            _tracer.StopTrace();
        }
    
        private void M2()
        {
            _tracer.StartTrace();
            Thread.Sleep(200);
            _tracer.StopTrace();
        }

    }
}
