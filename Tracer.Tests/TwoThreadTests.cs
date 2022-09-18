using Tracer.Core;
using Tracer.Example;

namespace Tracer.Tests;

public class TwoThreadTests
{
    private TraceResult res;
    
    [SetUp]
    public void Setup()
    {
        ITracer tracer = new Core.Tracer();
        Foo foo = new Foo(tracer);
        foo.M0();
        Thread thread = new Thread(foo.M0);
        thread.Start();
        thread.Join();
        res = tracer.GetTraceResult();
    }

    [Test]
    public void NumberOfThreads()
    {
        var count = res.Threads.Count;
        Assert.That(2, Is.EqualTo(count));
    }
    
    [Test]
    public void NumberOfMethods()
    {
        var count = res.Threads[1].methods.Count;
        Assert.That(2, Is.EqualTo(count));
    }
    
    [Test]
    public void ClassName()
    {
        var className = res.Threads[0].methods[0].ClassName;
        Assert.That("Foo", Is.EqualTo(className));
    }
    
    [Test]
    public void MethodName()
    {
        var className = res.Threads[0].methods[0].MethodName;
        Assert.That("M1", Is.EqualTo(className));
    }
}