using System.Collections.Concurrent;
using System.Diagnostics;
namespace Tracer.Core
{
    public class Tracer : ITracer
    {
        private TraceResult result;
        private ConcurrentDictionary<int, ThreadInfo> _methodsDictionary = new ConcurrentDictionary<int, ThreadInfo>();
        private ConcurrentDictionary<int, Stack<MethodInfo>> _stackMethodsInfo = new ConcurrentDictionary<int, Stack<MethodInfo>>();

        TraceResult ITracer.GetTraceResult()
        {
            var threads = new List<ThreadInfo>();
            foreach (var thread in _methodsDictionary)
            {
                ThreadInfo threadInfo = _methodsDictionary[thread.Key];
                threadInfo.EndThread();
                threads.Add(threadInfo);
            }
            return new TraceResult(threads);
        }

        void ITracer.StartTrace()
        {
            MethodInfo method = new MethodInfo();
            StackTrace stackTrace = new StackTrace(); 
            method.MethodName = stackTrace.GetFrame(1).GetMethod().Name;
            method.ClassName = stackTrace.GetFrame(1).GetMethod().DeclaringType.Name;
            Thread thread = Thread.CurrentThread;
            if (!_methodsDictionary.ContainsKey(thread.ManagedThreadId))
            {
                while (!_methodsDictionary.TryAdd(thread.ManagedThreadId, new ThreadInfo(thread.ManagedThreadId)))
                {
                }
            }
            if (! _stackMethodsInfo.ContainsKey(thread.ManagedThreadId))
            {
                while (! _stackMethodsInfo.TryAdd(thread.ManagedThreadId, new Stack<MethodInfo>()))
                {
                }
            }
            _stackMethodsInfo[thread.ManagedThreadId].Push(method);
        }

        void ITracer.StopTrace()
        {
            Console.WriteLine("stop method");
            Thread thread = Thread.CurrentThread;
            MethodInfo method = _stackMethodsInfo[thread.ManagedThreadId].Pop();
            method.EndMethod();
            if (_stackMethodsInfo[thread.ManagedThreadId].Count == 0)
            {
                ThreadInfo currentThread = _methodsDictionary[thread.ManagedThreadId];
                currentThread.methods.Add(method);
            }
            else
            {
                MethodInfo prevResult = _stackMethodsInfo[thread.ManagedThreadId].Pop();
                if (prevResult.ListOfMethods == null)
                {
                    prevResult.ListOfMethods = new List<MethodInfo>();
                }
                prevResult.ListOfMethods.Add(method);
                _stackMethodsInfo[thread.ManagedThreadId].Push(prevResult);
            }
        }
    }
}
