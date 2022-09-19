using Microsoft.VisualBasic.CompilerServices;
using Tracer.Core;
using Tracer.Example;

namespace Tracer.Tests;

public class Tests
{
    private TraceResult res;
    
    [SetUp]
    public void Setup()
    {
        ITracer tracer = new Core.Tracer();
        Foo foo = new Foo(tracer);
        foo.M0();
        res = tracer.GetTraceResult();
    }

    [Test]
    public void NumberOfThreads()
    {
        var count = res.Threads.Count;
        Assert.That(1, Is.EqualTo(count));
    }
    
    [Test]
    public void NumberOfMethods()
    {
        var count = res.Threads[0].methods.Count;
        Assert.That(2, Is.EqualTo(count));
    }
    
    [Test]
    public void ClassNameM1()
    {
        var className = res.Threads[0].methods[0].ClassName;
        Assert.That("Foo", Is.EqualTo(className));
    }
    
    [Test]
    public void ClassNameM2()
    {
        var className = res.Threads[0].methods[1].ClassName;
        Assert.That("Foo", Is.EqualTo(className));
    }
    
    [Test]
    public void MethodNameM1()
    {
        var className = res.Threads[0].methods[0].MethodName;
        Assert.That("M1", Is.EqualTo(className));
    }
    
    [Test]
    public void MethodNameM2()
    {
        var className = res.Threads[0].methods[1].MethodName;
        Assert.That("M2", Is.EqualTo(className));
    }
    
    [Test]
    public void PerformanceTimeM1()
        {
            string strTime = res.Threads[0].methods[0].PerformanceTime;
            int time = Convert.ToInt32(strTime.Substring(0, strTime.Length - 2));
            Assert.That(time, Is.GreaterThanOrEqualTo(100));
        }
    [Test]
    public void PerformanceTimeM2()
    {
        string strTime = res.Threads[0].methods[1].PerformanceTime;
        int time = Convert.ToInt32(strTime.Substring(0, strTime.Length - 2));
        Assert.That(time, Is.GreaterThanOrEqualTo(200));
    }
    [Test]
    public void PerformanceTimeThread()
    {
        string strTime = res.Threads[0].PerformanceTime;
        int time = Convert.ToInt32(strTime.Substring(0, strTime.Length - 2));
        Assert.That(time, Is.GreaterThanOrEqualTo(300));
    }
}