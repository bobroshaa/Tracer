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
    public void NumberOfMethodsInFirstThread()
    {
        var count = res.Threads[0].methods.Count;
        Assert.That(2, Is.EqualTo(count));
    }
    [Test]
    public void NumberOfMethodsInSecondThread()
    {
        var count = res.Threads[1].methods.Count;
        Assert.That(2, Is.EqualTo(count));
    }
    
    [Test]
    public void ClassNameInFirstThread()
    {
        var className = res.Threads[0].methods[0].ClassName;
        Assert.That("Foo", Is.EqualTo(className));
        className = res.Threads[0].methods[1].ClassName;
        Assert.That("Foo", Is.EqualTo(className));
    }
    
    [Test]
    public void ClassNameInSecondThread()
    {
        var className = res.Threads[1].methods[0].ClassName;
        Assert.That("Foo", Is.EqualTo(className));
        className = res.Threads[1].methods[1].ClassName;
        Assert.That("Foo", Is.EqualTo(className));
    }
    
    [Test]
    public void MethodNameInFirstThread()
    {
        var methodName = res.Threads[0].methods[0].MethodName;
        Assert.That("M1", Is.EqualTo(methodName));
        methodName = res.Threads[0].methods[1].MethodName;
        Assert.That("M2", Is.EqualTo(methodName));
    }
    
    [Test]
    public void MethodNameInSecondThread()
    {
        var methodName = res.Threads[1].methods[0].MethodName;
        Assert.That("M1", Is.EqualTo(methodName));
        methodName = res.Threads[1].methods[1].MethodName;
        Assert.That("M2", Is.EqualTo(methodName));
    }
    
    [Test]
    public void PerformanceTimeInFirstThread()
    {
        string strTime = res.Threads[0].methods[0].PerformanceTime;
        int time = Convert.ToInt32(strTime.Substring(0, strTime.Length - 2));
        Assert.That(time, Is.GreaterThanOrEqualTo(100));
        strTime = res.Threads[0].methods[1].PerformanceTime;
        time = Convert.ToInt32(strTime.Substring(0, strTime.Length - 2));
        Assert.That(time, Is.GreaterThanOrEqualTo(200));
    }
    
    [Test]
    public void PerformanceTimeInSecondThread()
    {
        string strTime = res.Threads[1].methods[0].PerformanceTime;
        int time = Convert.ToInt32(strTime.Substring(0, strTime.Length - 2));
        Assert.That(time, Is.GreaterThanOrEqualTo(100));
        strTime = res.Threads[1].methods[1].PerformanceTime;
        time = Convert.ToInt32(strTime.Substring(0, strTime.Length - 2));
        Assert.That(time, Is.GreaterThanOrEqualTo(200));
    }

    [Test]
    public void PerformanceTimeOfFirstThread()
    {
        string strTime = res.Threads[0].PerformanceTime;
        int time = Convert.ToInt32(strTime.Substring(0, strTime.Length - 2));
        Assert.That(time, Is.GreaterThanOrEqualTo(300));
    }
    
    [Test]
    public void PerformanceTimeOfSecondThread()
    {
        string strTime = res.Threads[1].PerformanceTime;
        int time = Convert.ToInt32(strTime.Substring(0, strTime.Length - 2));
        Assert.That(time, Is.GreaterThanOrEqualTo(300));
    }
}